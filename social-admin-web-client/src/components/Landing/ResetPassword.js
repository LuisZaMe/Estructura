import React, {useState} from "react";
import AccountService from "../../services/AccountService";
import {useHistory} from "react-router-dom";

function ResetPassword() {
  const [password, setPassword] = useState("")
  const [repeatedPassword, setRepeatedPassword] = useState("")

  const history = useHistory()

  const onSubmit = (event) => {
    event.preventDefault()

    const queryParams = new URLSearchParams(window.location.search)
    const token = queryParams.get("token")

    if (password !== repeatedPassword) {
      return
    }

    AccountService.resetPassword(btoa(password), token)
      .then(() => history.push("/"))
      .catch(error => console.log(error))
  }
  return (
    <div className={"form"}>
      <div className={"form-header"}>
        <label>Cambiar Contraseña</label>
      </div>
      <form onSubmit={onSubmit}>
        <div className={"form-field"}>
          <label>Contraseña</label>
          <input type={"password"} placeholder={"Agregar contraseña"} value={password}
            onChange={event => setPassword(event.target.value)} required />
        </div>
        <div className={"form-field"}>
          <label>Repetir Contraseña</label>
          <input type={"password"} placeholder={"Agregar contraseña"} value={repeatedPassword}
            onChange={event => setRepeatedPassword(event.target.value)} required />
        </div>
        <button className={"form-button"} type={"submit"}>Enviar</button>
      </form>
    </div>
  )
}

export default ResetPassword