using Evaluacion_Net_2.Data;
using Evaluacion_Net_2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Evaluacion_Net_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrestamosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PrestamosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/prestamos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Prestamo>>> GetPrestamos()
        {
            return await _context.Prestamos.ToListAsync();
        }

        // POST: api/prestamos
        [HttpPost]
        public async Task<ActionResult<Prestamo>> CrearPrestamo([FromBody] PrestamoRequest request)
        {
            // Validar modelo
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Verificar libro
            var libro = await _context.Libros.FindAsync(request.LibroId);
            if (libro == null)
                return NotFound("Libro no encontrado");

            if (libro.UnidadesDisponibles <= 0)
                return BadRequest("Libro no disponible");

            // Verificar usuario
            if (!await _context.Usuarios.AnyAsync(u => u.Id == request.UsuarioId))
                return NotFound("Usuario no encontrado");

            // Crear préstamo
            var prestamo = new Prestamo
            {
                LibroId = request.LibroId,
                UsuarioId = request.UsuarioId,
                FechaPrestamo = DateTime.Now
            };

            // Actualizar unidades
            libro.UnidadesDisponibles--;

            _context.Prestamos.Add(prestamo);
            await _context.SaveChangesAsync();

            return Ok(prestamo);
        }

        // POST: api/prestamos/devolver/{id}
        [HttpPost("devolver/{id}")]
        public async Task<IActionResult> DevolverLibro(int id)
        {
            var prestamo = await _context.Prestamos.FindAsync(id);
            if (prestamo == null)
                return NotFound();

            if (prestamo.FechaDevolucion != null)
                return BadRequest("Libro ya devuelto");

            var libro = await _context.Libros.FindAsync(prestamo.LibroId);
            if (libro == null)
                return NotFound("Libro no encontrado");

            // Registrar devolución
            prestamo.FechaDevolucion = DateTime.Now;
            libro.UnidadesDisponibles++;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // GET: api/prestamos/usuario/{usuarioId}
        [HttpGet("usuario/{usuarioId}")]
        public async Task<ActionResult<IEnumerable<Prestamo>>> GetPrestamosPorUsuario(int usuarioId)
        {
            return await _context.Prestamos
                .Where(p => p.UsuarioId == usuarioId)
                .ToListAsync();
        }
    }

    public class PrestamoRequest
    {
        [Required]
        public int LibroId { get; set; }

        [Required]
        public int UsuarioId { get; set; }
    }
}