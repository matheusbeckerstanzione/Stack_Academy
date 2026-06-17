using StackAcademy.BdContextCursos;
using StackAcademy.Interface;
using StackAcademy.Models;

namespace StackAcademy.Repositories;

public class CursoRepository : ICursoRepository
{
    private readonly CursosContext _context;

    public CursoRepository(CursosContext context) 
    {
        _context = context;
    }

    public void AtualizarIdCorpo(Curso CursoAtualizado)
    {
        try
        {
            Curso CursoBuscado = _context.Cursos.Find(CursoAtualizado.IdCurso);

            if (CursoBuscado != null)
            {
                CursoBuscado.Nome = CursoAtualizado.Nome;
            }

            _context.Cursos.Update(CursoBuscado);
            _context.SaveChanges();

        }
        catch (Exception)
        {
            throw;
        }
    }

    public void AtualizarIdUrl(Guid Id, Curso CursoAtualizado) 
    {
        try
        {
            Curso CursoBuscado = _context.Cursos.Find(Id)!;

            if (CursoBuscado != null)
            {
                CursoBuscado.Nome = CursoAtualizado.Nome;

                _context.Cursos.Update(CursoBuscado);
                _context.SaveChanges();
            }

        }
        catch (Exception)
        {
            throw;
        }
    }

    public Curso BuscarPorId(Guid Id) 
    {
        try
        {
            Curso CursoBuscado = _context.Cursos.Find(Id)!;
            return CursoBuscado;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public void Cadastrar(Curso NovoCurso)
    {
        try
        {
            NovoCurso.IdCurso = Guid.NewGuid();

            _context.Cursos.Add(NovoCurso);
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
            Curso CursoBuscado = _context.Cursos.Find(Id)!;

            if (CursoBuscado != null)
            {
                _context.Cursos.Remove(CursoBuscado);
            }

            _context.SaveChanges();

        }
        catch (Exception)
        {
            throw;
        }
    }

    public List<Curso> Listar()
    {
        try
        {
            List<Curso> ListarCurso = _context.Cursos.ToList();

            return ListarCurso;
        }
        catch (Exception)
        {
            throw;
        }
    }

}
