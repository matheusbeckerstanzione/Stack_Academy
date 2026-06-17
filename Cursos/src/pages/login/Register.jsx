import { useState, useEffect } from "react";
import { useNavigate, Link } from "react-router-dom";
import api from "../../services/Services";
import { Alerta } from "../../components/alerta/Alerta";
import "../../components/alerta/Alerta.css";
import "./Register.css";

const Register = () => {
  const navigate = useNavigate();

  const [nome, setNome] = useState("");
  const [email, setEmail] = useState("");
  const [cpf, setCpf] = useState("");
  const [senha, setSenha] = useState("");
  const [role, setRole] = useState("aluno"); // default is aluno

  useEffect(() => {
    // Se o usuário já estiver logado, redireciona para a Home
    const loggedUser = localStorage.getItem("usuario");
    if (loggedUser) {
      navigate("/");
    }
  }, [navigate]);

  const formatCPF = (value) => {
    const cleanValue = value.replace(/\D/g, "");
    const truncated = cleanValue.slice(0, 11);
    
    if (truncated.length <= 3) return truncated;
    if (truncated.length <= 6) return `${truncated.slice(0, 3)}.${truncated.slice(3)}`;
    if (truncated.length <= 9) return `${truncated.slice(0, 3)}.${truncated.slice(3, 6)}.${truncated.slice(6)}`;
    return `${truncated.slice(0, 3)}.${truncated.slice(3, 6)}.${truncated.slice(6, 9)}-${truncated.slice(9)}`;
  };

  const handleRegister = async (e) => {
    e.preventDefault();

    if (!nome.trim() || !email.trim() || !senha.trim() || !cpf.trim()) {
      Alerta({
        title: "Atenção",
        text: "Por favor, preencha todos os campos obrigatórios.",
        icon: "warning",
        confirmButtonText: "Ok",
        confirmButtonColor: "#7C3AFF"
      });
      return;
    }

    if (cpf.replace(/\D/g, "").length !== 11) {
      Alerta({
        title: "CPF Inválido",
        text: "O CPF deve conter exatamente 11 dígitos.",
        icon: "warning",
        confirmButtonText: "Corrigir",
        confirmButtonColor: "#7C3AFF"
      });
      return;
    }

    try {
      // Verificar se e-mail já existe
      const response = await api.get("/usuarios");
      const usuarios = response.data;

      const emailExiste = usuarios.some(
        (user) => user.email.toLowerCase() === email.toLowerCase()
      );

      if (emailExiste) {
        Alerta({
          title: "E-mail Já Cadastrado",
          text: "Este endereço de e-mail já está em uso na plataforma.",
          icon: "warning",
          confirmButtonText: "Entendido",
          confirmButtonColor: "#7C3AFF"
        });
        return;
      }

      // Salvar novo usuário
      const novoUsuario = {
        nome: nome.trim(),
        email: email.trim().toLowerCase(),
        cpf: cpf,
        senha: senha,
        role: role
      };

      await api.post("/usuarios", novoUsuario);

      await Alerta({
        title: "Conta Criada!",
        text: `Parabéns, ${nome}! Sua conta foi cadastrada com sucesso como ${
          role === "admin" ? "Administrador" : "Aluno"
        }.`,
        icon: "success",
        confirmButtonText: "Ir para Login",
        confirmButtonColor: "#7C3AFF"
      });

      navigate("/login");
    } catch (error) {
      console.error("Erro ao cadastrar usuário:", error);
      Alerta({
        title: "Erro no Cadastro",
        text: "Não foi possível conectar ao servidor. Tente novamente.",
        icon: "error",
        confirmButtonText: "Fechar",
        confirmButtonColor: "#e74c3c"
      });
    }
  };

  return (
    <div className="register-container">
      <div className="register-card">
        <div className="register-header">
          <h2>Criar Nova Conta</h2>
          <p>Junte-se à nossa plataforma de cursos</p>
        </div>

        <form onSubmit={handleRegister} className="register-form">
          <div className="register-field-group">
            <label htmlFor="nome">Nome Completo</label>
            <input
              id="nome"
              type="text"
              placeholder="Digite seu nome"
              value={nome}
              onChange={(e) => setNome(e.target.value)}
              required
            />
          </div>

          <div className="register-field-group">
            <label htmlFor="email">E-mail</label>
            <input
              id="email"
              type="email"
              placeholder="Digite seu melhor e-mail"
              value={email}
              onChange={(e) => setEmail(e.target.value)}
              required
            />
          </div>

          <div className="register-field-group">
            <label htmlFor="cpf">CPF</label>
            <input
              id="cpf"
              type="text"
              placeholder="000.000.000-00"
              value={cpf}
              onChange={(e) => setCpf(formatCPF(e.target.value))}
              required
            />
          </div>

          <div className="register-field-group">
            <label htmlFor="senha">Senha</label>
            <input
              id="senha"
              type="password"
              placeholder="Crie uma senha de acesso"
              value={senha}
              onChange={(e) => setSenha(e.target.value)}
              required
            />
          </div>

          {/* Seleção de Perfil (Role Cards) */}
          <div className="register-field-group">
            <label>Tipo de Perfil</label>
            <div className="role-cards-container">
              <div
                className={`role-card ${role === "aluno" ? "active" : ""}`}
                onClick={() => setRole("aluno")}
              >
                <div className="role-card-icon">🎓</div>
                <div className="role-card-info">
                  <h4>Aluno</h4>
                  <p>Aprenda e assista</p>
                </div>
              </div>

              <div
                className={`role-card ${role === "admin" ? "active" : ""}`}
                onClick={() => setRole("admin")}
              >
                <div className="role-card-icon">⚡</div>
                <div className="role-card-info">
                  <h4>Admin</h4>
                  <p>Cadastre e gerencie</p>
                </div>
              </div>
            </div>
          </div>

          <button type="submit" className="register-btn">
            Cadastrar Conta
          </button>
        </form>

        <div className="register-footer">
          <p>
            Já tem uma conta? <Link to="/login">Faça login</Link>
          </p>
        </div>
      </div>
    </div>
  );
};

export default Register;
