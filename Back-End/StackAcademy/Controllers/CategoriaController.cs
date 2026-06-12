using Microsoft.AspNetCore.Mvc;
using StackAcademy.Interface;
using StackAcademy.Models;

namespace StackAcademy.Controllers;

[Route("api/[controller]")]
[ApiController]

public class CategoriaController : Controller
{
    private readonly ICategoriaRepository _CategoriaRepository;

    public CategoriaController(ICategoriaRepository CategoriaRepository)
    {
        _CategoriaRepository = CategoriaRepository;
    }

    [HttpGet("{id}")]
    public IActionResult GetById(Guid id)
    {
        try
        {
            return Ok(_CategoriaRepository.BuscarPorId(id));
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
            return Ok(_CategoriaRepository.Listar());
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public IActionResult Post(Categoria NovaCategoria)
    {
        try
        {
            _CategoriaRepository.Cadastrar(NovaCategoria);
            return StatusCode(201);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public IActionResult PutById(Guid id, Categoria CategoriaAtualizada) 
    {
        try
        {
            _CategoriaRepository.AtualizarIdUrl(id, CategoriaAtualizada);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut]
    public IActionResult Put(Categoria CategoriaAtualizada) 
    {
        try 
        {
            _CategoriaRepository.AtualizarIdCorpo(CategoriaAtualizada);
            return NoContent();
        } 
        catch  (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id) 
    {
        try
        {
            _CategoriaRepository.Deletar(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

}
