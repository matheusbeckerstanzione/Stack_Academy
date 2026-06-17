import "./Header.css";
import { Link, useNavigate } from "react-router-dom";
import { useState } from "react";
import chatgptLogo from "../../assets/img/chatgpt-logo.png";

const Header = () => {
    const navigate = useNavigate();
    const [user] = useState(() => {
        const loggedUser = localStorage.getItem("usuario");
        return loggedUser ? JSON.parse(loggedUser) : null;
    });

    const handleLogout = () => {
        localStorage.removeItem("usuario");
        navigate("/login");
    };

    return (
        <header>
            <div className="cabecalho" style={{ justifyContent: "space-between" }}>
                {/* Lado esquerdo: Logo + Usuário logado */}
                <div style={{ display: "flex", alignItems: "center", gap: "20px" }}>
                    <Link to="/" style={{ display: "flex", alignItems: "center" }}>
                        <img 
                            src={chatgptLogo} 
                            alt="Plataforma Logo" 
                            style={{ height: "35px", width: "auto", cursor: "pointer" }} 
                        />
                    </Link>

                    {user && (
                        <div style={{ color: "#aaa", fontSize: "0.85rem", display: "flex", alignItems: "center", gap: "8px" }}>
                            <span style={{ width: "8px", height: "8px", borderRadius: "50%", background: user.role === "admin" ? "#7c4dff" : "#2ecc71" }} />
                            <span>Olá, <strong style={{ color: "#fff" }}>{user.nome}</strong> ({user.role === "admin" ? "Admin" : "Aluno"})</span>
                        </div>
                    )}
                </div>

                {/* Lado direito: Navegação */}
                <nav className="nav_header">
                    <Link className="link_header" to="/">Home</Link>
                    
                    {/* Aluno vê como "Cursos", Admin vê como "Gerenciar Cursos" */}
                    <Link className="link_header" to="/cursos">
                        {user?.role === "admin" ? "Gerenciar Cursos" : "Cursos"}
                    </Link>

                    {/* Somente Admin vê Cadastrar Curso */}
                    {user?.role === "admin" && (
                        <Link className="link_header" to="/cadastrar-curso">Cadastrar Curso</Link>
                    )}

                    {user && (
                        <button onClick={handleLogout} className="link_header btn_logout" style={{ marginLeft: "12px", border: "1px solid #333" }}>
                            Sair
                        </button>
                    )}
                </nav>
            </div>
        </header>
    );
};

export default Header;
