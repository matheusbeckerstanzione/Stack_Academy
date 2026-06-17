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

    [HttpGet("{Id}")]
    public IActionResult GetById(Guid Id)
    {
        try
        {
            return Ok(_CategoriaRepository.BuscarPorId(Id));
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

    [HttpPut("{Id}")]
    public IActionResult PutById(Guid Id, Categoria CategoriaAtualizada) 
    {
        try
        {
            _CategoriaRepository.AtualizarIdUrl(Id, CategoriaAtualizada);
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

    [HttpDelete("{Id}")]
    public IActionResult Delete(Guid Id) 
    {
        try
        {
            _CategoriaRepository.Deletar(Id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

}
