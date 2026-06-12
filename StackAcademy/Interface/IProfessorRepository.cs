namespace StackAcademy.Interface;

using StackAcademy.Models;
using static System.Net.WebRequestMethods;

public interface IProfessorRepository
{
    void Cadastrar(Professor NovoProfessor);

    Professor BuscarPorId(Guid id);

    Professor BuscarPorEmailSenha(string Email, string Senha);

    List<Professor> Listar();

    void Deletar(Guid Id);

    void AtualizarIdCorpo(Professor ProfessorAtualizado);
    void AtualizarIdUrl(Guid IdProfessor, Professor ProfessorAtualizado);
}
