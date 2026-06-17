using StackAcademy.BdContextCursos;
using StackAcademy.Interface;
using StackAcademy.Models;

namespace StackAcademy.Repositories;

public class CategoriaRepository : ICategoriaRepository
{
    private readonly CursosContext _context;

    public CategoriaRepository(CursosContext context) 
    {
        _context = context;
    }

    public void AtualizarIdCorpo(Categoria CategoriaAtualizada)
    {
        try 
        {
            Categoria CategoriaBuscada = _context.Categoria.Find(CategoriaAtualizada.IdCategoria);

            if (CategoriaBuscada != null)
            {
                CategoriaBuscada.Nome = CategoriaAtualizada.Nome;
            }

            _context.Categoria.Update(CategoriaBuscada);
            _context.SaveChanges();

        } 
        catch (Exception)
        {
            throw;
        }
    }

    public void AtualizarIdUrl(Guid Id, Categoria CategoriaAtualizada)
    {
        try
        {
            Categoria CategoriaBuscada = _context.Categoria.Find(Id)!;

            if (CategoriaBuscada != null)
            {
                CategoriaBuscada.Nome = CategoriaAtualizada.Nome;

                _context.Categoria.Update(CategoriaBuscada);
                _context.SaveChanges();
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    public Categoria BuscarPorId(Guid Id)
    {
        try
        {
            Categoria CategoriaBuscada = _context.Categoria.Find(Id)!;
            return CategoriaBuscada;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public void Cadastrar(Categoria NovaCategoria)
    {
        try
        {
            NovaCategoria.IdCategoria = Guid.NewGuid();

            _context.Categoria.Add(NovaCategoria);
            _context.SaveChanges();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public void Deletar(Guid Id) 
    {
        try 
        {
            Categoria CategoriaBuscada = _context.Categoria.Find(Id)!;

            if (CategoriaBuscada != null)
            {
                _context.Categoria.Remove(CategoriaBuscada);
            }

            _context.SaveChanges();

        } 
        catch (Exception)
        {
            throw;
        }
    }

    public List<Categoria> Listar() 
    {
        try
        {
            List<Categoria> ListarCategoria = _context.Categoria.ToList();

            return ListarCategoria;
        }
        catch (Exception)
        {
            throw;
        }
    }

}
