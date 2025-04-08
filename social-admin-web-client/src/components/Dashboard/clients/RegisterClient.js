import React, {useEffect, useState, useRef} from "react";
import {useHistory} from "react-router-dom";
import {useDispatch, useSelector} from "react-redux";

// Actions
import {hideRegisterClient, setClientId} from "../../../actions";

// Models
import Account from "../../../model/Account";
import Company from "../../../model/Company";

// Services
import AccountService from "../../../services/AccountService";

//css
import "./RegisterClient.css"; 

const RegisterClient = () => {
    const dispatch = useDispatch()
    const history = useHistory()

    const show = useSelector(state => state.registerClient)

    const [companyName, setCompanyName] = useState("")
    const [companyPhone, setCompanyPhone] = useState("")
    const [contactName, setContactName] = useState("")
    const [contactLastname, setContactLastname] = useState("")
    const [contactPhone, setContactPhone] = useState("")
    const [email, setEmail] = useState("")
    const [corporateName, setCorporateName] = useState("")
    const [taxId, setTaxId] = useState("")
    const [legalAddress, setLegalAddress] = useState("")
    const [taxRegime, setTaxRegime] = useState("")
    const [paymentMethod, setPaymentMethod] = useState(0)
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
            const company = new Company(companyName, companyPhone, legalAddress, paymentMethod, corporateName, taxId, taxRegime)
            const account = new Account(email, contactName, contactLastname, Account.CLIENT, contactPhone, company)

            const response = await AccountService.create(account)
            dispatch(setClientId(response.data.response.id))
            
            setShowSuccessMessage(true)
            timeoutRef.current = setTimeout(() => {
                dispatch(hideRegisterClient())
                history.push("/dashboard/clientes/ver")
            }, 3000)

        } catch (error) {
            console.log(error)
            setIsSubmitting(false)
        }
    }

    const showHideModal = show ? "modal display-block" : "modal display-none"

    return (
        <div className={showHideModal}>
            <div className={"form-register-client"}>
                {showSuccessMessage && (
                        <div className="confirmation-message">
                            ¡Cargando Cliente!
                        </div>
                    )}
                <div className={"close-modal"}>
                    <img src={"/images/icon-close.png"} alt={""} onClick={() => dispatch(hideRegisterClient())}/>
                </div>
                <h2>Registro de cliente</h2>
                <form className={"form-section no-scrollbar"} onSubmit={onSubmit}>
                    <h3 className={"form-subsection-header"}>Datos principales</h3>
                    <div className={"form-item"}>
                        <label htmlFor={"company-name"}>Nombre empresa*</label>
                        <input 
                            type={"text"} 
                            name={"company-name"} 
                            placeholder={"Agregar empresa"} 
                            required={true} 
                            value={companyName} 
                            onChange={event => setCompanyName(event.target.value)} 
                            maxLength={30}
                        />
                    </div>
                    <div className={"form-item"}>
                        <label htmlFor={"company-phone"}>Telefono empresa*</label>
                        <input
                            type={"tel"}
                            name={"company-phone"}
                            placeholder={"Agregar telefono"}
                            required={true}
                            value={companyPhone}
                            onChange={event => {
                                const newValue = event.target.value.replace(/\D/g, '');
                                if (newValue.length <= 10) {
                                    setCompanyPhone(newValue);
                                }
                            }}
                        />
                    </div>
                    <div className={"form-item"}>
                        <label htmlFor={"contact-name"}>Nombre(s) responsable*</label>
                        <input
                            type={"text"}
                            name={"contact-name"}
                            placeholder={"Agregar nombre"}
                            required={true}
                            value={contactName}
                            maxLength={40}
                            onChange={event => {
                                const input = event.target.value;
                                const cleanedInput = input.replace(/[^a-zA-ZáéíóúÁÉÍÓÚñÑ\s]/g, '');
                                const validInput = cleanedInput.split(' ').filter(Boolean);
                                if (validInput.length <= 2) {
                                    setContactName(cleanedInput);
                                }
                            }}
                            onBlur={event => {
                                const trimmedValue = contactName.trim();
                                setContactName(trimmedValue);
                            }}
                            onKeyDown={event => {
                                if (event.key === 'Enter') {
                                    const trimmedValue = contactName.trim();
                                    setContactName(trimmedValue);
                                }
                            }}
                        />
                    </div>
                    <div className={"form-item"}>
                        <label htmlFor={"contact-lastname"}>Apellido(s) responsable*</label>
                        <input
                            type={"text"}
                            name={"contact-lastname"}
                            placeholder={"Agregar apellido"}
                            required={true}
                            value={contactLastname}
                            onChange={event => {
                                const input = event.target.value;
                                const cleanedInput = input.replace(/[^a-zA-ZáéíóúÁÉÍÓÚñÑ\s]/g, '');
                                const validInput = cleanedInput.split(' ').filter(Boolean);
                                if (validInput.length <= 2) {
                                    setContactLastname(cleanedInput);
                                }
                            }}
                            onBlur={event => {  // Cuando se pierde el foco
                                const trimmedValue = contactLastname.trim();  // Aplica trim
                                setContactLastname(trimmedValue);
                            }}
                            onKeyDown={event => {  // Cuando se presiona Enter
                                if (event.key === 'Enter') {
                                    const trimmedValue = contactLastname.trim();  // Aplica trim
                                    setContactLastname(trimmedValue);
                                }
                            }}
                        />
                    </div>
                    <div className={"form-item"}>
                        <label htmlFor={"contact-phone"}>Telefono responsable*</label>
                        <input
                            type={"tel"}
                            name={"contact-phone"}
                            placeholder={"Agregar telefono"}
                            required={true}
                            value={contactPhone}
                            onChange={event => {
                                const input = event.target.value;
                                const cleanedInput = input.replace(/\D/g, ''); 
                                if (cleanedInput.length <= 10) {
                                    setContactPhone(cleanedInput);
                                }
                            }}
                        />
                    </div>
                    <div className={"form-item"}>
                        <label htmlFor={"email"}>Correo*</label>
                        <input
                            type={"text"} 
                            name={"email"}
                            placeholder={"Agregar correo"}
                            required={true}
                            value={email}
                            onChange={event => {
                                const input = event.target.value;
                                setEmail(input);
                            }}
                            onBlur={event => {
                                // Expresión regular mejorada para evitar ".." en el dominio
                                const emailPattern = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9-]+(\.[a-zA-Z]{2,})+$/;
                                const doubleDotPattern = /\.{2,}/; // Detecta ".."

                                const input = event.target.value;

                                if ((!emailPattern.test(input) || doubleDotPattern.test(input)) && input !== '') {
                                    alert('Por favor, ingresa un correo válido.');
                                }
                            }}
                        />
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
                        <label htmlFor={"regimen-fiscal"}>Régimen fiscal*</label>
                        <select
                            name={"regimen-fiscal"}
                            value={taxRegime}
                            onChange={event => setTaxRegime(event.target.value)}>
                            <option value={0}>Seleccionar</option> 
                            <option value={1}>Régimen General de Ley</option>
                            <option value={2}>Régimen de Personas Morales con Fines no Lucrativos</option>
                            <option value={3}>Régimen de Incorporación Fiscal (RIF)</option>
                            <option value={4}>Régimen de Actividades Agrícolas, Ganaderas, Silvícolas y Pesqueras</option>
                            <option value={5}>Régimen de Coordinados</option>
                            <option value={6}>Régimen de Empresas en Liquidación</option>
                            <option value={7}>Régimen de Pequeños Contribuyentes (REPECO)</option>
                            <option value={8}>Régimen de Arrendamiento</option>
                            <option value={9}>Régimen de Distribuidores de Energía</option>
                            <option value={10}>Régimen de Servicios Profesionales</option>
                            <option value={11}>Régimen de Cooperativas</option>
                            <option value={12}>Régimen de Actividades Empresariales con Ingresos Menores a 2 millones de pesos</option>
                            <option value={13}>Régimen de Honorarios Profesionales</option>
                            <option value={14}>Régimen de Empresas Familiares</option>
                            <option value={15}>Régimen de Sociedad de Inversión de Capitales</option>
                        </select>
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
                        {!isSubmitting ? (
                            <button className={"form-button-primary"}>Registrar</button>
                        ) : (
                            <div className="processing-message">Procesando solicitud...</div>
                        )}
                    </div>
                </form>
            </div>
        </div>
    )
}

export default RegisterClient