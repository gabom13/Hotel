using System.ComponentModel.DataAnnotations.Schema;

namespace WebTest5.Components.Model
{
    public class UsuariosModel
    {
        public int Cid { get; set; }
        public string Nombre_Completo { get; set; }
        public DateTime Fecha_Nacimiento { get; set; }
        public int Telefono { get; set; }
        public string Contraseña { get; set; }
        public int Nivel_Acceso { get; set; }
        public string NombreNivelAcceso { get; set; }
    }
}
