import React, {useState} from "react";
import {useHistory} from "react-router-dom";
import {useDispatch, useSelector} from "react-redux";

// Actions
import {hideRegisterClient, setClientId} from "../../../actions";

// Models
import Account from "../../../model/Account";
import Company from "../../../model/Company";

// Services
import AccountService from "../../../services/AccountService";

const RegisterClient = () => {
    const dispatch = useDispatch()
    const history = useHistory()

    const show = useSelector(state => state.registerClient)

    const [companyName, setCompanyName] = useState("")
    const [companyPhone, setCompanyPhone] = useState("")
    const [contactName, setContactName] = useState("")
    const [contactPhone, setContactPhone] = useState("")
    const [email, setEmail] = useState("")
    const [corporateName, setCorporateName] = useState("")
    const [taxId, setTaxId] = useState("")
    const [legalAddress, setLegalAddress] = useState("")
    const [taxRegime, setTaxRegime] = useState("")
    const [paymentMethod, setPaymentMethod] = useState(0)

    const onSubmit = async (event) => {
        event.preventDefault()

        try {
            const company = new Company(companyName, companyPhone, legalAddress, paymentMethod, corporateName, taxId, taxRegime)
            const account = new Account(email, contactName, "", Account.CLIENT, contactPhone, company)

            const response = await AccountService.create(account)
            dispatch(setClientId(response.data.response.id))

            dispatch(hideRegisterClient())
            history.push("/dashboard/clientes/ver")
        } catch (error) {
            console.log(error)
        }
    }

    const showHideModal = show ? "modal display-block" : "modal display-none"

    return (
        <div className={showHideModal}>
            <div className={"form-register-client"}>
                <div className={"close-modal"}>
                    <img src={"/images/icon-close.png"} alt={""} onClick={() => dispatch(hideRegisterClient())}/>
                </div>
                <h2>Registro de cliente</h2>
                <form className={"form-section no-scrollbar"} onSubmit={onSubmit}>
                    <h3 className={"form-subsection-header"}>Datos principales</h3>
                    <div className={"form-item"}>
                        <label htmlFor={"company-name"}>Nombre empresa*</label>
                        <input type={"text"} name={"company-name"} placeholder={"Agregar empresa"} required={true}
                               value={companyName} onChange={event => setCompanyName(event.target.value)}/>
                    </div>
                    <div className={"form-item"}>
                        <label htmlFor={"company-phone"}>Telefono empresa*</label>
                        <input type={"tel"} name={"company-phone"} placeholder={"Agregar telefono"} required={true}
                               value={companyPhone} onChange={event => setCompanyPhone(event.target.value)}/>
                    </div>
                    <div className={"form-item"}>
                        <label htmlFor={"contact-name"}>Nombre responsable*</label>
                        <input type={"text"} name={"contact-name"} placeholder={"Agregar nombre"} required={true}
                               value={contactName} onChange={event => setContactName(event.target.value)}/>
                    </div>
                    <div className={"form-item"}>
                        <label htmlFor={"contact-phone"}>Telefono responsable*</label>
                        <input type={"tel"} name={"contact-phone"} placeholder={"Agregar telefono"} required={true}
                               value={contactPhone} onChange={event => setContactPhone(event.target.value)}/>
                    </div>
                    <div className={"form-item"}>
                        <label htmlFor={"email"}>Correo*</label>
                        <input type={"email"} name={"email"} placeholder={"Agregar correo"} required={true}
                               value={email} onChange={event => setEmail(event.target.value)}/>
                    </div>
                    <h3 className={"form-subsection-header"}>Datos fiscales</h3>
                    <div className={"form-item"}>
                        <label htmlFor={"legal-name"}>Razon social*</label>
                        <input type={"text"} name={"legal-name"} placeholder={"Agregar razon social"} required={true}
                               value={corporateName} onChange={event => setCorporateName(event.target.value)}/>
                    </div>
                    <div className={"form-item"}>
                        <label htmlFor={"taxId"}>RFC*</label>
                        <input type={"text"} name={"taxId"} placeholder={"Agregar RFC"} required={true} value={taxId}
                               onChange={event => setTaxId(event.target.value)}/>
                    </div>
                    <div className={"form-item"}>
                        <label htmlFor={"legal-address"}>Direccion fiscal</label>
                        <input type={"text"} name={"legal-address"} placeholder={"Agregar Direccion Fiscal"}
                               value={legalAddress} onChange={event => setLegalAddress(event.target.value)}/>
                    </div>
                    <div className={"form-item"}>
                        <label htmlFor={"regimen-fiscal"}>Regimen fiscal</label>
                        <input type={"text"} name={"regimen-fiscal"} placeholder={"Agregar Regimen Fiscal"}
                               value={taxRegime} onChange={event => setTaxRegime(event.target.value)}/>
                    </div>
                    <div className={"form-item"}>
                        <label htmlFor={"payment-method"}>Metodo de pago</label>
                        <select name={"Metodo de pago"} value={paymentMethod}
                                onChange={event => setPaymentMethod(parseInt(event.target.value))}>
                            <option value={0}>Seleccionar</option>
                            <option value={1}>Transferencia Bancaria</option>
                            <option value={2}>Cheque</option>
                            <option value={3}>Efectivo</option>
                            <option value={4}>Por definir</option>
                        </select>
                    </div>
                    <div className={"form-action"}>
                        <button className={"form-button-primary"}>Registrar</button>
                    </div>
                </form>
            </div>
        </div>
    )
}

export default RegisterClient