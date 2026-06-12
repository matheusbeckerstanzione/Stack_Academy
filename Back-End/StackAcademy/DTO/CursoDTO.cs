namespace StackAcademy.DTO;

public class CursoDTO
{
    public Guid? IdCurso { get; set; }
    public string? Nome { get; set; }
    public DateTime? DataInicio { get; set; }
    public DateTime? DataFim { get; set; }
    public Guid? IdProfessor { get; set; }
    public Guid IdCategoria { get; set; }

}
