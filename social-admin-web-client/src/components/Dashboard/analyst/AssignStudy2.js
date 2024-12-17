import React, {useEffect, useState, useRef} from "react";
import {useDispatch, useSelector} from "react-redux";
import {useHistory} from "react-router-dom";
import _ from 'lodash';

// Actions
import {
    hideAssignedStudies,
    removeAnalystId,
    removeInterviewerId,
    setStudyId,
    hideAssignStudy,
} from "../../../actions";
import StudyService from "../../../services/StudyService";

// Services

const AssignStudy2 = () => {
    const dispatch = useDispatch()
    const history = useHistory()

    const analystId = useSelector(state => state.analyst)
    const interviewerId = useSelector(state => state.interviewer)
    const show = useSelector(state => state.assignStudy)

    const [study, setStudy] = useState(null)
    const [studies, setStudies] = useState([])
    const [page, setPage] = useState(0)
    const [search, setSearch] = useState("")
    const [allowMorePages, setAllowMorePages] = useState(true)

    //const inputRef = useRef(null);
    const BringStudiesOnly = {
        ALL: 0,
        ANALYST: 1,
        INTEVIEW: 2,
    };


    useEffect(() => {
        const getStudies = async () => {
            try {
                if (analystId) {
                    const response = await StudyService.getStudies(page, null, null, 0, null, null, null, analystId, false, BringStudiesOnly.ANALYST)
                    setAllowMorePages(_.get(response, "data.response.length", 0) !== 0);
                    setStudies([...studies, ...response.data.response])
                } else if (interviewerId) {
                    const response = await StudyService.getStudies(page, null, interviewerId, 0, null, null, null, null, false, BringStudiesOnly.INTEVIEW)
                    setAllowMorePages(_.get(response, "data.response.length", 0) !== 0);
                    setStudies([...studies, ...response.data.response])
                }
            } catch (error) {
                console.log("error")
            }
        }

        if (show) {
            getStudies()
            //if (inputRef && inputRef.current){
            //    debugger;
                //inputRef.current.focus();
            //}
        }
    }, [show, analystId, interviewerId, page, study])

    useEffect(() => {
        return () => {
            setPage(0)
            setStudies([])
        }
    }, [show])

    const handleScroll = (event) => {
        /*
        const newPage = Math.floor(event.target.scrollTop / 40 )
        console.log("event.target.scrollTop", event.target.scrollTop, Math.floor(event.target.scrollTop / 40), page);

        if (newPage > page) {
            setPage(newPage)
        }
        **/

       //debugger;
        const bottom = event.target.scrollHeight === event.target.scrollTop + event.target.clientHeight;
       if(bottom && allowMorePages){
         // add your code here
         setPage(page + 1)

       }
    }

    const viewStudy = (id) => {
        // Assign study
        dispatch(setStudyId(id))

        // Remove users ids
        dispatch(removeAnalystId())
        dispatch(removeInterviewerId())

        // Close Form
        dispatch(hideAssignStudy())

        // Navigate to View Study
        history.push("/dashboard/validaciones/ver")
    }

    const renderStudies = studies.map((option, index) => {
            return (
                <div className={"assigned-study"} key={`study-${option.id}`} >
                    <label>{_.get(option,"candidate.name", "---")}</label>
                    <div className={"button-box"} >
                        <button onClick={() => handleStudy(option)}>
                            <img src={"/images/actions-dropdown/user-plus.svg"} alt={""}/>
                        </button>
                        <button onClick={() => viewStudy(option.id)}>
                            <img src={"/images/actions-dropdown/eye.svg"} alt={""}/>
                        </button>
                    </div>
                </div>
            )
        }
    )

    const handleStudy = (study) => {
        setStudy(studies.find(x => x.id === study.id))
        console.log("study: ", study)
    }

    const getDate = (value) => {
        const date = new Date(value)
        return date.toLocaleDateString()
    }

    const getTime = (value) => {
        const date = new Date(value)
        return date.toLocaleTimeString()
    }

    const cancelFormData = () => {
        setStudies([])
        setSearch("")
        setPage(0)
        setStudy(null)
    }

    const onSubmit = async (event) => {
        event.preventDefault()
        try {

            if (analystId) {
                await StudyService.update({...study, analyst: {id: analystId}})
            } else if (interviewerId) {
                await StudyService.update({...study, interviewer: {id: interviewerId}})
            }

            dispatch(hideAssignStudy())
            cancelFormData()
            history.push("/dashboard/analistas/ver")
        } catch (error) {
            console.log(error)
        }
    }

    const showHideModal = show ? "modal display-block" : "modal display-none"

    const RenderInterViewerOrAnalizer = () => {
        const serviceType = _.get(study, 'serviceType', null);
        console.log("ServiceType: ", serviceType)
       if (serviceType === 1) { // "Socioeconomico"
            return (
                <div className={"assign-study-form-item long"}>
                    <label htmlFor={"interviewer"}>Entrevistador</label>
                    <input
                        name={"interviewer"}
                        placeholder={"-"}
                        readOnly
                        value={_.get(study, 'interviewer.name', '') + "/" +  _.get(study, 'interviewer.email', '')}
                    />
                </div>
            );
        } else if (serviceType === 2) { // "Laboral"
            return (
                <div className={"assign-study-form-item long"}>
                    <label htmlFor={"interviewer"}>Analista</label>
                    <input
                        name={"interviewer"}
                        placeholder={"-"}
                        readOnly
                        value={_.get(study, 'analyst.name', '') + "/" +  _.get(study, 'analyst.email', '')}
                    />
                </div>
            );
        }

        return (<></>);
    }

    const StudyFormData = () => {
        return (
            <div className={"form-assign-study"}>
            <div className={"close-modal"}>
                <img src={"/images/icon-close.png"} alt={""} onClick={() => {dispatch(hideAssignStudy()); cancelFormData(); }}/>
            </div>
            <h2>Asignar estudio {( !_.isNil(interviewerId) ? "socioeconomico" : "laboral")}</h2>
            <form onSubmit={onSubmit}>
                <h3>Datos del estudio</h3>
                <div className={"assign-study-form-item long"}>
                    <label htmlFor={"candidate"}>Candidato</label>
                    <label htmlFor={"candidate"}>{study ? study.candidate.name + "/" + (study.serviceType === 1 ? "Socioeconomico" : "Laboral") : null}</label>
                </div>

                <div className={"assign-study-form-item long"}>
                    <label htmlFor={"service-type"}>Tipo de servicio</label>
                    <input 
                        name={"service-type"}
                        placeholder={"-"}
                        readOnly
                        value={study ? (study.serviceType === 1 ? "Socioeconomico" : "Laboral") : ""}
                    />
                </div>
                <div className={"assign-study-form-item long"}>
                    <label htmlFor={"client"}>Empresa/cliente</label>
                    <input
                        name={"client"}
                        placeholder={"-"}
                        readOnly
                        value={_.get(study, 'candidate.client.companyInformation.companyName', 'Sin nombre')}
                    />
                </div>
                <RenderInterViewerOrAnalizer />
                {
                    /*
                <div className={"assign-study-form-item"}>
                    <label htmlFor={"request-date"}>Fecha de solicitud</label>
                    <input
                        name={"request-date"}
                        placeholder={"-"}
                        readOnly
                        value={study ? getDate(study.createdAt) : ""}
                    />
                </div>
               
                        <div className={"assign-study-form-item"}>
                            <label htmlFor={"request-hour"}>Hora de solicitud</label>
                            <input name={"request-hour"} placeholder={"-"} readOnly
                                value={study ? getTime(study.createdAt) : ""}/>
                        </div>
                    
                        <div className={"assign-study-form-item"}>
                            <label htmlFor={"delivery-date"}>Fecha de entrega</label>
                            <input name={"delivery-date"} placeholder={"DD/MM/YYYY"} readOnly/>
                        </div> 
                    */
                }
                <div className={"assign-study-form-item"}>
                    <label htmlFor={"status"}>Estatus</label>
                    <select 
                        name={"status"}
                        value={_.get(study, 'studyStatus', 2)}
                        onChange={
                            (event) => {
                                setStudy({...study, studyStatus: parseInt(event.target.value)})
                            }
                        }
                        readOnly
                    >
                        <option value={0} selected={_.get(study, 'studyStatus', 0) === 0}>Seleccionar</option>
                        <option value={1} selected={_.get(study, 'studyStatus', 0) === 1}>No se ha iniciado</option>
                        <option value={2} selected={_.get(study, 'studyStatus', 0) === 2}>En progreso</option>
                        <option value={3} selected={_.get(study, 'studyStatus', 0) === 3}>Recomendable</option>
                        <option value={4} selected={_.get(study, 'studyStatus', 0) === 4}>No recomendable</option>
                        <option value={5} selected={_.get(study, 'studyStatus', 0) === 5}>Recomendable con reserva</option>
                    </select>
                </div>
                <div className={"assign-study-form-action button-box"}>
                    <button className={"form-button-primary"} onClick={cancelFormData} >Regresar</button>
                    <button className={"form-button-primary"}>Asignar</button>
                </div>
            </form>
        </div>
        );
    }

    const StudyList = () => {
        return (
            <div className={"assigned-studies"}>
                <div className={"close-modal"}>
                    <img src={"/images/icon-close.png"} alt={""} onClick={() => dispatch(hideAssignStudy())}/>
                </div>
                <h2>Asignar Estudio {( !_.isNil(interviewerId) ? "socioeconomico" : "laboral")}</h2>
                <div className={"search-form"}>
                    <input className={"search-field"} type={"search"} placeholder={"Buscar candidato..."} value={search}
                           onChange={event => setSearch(event.target.value)}/>
                    <button className={"search-button"}>
                        <img src={"/images/search.png"} alt={""}/>
                    </button>
                </div>
                <div className={"assigned-studies-list"} onScroll={handleScroll} >
                    {renderStudies}
                </div>
            </div>
        );
    };

    return (
        <div className={showHideModal}>
            {
                _.isNil(study) && <StudyList />
            }
            {
                !_.isNil(study) && <StudyFormData /> 
            }
        </div>
    )
}

export default AssignStudy2;