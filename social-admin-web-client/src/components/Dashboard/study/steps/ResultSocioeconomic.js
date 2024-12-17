import React, { useEffect, useRef, useState } from "react";
import { useSelector } from "react-redux";
import StudyService from "../../../../services/StudyService";
import FinalResultService from "../../../../services/FinalResultService";
import CandidateService from "../../../../services/CandidateService";
import AuthService from "../../../../services/AuthService";
import { isDisabled } from "@testing-library/user-event/dist/utils";
import { useHistory } from "react-router-dom";
import moment from "moment";
import Popup from '../../../Landing/PopUp';

const ResultSocioeconomic = () => {
    const fileRef = useRef()
    const date = moment();
    const dateVisit = moment();
    const history = useHistory()

    const disableForm = AuthService.getIdentity().role === 4;

    // Form data
    const [finalResult, setFinalResult] = useState({
        positionSummary: "",
        attitudeSummary: "",
        workHistorySummary: "",
        arbitrationAndConciliationSummary: "",
        finalResultsBy: "",
        finalResultsPositionBy: "",
        applicationDate: date,
        visitDate: dateVisit, 
    })
    const [picture, setPicture] = useState("")
    const [candidate, setCandidate] = useState(null)
    const [client, setClient] = useState(null)
    const [interviewer, setInterviewer] = useState(null)

    // Dates
    const [visitDateTime, setVisitDateTime] = useState(date.format("yyyy-MM-DD HH:mm"));
    const [applicationDateTime, setApplicationDateTime] = useState(dateVisit.format("yyyy-MM-DD"));

    // Get Study id from Redux
    const studyId = useSelector(state => state.study)
    // Add study to state
    const [study, setStudy] = useState({})

    // Make API request to Study Service and search for the study
    const getStudy = async () => {
        let response = await StudyService.getStudy(studyId)
        let study = response.data.response[0]
        setStudy(study)

        // If there is a studyFinalResult object, pass the values to the form data
        if (study.studyFinalResult) {
            setFinalResult({ ...study.studyFinalResult })
        }
        // Set candidate
        setCandidate({ ...study.candidate })
        // Set Client
        setClient(study.candidate.client)
        // Set Interviewer
        setInterviewer(study.interviewer)

        // Set picture
        if (study.candidate.media && study.candidate.media.mediaURL) {
            setPicture(study.candidate.media.mediaURL)
        }

        let studyVisitDate = moment(study.studyFinalResult.visitDate);
        let studyDate = moment(study.studyFinalResult.applicationDate);

        setVisitDateTime(studyVisitDate.format("yyyy-MM-DD HH:mm"));
        setApplicationDateTime(studyDate.format("yyyy-MM-DD"));

    }

    // Get Study
    useEffect(() => {
        if (studyId) {
            getStudy()
        }
    }, [studyId])

    // Submit data
    const submit = async () => {
        // If Study Final Result exists update
        if (study.studyFinalResult) {
            try {
                // Update Final Result
                finalResult.applicationDate = applicationDateTime;
                finalResult.visitDate = visitDateTime;
                await FinalResultService.update(finalResult)
                openPopup("Estudio guardado con éxito.");

                // If candidate picture was update, upload it
                if (candidate.media.base64Image) {
                    await CandidateService.update(candidate)
                }
                history.push("/dashboard")
                // Refresh data
                //getStudy()
            } catch (error) {
                history.push("/dashboard")
                console.log(error)
            }
        } else {
            try {
                // Create Final Result
                finalResult.studyId = study.id
                finalResult.applicationDate = date;
                finalResult.visitDate = dateVisit;
                
                await FinalResultService.create(finalResult)

                // If candidate picture was update, upload it
                if (candidate.media.base64Image) {
                    await CandidateService.update(candidate)
                }

                // Refresh data
                //getStudy()
                openPopup("Estudio guardado con éxito.");
                history.push("/dashboard")
            } catch (error) {
                history.push("/dashboard")
                console.log(error)
            }
        }
    }

    const [showPopup, setShowPopup] = useState(false);
    const [popupText, setPopupText] = useState(""); // Estado para almacenar el texto del pop-up

    const openPopup = (texto) => {
        setPopupText(texto);
        setShowPopup(true);
    };

    const closePopup = () => {
        setShowPopup(false);
    };

    const handleFile = (event) => {
        let data = { ...candidate }

        if (event.target.files[0]) {
            let reader = new FileReader()
            reader.readAsDataURL(event.target.files[0])

            reader.onload = (e) => {
                data.media = {
                    base64Image: e.target.result.replace(e.target.result.split(",")[0] + ",", "")
                }
                setCandidate(data)
            }
            setPicture(URL.createObjectURL(event.target.files[0]))
        }
    }

    const handleResult = (event) => {
        let data = { ...finalResult }
        data[event.target.name] = event.target.value
        setFinalResult(data)
    }

    const getServiceType = () => {
        if (study) {
            if (study.serviceType === 1) {
                return "socioeconómico"
            }
            return "laboral"
        }
    }

    return (
        <div className={"result-socioeconomic"}>
            <div className={"result-socioeconomic-title"}>
                <h1>Resultados del estudio {getServiceType()}</h1>
            </div>

            <div className={"result-socioeconomic-candidate"}>
                <div className={"candidate-picture"}>
                    <img src={picture !== "" ? picture : "/images/blank_picture.png"} alt={""} />
                    <button className={"edit-photo"} onClick={() => fileRef.current.click()} disabled={disableForm}>
                        <img src={"/images/actions-dropdown/edit.svg"} alt={""} />
                        <input ref={fileRef} type={"file"} onChange={handleFile} hidden accept={".jpg, .jpeg"} />
                    </button>
                    <label>Recomendable</label>
                </div>
                <div className={"candidate-data"}>
                    <h2>Candidato</h2>
                    <div className={"candidate-data-item"}>
                        <label>Nombre del Candidato</label>
                        <input type={"text"} value={candidate ? candidate.name : ""} readOnly disabled={disableForm} />
                    </div>
                    <div className={"candidate-data-item"}>
                        <label>Fecha de Solicitud</label>
                        <input type={"date"} value={applicationDateTime} onChange={event => setApplicationDateTime(event.target.value)} disabled={disableForm} />
                    </div>
                    <div className={"candidate-data-item"}>
                        <label>Puesto</label>
                        <input type={"text"} value={candidate ? candidate.position : ""} readOnly disabled={disableForm} />
                    </div>
                    <div className={"candidate-data-item"}>
                        <label>Fecha y hora de visita</label>
                        <input type={"datetime-local"} value={visitDateTime} onChange={event => setVisitDateTime(event.target.value)} disabled={disableForm} />
                    </div>
                    <div className={"candidate-data-item"}>
                        <label>Empresa/Cliente</label>
                        <input type={"text"} value={client ? client.companyInformation.companyName : ""} readOnly disabled={disableForm} />
                    </div>
                    <div className={"candidate-data-item"}>
                        <label>Entrevistador</label>
                        <input type={"text"} value={interviewer ? interviewer.name : ""} readOnly disabled={disableForm} />
                    </div>
                </div>
            </div>

            <div className={"result-socioeconomic-summary"}>
                <h2>Resumen final</h2>
                <div className={"summary-item"}>
                    <label>Actitud en la entrevista:</label>
                    <textarea name={"attitudeSummary"} value={finalResult.attitudeSummary}
                        onChange={event => handleResult(event)} disabled={disableForm} />
                </div>
                <div className={"summary-item"}>
                    <label>Verificación escolar:</label>
                    <textarea name={"positionSummary"} value={finalResult.positionSummary}
                        onChange={event => handleResult(event)} disabled={disableForm} />
                </div>
                <div className={"summary-item"}>
                    <label>Trayectoria laboral:</label>
                    <textarea name={"workHistorySummary"} value={finalResult.workHistorySummary}
                        onChange={event => handleResult(event)} disabled={disableForm} />
                </div>
                <div className={"summary-item"}>
                    <label>Junta de conciliación y arbitraje:</label>
                    <textarea name={"arbitrationAndConciliationSummary"}
                        value={finalResult.arbitrationAndConciliationSummary}
                        onChange={event => handleResult(event)} disabled={disableForm} />
                </div>
            </div>

            <div className={"signature"}>
                <label>Atentamente</label>
                <input name={"finalResultsBy"} value={finalResult.finalResultsBy} placeholder={"Agregar nombre..."}
                    onChange={event => handleResult(event)} disabled={disableForm} />
                <input name={"finalResultsPositionBy"} value={finalResult.finalResultsPositionBy}
                    placeholder={"Agregar puesto..."} onChange={event => handleResult(event)} disabled={disableForm} />
            </div>

            <div className={"result-socioeconomic-save"} onClick={submit}>
                <button className={"form-button-primary save-step"} disabled={disableForm}>Guardar</button>
               
            </div>
        </div>
    )
}

export default ResultSocioeconomic