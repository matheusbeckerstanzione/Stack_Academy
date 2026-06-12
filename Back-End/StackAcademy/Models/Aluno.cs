using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace StackAcademy.Models;

[Table("Aluno")]
[Index("Email", Name = "UQ__Aluno__A9D105345C93E4C4", IsUnique = true)]
[Index("Cpf", Name = "UQ__Aluno__C1FF9309FC9AE0AD", IsUnique = true)]
public partial class Aluno
{
    [Key]
    public Guid IdAluno { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string Nome { get; set; } = null!;

    [StringLength(255)]
    public string Email { get; set; } = null!;

    [StringLength(40)]
    public string Senha { get; set; } = null!;

    [StringLength(14)]
    public string Cpf { get; set; } = null!;

    [StringLength(100)]
    [Unicode(false)]
    public string? Imagem { get; set; }

    [JsonIgnore]
    [ForeignKey("IdAluno")]
    [InverseProperty("IdAlunos")]
    public virtual ICollection<Curso> IdCursos { get; set; } = new List<Curso>();
}
