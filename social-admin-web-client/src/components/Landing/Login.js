import React, {useState} from "react";
import AuthService from "../../services/AuthService";
import {Link, useHistory} from "react-router-dom";
import Popup from './PopUp';
import './styles.css';

const Login = () => {
    /*
    Admin: ricardo.belmont21@gmail.com
    Analista: analista@email.com
    Entrevistador: entrevistador1@email.com
    Cliente: admin@estructura.com
    */
    const [email, setEmail] = useState("")
    const [password, setPassword] = useState("")

    const history = useHistory()

    const handleLogin = async (event) => {
        event.preventDefault()
        if (email === "") {
            setShowPopup(true);
            openPopup("Escribe el correo electrónico.");
        } else {
            setShowPopup(false);
            if (password === "") {
                setShowPopup(true);
                openPopup("Escribe la contraseña.");
            } else {
                setShowPopup(false);
                const response = await AuthService.login(email, password)
                if (response === 200) {
                    history.push("/dashboard")
                } else {
                    openPopup("Usuario y/o contraseña incorrectos. Intente de nuevo.");
                }
            }
        }
    }

    const [showPopup, setShowPopup] = useState(false);
    const [popupText, setPopupText] = useState(""); // Estado para almacenar el texto del pop-up

    const openPopup = (texto) => {
        setPopupText(texto);
        setShowPopup(true);
    };

    const closePopup = () => {
        setShowPopup(false);
    };

    return (
        <div className={"form"}>
            {/* <div className={"login-signup-section"}>
                <Link to={"/"}>
                    <button className={"login-button"}>login</button>
                </Link>
                <Link to={"/registrar"}>
                    <button className={"signup-button"}>sign up</button>
                </Link>
            </div> */}
            <form onSubmit={handleLogin}>
                <div className={"form-field"}>
                    <label>Correo</label>
                    <input type={"email"} placeholder={"Agregar correo"} value={email}
                           onChange={event => setEmail(event.target.value)}/>
                </div>
                <div className={"form-field"}>
                    <label>Contraseña</label>
                    <input type={"password"} placeholder={"Agregar Contraseña"} value={password}
                           onChange={event => setPassword(event.target.value)}/>
                </div>
                <div className={"forgotten-password"}>
                    <Link to={"/recuperar"}>
                        <label>¿Has olvidado tu contraseña?</label>
                    </Link>
                </div>
                <button className={"form-button"} type={"submit"}>Iniciar Sesión</button>
            </form>
            <div className="App">
                {showPopup && <Popup text={popupText} onClose={closePopup} />}
            </div>
        </div>
    )
}

export default Login