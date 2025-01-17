﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Titulacion.Models
{
    public partial class TutoriasContext : DbContext
    {
        public TutoriasContext()
        {
        }

        public TutoriasContext(DbContextOptions<TutoriasContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Alumno> Alumno { get; set; }
        public virtual DbSet<AreaTutorias> AreaTutorias { get; set; }
        public virtual DbSet<Inscripcion> Inscripcion { get; set; }
        public virtual DbSet<Profesor> Profesor { get; set; }
        public virtual DbSet<Usuarios> Usuarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=tcp:tutoriaexpress.database.windows.net,1433;Initial Catalog=tutoriaexpress;Persist Security Info=False;User ID=Administrador;Password=Tutoria1.;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Alumno>(entity =>
            {
                entity.HasKey(e => e.IdAlumno)
                    .HasName("PK__Alumno__B996CB12F126343D");

                entity.Property(e => e.IdAlumno).HasColumnName("Id_Alumno");

                entity.Property(e => e.ApellidoMat)
                    .IsRequired()
                    .HasColumnName("Apellido_Mat")
                    .HasMaxLength(40);

                entity.Property(e => e.ApellidoPat)
                    .IsRequired()
                    .HasColumnName("Apellido_Pat")
                    .HasMaxLength(40);

                entity.Property(e => e.Correo).HasMaxLength(30);

                entity.Property(e => e.Grupo)
                    .IsRequired()
                    .HasMaxLength(6);

                entity.Property(e => e.IdUsuario).HasColumnName("Id_Usuario");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Tutoria).HasColumnName("TUTORIA");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Alumno)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Usuario_Alumno");
            });

            modelBuilder.Entity<AreaTutorias>(entity =>
            {
                entity.HasKey(e => e.IdArea)
                    .HasName("PK__AreaTuto__9C42D7FE9EF3DC23");

                entity.Property(e => e.IdArea).HasColumnName("Id_Area");

                entity.Property(e => e.Administrador)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.IdUsuario).HasColumnName("Id_Usuario");

                entity.Property(e => e.NombreArea)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.AreaTutorias)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Usuario_Admin");
            });

            modelBuilder.Entity<Inscripcion>(entity =>
            {
                entity.HasKey(e => e.IdInscripcion)
                    .HasName("PK__Inscripc__C7E9D2F572613021");

                entity.Property(e => e.IdInscripcion).HasColumnName("Id_Inscripcion");

                entity.Property(e => e.Fecha).HasColumnType("date");

                entity.Property(e => e.Folio)
                    .IsRequired()
                    .HasMaxLength(11);

                entity.Property(e => e.IdAlumno).HasColumnName("Id_Alumno");

                entity.Property(e => e.IdProfesor).HasColumnName("Id_Profesor");

                entity.HasOne(d => d.IdAlumnoNavigation)
                    .WithMany(p => p.Inscripcion)
                    .HasForeignKey(d => d.IdAlumno)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Alumno");

                entity.HasOne(d => d.IdProfesorNavigation)
                    .WithMany(p => p.Inscripcion)
                    .HasForeignKey(d => d.IdProfesor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Profesor");
            });

            modelBuilder.Entity<Profesor>(entity =>
            {
                entity.HasKey(e => e.IdProfesor)
                    .HasName("PK__Profesor__45D4152A55DCF9BF");

                entity.Property(e => e.IdProfesor).HasColumnName("Id_Profesor");

                entity.Property(e => e.ApellidoMat)
                    .IsRequired()
                    .HasColumnName("Apellido_Mat")
                    .HasMaxLength(50);

                entity.Property(e => e.ApellidoPat)
                    .IsRequired()
                    .HasColumnName("Apellido_Pat")
                    .HasMaxLength(50);

                entity.Property(e => e.Correo).HasMaxLength(50);

                entity.Property(e => e.Grupo)
                    .IsRequired()
                    .HasMaxLength(6);

                entity.Property(e => e.IdUsuario).HasColumnName("Id_Usuario");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Profesor)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Usuario_Profesor");
            });

            modelBuilder.Entity<Usuarios>(entity =>
            {
                entity.HasKey(e => e.IdUsuario)
                    .HasName("PK__Usuarios__63C76BE2F6713D93");

                entity.Property(e => e.IdUsuario).HasColumnName("Id_Usuario");

                entity.Property(e => e.Pass).IsRequired();

                entity.Property(e => e.Tipo)
                    .HasColumnName("TIPO")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.User)
                    .IsRequired()
                    .HasMaxLength(10);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
