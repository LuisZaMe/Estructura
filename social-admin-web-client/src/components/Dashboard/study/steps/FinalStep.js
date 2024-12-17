import React, {useEffect, useState} from "react";
import {useHistory} from "react-router-dom";
import {useSelector} from "react-redux";

// Components
import Select from "../../common/Select";

// Services
import StudyService from "../../../../services/StudyService";
import AccountService from "../../../../services/AccountService";

// Models
import Account from "../../../../model/Account";
import AuthService from "../../../../services/AuthService";

const FinalStep = () => {
    const history = useHistory()

    const [analysts, setAnalysts] = useState([])
    const [analyst, setAnalyst] = useState(null)
    const [analystPage, setAnalystPage] = useState(0)

    // Get Study id from Redux
    const studyId = useSelector(state => state.study)
    // Add study to state
    const [study, setStudy] = useState({})

    // Make API request to Study Service and search for the study
    const getStudy = async () => {
        let response = await StudyService.getStudy(studyId)
        let study = response.data.response[0]

        setStudy(study)
    }

    // Get Study
    useEffect(() => {
        if (studyId) {
            getStudy()
        }
    }, [studyId])

    useEffect(() => {
        const getAnalysts = async () => {
            try {
                let identity = AuthService.getIdentity()

                const response = await AccountService.getAccounts(Account.ANALYST, null, analystPage, identity.underAdminUserId)
                setAnalysts([...analysts, ...response.data.response])
            } catch (error) {
                console.log(error)
            }
        }

        getAnalysts()
    }, [analystPage])

    const handleAnalyst = (analyst) => {
        setAnalyst(analysts.find(x => x.id === analyst.id))
    }

    const analystOptions = analysts.map(analyst => {
        return {
            id: analyst.id,
            value: analyst.name
        }
    })

    const submitToAdmin = async () => {
        try {
            let temp = {
                id: study.id,
                studyProgressStatus: 2 // Under admin
            }

            // Submit request
            await StudyService.update(temp)

            history.push("/dashboard/validaciones/administrador-asignado")
        } catch (error) {
            console.log(error)
        }
    }

    const submitToAnalyst = async () => {
        try {
            let temp = {
                id: study.id,
                analyst: { id: analyst.id },
                studyProgressStatus: 3 // Under analyst
            }

            // Submit request
            await StudyService.update(temp)

            history.push("/dashboard/validaciones/analista-asignado")
        } catch (error) {
            console.log(error)
        }
    }

    const approveStudy = async () => {
        try {
            let temp = {...study}
            temp.studyStatus = 3 // Accept study
            temp.studyProgressStatus = 4 // Under client
            // Submit request
            await StudyService.update(temp)

            history.push("/dashboard/validaciones/estudio-aprobado")
        } catch (error) {
            console.log(error)
        }
    }

    const rejectStudy = async () => {
        try {
            let temp = {...study}
            temp.studyStatus = 2 // Reject study
            temp.studyProgressStatus = 4 // Under client

            // Submit request
            await StudyService.update(temp)

            history.push("/dashboard/validaciones/estudio-rechazado")
        } catch (error) {
            console.log(error)
        }
    }

    // If study is under interviewer or analyst, send to admin
    if (study && (study.studyProgressStatus === 1 || study.studyProgressStatus === 3)) {
        return (
            <div className={"final-step"}>
                <h2>Completar Estudio</h2>
                <div className={"final-step-body"}>
                    <div className={"result-socioeconomic-save"}>
                        <button className={"form-button-primary save-step"}
                                onClick={submitToAdmin}>Subir a admin
                        </button>
                    </div>
                </div>
            </div>
        )
    }

    // If study is under admin and has not an analyst, send analyst
    if (study && study.studyProgressStatus === 2 && !study.analyst) {
        return (
            <div className={"final-step"}>
                <h2>Completar Estudio</h2>
                <div className={"final-step-body"}>
                    <h3>Analistas</h3>
                    <Select options={analystOptions}
                            selectedOption={analyst ? analyst.name : null}
                            onChange={handleAnalyst}
                            page={analystPage}
                            setPage={setAnalystPage}/>
                </div>
                <div className={"result-socioeconomic-save"}>
                    <button className={"form-button-primary save-step"} disabled={!analyst}
                            onClick={submitToAnalyst}>Asignar analista
                    </button>
                </div>
            </div>
        )
    }

    // If study is under admin and has an analyst, approve or reject
    if (study && study.studyProgressStatus === 2 && study.analyst) {
        return (
            <div className={"final-step"}>
                <h2>Completar Estudio</h2>
                <div className={"final-step-body"}>
                    <div className={"final-step-approve-reject"}>
                        <button className={"form-button-primary"}
                                onClick={approveStudy}>Autorizar
                        </button>
                        <button className={"form-button-primary reject"}
                                onClick={rejectStudy}>Rechazar
                        </button>
                    </div>
                </div>
            </div>
        )
    }
}

export default FinalStep