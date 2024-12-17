import React, {useState} from "react";
import {useHistory} from "react-router-dom";
import {useDispatch, useSelector} from "react-redux";

// Actions
import {hideRegisterAnalyst, setAnalystId} from "../../../actions";

// Models
import Account from "../../../model/Account";

// Services
import AccountService from "../../../services/AccountService";

const RegisterAnalyst = () => {
    const dispatch = useDispatch()
    const history = useHistory()

    const show = useSelector(state => state.registerAnalyst)

    const [name, setName] = useState("")
    const [phone, setPhone] = useState("")
    const [email, setEmail] = useState("")

    const onSubmit = async (event) => {
        event.preventDefault()

        try {
            const analyst = new Account(email, name, "", Account.ANALYST, phone, null)

            const response = await AccountService.create(analyst)
            dispatch(setAnalystId(response.data.response.id))

            dispatch(hideRegisterAnalyst())
            history.push("/dashboard/analistas/ver")
        } catch (error) {
            console.log(error)
        }
    }

    const showHideModal = show ? "modal display-block" : "modal display-none"

    return (
        <div className={showHideModal}>
            <div className={"form-register-analyst"}>
                <div className={"close-modal"}>
                    <img src={"/images/icon-close.png"} alt={""} onClick={() => dispatch(hideRegisterAnalyst())}/>
                </div>
                <h2>Registro de analista</h2>
                <form className={"form-section"} onSubmit={onSubmit}>
                    <h3 className={"form-subsection-header"}>Datos principales</h3>
                    <div className={"form-item"}>
                        <label htmlFor={"name"}>Nombre*</label>
                        <input type={"text"} name={"name"} placeholder={"Agregar nombre"} required value={name}
                               onChange={event => setName(event.target.value)}/>
                    </div>
                    <div className={"form-item"}>
                        <label htmlFor={"phone"}>Telefono*</label>
                        <input type={"tel"} name={"phone"} placeholder={"Agregar telefono"} required value={phone}
                               onChange={event => setPhone(event.target.value)}/>
                    </div>
                    <div className={"form-item"}>
                        <label htmlFor={"email"}>Correo*</label>
                        <input type={"email"} name={"email"} placeholder={"Agregar correo"} required value={email}
                               onChange={event => setEmail(event.target.value)}/>
                    </div>
                    <div className={"form-action"}>
                        <button className={"form-button-primary"}>Registrar</button>
                    </div>
                </form>
            </div>
        </div>
    )
}

export default RegisterAnalyst