using Microsoft.AspNetCore.Mvc;
using StackAcademy.DTO;
using StackAcademy.Interface;
using StackAcademy.Models;
using StackAcademy.Utils;

namespace StackAcademy.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProfessorController : ControllerBase
{
    private readonly IProfessorRepository _ProfessorRepository;

    public ProfessorController(IProfessorRepository ProfessorRepository)
    {
        _ProfessorRepository = ProfessorRepository;
    }

    [HttpGet]
    public IActionResult Get()
    {
        try
        {
            return Ok(_ProfessorRepository.Listar());
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
            var professor = _ProfessorRepository.BuscarPorId(id);
            if (professor == null)
                return NotFound("Professor não encontrado");

            return Ok(professor);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromForm] ProfessorDTO NovoProfessor)
    {
        if (String.IsNullOrWhiteSpace(NovoProfessor.Nome))
            return BadRequest("O nome é obrigatório");

        if (String.IsNullOrWhiteSpace(NovoProfessor.Senha))
            return BadRequest("A senha é obrigatória");

        Professor Professor = new Professor();

        if (NovoProfessor.Imagem != null && NovoProfessor.Imagem.Length > 0)
        {
            var extensao = Path.GetExtension(NovoProfessor.Imagem.FileName);
            var nomeArquivo = $"{Guid.NewGuid()}{extensao}";
            var pastaRelativa = "wwwroot/imagens";
            var caminhoPasta = Path.Combine(Directory.GetCurrentDirectory(), pastaRelativa);

            if (!Directory.Exists(caminhoPasta))
                Directory.CreateDirectory(caminhoPasta);

            var caminhoCompleto = Path.Combine(caminhoPasta, nomeArquivo);

            using (var stream = new FileStream(caminhoCompleto, FileMode.Create))
            {
                await NovoProfessor.Imagem.CopyToAsync(stream);
            }

            Professor.Imagem = nomeArquivo;
        }

        Professor.IdProfessor = NovoProfessor.IdProfessor.Value;
        Professor.Nome = NovoProfessor.Nome!;
        Professor.Email = NovoProfessor.Email!;         // ✅ adicionado
        Professor.Cpf = NovoProfessor.Cpf!;             // ✅ adicionado
        Professor.Senha = Criptografia.GerarHash(NovoProfessor.Senha!); // ✅ adicionado

        try
        {
            _ProfessorRepository.Cadastrar(Professor);
            return StatusCode(201);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(Guid id, [FromForm] ProfessorDTO ProfessorAtualizado)
    {
        var ProfessorBuscado = _ProfessorRepository.BuscarPorId(id);
        if (ProfessorBuscado == null)
            return NotFound("Professor não encontrado");

        if (!String.IsNullOrWhiteSpace(ProfessorAtualizado.Nome))
            ProfessorBuscado.Nome = ProfessorAtualizado.Nome;

        if (!String.IsNullOrWhiteSpace(ProfessorAtualizado.Email))
            ProfessorBuscado.Email = ProfessorAtualizado.Email;

        if (!String.IsNullOrWhiteSpace(ProfessorAtualizado.Cpf))
            ProfessorBuscado.Cpf = ProfessorAtualizado.Cpf;

        if (!String.IsNullOrWhiteSpace(ProfessorAtualizado.Senha))
            ProfessorBuscado.Senha = Criptografia.GerarHash(ProfessorAtualizado.Senha); // ✅ adicionado

        if (ProfessorAtualizado.Imagem != null && ProfessorAtualizado.Imagem.Length > 0)
        {
            var pastaRelativa = "wwwroot/imagens";
            var caminhoPasta = Path.Combine(Directory.GetCurrentDirectory(), pastaRelativa);

            if (!String.IsNullOrEmpty(ProfessorBuscado.Imagem)) // ✅ lógica corrigida
            {
                var caminhoAntigo = Path.Combine(caminhoPasta, ProfessorBuscado.Imagem);
                if (System.IO.File.Exists(caminhoAntigo))
                    System.IO.File.Delete(caminhoAntigo);
            }

            var extensao = Path.GetExtension(ProfessorAtualizado.Imagem.FileName);
            var nomeArquivo = $"{Guid.NewGuid()}{extensao}";

            if (!Directory.Exists(caminhoPasta))
                Directory.CreateDirectory(caminhoPasta);

            var caminhoCompleto = Path.Combine(caminhoPasta, nomeArquivo);
            using (var stream = new FileStream(caminhoCompleto, FileMode.Create))
            {
                await ProfessorAtualizado.Imagem.CopyToAsync(stream);
            }

            ProfessorBuscado.Imagem = nomeArquivo;
        }

        try
        {
            _ProfessorRepository.AtualizarIdUrl(id, ProfessorBuscado);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public IActionResult Deletar(Guid id)
    {
        var ProfessorBuscado = _ProfessorRepository.BuscarPorId(id);
        if (ProfessorBuscado == null)
            return NotFound("Professor não encontrado");

        if (!String.IsNullOrEmpty(ProfessorBuscado.Imagem)) // ✅ lógica corrigida
        {
            var pastaRelativa = "wwwroot/imagens";
            var caminhoPasta = Path.Combine(Directory.GetCurrentDirectory(), pastaRelativa);
            var caminho = Path.Combine(caminhoPasta, ProfessorBuscado.Imagem);

            if (System.IO.File.Exists(caminho))
                System.IO.File.Delete(caminho);
        }

        try
        {
            _ProfessorRepository.Deletar(id);
            return NoContent();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}