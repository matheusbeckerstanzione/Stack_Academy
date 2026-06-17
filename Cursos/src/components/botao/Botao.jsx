import "./Botao.css"

const Botao = (props) => {
    return(
        // Na função de login olá no Login.jsx
        
        <button 
            className="botao" 
            type={
                (props.btnEditar || props.btnLogin) ? "button" : "submit"
            }

            onClick={()=>{
                if(props.btnEditar){//tela de editar
                    props.cancelarEdicao()
                } else if (props.btnLogin) {//tela de login
                    props.fnLogin()
                } else {//qualquer outra tela
                    null
                }
                
            }}
        >
            {props.nomeDoBotao}
        </button>

    )
}

export default Botao;