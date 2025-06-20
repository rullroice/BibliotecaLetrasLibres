﻿using Evaluacion_Net_2.Models;
using Microsoft.EntityFrameworkCore;

namespace Evaluacion_Net_2.Data
{
    // Contexto de la base de datos para la aplicación
    public class AppDbContext : DbContext
    {
        // Constructor que recibe las opciones de configuración del contexto
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Libro> Libros { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Prestamo> Prestamos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuración MÍNIMA requerida
            modelBuilder.Entity<Prestamo>()
                .HasOne<Libro>()
                .WithMany()
                .HasForeignKey(p => p.LibroId);

            modelBuilder.Entity<Prestamo>()
                .HasOne<Usuario>()
                .WithMany()
                .HasForeignKey(p => p.UsuarioId);
        }
    }
}