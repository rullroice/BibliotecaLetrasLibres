using System;

namespace Evaluacion_Net_2.Models
{
    public class Prestamo
    {
        public int Id { get; set; }
        public int LibroId { get; set; }  // Solo el ID, sin propiedad de navegación
        public int UsuarioId { get; set; } // Solo el ID, sin propiedad de navegación
        public DateTime FechaPrestamo { get; set; } = DateTime.Now;
        public DateTime? FechaDevolucion { get; set; }
    }
}