import React, {useEffect, useState} from "react"
import {useHistory} from "react-router-dom";
import {useDispatch, useSelector} from "react-redux";

// Services
import AccountService from "../../../services/AccountService";
// Actions
import {removeClientId} from "../../../actions";

const ClientEdit = () => {
    const dispatch = useDispatch()
    const history = useHistory()

    const id = useSelector(state => state.client)

    const [client, setClient] = useState(null)

    useEffect(() => {
        const getClient = async () => {
            try {
                const response = await AccountService.getAccount(id)
                setClient(response.data.response[0])
            } catch (error) {
                console.log(error)
            }
        }
        getClient()
    }, [id])

    const saveChanges = async () => {
        try {
            await AccountService.update(client)
            history.push("/dashboard/clientes/ver")
        } catch (error) {
            console.log(error)
        }
    }

    const onClickView = () => {
        dispatch(removeClientId())
        history.push("/dashboard/clientes/ver")
    }

    
    return (
        <div className={"container"}>
            <div className={"content client"}>
                <div className={"top-section"}>
                    <div className={"client-header"}>
                        <div className={"company-name"}>
                            <label className={"company-name-title"}>Nombre de empresa</label>
                            <input className={"client-section-input"} type={"text"}
                                   value={client ? client.companyInformation.companyName : ""} disabled/>
                        </div>
                        <button className={"edit-client"} onClick={onClickView}>
                            <img src={"/images/actions-dropdown/edit.svg"} alt={""}/>
                            Editar
                        </button>
                    </div>
                </div>
                <div className={"main-section client shadow"}>
                    <div className={"client-edit no-scrollbar"}>
                        <div className={"client-main-info"}>
                            <label className={"client-section-title"}>Datos principales</label>
                            <div className={"client-section-item"}>
                                <label className={"property"}>Telefono empresa</label>
                                <input className={"client-section-input"} type={"tel"}
                                        value={client ? client.companyInformation.companyPhone : ""}
                                        onChange={event => setClient({
                                        ...client,
                                        companyInformation: {
                                            ...client.companyInformation,
                                            companyPhone: event.target.value
                                        } 
                                    })}/>
                            </div>
                            <div className={"client-section-item"}>
                                <label className={"property"}>Nombre responsable</label>
                                <input className={"client-section-input"} type={"text"}
                                        value={client ? client.name : ""}
                                        onChange={event => setClient({
                                            ...client,
                                            name: event.target.value
                                        })}/>
                            </div>
                            <div className={"client-section-item"}>
                                <label className={"property"}>Telefono responsable</label>
                                <input className={"client-section-input"} type={"text"}
                                    value={client ? client.phone : ""}
                                    onChange={event => setClient({
                                        ...client,
                                        phone: event.target.value
                                    })}/>
                            </div>
                            <div className={"client-section-item"}>
                                <label className={"property"}>Correo</label>
                                <input className={"client-section-input"} type={"email"}
                                        value={client ? client.email : ""}
                                        onChange={event => setClient({
                                        ...client,
                                        email: event.target.value
                                    })}/>
                            </div>
                        </div>
                        <div className={"client-tax-info"}>
                            <label className={"client-section-title"}>Datos fiscales</label>
                            <div className={"client-section-item"}>
                                <label className={"property"}>Razon social</label>
                                <input className={"client-section-input"} type={"text"}
                                        value={client ? client.companyInformation.razonSocial : ""}
                                        onChange={event => setClient({
                                        ...client,
                                        companyInformation: {
                                            ...client.companyInformation,
                                            razonSocial: event.target.value
                                        } 
                                    })}
                                    />
                            </div>
                            <div className={"client-section-item"}>
                                <label className={"property"}>RFC</label>
                                <input className={"client-section-input"} type={"text"}
                                        value={client ? client.companyInformation.rfc : ""}
                                        onChange={event => setClient({
                                        ...client,
                                        companyInformation: {
                                            ...client.companyInformation,
                                            rfc: event.target.value
                                        } 
                                    })}/>
                            </div>
                            <div className={"client-section-item"}>
                                <label className={"property"}>Direccion fiscal</label>
                                <input className={"client-section-input"} type={"text"}
                                        value={client ? client.companyInformation.direccionFiscal : ""}
                                        onChange={event => setClient({
                                        ...client,
                                        companyInformation: {
                                            ...client.companyInformation,
                                            direccionFiscal: event.target.value
                                        } 
                                    })}/>
                            </div>
                            <div className={"client-section-item"}>
                                <label className={"property"}>Regimen fiscal</label>
                                <input className={"client-section-input"} type={"text"}
                                        value={client ? client.companyInformation.regimenFiscal : ""} 
                                        onChange={event => setClient({
                                        ...client,
                                        companyInformation: {
                                            ...client.companyInformation,
                                            regimenFiscal: event.target.value
                                            } 
                                        })}
                                    />
                            </div>
                            <div className={"client-section-item"}>
                                <label className={"property"}>Metodo de pago</label>
                                <select className={"client-section-input"}
                                        value={client ? client.companyInformation.payment.id : 0} 
                                        // onChange={event => setClient({
                                        //     ...client,
                                        //     companyInformation: {
                                        //         ...client.companyInformation,
                                        //         payment: {
                                        //             ...client.companyInformation.payment,
                                        //             id: event.target.value
                                        //         }
                                        //         } 
                                        //     })}
                                        >
                                    <option value={0}>Seleccionar</option>
                                    <option value={1}>Transferencia Bancaria</option>
                                    <option value={2}>Cheque</option>
                                    <option value={3}>Efectivo</option>
                                    <option value={4}>Por definir</option>
                                </select>
                            </div>
                        </div>
                    </div>
                    <button className={"client-view-accept"} onClick={saveChanges}>Guardar</button>
                </div>
            </div>
        </div>
    )
}

export default ClientEdit