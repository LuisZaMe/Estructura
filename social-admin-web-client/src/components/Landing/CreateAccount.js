import React, {useState} from "react"
import {useHistory} from "react-router-dom"

const CreateAccount = () => {
    const [firstName, setFirstName] = useState("")
    const [lastName, setLastName] = useState("")
    const [phone, setPhone] = useState("")
    const [email, setEmail] = useState("")
    const [password, setPassword] = useState("")
    const [repeatedPassword, setRepeatedPassword] = useState("")

    const history = useHistory()

    const onSubmit = (event) => {
        event.preventDefault()
    }

    return (
        <div className={"form"}>
            <div className={"form-header"}>
                <button className={"back-button"} onClick={history.goBack}>{"<"}</button>
                <label>Crear cuenta</label>
            </div>
            <form onSubmit={onSubmit}>
                <div className={"form-field"}>
                    <label>Nombre(s)</label>
                    <input type={"text"} placeholder={"Agregar nombre"} value={firstName}
                           onChange={event => setFirstName(event.target.value)} required/>
                </div>
                <div className={"form-field"}>
                    <label>Apellido(s)</label>
                    <input type={"text"} placeholder={"Agregar apellido"} value={lastName}
                           onChange={event => setLastName(event.target.value)} required/>
                </div>
                <div className={"form-field"}>
                    <label>Telefono</label>
                    <input type={"tel"} placeholder={"Agregar telefono"} value={phone}
                           onChange={event => setPhone(event.target.value)} required/>
                </div>
                <div className={"form-field"}>
                    <label>Correo</label>
                    <input type={"email"} placeholder={"Agregar correo"} value={email}
                           onChange={event => setEmail(event.target.value)} required/>
                </div>
                <div className={"form-field"}>
                    <label>Contraseña</label>
                    <input type={"password"} placeholder={"Agregar contraseña"} value={password}
                           onChange={event => setPassword(event.target.value)} required/>
                </div>
                <div className={"form-field"}>
                    <label>Repetir contraseña</label>
                    <input type={"password"} placeholder={"Agregar contraseña"} value={repeatedPassword}
                           onChange={event => setRepeatedPassword(event.target.value)} required/>
                </div>
                <button className={"form-button"} type={"submit"}>Registrar</button>
            </form>
        </div>
    )
}

export default CreateAccount