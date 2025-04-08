import React, { useEffect, useState } from "react"
import { useDispatch, useSelector } from "react-redux";
import { useHistory } from "react-router-dom";

// Actions
import { 
    removeClientId, 
    setClientId, 
    showRegisterClient 
} from "../../../actions";

// Components
import RegisterClient from "./RegisterClient";
import ActionDropdown from "../common/ActionDropdown";
import DeleteAccountForm from "../common/DeleteAccountForm";
import Pagination from "../common/Pagination";

// Models
import Account from "../../../model/Account";

// Services
import AccountService from "../../../services/AccountService";



const ClientList = () => {
    const dispatch = useDispatch()
    const history = useHistory()
    const client = useSelector(state => state.client)
    const [clients, setClients] = useState([])
    const [filteredClients, setFilteredClients] = useState([])
    const [searchTerm, setSearchTerm] = useState("")
    const [page, setPage] = useState(0)
    const [pages, setPages] = useState(0)
    const [showDeleteClient, setShowDeleteClient] = useState(false)
    const [isLoading, setIsLoading] = useState(false)
    const taxRegimes = [
        "Seleccionar",
        "Régimen General de Ley",
        "Régimen de Personas Morales con Fines no Lucrativos",
        "Régimen de Incorporación Fiscal (RIF)",
        "Régimen de Actividades Agrícolas, Ganaderas, Silvícolas y Pesqueras",
        "Régimen de Coordinados",
        "Régimen de Empresas en Liquidación",
        "Régimen de Pequeños Contribuyentes (REPECO)",
        "Régimen de Arrendamiento",
        "Régimen de Distribuidores de Energía",
        "Régimen de Servicios Profesionales",
        "Régimen de Cooperativas",
        "Régimen de Actividades Empresariales con Ingresos Menores a 2 millones de pesos",
        "Régimen de Honorarios Profesionales",
        "Régimen de Empresas Familiares",
        "Régimen de Sociedad de Inversión de Capitales"
    ];      

    const getClients = async () => {
        setIsLoading(true);
        try {
            const response = await AccountService.getAccounts(Account.CLIENT, "", page)
            setClients(response.data.response)
            setIsLoading(false);
        } catch (error) {
            console.log(error)
            setIsLoading(false);
        }
    }

    const getPages = async () => {
        try {
            const response = await AccountService.getPages(Account.CLIENT, searchTerm)
            setPages(response.data.response)
        } catch (error) {
            console.log(error)
        }
    }

    useEffect(() => {
        getClients()
        getPages()
    }, [page])    

    useEffect(() => {
        const filtered = clients.filter(client => {
            return client.companyInformation?.companyName.toLowerCase().includes(searchTerm.toLowerCase()) ||
                `${client.name} ${client.lastname}`.toLowerCase().includes(searchTerm.toLowerCase()) ||
                client.email.toLowerCase().includes(searchTerm.toLowerCase())
        })
        setFilteredClients(filtered)
    }, [searchTerm, clients])

    const handleDelete = () => {
        dispatch(removeClientId())
        getClients();
    }

    const onClickView = (id) => {
        dispatch(setClientId(id))
        history.push("/dashboard/clientes/ver")
    }

    const onClickEdit = (id) => {
        dispatch(setClientId(id))
        history.push("/dashboard/clientes/editar")
    }

    const onClickDelete = (id) => {
        dispatch(setClientId(id))
        setShowDeleteClient(true)
    }

    const renderClients = filteredClients.map(client => {
        return (
            <div key={client.id} className={"table-row"}>
                <label className={"table-cell"}>{client.companyInformation ? client.companyInformation.companyName : ""}</label>
                <label className={"table-cell"}>{client.companyInformation ? client.companyInformation.companyPhone : ""}</label>
                <label className={"table-cell"}>{`${client.name || ''} ${client.lastname || ''}`.trim()}</label>
                <label className={"table-cell"}>{client.phone}</label>
                <label className={"table-cell"}>{client.email}</label>
                <label className={"table-cell"}>{client.companyInformation ? client.companyInformation.razonSocial : ""}</label>
                <label className={"table-cell"}>{client.companyInformation ? client.companyInformation.rfc : ""}</label>
                <label className={"table-cell"}>{client.companyInformation ? client.companyInformation.direccionFiscal : ""}</label>
                <label className={"table-cell"}>{client && client.companyInformation && client.companyInformation.regimenFiscal !== null ? taxRegimes[client.companyInformation.regimenFiscal] : "No definido"}</label>
                <label className={"table-cell"}>{client.companyInformation ? client.companyInformation.payment.description : ""}</label>
                <label className={"table-cell"}>{client.companyInformation ? client.companyInformation.totalStudies : ""}</label>
                <label className={"table-cell"}>{`${client.companyInformation ? client.companyInformation.completedStudies : "0"} de ${client.companyInformation ? client.companyInformation.totalStudies : "0"}`}</label>
                <ActionDropdown key={`action-${client.id}`} onClickView={onClickView} onClickEdit={onClickEdit}
                    onClickDelete={onClickDelete} userId={client.id} />
            </div>
        )
    })

    return (
        <div className={"container"}>
            <div className={"content clients"}>
                <div className={"main-section clients-list shadow"}>
                    <div className={"clients-list-top"}>
                        <div className={"search-form"}>
                            <input className={"search-field"} type={"search"} placeholder={"Buscar..."}
                                value={searchTerm} onChange={event => setSearchTerm(event.target.value)} />
                            <button className={"search-button"}>
                                <img src={"/images/search.png"} alt={""} />
                            </button>
                        </div>
                        <div className={"list-actions"}>
                            <button className={"reload"} onClick={getClients}>
                                <img src={"/images/refresh-icon.png"} alt={""} />
                            </button>
                            <button className={"register"} onClick={() => dispatch(showRegisterClient())}>Registrar
                            </button>
                            <RegisterClient />
                            <DeleteAccountForm show={showDeleteClient} handleClose={setShowDeleteClient}
                                handleDelete={handleDelete} id={client} userType={"client"} />
                        </div>
                    </div>
                    <div className={"table clients"}>
                        <div className={"table-row"}>
                            <label className={"table-cell-header"}>Nombre Empresa</label>
                            <label className={"table-cell-header"}>Teléfono Empresa</label>
                            <label className={"table-cell-header"}>Responsable</label>
                            <label className={"table-cell-header"}>Teléfono de responsable</label>
                            <label className={"table-cell-header"}>Correo</label>
                            <label className={"table-cell-header"}>Razón Social</label>
                            <label className={"table-cell-header"}>RFC</label>
                            <label className={"table-cell-header"}>Dirección Fiscal</label>
                            <label className={"table-cell-header"}>Regimen Fiscal</label>
                            <label className={"table-cell-header"}>Método de pago</label>
                            <label className={"table-cell-header"}>Solicitudes</label>
                            <label className={"table-cell-header"}>Avance de Solicitudes</label>
                        </div>
                        {renderClients}
                    </div>
                    {/* <div className={"table-actions"}>
                        {renderActions}
                    </div> */}
                </div>
                <div className={"pagination"}>
                    <Pagination page={page} setPage={setPage} pages={pages} isLoading={isLoading} />
                </div>
            </div>
        </div>
    )
}

export default ClientList