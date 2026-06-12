namespace StackAcademy.DTO;

public class AlunoDTO
{
    public string? Nome { get; set; }

    public Guid? IdAluno { get; set; }

    public string? Senha { get; set; }

    public string? Cpf { get; set; }

    public string? Email { get; set; }

    public IFormFile? Imagem { get; set; }

   
}
  