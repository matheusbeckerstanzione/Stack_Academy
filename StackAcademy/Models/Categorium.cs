using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace StackAcademy.Models;

public partial class Categorium
{
    [Key]
    public Guid IdCategoria { get; set; }

    [StringLength(255)]
    public string Nome { get; set; } = null!;

    [InverseProperty("IdCategoriaNavigation")]
    public virtual ICollection<Curso> Cursos { get; set; } = new List<Curso>();
}
