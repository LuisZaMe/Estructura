import React, {useEffect, useState, useRef } from "react";
import {useHistory} from "react-router-dom";
import {useDispatch, useSelector} from "react-redux";

// Actions
import {hideRegisterAnalyst, setAnalystId} from "../../../actions";

// Models
import Account from "../../../model/Account";

// Services
import AccountService from "../../../services/AccountService";

//css
import "./RegisterAnalyst.css";

const RegisterAnalyst = () => {
    const dispatch = useDispatch()
    const history = useHistory()

    const show = useSelector(state => state.registerAnalyst)

    const [name, setName] = useState("")
    const [lastname, setLastname] = useState("")
    const [phone, setPhone] = useState("")
    const [email, setEmail] = useState("")
    const [showSuccessMessage, setShowSuccessMessage] = useState(false)
    const [isSubmitting, setIsSubmitting] = useState(false);
    const timeoutRef = useRef()

    useEffect(() => {
        return () => {
            if (timeoutRef.current) {
                clearTimeout(timeoutRef.current)
            }
        }
    }, [])

    const onSubmit = async (event) => {
        event.preventDefault()
        if (isSubmitting) return;
    
        setIsSubmitting(true);

        try {
            const analyst = new Account(email, name, lastname, Account.ANALYST, phone, null)

            const response = await AccountService.create(analyst)
            dispatch(setAnalystId(response.data.response.id))
            setShowSuccessMessage(true)
            timeoutRef.current = setTimeout(() => {
                dispatch(hideRegisterAnalyst())
                history.push("/dashboard/analistas/ver")
            }, 3000)
        } catch (error) {
            console.log(error)
            setIsSubmitting(false)
        }
    }

    const showHideModal = show ? "modal display-block" : "modal display-none"

    return (
        <div className={showHideModal}>
            <div className={"form-register-analyst"}>
                {showSuccessMessage && (
                            <div className="confirmation-message">
                                ¡Cargando Analista!
                            </div>
                        )}
                <div className={"close-modal"}>
                    <img src={"/images/icon-close.png"} alt={""} onClick={() => dispatch(hideRegisterAnalyst())}/>
                </div>
                <h2>Registro de analista</h2>
                <form className={"form-section"} onSubmit={onSubmit}>
                    <h3 className={"form-subsection-header"}>Datos principales</h3>
                    <div className={"form-item"}>
                    <label htmlFor={"name"}>Nombre(s)*</label>
                    <input
                        type={"text"}
                        name={"name"}
                        placeholder={"Agregar nombre(s)"}
                        required
                        value={name}
                        maxLength={40}
                        onChange={(event) => {
                        const input = event.target.value;

                        const cleanedInput = input.replace(/[^a-zA-ZáéíóúÁÉÍÓÚñÑ\s]/g, '');

                        const validInput = cleanedInput.split(' ').filter(Boolean);

                        if (validInput.length <= 2) {
                            setName(cleanedInput);
                        }
                        }}
                        onBlur={() => setName(name.trim())}
                        onKeyDown={(event) => {
                        if (event.key === 'Enter') {
                            setName(name.trim());
                        }
                        }}
                    />
                    </div>
                    <div className={"form-item"}>
                    <label htmlFor={"lastname"}>Apellido(s)*</label>
                    <input
                        type={"text"}
                        name={"lastname"}
                        placeholder={"Agregar apellido(s)"}
                        required
                        value={lastname}
                        maxLength={40}
                        onChange={(event) => {
                        const input = event.target.value;
                        const cleanedInput = input.replace(/[^a-zA-ZáéíóúÁÉÍÓÚñÑ\s]/g, '');
                        const validInput = cleanedInput.split(' ').filter(Boolean);
                        if (validInput.length <= 2) {
                            setLastname(cleanedInput);
                        }
                        }}
                        onBlur={() => setLastname(lastname.trim())}
                        onKeyDown={(event) => {
                        if (event.key === 'Enter') {
                            setLastname(lastname.trim());
                        }
                        }}
                    />
                    </div>
                    <div className={"form-item"}>
                    <label htmlFor={"phone"}>Telefono*</label>
                    <input
                        type={"tel"}
                        name={"phone"}
                        placeholder={"Agregar telefono"}
                        required
                        value={phone}
                        maxLength={10}
                        onChange={event => {
                        const newPhone = event.target.value.replace(/[^0-9]/g, '');
                        if (newPhone.length <= 10) {
                            setPhone(newPhone);
                        }
                        }}
                    />
                    </div>
                    <div className={"form-item"}>
                    <label htmlFor={"email"}>Correo*</label>
                    <input
                        type={"email"}
                        name={"email"}
                        placeholder={"Agregar correo"}
                        required
                        value={email}
                        onChange={event => setEmail(event.target.value)}
                        onBlur={event => {
                        const emailPattern = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9-]+(\.[a-zA-Z]{2,})+$/;
                        const doubleDotPattern = /\.{2,}/;
                        const input = event.target.value;
                        if ((!emailPattern.test(input) || doubleDotPattern.test(input)) && input !== '') {
                            alert('Por favor, ingresa un correo válido.');
                        }
                        }}
                    />
                    </div>
                    <div className={"form-action"}>
                        {!isSubmitting ? (
                            <button  className={"form-button-primary"}>Registrar</button>
                            ) : (
                                <div className="processing-message">Procesando solicitud...</div>
                            )}
                    </div>
                </form>
            </div>
        </div>
    )
}

export default RegisterAnalyst