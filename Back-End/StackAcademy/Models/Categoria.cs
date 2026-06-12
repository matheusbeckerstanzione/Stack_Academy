using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace StackAcademy.Models;

[Table("Categoria")]
public partial class Categoria
{
    [Key]
    public Guid IdCategoria { get; set; }

    [StringLength(255)]
    public string Nome { get; set; } = null!;

    [JsonIgnore]
    [InverseProperty("IdCategoriaNavigation")]
    public virtual ICollection<Curso> Cursos { get; set; } = new List<Curso>();
}
