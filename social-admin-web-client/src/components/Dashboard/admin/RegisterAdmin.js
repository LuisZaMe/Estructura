import React, {useState} from "react";
import {useHistory} from "react-router-dom";
import {useDispatch, useSelector} from "react-redux";

// Actions
import {hideRegisterAdmin, setAdminId} from "../../../actions";

// Models
import Account from "../../../model/Account";

// Services
import AccountService from "../../../services/AccountService";
import AuthService from "../../../services/AuthService";

const RegisterAdmin = () => {
    const dispatch = useDispatch()
    const history = useHistory()

    const show = useSelector(state => state.registerAdmin)

    const [name, setName] = useState("")
    const [lastname, setLastname] = useState("")
    const [phone, setPhone] = useState("")
    const [email, setEmail] = useState("")
    const [isSuperAdmin, setIsSuperAdmin] = useState(false)

    const userIdentity = AuthService.getIdentity();

    const onSubmit = async (event) => {
        event.preventDefault()

        try {

            const role = isSuperAdmin ? Account.SUPER_ADMINISTRADOR : Account.ADMIN;

            const admin = new Account(email, name, lastname, role, phone, null)

            const response = await AccountService.create(admin)
            dispatch(setAdminId(response.data.response.id))

            dispatch(hideRegisterAdmin())
            history.push("/dashboard/administrador/ver")
        } catch (error) {
            console.log(error)
        }
    }

    const showHideModal = show ? "modal display-block" : "modal display-none";

    const RenderSuperAdminCheck = () => {
        if (userIdentity.role !== Account.SUPER_ADMINISTRADOR) {
            return null;
        }
        return (
            <div className={"form-item full"} >
                <label>Crear usuario super admin</label>
                <input
                    type={"checkbox"}
                    name={"isSuperAdmin"}
                    value={isSuperAdmin}
                    onChange={event => setIsSuperAdmin(event.target.checked)}
                    //checked={isSuperAdmin}
                    disabled={false}
                    // style={"height: 100%"}
                />
            </div>
        );

    }

    return (
        <div className={showHideModal}>
            <div className={"form-register-admin"}>
                <div className={"close-modal"}>
                    <img src={"/images/icon-close.png"} alt={""} onClick={() => dispatch(hideRegisterAdmin())}/>
                </div>
                <h2>Registro de administrador</h2>
                <form className={"form-section"} onSubmit={onSubmit}>
                    <h3 className={"form-subsection-header"}>Datos Principales</h3>
                    <div className={"form-item"}>
                        <label htmlFor={"name"}>Nombre(s)*</label>
                        <input type={"text"} name={"name"} placeholder={"Agregar nombre(s)"} required={true} value={name}
                               onChange={event => setName(event.target.value)}/>
                    </div>
                    <div className={"form-item"}>
                        <label htmlFor={"lastname"}>Apellido(s)*</label>
                        <input type={"text"} name={"lastname"} placeholder={"Agregar apellido(s)"} required={true} value={lastname}
                               onChange={event => setLastname(event.target.value)}/>
                    </div>
                    <div className={"form-item"}>
                        <label htmlFor={"phone"}>Telefono*</label>
                        <input type={"tel"} name={"phone"} placeholder={"Agregar telefono"} required={true}
                               value={phone} onChange={event => setPhone(event.target.value)}/>
                    </div>
                    <div className={"form-item"}>
                        <label htmlFor={"email"}>Correo*</label>
                        <input type={"email"} name={"email"} placeholder={"Agregar correo"} required={true}
                               value={email} onChange={event => setEmail(event.target.value)}/>
                    </div>
                    {RenderSuperAdminCheck()}
                    <div className={"form-action"}>
                        <button className={"form-button-primary"}>Registrar</button>
                    </div>
                </form>
            </div>
        </div>
    )
}

export default RegisterAdmin