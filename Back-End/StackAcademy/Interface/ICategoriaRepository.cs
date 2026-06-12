using StackAcademy.Models;

namespace StackAcademy.Interface;

public interface ICategoriaRepository
{
    Categoria BuscarPorId(Guid Id);
    List<Categoria> Listar();
    void Cadastrar(Categoria categoria);
    void Deletar(Guid Id);
    void AtualizarIdCorpo(Categoria CategoriaAtualizada);
    void AtualizarIdUrl(Guid Id, Categoria CategoriaAtualizada);

}
