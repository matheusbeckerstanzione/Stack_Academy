namespace StackAcademy.Interface;

using StackAcademy.Models;
using static System.Net.WebRequestMethods;

public interface IAlunoRepository
{

    void Cadastrar(Aluno NovoAluno);

    Aluno BuscarPorId(Guid Id);

    Aluno BuscarPorEmailSenha(string Email, string Senha);

    List<Aluno> Listar();

    void Deletar(Guid Id);

    void AtualizarIdCorpo(Aluno AlunoAtualizado);
    void AtualizarIdUrl(Guid IdAluno, Aluno AlunoAtualizado);

}