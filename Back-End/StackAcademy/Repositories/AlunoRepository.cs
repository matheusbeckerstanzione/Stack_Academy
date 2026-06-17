using Microsoft.AspNetCore.Mvc.Filters;
using StackAcademy.BdContextCursos;
using StackAcademy.Interface;
using StackAcademy.Models;
using StackAcademy.Utils;
using static System.Net.WebRequestMethods;

namespace StackAcademy.Repository;

public class AlunoRepository : IAlunoRepository
{
    private readonly CursosContext _context;

    public AlunoRepository(CursosContext context)
    {
        _context = context;
    }

    public void AtualizarIdCorpo(Aluno AlunoAtualizado)
    {
        try
        {
            Aluno AlunoBuscando = _context.Alunos.Find(AlunoAtualizado.IdAluno)!;

            if (AlunoBuscando != null)
            {
                AlunoBuscando.Nome = AlunoAtualizado.Nome;
            }

            _context.Alunos.Update(AlunoBuscando);
            _context.SaveChanges();

        }
        catch (Exception)
        {

            throw;
        }
    }

    public void AtualizarIdUrl(Guid IdAluno, Aluno AlunoAtualizado)
    {
        try
        {
            Aluno AlunoBuscado = _context.Alunos.Find(IdAluno)!;

            if (AlunoBuscado != null)
            {
                AlunoBuscado.Nome = AlunoAtualizado.Nome;
            }

            _context.Alunos.Update(AlunoBuscado!);
            _context.SaveChanges();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public Aluno BuscarPorEmailSenha(string Email, string Senha)
    {
        try
        {
            Aluno AlunoBuscado = _context.Alunos.FirstOrDefault(u => u.Email == Email)!;

            if (AlunoBuscado != null)
            {
                bool confere = Criptografia.ComapararHash(Senha, AlunoBuscado.Senha);

                if (confere)
                {

                    {
                        return AlunoBuscado;
                    }

                }

            }
            return null;

        }

        catch (Exception)
        {

            throw;
        }
    }

    public Aluno BuscarPorId(Guid Id)
    {
        try
        {
            Aluno AlunoBuscando = _context.Alunos.Find(Id)!;
            return AlunoBuscando;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public void Cadastrar(Aluno NovoAluno)
    {
        try
        {
            NovoAluno.IdAluno = Guid.NewGuid();

            NovoAluno.Senha = Criptografia.GerarHash(NovoAluno.Senha!);


            _context.Alunos.Add(NovoAluno);

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
            Aluno AlunoBuscando = _context.Alunos.Find(Id)!;

            if (AlunoBuscando != null)
            {
                _context.Alunos.Remove(AlunoBuscando);
            }
            _context.SaveChanges();
        }
        catch (Exception)
        {

            throw;
        }
    }

    public List<Aluno> Listar()
    {
        try
        {
            List<Aluno> ListarAlunos = _context.Alunos.ToList();

            return ListarAlunos;
        }

        catch (Exception ex)
        {
            throw;
        }
    }


}