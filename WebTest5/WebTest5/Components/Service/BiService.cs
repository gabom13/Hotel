using Dapper;
using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Transforms;
using WebTest5.Components.Interface;
using WebTest5.Components.Data;

namespace WebTest5.Components.Service
{
    public class BiService : IBiService
    {
        private readonly DataContext _context;
        private DateTime ultimaFecha;
        ILogger _logger;

        public BiService(DataContext context,ILogger<BiService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task Iniciar()
        {
            // Crear el contexto de ML.NET
            var mlContext = new MLContext();

            // Cargar datos históricos
            var data = (await CargarDatos()).ToList();

            // Convertir los datos a IDataView
            IDataView dataView = mlContext.Data.LoadFromEnumerable(data);

            // Configurar el pipeline de entrenamiento
            var pipeline = mlContext.Transforms.CustomMapping<HotelBooking, HotelBookingTransformed>(TransformData, "TransformData")
                .Append(mlContext.Transforms.Categorical.OneHotEncoding(outputColumnName: "YearEncoded", inputColumnName: nameof(HotelBookingTransformed.Year), outputKind: OneHotEncodingEstimator.OutputKind.Indicator))
                .Append(mlContext.Transforms.Categorical.OneHotEncoding(outputColumnName: "MonthEncoded", inputColumnName: nameof(HotelBookingTransformed.Month), outputKind: OneHotEncodingEstimator.OutputKind.Indicator))
                .Append(mlContext.Transforms.Concatenate("Features", "YearEncoded", "MonthEncoded"))
                .Append(mlContext.Transforms.NormalizeMinMax("Features"))
                .Append(mlContext.Transforms.CopyColumns(outputColumnName: "Label", inputColumnName: nameof(HotelBookingTransformed.Reservations)))
                .Append(mlContext.Regression.Trainers.Sdca(labelColumnName: "Label", maximumNumberOfIterations: 100));

            // Entrenar el modelo
            var model = pipeline.Fit(dataView);

            // Crear predicciones
            var predictionEngine = mlContext.Model.CreatePredictionEngine<HotelBooking, HotelBookingPrediction>(model);
            ultimaFecha = data.Last().Fecha_Llegada;
            List<HotelBooking> predicciones = new List<HotelBooking>();

            for (int i = 1; i <= 12; i++)
            {
                var forecast = predictionEngine.Predict(new HotelBooking
                {
                    Fecha_Llegada = ultimaFecha.AddMonths(i)
                });

                predicciones.Add(new HotelBooking
                {
                    Fecha_Llegada = ultimaFecha.AddMonths(i),
                    Cantidad_Reservas = (float)Math.Round(forecast.Score)
                });
            }
            await AddPrediccion(predicciones);
        }

        public async Task AddPrediccion(List<HotelBooking> predicciones)
        {
            using var connection = _context.CreateConnection();
            var sql = "DELETE FROM Bi;";
            await connection.ExecuteAsync(sql);
            sql = "INSERT INTO Bi(fecha_llegada, cantidad_reservas) VALUES (@Fecha_Llegada, @Cantidad_Reservas)";
            foreach (var prediccion in predicciones)
            {
                await connection.ExecuteAsync(sql, new { prediccion.Fecha_Llegada, prediccion.Cantidad_Reservas });
            }
        }

        public void TransformData(HotelBooking input, HotelBookingTransformed output)
        {
            output.Year = input.Fecha_Llegada.Year;
            output.Month = input.Fecha_Llegada.Month;
            output.Reservations = input.Cantidad_Reservas;
        }

        public async Task<IEnumerable<HotelBooking>> CargarDatos()
        {
            try
            {
                _logger.LogInformation("Iniciando la carga de datos...");

                // Aumentar el tiempo de espera de la conexión
                using var connection = _context.CreateConnection();

                _logger.LogInformation("Conexión a la base de datos establecida.");

                var sql = "SELECT r.fecha_llegada AS Fecha_Llegada, COUNT(*) AS Cantidad_Reservas " +
                          "FROM reservas r " +
                          "WHERE NOT(YEAR(r.fecha_llegada) = YEAR(CURDATE()) AND MONTH(r.fecha_llegada) = MONTH(CURDATE())) " +
                          "GROUP BY YEAR(r.fecha_llegada), MONTH(r.fecha_llegada) " +
                          "ORDER BY YEAR(r.fecha_llegada), MONTH(r.fecha_llegada);";

                _logger.LogInformation("Ejecutando la consulta SQL: " + sql);

                var result = await connection.QueryAsync<HotelBooking>(sql);

                _logger.LogInformation("Consulta SQL ejecutada.");

                if (!result.Any())
                {
                    _logger.LogInformation("No se encontraron registros.");
                }
                else
                {
                    _logger.LogInformation($"Se encontraron {result.Count()} registros.");
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al cargar datos: {ex.Message}");
                throw;
            }
        }
    }

    public class HotelBooking
    {
        [LoadColumn(0)]
        public DateTime Fecha_Llegada { get; set; }

        [LoadColumn(1)]
        public float Cantidad_Reservas { get; set; }
    }

    public class HotelBookingPrediction
    {
        public float Score { get; set; }
    }

    public class HotelBookingTransformed
    {
        public float Year { get; set; }
        public float Month { get; set; }
        public float Reservations { get; set; }
    }
    /*
    public class DataContext
    {
        private readonly string _connectionString;

        public DataContext()
        {
            _connectionString = "Server=sql10.freesqldatabase.com;Database=sql10715093;User=sql10715093; Password=MVMQ88it49";
        }

        public MySqlConnection CreateConnection()
        {
            var connection = new MySqlConnection(_connectionString);
            connection.Open();
            return connection;
        }
    }*/
}