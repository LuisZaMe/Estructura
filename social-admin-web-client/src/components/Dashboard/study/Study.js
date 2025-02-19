import React, { useEffect, useState } from "react";
import { useHistory } from "react-router-dom";

// Components
import ProgressBar from "../ProgressBar";
import Select from "../common/Select";

// Models
import Account from "../../../model/Account";
import Service from "../../../model/Service";

// Services
import AccountService from "../../../services/AccountService";
import CandidateService from "../../../services/CandidateService";
import StudyService from "../../../services/StudyService";
import AuthService from "../../../services/AuthService";

const Study = () => {
    const history = useHistory()

    let identity = AuthService.getIdentity()

    const [progress, setProgress] = useState(1)
    const [candidate, setCandidate] = useState(null)
    const [candidates, setCandidates] = useState([])
    const [study, setStudy] = useState(0)
    const [document, setDocument] = useState({})
    const [fields, setFields] = useState({})
    const [interviewer, setInterviewer] = useState(null)
    const [candidatePage, setCandidatePage] = useState(0)
    const [interviewerPage, setInterviewerPage] = useState(0)
    const [interviewers, setInterviewers] = useState([])
    const [analystPage, setAnalystPage] = useState(0)
    const [analysts, setAnalysts] = useState([])
    const [analyst, setAnalyst] = useState(null)

    // Get Candidates
    useEffect(() => {
        const getCandidates = async () => {
            try {
                const response = await CandidateService.getCandidates(null, candidatePage)
                setCandidates([...candidates, ...response.data.response])
            } catch (error) {
                console.log(error)
            }
        }

        getCandidates()
    }, [candidatePage])

    // Get Interviewers
    useEffect(() => {
        const getInterviewers = async () => {
            try {
                const response = await AccountService.getAccounts(Account.INTERVIEWER, null, interviewerPage, identity.underAdminUserId)
                setInterviewers([...interviewers, ...response.data.response])
            } catch (error) {
                console.log(error)
            }
        }

        getInterviewers()
    }, [interviewerPage])

    // Get Analysts
    useEffect(() => {
        const getAnalysts = async () => {
            try {
                const response = await AccountService.getAccounts(Account.ANALYST, null, analystPage, identity.underAdminUserId)
                setAnalysts([...analysts, ...response.data.response])
            } catch (error) {
                console.log(error)
            }
        }

        getAnalysts()
    }, [analystPage])

    const handleCandidate = (option) => {
        setCandidate(candidates.find(x => x.id === option.id))
    }

    const candidateOptions = candidates.map(candidate => {
        return {
            id: candidate.id,
            value: `${candidate.name} ${candidate.lastname || ""}`.trim()
        }
    })

    const handleInterviewer = (interviewer) => {
        setInterviewer(interviewers.find(x => x.id === interviewer.id))
    }

    const interviewerOptions = interviewers.map(interviewer => {
        return {
            id: interviewer.id,
            value: `${interviewer.name} ${interviewer.lastname}`
        }
    })

    const handleAnalyst = (analyst) => {
        setAnalyst(analysts.find(x => x.id === analyst.id))
    }

    const analystOptions = analysts.map(analyst => {
        return {
            id: analyst.id,
             value: `${analyst.name} ${analyst.lastname}`
        }
    })

    const selectStudy = () => {
        // Estudio Socioeconomico
        if (study === 1) {
            setDocument({
                ine: {
                    name: "Credencial INE",
                    selected: false
                },
                proofOfAddress: {
                    name: "Comprobante de Domicilio",
                    selected: false
                },
                birthCertificate: {
                    name: "Acta de nacimiento",
                    selected: false
                },
                curp: {
                    name: "CURP",
                    selected: false
                },
                proofOfStudy: {
                    name: "Comprobante de Estudios",
                    selected: false
                },
                imssNumber: {
                    name: "Numero de IMSS",
                    selected: false
                }
            })
            //    Estudio laboral
        } else if (study === 2) {
            setDocument({
                ine: {
                    name: "Credencial INE",
                    selected: false
                },
                proofOfAddress: {
                    name: "Comprobante de Domicilio",
                    selected: false
                },
                birthCertificate: {
                    name: "Acta de nacimiento",
                    selected: false
                },
                curp: {
                    name: "CURP",
                    selected: false
                },
                proofOfStudy: {
                    name: "Comprobante de Estudios",
                    selected: false
                },
                imssNumber: {
                    name: "Numero de IMSS",
                    selected: false
                },
                taxId: {
                    name: "RFC",
                    selected: false
                },
                militaryServiceCertificate: {
                    name: "Carta militar",
                    selected: false
                },
                criminalRecordCertificate: {
                    name: "Carta de antecedentes penales",
                    selected: false
                }
            })
        }
    }

    const loadFields = (type) => {
        if (type == 2) {
            setFields({
                summary: {
                    name: "Resumen",
                    selected: false
                },
                overallData: {
                    name: "Datos generales",
                    selected: false
                },
                recommendationLetter: {
                    name: "Carta de recomendacion",
                    selected: false
                },
                ine: {
                    name: "INE/IFE (Fotos)",
                    selected: false
                },
                scholarship: {
                    name: "Escolaridad",
                    selected: false
                },
                extracurricular: {
                    name: "Extracurriular",
                    selected: false
                },
                academicValidation: {
                    name: "Verificacion escolar",
                    selected: false
                },
                workHistory: {
                    name: "Trayectoria laboral",
                    selected: false
                },
                imssValidation: {
                    name: "Validacion IMSS",
                    selected: false
                },
                personalReferences: {
                    name: "Referencias Personales",
                    selected: false
                },
            })
        } else {
            setFields({
                summary: {
                    name: "Resumen",
                    selected: false
                },
                overallData: {
                    name: "Datos generales",
                    selected: false
                },
                recommendationLetter: {
                    name: "Carta de recomendacion",
                    selected: false
                },
                ine: {
                    name: "INE/IFE (Fotos)",
                    selected: false
                },
                scholarship: {
                    name: "Escolaridad",
                    selected: false
                },
                extracurricular: {
                    name: "Extracurriular",
                    selected: false
                },
                academicValidation: {
                    name: "Verificacion escolar",
                    selected: false
                },
                family: {
                    name: "Familia",
                    selected: false
                },
                employmentStatus: {
                    name: "Situacion economica",
                    selected: false
                },
                social: {
                    name: "Social",
                    selected: false
                },
                workHistory: {
                    name: "Trayectoria laboral",
                    selected: false
                },
                imssValidation: {
                    name: "Validacion IMSS",
                    selected: false
                },
                personalReferences: {
                    name: "Referencias Personales",
                    selected: false
                },
                photos: {
                    name: "Fotografias",
                    selected: false
                }
            })
        }
    }

    const submitStudy = async () => {
        try {
            const service = new Service(
                candidate,
                fields,
                interviewer,
                analyst,
                study,
                document
            )
            
            await StudyService.create(service)

            setProgress(6)
        } catch (error) {
            console.log(error)
        }

    }

    const renderDocuments = () => {
        const renderedDocuments = []

        for (const property in document) {
            const newDocument = (
                <div key={property} className={"grid-item"}>
                    <label>{document[property].name}</label>
                    <input type={"checkbox"} checked={document[property].selected} readOnly={true} />
                    <span className={"checkmark"} onClick={() => {
                        const tempDocument = { ...document }
                        tempDocument[property].selected = !tempDocument[property].selected
                        setDocument(tempDocument)
                    }}></span>
                </div>
            )
            renderedDocuments.push(newDocument)
        }

        return renderedDocuments
    }

    const preloadDocuments = () => {
        const loadedDocuments = []

        for (const property in document) {
            if (document[property].selected) {
                const newDocument = <span key={`load-${property}`}>{document[property].name}</span>
                loadedDocuments.push(newDocument)
            }
        }

        return loadedDocuments
    }

    const renderRequiredFields = () => {
        const renderedRequiredFields = []

        for (const property in fields) {
            const newRequiredFields = (
                <div key={property} className={"grid-item"}>
                    <label>{fields[property].name}</label>
                    <input type={"checkbox"} checked={fields[property].selected} readOnly={true} />
                    <span className={"checkmark"} onClick={() => {
                        const tempFields = { ...fields }
                        tempFields[property].selected = !tempFields[property].selected
                        setFields(tempFields)
                    }}></span>
                </div>
            )
            renderedRequiredFields.push(newRequiredFields)
        }

        return renderedRequiredFields
    }

    const preloadRequiredFields = () => {
        const loadedFields = []

        for (const property in fields) {
            if (fields[property].selected) {
                const newDocument = <span key={`load-${property}`}>{fields[property].name}</span>
                loadedFields.push(newDocument)
            }
        }

        return loadedFields
    }

    // Render Component
    if (progress >= 1 && progress <= 4) {
        return (
            <div className={"container"}>
                <div className={"content study"}>
                    <div className={"top-section"}>
                        <ProgressBar progress={progress} onProgressChange={setProgress} />
                    </div>
                    {progress === 1 &&
                        <div className={"main-section select-candidate shadow"}>
                            <h2 className={"study-header select-candidate"}>Seleccionar candidato</h2>
                            <div className={"study-dropdown select-candidate"}>
                                <Select 
                                    options={candidateOptions}
                                    selectedOption={candidate ? `${candidate.name} ${candidate.lastname || ""}`.trim() : null}
                                    onChange={handleCandidate}
                                    page={candidatePage}
                                    setPage={setCandidatePage} 
                                />
                            </div>
                            <button disabled={!candidate} className={"study-button select-candidate"}
                                onClick={() => setProgress(2)}>Siguiente
                            </button>
                        </div>
                    }

                    {progress === 2 &&
                        <div className={"main-section select-study shadow"}>
                            <h2 className={"study-header select-study"}>Seleccionar estudio</h2>
                            <select value={study} className={"study-dropdown select-study"}
                                onChange={event => setStudy(parseInt(event.target.value))}>
                                <option value={0}>Seleccionar</option>
                                <option value={1}>Socioeconomico</option>
                                <option value={2}>Laboral</option>
                            </select>
                            <button disabled={study === 0} className={"study-button select-study"}
                                onClick={() => {
                                    selectStudy()
                                    setProgress(3)
                                }}>Siguiente
                            </button>
                        </div>
                    }

                    {progress === 3 &&
                        <div
                            className={`main-section select-documents ${study === 1 ? "social-assessment" : "work-assessment"} shadow`}>
                            <h2 className={"study-header select-documents"}>Seleccionar documentos</h2>
                            <div className={"documents-grid select-documents no-scrollbar"}>
                                {renderDocuments()}
                            </div>
                            <button disabled={study === 0} className={"study-button select-documents"}
                                onClick={() => {
                                    setProgress(4)
                                    loadFields(study)
                                }}>Siguiente
                            </button>
                        </div>
                    }

                    {progress === 4 &&
                        <div className={`main-section select-fields shadow`}>
                            <h2 className={"study-header select-fields"}>Campos a llenar</h2>
                            <div className={"documents-grid select-fields no-scrollbar"}>
                                {renderRequiredFields()}
                            </div>
                            <button disabled={study === 0} className={"study-button select-fields"}
                                onClick={() => {
                                    setProgress(5)
                                }}>Precargar candidato
                            </button>
                        </div>
                    }
                </div>
            </div>
        )
    }

    if (progress === 5) {
        return (
            <div className={"container"}>
                <div className={"content study"}>
                    <div className={"main-section preload-study shadow"}>
                        <label className={"preload-study-header"}>Se han precargado los datos del candidato, favor de
                            seleccionar un entrevistador:</label>
                            <div className={"preload-study-candidate"}>
                                <label className={"preload-study-label"}>Candidato</label>
                                <span className={"preload-study-span"}> {candidate ? `${candidate.name} ${candidate.lastname || ""}`.trim() : ""}</span>
                            </div>
                        <div className={"preload-study-study"}>
                            <label className={"preload-study-label"}>Estudio</label>
                            <span className={"preload-study-span"}>{study === 1 ? "Socioeconomico" : "Laboral"}</span>
                        </div>
                        <div className={"preload-study-documents"}>
                            <label className={"preload-study-label"}>Documentacion a entregar</label>
                            {preloadDocuments()}
                        </div>
                        <div className={"preload-study-fields"}>
                            <label className={"preload-study-label"}>Campos a llenar</label>
                            {preloadRequiredFields()}
                        </div>
                        <div className={"preload-study-interviewer"}>
                            {study === 1 ?
                                <>
                                    <label className={"preload-study-label"}>Seleccionar entrevistador</label>
                                    <div className={"study-dropdown select-interviewer"}>
                                    <Select 
                                    options={interviewerOptions}
                                    selectedOption={interviewer ? `${interviewer.name} ${interviewer.lastname}` : null}
                                    onChange={handleInterviewer}
                                    page={interviewerPage}
                                    setPage={setInterviewerPage}
                                    />
                                    </div>
                                </> :
                                <>
                                    <label className={"preload-study-label"}>Seleccionar analista</label>
                                    <div className={"study-dropdown select-interviewer"}>
                                    <Select 
                                    options={analystOptions}
                                    selectedOption={analyst ? `${analyst.name} ${analyst.lastname}` : null}
                                    onChange={handleAnalyst}
                                    page={analystPage}
                                    setPage={setAnalystPage}
                                    />
                                    </div>
                                </>
                            }
                        </div>
                        <button disabled={!interviewer && !analyst} className={"study-button submit-study"}
                            onClick={submitStudy}>Finalizar
                        </button>
                    </div>
                </div>
            </div>
        )
    }

    if (progress === 6) {
        return (
            <div className={"container"}>
                <div className={"study-generated"}>
                    <img src={"/images/study-generated.png"} alt={""} />
                    <h2>Proceso finalizado</h2>
                    <label>Se ha notificado al entrevistador</label>
                    <button className={"study-button"} onClick={() => history.push("/dashboard")}>Aceptar</button>
                </div>
            </div>
        )
    }
}

export default Study