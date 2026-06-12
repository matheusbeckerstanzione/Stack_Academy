using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace StackAcademy.Models;

[Table("Curso")]
public partial class Curso
{
    [Key]
    public Guid IdCurso { get; set; }

    [StringLength(255)]
    public string Nome { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime DataInicio { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime DataFim { get; set; }

    public Guid? IdCategoria { get; set; }

    public Guid? IdProfessor { get; set; }

    [ForeignKey("IdCategoria")]
    [InverseProperty("Cursos")]
    public virtual Categorium? IdCategoriaNavigation { get; set; }

    [ForeignKey("IdProfessor")]
    [InverseProperty("Cursos")]
    public virtual Professor? IdProfessorNavigation { get; set; }

    [ForeignKey("IdCurso")]
    [InverseProperty("IdCursos")]
    public virtual ICollection<Aluno> IdAlunos { get; set; } = new List<Aluno>();
}
