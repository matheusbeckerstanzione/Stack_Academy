using Microsoft.AspNetCore.Mvc.Filters;
using StackAcademy.BdContextCursos;
using StackAcademy.Interface;
using StackAcademy.Models;
using StackAcademy.Utils;

namespace StackAcademy.Repository;

public class ProfessorRepository : IProfessorRepository
{

    private readonly CursosContext _context;

    public ProfessorRepository(CursosContext context)
    {
        _context = context;
    }

    public void AtualizarIdCorpo(Professor ProfessorAtualizado)
    {
        try
        {
            Professor ProfessorBuscando = _context.Professors.Find(ProfessorAtualizado.IdProfessor)!;

            if (ProfessorBuscando != null)
            {
                ProfessorBuscando.Nome = ProfessorAtualizado.Nome;
            }

            _context.Professors.Update(ProfessorBuscando);
            _context.SaveChanges();

        }
        catch (Exception)
        {

            throw;
        }
    }

    public void AtualizarIdUrl(Guid IdProfessor, Professor ProfessorAtualizado)
    {
        try
        {
            Professor ProfessorBuscado = _context.Professors.Find(IdProfessor);

            if (ProfessorBuscado != null)
            {
                ProfessorBuscado.Nome = ProfessorAtualizado.Nome;
            }

            _context.Professors.Update(ProfessorBuscado!);
            _context.SaveChanges();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public Professor BuscarPorEmailSenha(string Email, string Senha)
    {
        //começo do try
        try
        {
            Professor ProfessorBuscado = _context.Professors.FirstOrDefault(u => u.Email == Email)!;

            if (ProfessorBuscado != null)
            {
                bool confere = Criptografia.ComapararHash(Senha, ProfessorBuscado.Senha);//aq esta tento uma comparação 

                if (confere)
                {

                    {
                        return ProfessorBuscado;
                    }

                }

            }
            return null;//retornando nulo

        }//fim do try

        catch (Exception)
        {

            throw;
        }
    }

    public Professor BuscarPorId(Guid id)
    {
        try
        {
            Professor ProfessorBuscando = _context.Professors.Find(id.ToString());
            return ProfessorBuscando;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public void Cadastrar(Professor NovoProfessor)
    {
        try
        {
            NovoProfessor.IdProfessor = Guid.NewGuid();

            NovoProfessor.Senha = Criptografia.GerarHash(NovoProfessor.Senha!);


            _context.Professors.Add(NovoProfessor);

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
            Professor ProfessorBuscando = _context.Professors.Find(Id.ToString())!;

            if (ProfessorBuscando != null)
            {
                _context.Professors.Remove(ProfessorBuscando);
            }
            _context.SaveChanges();
        }
        catch (Exception)
        {

            throw;
        }
    }

    public List<Professor> Listar()
    {
        try
        {
            List<Professor> ListarProfessor = _context.Professors.ToList();

            return ListarProfessor;
        }

        catch (Exception ex)
        {
            throw;
        }
    }
}
