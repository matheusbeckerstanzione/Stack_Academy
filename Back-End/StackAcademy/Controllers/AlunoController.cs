using Microsoft.AspNetCore.Mvc;
using StackAcademy.DTO;
using StackAcademy.Interface;
using StackAcademy.Models;
using StackAcademy.Utils;

namespace StackAcademy.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AlunoController : ControllerBase
{
    private readonly IAlunoRepository _AlunoRepository;

    public AlunoController(IAlunoRepository AlunoRepository)
    {
        _AlunoRepository = AlunoRepository;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromForm] AlunoDTO NovoAluno)
    {
        if (String.IsNullOrWhiteSpace(NovoAluno.Nome))
            return BadRequest("O nome é obrigatório");

        if (String.IsNullOrWhiteSpace(NovoAluno.Senha))
            return BadRequest("A senha é obrigatória");

        Aluno Aluno = new Aluno();

        if (NovoAluno.Imagem != null && NovoAluno.Imagem.Length > 0)
        {
            var extensao = Path.GetExtension(NovoAluno.Imagem.FileName);
            var nomeArquivo = $"{Guid.NewGuid()}{extensao}";
            var pastaRelativa = "wwwroot/imagens";
            var caminhoPasta = Path.Combine(Directory.GetCurrentDirectory(), pastaRelativa);

            if (!Directory.Exists(caminhoPasta))
                Directory.CreateDirectory(caminhoPasta);

            var caminhoCompleto = Path.Combine(caminhoPasta, nomeArquivo);

            using (var stream = new FileStream(caminhoCompleto, FileMode.Create))
            {
                await NovoAluno.Imagem.CopyToAsync(stream);
            }

            Aluno.Imagem = nomeArquivo;
        }

        Aluno.IdAluno = NovoAluno.IdAluno.Value;
        Aluno.Nome = NovoAluno.Nome!;
        Aluno.Email = NovoAluno.Email!;
        Aluno.Cpf = NovoAluno.Cpf!;
        Aluno.Senha = Criptografia.GerarHash(NovoAluno.Senha!);

        try
        {
            _AlunoRepository.Cadastrar(Aluno);
            return StatusCode(201);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    public IActionResult Get()
    {
        try
        {
            return Ok(_AlunoRepository.Listar());
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("{id}")]
    public IActionResult GetById(Guid id)
    {
        try
        {
            var aluno = _AlunoRepository.BuscarPorId(id);
            if (aluno == null)
                return NotFound("Aluno não encontrado");

            return Ok(aluno);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("{id}")]
    public IActionResult Deletar(Guid id)
    {
        var AlunoBuscado = _AlunoRepository.BuscarPorId(id);
        if (AlunoBuscado == null)
            return NotFound("Aluno não encontrado");

        if (!String.IsNullOrEmpty(AlunoBuscado.Imagem))
        {
            var pastaRelativa = "wwwroot/imagens";
            var caminhoPasta = Path.Combine(Directory.GetCurrentDirectory(), pastaRelativa);
            var caminho = Path.Combine(caminhoPasta, AlunoBuscado.Imagem);

            if (System.IO.File.Exists(caminho))
                System.IO.File.Delete(caminho);
        }

        try
        {
            _AlunoRepository.Deletar(id);
            return NoContent();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(Guid id, [FromForm] AlunoDTO AlunoAtualizado)
    {
        var AlunoBuscado = _AlunoRepository.BuscarPorId(id);
        if (AlunoBuscado == null)
            return NotFound("Aluno não encontrado");

        if (!String.IsNullOrWhiteSpace(AlunoAtualizado.Nome))
            AlunoBuscado.Nome = AlunoAtualizado.Nome;

        if (!String.IsNullOrWhiteSpace(AlunoAtualizado.Email))
            AlunoBuscado.Email = AlunoAtualizado.Email;

        if (!String.IsNullOrWhiteSpace(AlunoAtualizado.Cpf))
            AlunoBuscado.Cpf = AlunoAtualizado.Cpf;

        if (!String.IsNullOrWhiteSpace(AlunoAtualizado.Senha))
            AlunoBuscado.Senha = Criptografia.GerarHash(AlunoAtualizado.Senha);

        if (AlunoAtualizado.Imagem != null && AlunoAtualizado.Imagem.Length > 0)
        {
            var pastaRelativa = "wwwroot/imagens";
            var caminhoPasta = Path.Combine(Directory.GetCurrentDirectory(), pastaRelativa);

            if (!String.IsNullOrEmpty(AlunoBuscado.Imagem))
            {
                var caminhoAntigo = Path.Combine(caminhoPasta, AlunoBuscado.Imagem);
                if (System.IO.File.Exists(caminhoAntigo))
                    System.IO.File.Delete(caminhoAntigo);
            }

            var extensao = Path.GetExtension(AlunoAtualizado.Imagem.FileName);
            var nomeArquivo = $"{Guid.NewGuid()}{extensao}";

            if (!Directory.Exists(caminhoPasta))
                Directory.CreateDirectory(caminhoPasta);

            var caminhoCompleto = Path.Combine(caminhoPasta, nomeArquivo);
            using (var stream = new FileStream(caminhoCompleto, FileMode.Create))
            {
                await AlunoAtualizado.Imagem.CopyToAsync(stream);
            }

            AlunoBuscado.Imagem = nomeArquivo;
        }

        try
        {
            _AlunoRepository.AtualizarIdUrl(id, AlunoBuscado);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}