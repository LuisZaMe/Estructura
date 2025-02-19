import React, { useEffect, useState } from "react"
import { useHistory } from "react-router-dom";
import { useDispatch, useSelector } from "react-redux";
import './visit.css';

//Actions
import { removeVisitId, setVisitId, showRegisterVisit } from "../../../actions/index.js";


//Components
import ActionDropdown from "../common/ActionDropdown";
import DeleteAccountForm from "../common/DeleteAccountForm";
import Pagination from "../common/Pagination";
import { RegisterVisit } from "./RegisterVisit";

// Models
import Account from "../../../model/Account";

// Services
import VisitService from "../../../services/VisitService";
import { Calendar } from "./Calendar/Calendar.js";
import { CardDate } from "./Card/CardDate";
import _ from 'lodash';
import moment from 'moment';
import { EditVisit } from "./EditVisit";
const {
    addMonths,
    subMonths,
} = require('date-fns');
const {
    getYear,
    getMonth,
} = require('date-fns');

const VisitList = (props) => {
    const dispatch = useDispatch()
    const history = useHistory()
    const [globalDate, setGlobalDate] = useState(new Date());
    const day = new Date();

    const visit = useSelector(state => state)

    const [visits, setVisits] = useState([])
    const [filteredVisits, setFilteredVisits] = useState([]);
    const [visitToEdit, setVisitToEdit] = useState(null)
    const [view, setView] = useState('table')
    const [searchTerm, setSearchTerm] = useState("")
    const [page, setPage] = useState(0)
    const [pages, setPages] = useState(0)
    const [showDeleteVisit, setShowDeleteVisit] = useState(false)
    const [isLoading, setIsLoading] = useState(false);

    const MESES = [
        "ENERO",
        "FEBRERO",
        "MARZO",
        "ABRIL",
        "MAYO",
        "JUNIO",
        "JULIO",
        "AGOSTO",
        "SEPTIEMBRE",
        "OCTUBRE",
        "NOVIEMBRE",
        "DICIEMBRE",
    ];
    const getVisits = async () => {
        setIsLoading(true);
        try {
            const response = await VisitService.getVisit("", page)
            setVisits(response.data.response);
            setIsLoading(false);
        } catch (error) {
            console.log(error)
            setIsLoading(false);
        }
    }

    const getPages = async () => {
        try {
            const response = await VisitService.getPages()
            setPages(response.data.response)
        } catch (error) {
            console.log(error)
        }
    }

    useEffect(() => {
        const filtered = visits.filter(visit => {
            return (
                _.get(visit, 'study.candidate.client.companyInformation.companyName', '').toLowerCase().includes(searchTerm.toLowerCase()) ||
                _.get(visit, 'study.candidate.name', '').toLowerCase().includes(searchTerm.toLowerCase()) ||
                _.get(visit, 'study.candidate.lastname', '').toLowerCase().includes(searchTerm.toLowerCase()) ||
                _.get(visit, 'study.interviewer.name', '').toLowerCase().includes(searchTerm.toLowerCase()) ||
                moment.utc(_.get(visit, 'visitDate', '')).local().format("DD/MM/YYYY").includes(searchTerm) ||
                _.get(visit, 'state.name', '').toLowerCase().includes(searchTerm.toLowerCase()) ||
                _.get(visit, 'city.name', '').toLowerCase().includes(searchTerm.toLowerCase())
            );
        });
        
        setFilteredVisits(filtered);
    }, [searchTerm, visits]);

    useEffect(() => {
        getVisits()
        getPages()
    }, [page, visit]) // Elimina searchTerm de las dependencias

    const handleDelete = () => {
        try {
            dispatch(removeVisitId())
            setShowDeleteVisit(false)
            getVisits()
        } catch (error) {
            setShowDeleteVisit(false)
            getVisits()
        }
    }

    const onClickView = (id) => {
        dispatch(setVisitId(id))
        history.push("/dashboard/visitas/ver")
    }

    const onClickEdit = (id) => {
        dispatch(setVisitId(id))
        history.push("/dashboard/visitas/editar")
    }

    const onClickDelete = (id) => {
        dispatch(setVisitId(id))
        setShowDeleteVisit(true)
    }

    const renderActions = visits.map(visit => {
        return (
            <ActionDropdown key={`action-${visit.id}`} onClickView={onClickView} onClickEdit={onClickEdit}
                onClickDelete={onClickDelete} userId={visit.id} />
        )
    })

    const renderAdmins = filteredVisits.map(visit => {
        return (
            <div key={visit.id} className={"table-row"}>
                <label className={"table-cell"}>{visit.id}</label>
                <label className={"table-cell"}>{_.get(visit, 'study.candidate.client.companyInformation.companyName', '')}</label>
                <label className={"table-cell"}>{`${_.get(visit, 'study.candidate.name', '')} ${_.get(visit, 'study.candidate.lastname', '')}`.trim()}</label>
                <label className={"table-cell"}>{`${_.get(visit, 'study.interviewer.name', '')} ${_.get(visit, 'study.interviewer.lastname', '')}`.trim()}</label>
                <label className={"table-cell"}>{moment.utc(_.get(visit, 'visitDate', '')).local().format("DD/MM/YYYY")}</label>
                <label className={"table-cell"}>{moment.utc(_.get(visit, 'visitDate', '')).local().format("HH:mm")}</label>
                <label className={"table-cell"}>{_.get(visit, 'state.name', '')}</label>
                <label className={"table-cell"}>{_.get(visit, 'city.name', '')}</label>
                <label className={"table-cell"}>{_.get(visit, 'study.serviceType', 1) === 1 ? "Estudio Socioeconómico" : "Estudio Laboral" }</label>
                <label className={"table-cell"}>{_.get(visit, 'study.interviewer.name', '')} {_.get(visit, 'study.interviewer.lastname', '')}</label>
                <ActionDropdown key={`action-${visit.id}`} onClickView={onClickView} onClickEdit={onClickEdit}
                onClickDelete={onClickDelete} userId={visit.id} />
            </div>
        )
    })
    const subDates = () => {
        setGlobalDate(subMonths(globalDate, 1))
    }
    const addDates = () => {
        setGlobalDate(addMonths(globalDate, 1))
    }

    return (
        <div className={"container"}>
            <RegisterVisit />
            {
                visitToEdit &&
                <EditVisit visitToEdit={visitToEdit} />
            }
            {
                view === "table" &&
                <div className={"content visits"}>
                    <div className={"main-section visits-list shadow"}>
                        <div className={"visits-list-top"}>
                            <div className={"search-form"}>
                                <input className={"search-field"} type={"search"} placeholder={"Buscar..."}
                                    value={searchTerm} onChange={event => setSearchTerm(event.target.value)} />
                                <button className={"search-button"}>
                                    <img src={"/images/search.png"} alt={""} />
                                </button>
                            </div>
                            <div className={"visitas-list-actions"}>
                                <div className={"set-calendar-button"} onClick={() => { setView('calendar') }}>
                                    {/* <img src={"/images/CalendarIconActive.png"} alt={""}/> */}
                                </div>

                                <div className={"set-table-button"} onClick={() => { setView('table') }}>
                                    {/* <img src={"/images/TableIconActive.png"} alt={""}/> */}
                                </div>

                                <button className={"reload"} onClick={getVisits} style={{ margin: '0 10px' }}>
                                    <img src={"/images/refresh-icon.png"} alt={""} />
                                </button>
                                <button className={"register"} onClick={() => { dispatch(showRegisterVisit()) }}>Registrar
                                </button>


                                <DeleteAccountForm show={showDeleteVisit} handleClose={setShowDeleteVisit}
                                    handleDelete={handleDelete} id={visit} userType={"visit"} />
                            </div>
                        </div>
                        <div className={"table visits"}>
                            <div className={"table-row"}>
                                <label className={"table-cell-header"}>Id Visita</label>
                                <label className={"table-cell-header"}>Empresa/Cliente</label>
                                <label className={"table-cell-header"}>Candidato</label>
                                <label className={"table-cell-header"}>Entrevistador</label>
                                <label className={"table-cell-header"}>Fecha de visita</label>
                                <label className={"table-cell-header"}>Hora de visita</label>
                                <label className={"table-cell-header"}>Estado</label>
                                <label className={"table-cell-header"}>Región</label>
                                <label className={"table-cell-header"}>Tipo de servicio</label>
                                <label className={"table-cell-header"}>Estudio Target</label>
                            </div>
                            {renderAdmins}
                        </div>
                        {/* <div className={"table-actions"}>
                            {renderActions}
                        </div> */}
                    </div>
                    <div className={"pagination"}>
                        <Pagination page={page} setPage={setPage} pages={pages} isLoading={isLoading} />
                    </div>
                </div>
            }
            {
                view === "calendar" &&
                <div className="content-calendar">

                    <div className="header-container-calendar">
                        <div className="left-container-header">
                            <h2>
                                {
                                    (() => {
                                        let date;
                                        date = MESES[globalDate.getMonth()] + " DEL  " + getYear(globalDate)
                                        return date;
                                    })()
                                }
                            </h2>
                        </div>
                        <div className="right-container-header">
                            <div className='button-container-calendar'>
                                <div className='arrows'>
                                    <img src={"/images/Arrow_Calendar.png"} onClick={subDates} />
                                    <img className='right' src={"/images/Arrow_Calendar.png"} onClick={addDates} />
                                </div>
                            </div>
                            <div className={"visits-list-top"}>
                                <div className={"visitas-list-actions"}>
                                    <div className={"set-calendar-button"} onClick={() => { setView('calendar') }}>
                                        {/* <img src={"/images/CalendarIconActive.png"} alt={""}/> */}
                                    </div>

                                    <div className={"set-table-button"} onClick={() => { setView('table') }}>
                                        {/* <img src={"/images/TableIconActive.png"} alt={""}/> */}
                                    </div>

                                    <button className={"reload"} onClick={getVisits} style={{ margin: '0 10px' }}>
                                        <img src={"/images/refresh-icon.png"} alt={""} />
                                    </button>
                                    <button className={"register"} onClick={() => dispatch(showRegisterVisit())}>Registrar
                                    </button>
                                    {/* <RegisterVisit/> */}
                                    <DeleteAccountForm show={showDeleteVisit} handleClose={setShowDeleteVisit}
                                        handleDelete={handleDelete} id={visit.id} userType={"visit"} />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div className="body-container-calendar">
                        <div className="left-container">
                            <CardDate date={globalDate} visits={visits} setVisitToEdit={setVisitToEdit} />
                        </div>
                        <div className="right-container">
                            <Calendar
                                date={globalDate}
                                setGlobalDate={setGlobalDate}
                                handleDay={day}
                                name={'Calendar'}
                                selected={day}
                                visits={visits}
                            />
                        </div>
                    </div>
                </div>
            }
        </div>
    )
}

export default VisitList