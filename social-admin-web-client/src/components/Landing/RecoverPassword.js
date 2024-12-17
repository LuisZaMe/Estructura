import React, {useState} from "react";
import {useHistory} from "react-router-dom";
import AccountService from "../../services/AccountService";

const RecoverPassword = () => {
    const [email, setEmail] = useState("")

    const history = useHistory()
    const handleLogin = async () => {
        const response = await AccountService.sendRecoverPassword(email);
        if (response.status === 200) {
            history.push("/")
        }
    }

    return (
        <div className={"form"}>
            <div className={"form-header"}>
                <button className={"back-button"} onClick={history.goBack}>{"<"}</button>
                <label>Recuperar contraseña</label>
            </div>
            <form>
                <div className={"form-field"}>
                    <label>Correo</label>
                    <input type={"email"} placeholder={"Agregar correo"} value={email} onChange={event => setEmail(event.target.value)}/>
                </div>
                <button className={"form-button"} type={"button"} onClick={handleLogin}>Aceptar</button>
            </form>
        </div>
    )
}

export default RecoverPassword