import React, {useEffect, useState} from "react";
import {useDispatch, useSelector} from "react-redux";
import {useHistory} from "react-router-dom";

// Actions
import {hideAssignStudy} from "../../../actions";

// Components
import Select from "../common/Select";

// Services
import StudyService from "../../../services/StudyService";

const AssignStudy = () => {
    const dispatch = useDispatch()
    const history = useHistory()

    const analystId = useSelector(state => state.analyst)
    const show = useSelector(state => state.assignStudy)

    const [study, setStudy] = useState(null)
    const [studies, setStudies] = useState([])
    const [page, setPage] = useState(0)

    useEffect(() => {
        const getStudies = async () => {
            try {
                const response = await StudyService.getStudies(page)
                setStudies([...studies, ...response.data.response])
            } catch (error) {
                console.log(error)
            }
        }

        if (show)
            getStudies()
    }, [page, show])

    const studyOptions = studies.map(study => {
        return {
            id: study.id,
            value: study.candidate.name + "/" + (study.serviceType === 1 ? "Socioeconomico" : "Laboral")
        }
    })

    const handleStudy = (study) => {
        setStudy(studies.find(x => x.id === study.id))
    }

    const getDate = (value) => {
        const date = new Date(value)
        return date.toLocaleDateString()
    }

    const getTime = (value) => {
        const date = new Date(value)
        return date.toLocaleTimeString()
    }

    const onSubmit = async (event) => {
        event.preventDefault()

        try {
            await StudyService.update({...study, analyst: {id: analystId}})

            dispatch(hideAssignStudy())
            history.push("/dashboard/analistas/ver")
        } catch (error) {
            console.log(error)
        }
    }

    const showHideModal = show ? "modal display-block" : "modal display-none"

    return (
        <div className={showHideModal}>
            <div className={"form-assign-study"}>
                <div className={"close-modal"}>
                    <img src={"/images/icon-close.png"} alt={""} onClick={() => dispatch(hideAssignStudy())}/>
                </div>
                <h2>Asignar estudio</h2>
                <form onSubmit={onSubmit}>
                    <h3>Datos del estudio</h3>
                    <div className={"assign-study-form-item long"}>
                        <label htmlFor={"candidate"}>Candidato</label>
                        <Select options={studyOptions}
                                selectedOption={study ? study.candidate.name + "/" + (study.serviceType === "1" ? "Socioeconomico" : "Laboral") : null}
                                onChange={handleStudy}
                                page={page}
                                setPage={setPage}/>
                    </div>

                    <div className={"assign-study-form-item long"}>
                        <label htmlFor={"service-type"}>Tipo de servicio</label>
                        <input name={"service-type"} placeholder={"-"} readOnly
                               value={study ? (study.serviceType === 1 ? "Socioeconomico" : "Laboral") : ""}/>
                    </div>
                    <div className={"assign-study-form-item long"}>
                        <label htmlFor={"client"}>Empresa/cliente</label>
                        <input name={"client"} placeholder={"-"} readOnly/>
                    </div>
                    <div className={"assign-study-form-item long"}>
                        <label htmlFor={"interviewer"}>Entrevistador</label>
                        <input name={"interviewer"} placeholder={"-"} readOnly
                               value={study ? (study.interviewer ? study.interviewer.name : "") : ""}/>
                    </div>
                    <div className={"assign-study-form-item"}>
                        <label htmlFor={"request-date"}>Fecha de solicitud</label>
                        <input name={"request-date"} placeholder={"-"} readOnly
                               value={study ? getDate(study.createdAt) : ""}/>
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
                    <div className={"assign-study-form-item"}>
                        <label htmlFor={"status"}>Estatus</label>
                        <select name={"status"} value={study ? study.studyStatus : 0} onChange={event => {
                            setStudy({...study, studyStatus: parseInt(event.target.value)})
                        }}>
                            <option value={0}>Seleccionar</option>
                            <option value={1}>No se ha iniciado</option>
                            <option value={2}>En progreso</option>
                            <option value={3}>Recomendable</option>
                            <option value={4}>No recomendable</option>
                            <option value={5}>Recomendable con reserva</option>
                        </select>
                    </div>
                    <div className={"assign-study-form-action"}>
                        <button className={"form-button-primary"}>Agregar</button>
                    </div>
                </form>
            </div>
        </div>
    )
}

export default AssignStudy