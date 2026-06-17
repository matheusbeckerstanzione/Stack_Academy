import { useState, useEffect } from "react";
import { motion } from "motion/react";
import { useNavigate } from "react-router-dom";
import Header from "../../components/header/Header";
import Footer from "../../components/footer/Footer";
import api from "../../services/Services";
import "./Home.css";

/* ── Constantes ── */
const CARD_W   = 300;
const CARD_GAP = 24;
const ITEM_W   = CARD_W + CARD_GAP;
const SPEED    = 80; // px por segundo — velocidade constante

/* ── Cores por categoria ── */
const CAT = {
  Frontend: { text: "#a27cff", border: "rgba(124,77,255,0.5)", glow: "rgba(124,77,255,0.35)", bg: "rgba(124,77,255,0.12)", grad: "135deg,#1a1040 0%,#0d0d0d 100%" },
  Backend:  { text: "#2ecc71", border: "rgba(46,204,113,0.5)",  glow: "rgba(46,204,113,0.35)",  bg: "rgba(46,204,113,0.12)",  grad: "135deg,#0d2b1a 0%,#0d0d0d 100%" },
  Design:   { text: "#e74c3c", border: "rgba(231,76,60,0.5)",   glow: "rgba(231,76,60,0.35)",   bg: "rgba(231,76,60,0.12)",   grad: "135deg,#2b0d0d 0%,#0d0d0d 100%" },
  DevOps:   { text: "#3498db", border: "rgba(52,152,219,0.5)",  glow: "rgba(52,152,219,0.35)",  bg: "rgba(52,152,219,0.12)",  grad: "135deg,#0d1e2b 0%,#0d0d0d 100%" },
  Mobile:   { text: "#f39c12", border: "rgba(243,156,18,0.5)",  glow: "rgba(243,156,18,0.35)",  bg: "rgba(243,156,18,0.12)",  grad: "135deg,#2b1f0d 0%,#0d0d0d 100%" },
  default:  { text: "#a27cff", border: "rgba(124,77,255,0.5)", glow: "rgba(124,77,255,0.35)", bg: "rgba(124,77,255,0.12)", grad: "135deg,#1a1040 0%,#0d0d0d 100%" },
};

/* ── ScrambleText ── */
function ScrambleText({ text, active }) {
  const [display, setDisplay] = useState(text);
  const chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789@#$%&*";

  useEffect(() => {
    if (!active) {
      const t = setTimeout(() => setDisplay(text), 0);
      return () => clearTimeout(t);
    }
    let iter = 0;
    const iv = setInterval(() => {
      setDisplay(
        text.split("").map((_, i) =>
          i < iter ? text[i] : chars[Math.floor(Math.random() * chars.length)]
        ).join("")
      );
      if (iter >= text.length) clearInterval(iv);
      iter += 1 / 3;
    }, 30);
    return () => clearInterval(iv);
  }, [active, text]);

  return <span>{display}</span>;
}

/* ── Card ── */
function CourseCard({ curso, rank }) {
  const [hov, setHov] = useState(false);
  const navigate = useNavigate();
  const c = CAT[curso.categoria] || CAT.default;

  return (
    <motion.div
      className="big-card"
      onHoverStart={() => setHov(true)}
      onHoverEnd={() => setHov(false)}
      whileHover={{ scale: 1.04, y: -10, zIndex: 30 }}
      transition={{ type: "spring", stiffness: 320, damping: 22 }}
      style={{
        background: `linear-gradient(${c.grad})`,
        borderColor: hov ? c.border : "rgba(255,255,255,0.07)",
        boxShadow: hov
          ? `0 0 40px ${c.glow}, 0 20px 48px rgba(0,0,0,0.6)`
          : "0 12px 36px rgba(0,0,0,0.45)",
      }}
    >
      {/* Glow de fundo */}
      <div className="big-card-glow" style={{ background: c.glow }} />

      {/* Número */}
      <p className="big-card-rank" style={{ color: c.text }}>
        #{String(rank).padStart(2, "0")}
      </p>

      {/* Badge categoria */}
      <span
        className="big-card-cat"
        style={{ color: c.text, background: c.bg, border: `1px solid ${c.border}` }}
      >
        {curso.categoria}
      </span>

      {/* Título com scramble */}
      <h3 className="big-card-title">
        <ScrambleText text={curso.titulo.toUpperCase()} active={hov} />
      </h3>

      <p className="big-card-prof">{curso.professor}</p>

      <span className={`big-card-status ${curso.ativo ? "ativo" : "inativo"}`}>
        <span className="big-dot" />
        {curso.ativo ? "ATIVO" : "INATIVO"}
      </span>

      {/* Botão Saiba mais — navega para /cursos */}
      <motion.button
        className="big-card-cta"
        initial={{ opacity: 0, y: 8 }}
        animate={{ opacity: hov ? 1 : 0, y: hov ? 0 : 8 }}
        transition={{ duration: 0.18 }}
        style={{ color: c.text, borderColor: c.border }}
        onClick={() => navigate("/cursos")}
      >
        Saiba mais →
      </motion.button>

      {/* Borda brilhante hover */}
      <motion.div
        className="big-card-border"
        animate={{ opacity: hov ? 1 : 0 }}
        style={{ border: `1px solid ${c.border}` }}
      />
    </motion.div>
  );
}

/* ── Fileira infinita fluida (CSS @keyframes — zero corte) ── */
function InfiniteRow({ cursos }) {
  // Duração para percorrer uma cópia completa na velocidade constante
  const duration = (ITEM_W * cursos.length) / SPEED;
  // 2 cópias idênticas: o CSS move -50% (= 1 cópia) e reinicia — seamless
  const items = [...cursos, ...cursos];

  return (
    <div className="inf-row-outer">
      {/*
        A div usa CSS animation com translateX(-50%).
        Com 2 cópias idênticas, -50% da largura total = 1 cópia exata.
        O browser garante a continuidade sub-frame sem nenhum salto.
      */}
      <div
        className="inf-row-inner"
        style={{ animationDuration: `${duration}s` }}
      >
        {items.map((curso, i) => (
          <CourseCard
            key={`${curso.id}-${i}`}
            curso={curso}
            rank={(i % cursos.length) + 1}
          />
        ))}
      </div>
    </div>
  );
}

/* ── Home ── */
export default function Home() {
  const navigate = useNavigate();
  const [cursos, setCursos] = useState([]);
  const user = (() => {
    const loggedUser = localStorage.getItem("usuario");
    return loggedUser ? JSON.parse(loggedUser) : null;
  })();

  useEffect(() => {
    if (!user) {
      navigate("/login");
      return;
    }

    let alive = true;
    api.get("/cursos")
      .then(r => { if (alive) setCursos(r.data); })
      .catch(console.error);
    return () => { alive = false; };
  }, [user, navigate]);

  if (!user) {
    return null;
  }

  return (
    <>
      <Header />
      <div className="home-wrapper">

        {/* Hero */}
        <section className="home-hero">
          <motion.h1
            className="home-hero-title"
            initial={{ opacity: 0, y: 40 }}
            animate={{ opacity: 1, y: 0 }}
            transition={{ duration: 0.9, ease: [0.22, 1, 0.36, 1] }}
          >
            Explore nossos <span>Cursos</span>
          </motion.h1>
          <motion.p
            className="home-hero-sub"
            initial={{ opacity: 0 }}
            animate={{ opacity: 1 }}
            transition={{ delay: 0.5, duration: 0.8 }}
          >
            Passe o mouse nos cards e clique em Saiba mais
          </motion.p>
        </section>

        {/* Faixa diagonal + fileira infinita */}
        {cursos.length > 0 && (
          <div className="home-band">
            <div className="vel-fade-left" />
            <div className="vel-fade-right" />
            <div className="band-skew">
              <InfiniteRow cursos={cursos} />
            </div>
          </div>
        )}

        <div className="home-scroll-space" />
      </div>
      <Footer />
    </>
  );
}
