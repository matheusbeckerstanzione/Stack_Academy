using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StackAcademy.DTO;

public class ProfessorDTO
{

    public Guid? IdProfessor { get; set; }

    
    public string? Nome { get; set; } 


    public string? Email { get; set; } 

 
    public string? Cpf { get; set; } 

 
    public IFormFile? Imagem { get; set; } 

    
    public string? Senha { get; set; } 
}
