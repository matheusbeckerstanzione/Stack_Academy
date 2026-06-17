import "./Cadastro.css";
import Botao from "../botao/Botao";

// Destructuring nas props:
// const Cadastro = ({ 
//     cadastro, tituloCadastro, valor, setValor, estilo, 
//     valorSelect, setValorSelect, listaGeneros 
//   }) => {}
  

const Cadastro = (props) => {
    return (
        <section className="section_cadastro">
            <form onSubmit={props.funcCadastro} className="layout_grid form_cadastro" encType="multipart/form-data">
                <h1>{props.tituloCadastro}</h1>
                <hr />
                <div className="campos_cadastro">
                    <div className="campo_cad_nome">
                        <label htmlFor="nome">Nome</label>
                        <input type="text" name="nome" placeholder={`Digite o nome do ${props.placeholder}`} 
                        //O valor do input vem de props (estado do componente pai)
                        value={props.valor}
                        // Atualiza o estado do pai ao digitar
                        onChange={(e) => props.setValor(e.target.value)}
                        />
                    </div>
                    <div className="campo_cad_genero" style={{ display: props.visibilidade }}>
                        <label htmlFor="genero">Gênero</label>
                        <select name="genero" id=""
                        
                        onChange={(e)=>{
                            // para cadastro de filme
                            props.setValorGenero(e.target.value)
                        }}
                        >
                            <option value="" >Selecione</option>
                            {
                                props.listaGeneros?.map((item) => {
                                    return (
                                       <option key={item.idGenero} value={item.idGenero}>{item.nome}</option>
                                    )
                                })
                            }
                        </select>
                    </div>

                    <div className="campos_cadastro" style={{ display: props.visibilidade }}>  
                        <input 
                            type="file" 
                            id="imageUpload"
                            accept="image/*" 
                            value={props.imagem}
                            onChange={(e)=> {
                                props.setImagem(e.target.files[0])
                            }}  
                        />
                    </div>


                    {/* mostrar/esconder botão Cancelar */}
                    {
                        props.btnEditar && 
                        <Botao 
                            nomeDoBotao="Cancelar" 
                            btnEditar={props.btnEditar}
                            cancelarEdicao={props.cancelarEdicao}
                        />
                    }
                    <Botao nomeDoBotao="Cadastrar" />
                </div>
            </form>
        </section>
    )
}

export default Cadastro;