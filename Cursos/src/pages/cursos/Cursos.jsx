import "./Cursos.css";
import { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import Header from "../../components/header/Header";
import Footer from "../../components/footer/Footer";
import api from "../../services/Services";
import { Alerta } from "../../components/alerta/Alerta";
import "../../components/alerta/Alerta.css";

export const Cadastrar = () => {
    const navigate = useNavigate();
    const user = (() => {
        const loggedUser = localStorage.getItem("usuario");
        return loggedUser ? JSON.parse(loggedUser) : null;
    })();

    useEffect(() => {
        if (!user) {
            navigate("/login");
        } else if (user.role !== "admin") {
            navigate("/cursos");
        }
    }, [user, navigate]);

    const [titulo, setTitulo] = useState("");
    const [categoria, setCategoria] = useState("");
    const [atividade, setAtividade] = useState("");
    const [professor, setProfessor] = useState("");
    const [aberto, setAberto] = useState(false);

    if (!user || user.role !== "admin") {
        return null;
    }

    
    const cadastrarCurso = async (e) => {
        e.preventDefault();

        if (atividade === "") {
            Alerta({
                title: "Atenção",
                text: "Por favor, selecione se o curso está ativo.",
                icon: "warning",
                confirmButtonText: "Fechar",
                confirmButtonColor: "#7C3AFF"
            });
            return;
        }

        const novoCurso = {
            titulo: titulo.trim(),
            categoria: categoria.trim(),
            professor: professor.trim(),
            ativo: atividade === "sim"
        };

        try {
            await api.post("/cursos", novoCurso);
            
            await Alerta({
                title: "Cadastro de Curso",
                text: `${titulo} cadastrado com sucesso`,
                icon: "success",
                confirmButtonText: "Ir para a Lista",
                confirmButtonColor: "#7C3AFF"
            });
            
            limparDados();
            navigate("/cursos");
        } catch (error) {
            console.error(error);
            Alerta({
                title: "Erro",
                text: "Não foi possível cadastrar o curso.",
                icon: "error",
                confirmButtonText: "Fechar"
            });
        }
    };

    const limparDados = () => {
        setTitulo("");
        setCategoria("");
        setProfessor("");
        setAtividade("");
    };

    return (
        <>
            <Header />
            <div className="container-cursos">
                <form
                    className="form-cursos"
                    onSubmit={cadastrarCurso}
                >
                    <h1>Cadastrar Curso</h1>
                    
                    <input
                        required
                        placeholder="Nome do Curso"
                        value={titulo}
                        onChange={(e) => setTitulo(e.target.value)}
                    />
                    
                    <input
                        required
                        placeholder="Categoria"
                        value={categoria}
                        onChange={(e) => setCategoria(e.target.value)}
                    />
                    
                    <input
                        required
                        placeholder="Professor(a)"
                        value={professor}
                        onChange={(e) => setProfessor(e.target.value)}
                    />
                    
                    <div className="select-box">
                        <div
                            className="select-selected"
                            onClick={() => setAberto(!aberto)}
                        >
                            {atividade === ""
                                ? "Selecione se está ativo"
                                : atividade === "sim"
                                    ? "Sim"
                                    : "Não"
                            }
                            <span>⌄</span>
                        </div>
                        {aberto && (
                            <div className="select-options">
                                <div
                                    onClick={() => {
                                        setAtividade("sim");
                                        setAberto(false);
                                    }}
                                >
                                    Sim
                                </div>
                                <div
                                    onClick={() => {
                                        setAtividade("nao");
                                        setAberto(false);
                                    }}
                                >
                                    Não
                                </div>
                            </div>
                        )}
                    </div>
                    
                    <div style={{ display: "flex", gap: "12px", width: "100%", justifyContent: "center", marginTop: "15px" }}>
                        <button type="submit" style={{ margin: "0" }}>
                            Cadastrar
                        </button>
                        <button 
                            type="button" 
                            onClick={() => navigate("/cursos")} 
                            style={{ margin: "0", background: "#333", border: "1px solid #444" }}
                        >
                            Voltar
                        </button>
                    </div>
                </form>
            </div>
            <Footer />
        </>
    );
};