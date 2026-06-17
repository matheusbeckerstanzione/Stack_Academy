using StackAcademy.Models;

namespace StackAcademy.Interface;

public interface ICursoRepository
{
    Curso BuscarPorId(Guid Id);
    List<Curso> Listar();
    void Cadastrar(Curso curso);
    void Deletar(Guid Id);
    void AtualizarIdCorpo(Curso CursoAtualizado);
    void AtualizarIdUrl(Guid Id, Curso CursoAtualizado);
}
