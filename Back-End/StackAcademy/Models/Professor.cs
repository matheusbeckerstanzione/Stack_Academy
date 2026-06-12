using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace StackAcademy.Models;

[Table("Professor")]
[Index("Email", Name = "UQ__Professo__A9D10534E62DAA58", IsUnique = true)]
[Index("Cpf", Name = "UQ__Professo__C1FF93097299CBA3", IsUnique = true)]
public partial class Professor
{
    [Key]
    public Guid IdProfessor { get; set; }

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
    public string Imagem { get; set; } = null!;

    [JsonIgnore]
    [InverseProperty("IdProfessorNavigation")]
    public virtual ICollection<Curso> Cursos { get; set; } = new List<Curso>();
}
