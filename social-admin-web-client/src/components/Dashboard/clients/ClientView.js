import React, {useEffect, useState} from "react";
import {useHistory} from "react-router-dom";
import {useDispatch, useSelector} from "react-redux";

// Actions
import {removeClientId} from "../../../actions";

// Components
import Candidates from "./Candidates";

// Services
import AccountService from "../../../services/AccountService";

const ClientView = () => {
    const dispatch = useDispatch()
    const history = useHistory()

    const id = useSelector(state => state.client)

    const [client, setClient] = useState(null)
    const [showListOfCandidates, setShowListOfCandidates] = useState(false)


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

    const onClickAccept = () => {
        //dispatch(removeClientId())
        history.push("/dashboard/clientes")
    }

    const onClickEdit = () => {
        history.push("/dashboard/clientes/editar")
    }

    return (
        <div className={"container"}>
            <div className={"content client"}>
                <div className={"top-section"}>
                    <div className={"client-header"}>
                        <div className={"company-name"}>
                            <label className={"company-name-title"}>Nombre de empresa</label>
                            <label
                                className={"company-name-value"}>{client ? client.companyInformation.companyName : ""}
                            </label>
                        </div>
                        <button className={"edit-client"} onClick={onClickEdit}>
                            <img src={"/images/actions-dropdown/edit.svg"} alt={""}/>
                            Editar
                        </button>
                    </div>
                </div>
                <div className={"main-section client shadow"}>
                    <div className={"client-view no-scrollbar"}>
                        <div className={"client-main-info"}>
                            <label className={"client-section-title"}>Datos principales</label>
                            <div className={"client-section-item"}>
                                <label className={"property"}>Telefono empresa</label>
                                <label
                                    className={"value"}>{client ? client.companyInformation.companyPhone : ""}</label>
                            </div>
                            <div className={"client-section-item"}>
                                <label className={"property"}>Nombre(s) responsable</label>
                                <label className={"value"}>{client ? client.name : null}</label>
                            </div>
                            <div className={"client-section-item"}>
                                <label className={"property"}>Apellidos(s) responsable</label>
                                <label className={"value"}>{client ? client.lastname : null}</label>
                            </div>
                            <div className={"client-section-item"}>
                                <label className={"property"}>Telefono responsable</label>
                                <label className={"value"}>{client ? client.phone : null}</label>
                            </div>
                            <div className={"client-section-item"}>
                                <label className={"property"}>Correo</label>
                                <label className={"value"}>{client ? client.email : null}</label>
                            </div>
                        </div>
                        <div className={"client-tax-info"}>
                            <label className={"client-section-title"}>Datos fiscales</label>
                            <div className={"client-section-item"}>
                                <label className={"property"}>Razon social</label>
                                <label
                                    className={"value"}>{client ? client.companyInformation.razonSocial : null}</label>
                            </div>
                            <div className={"client-section-item"}>
                                <label className={"property"}>RFC</label>
                                <label className={"value"}>{client ? client.companyInformation.rfc : null}</label>
                            </div>
                            <div className={"client-section-item"}>
                                <label className={"property"}>Direccion fiscal</label>
                                <label
                                    className={"value"}>{client ? client.companyInformation.direccionFiscal : null}</label>
                            </div>
                            <div className={"client-section-item"}>
                                <label className={"property"}>Regimen fiscal</label>
                                <label
                                    className={"value"}>{client ? client.companyInformation.regimenFiscal : null}</label>
                            </div>
                            <div className={"client-section-item"}>
                                <label className={"property"}>Metodo de pago</label>
                                <label
                                    className={"value"}>{client ? client.companyInformation.payment.description : null}</label>
                            </div>
                        </div>
                    </div>
                    <button className={"client-view-accept"} onClick={onClickAccept}>Aceptar</button>
                </div>
            </div>
        </div>
    )
}

export default ClientView