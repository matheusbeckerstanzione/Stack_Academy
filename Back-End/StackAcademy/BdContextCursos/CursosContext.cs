using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using StackAcademy.Models;

namespace StackAcademy.BdContextCursos;

public partial class CursosContext : DbContext
{
    public CursosContext()
    {
    }

    public CursosContext(DbContextOptions<CursosContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Aluno> Alunos { get; set; }

    public virtual DbSet<Categoria> Categoria { get; set; }

    public virtual DbSet<Curso> Cursos { get; set; }

    public virtual DbSet<Professor> Professors { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=StackAcademy;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Aluno>(entity =>
        {
            entity.HasKey(e => e.IdAluno).HasName("PK__Aluno__8092FCB3F6C485D9");

            entity.Property(e => e.IdAluno).ValueGeneratedNever();

            entity.HasMany(d => d.IdCursos).WithMany(p => p.IdAlunos)
                .UsingEntity<Dictionary<string, object>>(
                    "AlunoCurso",
                    r => r.HasOne<Curso>().WithMany()
                        .HasForeignKey("IdCurso")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__AlunoCurs__IdCur__08B54D69"),
                    l => l.HasOne<Aluno>().WithMany()
                        .HasForeignKey("IdAluno")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__AlunoCurs__IdAlu__07C12930"),
                    j =>
                    {
                        j.HasKey("IdAluno", "IdCurso").HasName("PK__AlunoCur__F0170ECEE3E7A1D3");
                        j.ToTable("AlunoCurso");
                    });
        });

        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.IdCategoria).HasName("PK__Categori__A3C02A10FE258561");

            entity.Property(e => e.IdCategoria).ValueGeneratedNever();
        });

        modelBuilder.Entity<Curso>(entity =>
        {
            entity.HasKey(e => e.IdCurso).HasName("PK__Curso__085F27D6863C78FC");

            entity.Property(e => e.IdCurso).ValueGeneratedNever();

            entity.HasOne(d => d.IdCategoriaNavigation).WithMany(p => p.Cursos).HasConstraintName("FK__Curso__IdCategor__03F0984C");

            entity.HasOne(d => d.IdProfessorNavigation).WithMany(p => p.Cursos).HasConstraintName("FK__Curso__IdProfess__04E4BC85");
        });

        modelBuilder.Entity<Professor>(entity =>
        {
            entity.HasKey(e => e.IdProfessor).HasName("PK__Professo__9D84BE1B29C42695");

            entity.Property(e => e.IdProfessor).ValueGeneratedNever();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
