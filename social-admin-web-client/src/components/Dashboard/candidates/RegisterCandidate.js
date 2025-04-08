import React, {useEffect, useState, useRef} from "react";
import {useHistory} from "react-router-dom";
import {useDispatch, useSelector} from "react-redux";

// Actions
import {hideRegisterCandidate, registerCandidateFromClient, removeClientId, setCandidateId} from "../../../actions";

// Components
import Select from "../common/Select";

// Models
import Account from "../../../model/Account";
import Candidate from "../../../model/Candidate";

// Services
import AccountService from "../../../services/AccountService";
import CandidateService from "../../../services/CandidateService";
import LocationService from "../../../services/LocationService";

//css
import "./RegisterCandidate.css"; 

const RegisterCandidate = () => {
    const dispatch = useDispatch()
    const history = useHistory()

    const show = useSelector(state => state.registerCandidate)
    let invokedFromClient = useSelector(state => state.registerCandidateFromClient)
    const clientPreloadedId = useSelector(state => state.client)

    const [name, setName] = useState("")
    const [lastname, setLastname] = useState("")
    const [phone, setPhone] = useState("")
    const [email, setEmail] = useState("")
    const [curp, setCurp] = useState("")
    const [nss, setNss] = useState("")
    const [address, setAddress] = useState("")
    const [position, setPosition] = useState("")
    const [state, setState] = useState(0)
    const [city, setCity] = useState(0)
    const [client, setClient] = useState(null)
    const [clients, setClients] = useState([])
    const [page, setPage] = useState(0)
    const [states, setStates] = useState([])
    const [cities, setCities] = useState([])
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

    // Request clients
    const getClients = async () => {
        try {
            const response = await AccountService.getAccounts(Account.CLIENT, null, page)
            setClients([...clients, ...response.data.response])
        } catch (error) {
            console.log(error)
        }
    }

    useEffect(() => {
        if (show)
            getClients()
    }, [page, show])

    // Request states
    useEffect(() => {
        const getStates = async () => {
            try {
                const response = await LocationService.getStates()
                setStates(response.data.response)
            } catch (error) {
                console.log(error)
            }
        }
        if (show)
            getStates()

        return () => {
            setPage(0)
            setClients([])
        }
    }, [show])

    // Request cities
    useEffect(() => {
        const getCities = async () => {
            try {
                const response = await LocationService.getCities(state)
                setCities(response.data.response)
            } catch (error) {
                console.log(error)
            }
        }

        if (state) {
            getCities()
        }
    }, [state])

    // Request client
    useEffect(() => {
        const getClient = async () => {
            try {
                const response = await AccountService.getAccount(clientPreloadedId)
                setClient(response.data.response[0])
            } catch (error) {
                console.log(error)
            }
        }

        if (invokedFromClient) {
            getClient()
        }
    }, [])

    const onSubmit = async (event) => {
        event.preventDefault()
        if (isSubmitting) return;
    
        setIsSubmitting(true);

        try {
            const candidate = new Candidate(name, lastname, phone, email, curp, nss, address, position, {id: state}, {id: city}, client)

            const response = await CandidateService.create(candidate)
            dispatch(setCandidateId(response.data.response.id))
            setShowSuccessMessage(true)
            
            timeoutRef.current = setTimeout(() => {
                disableFlagsAndClose()
                history.push("/dashboard/candidatos/ver")
            }, 3000)

        } catch (error) {
            console.log(error)
            setIsSubmitting(false)
        }
    }

    const disableFlagsAndClose = () => {
        dispatch(removeClientId())
        if (invokedFromClient) {
            dispatch(registerCandidateFromClient())
            invokedFromClient = false
        }
        dispatch(hideRegisterCandidate())
    }

    const handleClient = (client) => {
        setClient(clients.find(x => x.id === client.id))
    }

    const clientOptions = clients.map(client => {
        return {
            id: client.id,
            value: client.companyInformation.companyName
        }
    })

    const stateOptions = states.map(state => {
        return (
            <option key={"state-" + state.id} value={state.id}>{state.name}</option>
        )
    })

    const cityOptions = cities.map(city => {
        return (
            <option key={"city-" + city.id} value={city.id}>{city.name}</option>
        )
    })

    const showHideModal = show ? "modal display-block" : "modal display-none"

    return (
        <div className={showHideModal}>
            <div className={"form-register-candidate"}>
                {showSuccessMessage && (
                    <div className="confirmation-message">
                        ¡Cargando Candidato!
                    </div>
                )}
                <div className={"close-modal"}>
                    <img src={"/images/icon-close.png"} alt={""} onClick={disableFlagsAndClose}/>
                </div>
                <h2>Registro de candidato</h2>
                <form className={"form-section no-scrollbar"} onSubmit={onSubmit}>
                <div className={"form-item"}>
                    <label htmlFor={"name"}>Nombre(s)</label>
                    <input 
                        type={"text"}
                        name={"name"}
                        placeholder={"Nombre(s) candidato"}
                        required
                        value={name}
                        maxLength={40}
                        onChange={event => {
                            const input = event.target.value;
                            const cleanedInput = input.replace(/[^a-zA-ZáéíóúÁÉÍÓÚñÑ\s]/g, '');
                            const validInput = cleanedInput.split(' ').filter(Boolean);
                            if (validInput.length <= 2) {
                                setName(cleanedInput);
                            }
                        }}
                        onBlur={event => {
                            const trimmedValue = name.trim();
                            setName(trimmedValue);
                        }}
                        onKeyDown={event => {
                            if (event.key === 'Enter') {
                                const trimmedValue = name.trim();
                                setName(trimmedValue);
                            }
                        }}
                    />
                </div>
                <div className={"form-item"}>
                    <label htmlFor={"lastname"}>Apellidos(s)</label>
                    <input 
                        type={"text"}
                        name={"lastname"}
                        placeholder={"Apellidos(s) candidato"}
                        required
                        value={lastname}
                        maxLength={40}
                        onChange={event => {
                            const input = event.target.value;
                            const cleanedInput = input.replace(/[^a-zA-ZáéíóúÁÉÍÓÚñÑ\s]/g, '');
                            const validInput = cleanedInput.split(' ').filter(Boolean);
                            if (validInput.length <= 2) {
                                setLastname(cleanedInput);
                            }
                        }}
                        onBlur={event => {
                            const trimmedValue = lastname.trim();
                            setLastname(trimmedValue);
                        }}
                        onKeyDown={event => {
                            if (event.key === 'Enter') {
                                const trimmedValue = lastname.trim();
                                setLastname(trimmedValue);
                            }
                        }}
                    />
                </div>
                    <div className={"form-item"}>
                        <label htmlFor={"client"}>Nombre del cliente</label>
                        {
                            invokedFromClient ?
                                <input value={client ? client.companyInformation.companyName : ""} disabled/> :
                                <Select options={clientOptions}
                                        selectedOption={client ? client.companyInformation.companyName : null}
                                        onChange={handleClient}
                                        page={page}
                                        setPage={setPage} />
                        }
                    </div>
                    <div className={"form-item"}>
                        <label htmlFor={"phone"}>Teléfono</label>
                        <input 
                            type={"tel"} 
                            name={"phone"} 
                            placeholder={"Agregar teléfono"} 
                            required 
                            value={phone}
                            onChange={event => {
                                const input = event.target.value;
                                if (/^\d{0,10}$/.test(input)) {
                                    setPhone(input);
                                }
                            }}
                            onBlur={event => {
                                if (phone.length !== 10) {
                                    alert('El teléfono debe tener 10 dígitos.');
                                }
                            }}
                        />
                    </div>
                    <div className={"form-item"}>
                        <label htmlFor={"email"}>Correo</label>
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
                    <div className={"form-item"}>
                        <label htmlFor={"curp"}>CURP</label>
                        <input type={"text"} name={"curp"} placeholder={"Agregar CURP"} required value={curp}
                               onChange={event => setCurp(event.target.value)}/>
                    </div>
                    <div className={"form-item"}>
                        <label htmlFor={"nss"}>NSS</label>
                        <input type={"text"} name={"nss"} placeholder={"Agregar NSS"} required value={nss}
                               onChange={event => setNss(event.target.value)}/>
                    </div>
                    <div className={"form-item"}>
                        <label htmlFor={"address"}>Domicilio</label>
                        <input type={"text"} name={"address"} placeholder={"Agregar domicilio"} required value={address}
                               onChange={event => setAddress(event.target.value)}/>
                    </div>
                    <div className={"form-item"}>
                        <label htmlFor={"position"}>Puesto</label>
                        <input type={"text"} name={"position"} placeholder={"Agregar puesto"} required value={position}
                               onChange={event => setPosition(event.target.value)}/>
                    </div>
                    <div className={"form-item"}>
                        <label htmlFor={"state"}>Estado</label>
                        <select name={"state"} value={state} onChange={event => setState(parseInt(event.target.value))}>
                            <option value={0}>Seleccionar</option>
                            {stateOptions}
                        </select>
                    </div>
                    <div className={"form-item"}>
                        <label htmlFor={"city"}>Ciudad</label>
                        <select name={"city"} value={city} onChange={event => setCity(parseInt(event.target.value))}>
                            <option value={0}>Seleccionar</option>
                            {cityOptions}
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

export default RegisterCandidate