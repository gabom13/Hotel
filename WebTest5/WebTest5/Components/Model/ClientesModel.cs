using System.ComponentModel.DataAnnotations.Schema;

namespace WebTest5.Components.Model
{
    public class ClientesModel
    {
        public int Cid { get; set; }
        public string Nombre_Completo { get; set; }
        public string Direccion_Residencia { get; set; }
        public string Correo_Electronico { get; set; }
        public int Pais { get; set; }
        public int Ciudad_Procedencia { get; set; }
        public int Profesion { get; set; }
        public DateTime Fecha_Nacimiento { get; set; }
        public int Telefono { get; set; }
        public string NombrePais { get; set; }
        public string NombreCiudad { get; set; }
        public string NombreProfesion { get; set; }

    }
}
