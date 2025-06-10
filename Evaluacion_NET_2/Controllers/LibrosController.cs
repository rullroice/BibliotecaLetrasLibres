using System;
using Evaluacion_Net_2.Data;
using Evaluacion_Net_2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Evaluacion_Net_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibrosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public LibrosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Libro>>> Get()
        {
            return await _context.Libros.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Libro>> Get(int id)
        {
            var libro = await _context.Libros.FindAsync(id);
            return libro == null ? NotFound() : libro;
        }

        [HttpPost]
        public async Task<ActionResult<Libro>> Post(Libro libro)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Validación adicional: ISBN único
            bool isbnExiste = await _context.Libros.AnyAsync(l => l.ISBN == libro.ISBN);
            if (isbnExiste)
                return BadRequest("Ya existe un libro con el mismo ISBN.");

            _context.Libros.Add(libro);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = libro.Id }, libro);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Libro libro)
        {
            if (id != libro.Id)
                return BadRequest("El ID de la URL no coincide con el del libro.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var libroExistente = await _context.Libros.FindAsync(id);
            if (libroExistente == null)
                return NotFound("No se encontró el libro que se desea actualizar.");

            libroExistente.Titulo = libro.Titulo;
            libroExistente.Autor = libro.Autor;
            libroExistente.ISBN = libro.ISBN;
            libroExistente.AnioPublicacion = libro.AnioPublicacion;
            libroExistente.UnidadesDisponibles = libro.UnidadesDisponibles;

            await _context.SaveChangesAsync();
            return Ok("Libro actualizado correctamente.");
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var libro = await _context.Libros.FindAsync(id);
            if (libro == null) return NotFound();

            // Verifica si el libro tiene préstamos activos (sin fecha de devolución)
            bool estaPrestado = await _context.Prestamos
                .AnyAsync(p => p.LibroId == id && p.FechaDevolucion == null);
            
            if (estaPrestado)
            {
                return BadRequest("No se puede eliminar el libro porque actualmente está prestado.");
            }

            _context.Libros.Remove(libro);
            await _context.SaveChangesAsync();
            return Ok("Libro Eliminado correctamente.");
        }

    }
}