using System.ComponentModel.DataAnnotations;

namespace Evaluacion_Net_2.Models
{
    public class Libro
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El título es obligatorio")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "El autor es obligatorio")]
        public string Autor { get; set; }

        [Required(ErrorMessage = "El ISBN es obligatorio")]
        public string ISBN { get; set; }

        [Range(1000, 2100, ErrorMessage = "Año de publicación inválido")]
        public int AnioPublicacion { get; set; }

        public int UnidadesDisponibles { get; set; }

        public bool Disponible => UnidadesDisponibles > 0;
    }
}