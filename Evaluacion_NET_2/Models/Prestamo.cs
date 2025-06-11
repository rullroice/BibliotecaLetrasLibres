using System;

namespace Evaluacion_Net_2.Models
{
    // Modelo que representa un préstamo de libro en la biblioteca
    public class Prestamo
    {
        public int Id { get; set; }
        public int LibroId { get; set; }
        public int UsuarioId { get; set; }
        public DateTime FechaPrestamo { get; set; } = DateTime.Now;
        public DateTime? FechaDevolucion { get; set; }
    }
}