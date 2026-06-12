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

    [HttpGet("{id}")]
    public IActionResult GetByid(Guid id)
    {
        try
        {
            return Ok(_CursoRepository.BuscarPorId(id));
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

    [HttpPut("{id}")]
    public IActionResult PutById(Guid id, Curso CursoAtualizado)
    {
        try
        {
            _CursoRepository.AtualizarIdUrl(id, CursoAtualizado);
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

    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id) 
    {
        try
        {
            _CursoRepository.Deletar(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

}
