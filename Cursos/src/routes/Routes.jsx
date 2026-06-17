import { BrowserRouter, Route, Routes } from "react-router-dom";
import ListasCursos from "../pages/listasCursos/ListasCursos";
import Home from "../pages/home/Home";
import { Cadastrar } from "../pages/cursos/Cursos";
import Login from "../pages/login/Login";
import Register from "../pages/login/Register";

const Rotas = () => {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<Home />} />
        <Route path="/cursos" element={<ListasCursos />} />
        <Route path="/cadastrar-curso" element={<Cadastrar />} />
        <Route path="/login" element={<Login />} />
        <Route path="/cadastro" element={<Register />} />
      </Routes>
    </BrowserRouter>
  );
};
export default Rotas;
