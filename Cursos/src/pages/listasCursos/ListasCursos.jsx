import Header from "../../components/header/Header";
import Footer from "../../components/footer/Footer";
import "./ListasCursos.css";
import { Alerta } from "../../components/alerta/Alerta";
import { useEffect, useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import api from "../../services/Services";

const ListasCursos = () => {
  const navigate = useNavigate();
  const user = (() => {
    const loggedUser = localStorage.getItem("usuario");
    return loggedUser ? JSON.parse(loggedUser) : null;
  })();

  const [listaCursos, setListaCursos] = useState([]);
  const [titulo, setTitulo] = useState("");
  const [categoria, setCategoria] = useState("");
  const [professor, setProfessor] = useState("");
  const [ativo, setAtivo] = useState(true);
  const [busca, setBusca] = useState("");

  const [editando, setEditando] = useState(false);
  const [idCursoEdicao, setIdCursoEdicao] = useState(null);
  const [refreshTrigger, setRefreshTrigger] = useState(0);

  // Verificação de autenticação
  useEffect(() => {
    if (!user) {
      navigate("/login");
    }
  }, [user, navigate]);

  // Buscar cursos
  useEffect(() => {
    if (!user) return;
    let active = true;
    const fetchCursos = async () => {
      try {
        const response = await api.get("/cursos");
        if (active) {
          setListaCursos(response.data);
        }
      } catch (error) {
        console.error("Erro ao buscar cursos:", error);
        if (active) {
          Alerta({
            title: "Erro",
            text: "Não foi possível carregar a lista de cursos.",
            icon: "error",
            confirmButtonText: "OK",
          });
        }
      }
    };
    fetchCursos();
    return () => {
      active = false;
    };
  }, [user, refreshTrigger]);

  const triggerRefresh = () => {
    setRefreshTrigger((prev) => prev + 1);
  };



  const preEditar = (curso) => {
    setEditando(true);
    setIdCursoEdicao(curso.id);
    setTitulo(curso.titulo);
    setCategoria(curso.categoria);
    setProfessor(curso.professor);
    setAtivo(curso.ativo);
  };

  const salvarEdicao = async (e) => {
    e.preventDefault();

    if (!titulo.trim() || !categoria.trim() || !professor.trim()) {
      Alerta({
        title: "Atenção",
        text: "Preencha todos os campos do formulário.",
        icon: "warning",
        confirmButtonText: "OK",
      });
      return;
    }

    const cursoEditado = {
      titulo: titulo.trim(),
      categoria: categoria.trim(),
      professor: professor.trim(),
      ativo: ativo,
    };

    try {
      await api.put(`/cursos/${idCursoEdicao}`, cursoEditado);
      Alerta({
        title: "Atualizado!",
        text: `Curso "${titulo}" atualizado com sucesso.`,
        icon: "success",
        confirmButtonText: "OK",
      });
      limparFormulario();
      triggerRefresh();
    } catch (error) {
      console.error("Erro ao atualizar curso:", error);
      Alerta({
        title: "Erro",
        text: "Não foi possível atualizar o curso.",
        icon: "error",
        confirmButtonText: "OK",
      });
    }
  };

  const excluirCurso = async (curso) => {
    const result = await Alerta({
      title: "Confirmar exclusão",
      text: `Tem certeza de que deseja excluir o curso "${curso.titulo}"?`,
      icon: "warning",
      showCancelButton: true,
      confirmButtonColor: "#3085d6",
      cancelButtonColor: "#d33",
      confirmButtonText: "Sim, excluir!",
      cancelButtonText: "Não, cancelar!",
    });

    if (!result.isConfirmed) {
      return;
    }

    try {
      await api.delete(`/cursos/${curso.id}`);
      Alerta({
        title: "Excluído!",
        text: `Curso "${curso.titulo}" excluído com sucesso.`,
        icon: "success",
        confirmButtonText: "OK",
      });
      triggerRefresh();
    } catch (error) {
      console.error("Erro ao excluir curso:", error);
      Alerta({
        title: "Erro",
        text: "Não foi possível excluir o curso.",
        icon: "error",
        confirmButtonText: "OK",
      });
    }
  };

  const limparFormulario = () => {
    setTitulo("");
    setCategoria("");
    setProfessor("");
    setAtivo(true);
    setEditando(false);
    setIdCursoEdicao(null);
  };

  const matricularNoCurso = (curso) => {
    Alerta({
      title: "Inscrição Confirmada!",
      text: `Inscrição realizada com sucesso no curso "${curso.titulo}"!`,
      icon: "success",
      confirmButtonText: "Fechar",
      confirmButtonColor: "#7c4dff"
    });
  };

  const cursosFiltrados = listaCursos.filter(
    (curso) =>
      curso.titulo?.toLowerCase().includes(busca.toLowerCase()) ||
      curso.categoria?.toLowerCase().includes(busca.toLowerCase()) ||
      curso.professor?.toLowerCase().includes(busca.toLowerCase())
  );

  if (!user) {
    return null;
  }

  return (
    <>
      <Header />
      <div className="cc-page">
        <div className="cc-dashboard">
          {/* Card de Cadastro / Edição */}
          {editando && (
            <div className="cc-card">
              <h2 className="cc-title">Editar Curso</h2>
              <form onSubmit={salvarEdicao} className="cc-form">
                <div className="cc-field-group">
                  <label className="cc-label" htmlFor="titulo">
                    Nome do Curso
                  </label>
                  <input
                    id="titulo"
                    type="text"
                    className="cc-input"
                    placeholder="Ex: React para Iniciantes"
                    value={titulo}
                    onChange={(e) => setTitulo(e.target.value)}
                  />
                </div>

                <div className="cc-field-group">
                  <label className="cc-label" htmlFor="categoria">
                    Categoria
                  </label>
                  <input
                    id="categoria"
                    type="text"
                    className="cc-input"
                    placeholder="Ex: Frontend, Backend, Design..."
                    value={categoria}
                    onChange={(e) => setCategoria(e.target.value)}
                  />
                </div>

                <div className="cc-field-group">
                  <label className="cc-label" htmlFor="professor">
                    Professor
                  </label>
                  <input
                    id="professor"
                    type="text"
                    className="cc-input"
                    placeholder="Ex: Lucas Silva"
                    value={professor}
                    onChange={(e) => setProfessor(e.target.value)}
                  />
                </div>

                <div className="cc-field-group-checkbox">
                  <label className="cc-label-checkbox">
                    <input
                      type="checkbox"
                      checked={ativo}
                      onChange={(e) => setAtivo(e.target.checked)}
                    />
                    <span>Curso Ativo</span>
                  </label>
                </div>

                <div className="cc-actions">
                  <button type="submit" className="cc-btn-primary">
                    Salvar
                  </button>
                  <button
                    type="button"
                    className="cc-btn-cancel"
                    onClick={limparFormulario}
                  >
                    Cancelar
                  </button>
                </div>
              </form>
            </div>
          )}

          {/* Card da Tabela de Listagem */}
          <div className="cc-table-card">
            <div className="cc-table-header">
              <h2 className="cc-table-title">Cursos Cadastrados</h2>
              <div style={{ display: "flex", gap: "16px", alignItems: "center" }}>
                {user?.role === "admin" && (
                  <Link to="/cadastrar-curso" className="cc-btn-add">
                    + Cadastrar Curso
                  </Link>
                )}
                <div className="cc-search-wrapper">
                  <input
                    type="text"
                    placeholder="Buscar cursos..."
                    className="cc-search-input"
                    value={busca}
                    onChange={(e) => setBusca(e.target.value)}
                  />
                </div>
              </div>
            </div>

            <table className="cc-table">
              <thead>
                <tr>
                  <th>Curso</th>
                  <th>Categoria</th>
                  <th>Professor</th>
                  <th>Status</th>
                  <th>Ações</th>
                </tr>
              </thead>
              <tbody>
                {cursosFiltrados.length === 0 ? (
                  <tr>
                    <td colSpan={5} className="cc-empty-cell">
                      <div className="cc-empty">
                        <span className="cc-empty-icon">📚</span>
                        <p>Nenhum curso encontrado.</p>
                      </div>
                    </td>
                  </tr>
                ) : (
                  cursosFiltrados.map((curso) => (
                    <tr key={curso.id}>
                      <td className="cc-td-bold">{curso.titulo}</td>
                      <td>
                        <span className="cc-badge-category">
                          {curso.categoria}
                        </span>
                      </td>
                      <td>{curso.professor}</td>
                      <td>
                        <span
                          className={`cc-status-badge ${
                            curso.ativo ? "ativo" : "inativo"
                          }`}
                        >
                          {curso.ativo ? "Ativo" : "Inativo"}
                        </span>
                      </td>
                      <td className="cc-td-actions">
                        {user?.role === "admin" ? (
                          <>
                            <button
                              className="cc-table-action editar"
                              onClick={() => preEditar(curso)}
                            >
                              Editar
                            </button>
                            <button
                              className="cc-table-action excluir"
                              onClick={() => excluirCurso(curso)}
                            >
                              Excluir
                            </button>
                          </>
                        ) : (
                          <button
                            className="cc-table-action editar"
                            onClick={() => matricularNoCurso(curso)}
                            style={{
                              borderColor: curso.ativo ? "#2ecc71" : "#444",
                              color: curso.ativo ? "#2ecc71" : "#888",
                              cursor: curso.ativo ? "pointer" : "not-allowed",
                              backgroundColor: curso.ativo ? "rgba(46, 204, 113, 0.1)" : "transparent"
                            }}
                            disabled={!curso.ativo}
                          >
                            {curso.ativo ? "Matricular-se" : "Indisponível"}
                          </button>
                        )}
                      </td>
                    </tr>
                  ))
                )}
              </tbody>
            </table>
          </div>
        </div>
      </div>
      <Footer />
    </>
  );
};

export default ListasCursos;
