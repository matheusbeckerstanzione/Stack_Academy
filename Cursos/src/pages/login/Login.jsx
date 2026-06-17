import { useState, useEffect } from "react";
import { useNavigate, Link } from "react-router-dom";
import api from "../../services/Services";
import { Alerta } from "../../components/alerta/Alerta";
import "../../components/alerta/Alerta.css";
import "./Login.css";

const Login = () => {
  const navigate = useNavigate();
  const [email, setEmail] = useState("");
  const [senha, setSenha] = useState("");

  useEffect(() => {
    // Se o usuário já estiver logado, redireciona para a Home
    const loggedUser = localStorage.getItem("usuario");
    if (loggedUser) {
      navigate("/");
    }
  }, [navigate]);

  const handleLogin = async (e) => {
    e.preventDefault();

    if (!email.trim() || !senha.trim()) {
      Alerta({
        title: "Atenção",
        text: "Preencha todos os campos.",
        icon: "warning",
        confirmButtonText: "Entendido",
        confirmButtonColor: "#7C3AFF"
      });
      return;
    }

    try {
      // Buscar usuários cadastrados
      const response = await api.get("/usuarios");
      const usuarios = response.data;

      // Encontrar usuário correspondente
      const usuarioEncontrado = usuarios.find(
        (user) => user.email.toLowerCase() === email.toLowerCase() && user.senha === senha
      );

      if (usuarioEncontrado) {
        localStorage.setItem("usuario", JSON.stringify(usuarioEncontrado));
        
        await Alerta({
          title: "Bem-vindo!",
          text: `Olá, ${usuarioEncontrado.nome}! Login realizado com sucesso.`,
          icon: "success",
          confirmButtonText: "Entrar",
          confirmButtonColor: "#7C3AFF"
        });

        navigate("/");
      } else {
        Alerta({
          title: "Erro de Acesso",
          text: "E-mail ou senha inválidos.",
          icon: "error",
          confirmButtonText: "Tentar Novamente",
          confirmButtonColor: "#e74c3c"
        });
      }
    } catch (error) {
      console.error("Erro ao realizar login:", error);
      Alerta({
        title: "Erro",
        text: "Houve um problema de conexão com o servidor.",
        icon: "error",
        confirmButtonText: "Fechar",
        confirmButtonColor: "#e74c3c"
      });
    }
  };

  return (
    <div className="login-container">
      <div className="login-card">
        <div className="login-header">
          <h2>Entrar na Plataforma</h2>
          <p>Insira suas credenciais para continuar</p>
        </div>

        <form onSubmit={handleLogin} className="login-form">
          <div className="login-field-group">
            <label htmlFor="email">E-mail</label>
            <input
              id="email"
              type="email"
              placeholder="Ex: seuemail@dominio.com"
              value={email}
              onChange={(e) => setEmail(e.target.value)}
              required
            />
          </div>

          <div className="login-field-group">
            <label htmlFor="senha">Senha</label>
            <input
              id="senha"
              type="password"
              placeholder="••••••••"
              value={senha}
              onChange={(e) => setSenha(e.target.value)}
              required
            />
          </div>

          <button type="submit" className="login-btn">
            Entrar
          </button>
        </form>

        <div className="login-footer">
          <p>
            Não tem uma conta? <Link to="/cadastro">Cadastre-se aqui</Link>
          </p>
        </div>
      </div>
    </div>
  );
};

export default Login;
