namespace WebTest5.Components.Model
{
    public class HabitacionesModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Tipo { get; set; }
        public int Id_Estado { get; set; }
        public string NombreEstado { get; set; }
        public string NombreTipo { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is HabitacionesModel model)
            {
                return Id == model.Id;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
