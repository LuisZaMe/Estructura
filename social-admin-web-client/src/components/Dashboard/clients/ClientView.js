import React, {useEffect, useState} from "react";
import {useHistory} from "react-router-dom";
import {useDispatch, useSelector} from "react-redux";

// Actions
import {removeClientId} from "../../../actions";

// Components
import Candidates from "./Candidates";

// Services
import AccountService from "../../../services/AccountService";

const taxRegimes = {
    0: "Seleccionar",
    1: "Régimen General de Ley",
    2: "Régimen de Personas Morales con Fines no Lucrativos",
    3: "Régimen de Incorporación Fiscal (RIF)",
    4: "Régimen de Actividades Agrícolas, Ganaderas, Silvícolas y Pesqueras",
    5: "Régimen de Coordinados",
    6: "Régimen de Empresas en Liquidación",
    7: "Régimen de Pequeños Contribuyentes (REPECO)",
    8: "Régimen de Arrendamiento",
    9: "Régimen de Distribuidores de Energía",
    10: "Régimen de Servicios Profesionales",
    11: "Régimen de Cooperativas",
    12: "Régimen de Actividades Empresariales con Ingresos Menores a 2 millones de pesos",
    13: "Régimen de Honorarios Profesionales",
    14: "Régimen de Empresas Familiares",
    15: "Régimen de Sociedad de Inversión de Capitales"
};

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
                            <label className={"client-section-title"}>DATOS PRINCIPALES</label>
                            <div className={"client-section-item"}>
                                <label className={"property"}>TELÉFONO EMPRESA</label>
                                <label
                                    className={"value"}>{client ? client.companyInformation.companyPhone : ""}</label>
                            </div>
                            <div className={"client-section-item"}>
                                <label className={"property"}>NOMBRE(S) RESPONSABLE</label>
                                <label className={"value"}>{client ? client.name : null}</label>
                            </div>
                            <div className={"client-section-item"}>
                                <label className={"property"}>APELLIDO(S) RESPONSABLE</label>
                                <label className={"value"}>{client ? client.lastname : null}</label>
                            </div>
                            <div className={"client-section-item"}>
                                <label className={"property"}>TELÉFONO RESPONSABLE</label>
                                <label className={"value"}>{client ? client.phone : null}</label>
                            </div>
                            <div className={"client-section-item"}>
                                <label className={"property"}>CORREO</label>
                                <label className={"value"}>{client ? client.email : null}</label>
                            </div>
                        </div>
                        <div className={"client-tax-info"}>
                            <label className={"client-section-title"}>DATOS FISCALES</label>
                            <div className={"client-section-item"}>
                                <label className={"property"}>RAZÓN SOCIAL</label>
                                <label
                                    className={"value"}>{client ? client.companyInformation.razonSocial : null}</label>
                            </div>
                            <div className={"client-section-item"}>
                                <label className={"property"}>RFC</label>
                                <label className={"value"}>{client ? client.companyInformation.rfc : null}</label>
                            </div>
                            <div className={"client-section-item"}>
                                <label className={"property"}>DIRECCIÓN FISCAL</label>
                                <label
                                    className={"value"}>{client ? client.companyInformation.direccionFiscal : null}</label>
                            </div>
                            <div className={"client-section-item"}>
                                <label className={"property"}>RÉGIMEN FISCAL</label>
                                <label className={"value"}>
                                    {client ? taxRegimes[client.companyInformation.regimenFiscal] : null}
                                </label>
                            </div>
                            <div className={"client-section-item"}>
                                <label className={"property"}>MÉTODO DE PAGO</label>
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