using Microsoft.AspNetCore.Mvc;
using StackAcademy.Interface;
using StackAcademy.Models;

namespace StackAcademy.Controllers;

[Route("api/[controller]")]
[ApiController]

public class CursoController : Controller
{
    private readonly ICursoRepository _CursoRepository;

    public CursoController(ICursoRepository CursoRepository)
    {
        _CursoRepository = CursoRepository;
    }

    [HttpGet("{Id}")]
    public IActionResult GetByid(Guid Id)
    {
        try
        {
            return Ok(_CursoRepository.BuscarPorId(Id));
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
            return Ok(_CursoRepository.Listar());
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public IActionResult Post(Curso NovoCurso)
    {
        try
        {
            _CursoRepository.Cadastrar(NovoCurso);
            return StatusCode(201);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{Id}")]
    public IActionResult PutById(Guid Id, Curso CursoAtualizado)
    {
        try
        {
            _CursoRepository.AtualizarIdUrl(Id, CursoAtualizado);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut]
    public IActionResult Put(Curso CursoAtualizado)
    {
        try
        {
            _CursoRepository.AtualizarIdCorpo(CursoAtualizado);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{Id}")]
    public IActionResult Delete(Guid Id) 
    {
        try
        {
            _CursoRepository.Deletar(Id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

}
