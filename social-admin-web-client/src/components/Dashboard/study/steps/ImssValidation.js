import React, { useEffect, useState } from "react";
import { useSelector } from "react-redux";

// Services
import StudyService from "../../../../services/StudyService";
import IMSSValidationService from "../../../../services/IMSSValidationService";
import AuthService from "../../../../services/AuthService";

const ImssValidation = () => {
    // Form data
    const disableForm = AuthService.getIdentity().role === 4;
    const [validations, setValidations] = useState([
        { companyBusinessName: "", startDate: "", endDate: "", result: "" }
    ])
    const [infonavit, setInfonavit] = useState({
        creditNumber: "", creditStatus: "", grantDate: ""
    })
    const [legalValidation, setLegalValidation] = useState("")

    // Get Study id from Redux
    const studyId = useSelector(state => state.study)
    // Add study to state
    const [study, setStudy] = useState({})

    // Date helper
    const pad = (number) => {
        if (number < 10) {
            return '0' + number
        }
        return number
    }

    // Make API request to Study Service and search for the study
    const getStudy = async () => {
        let response = await StudyService.getStudy(studyId)
        let study = response.data.response[0]
        setStudy(study)

        // If there is a studyIMSSValidation, pass the values to the form data
        if (study.studyIMSSValidation) {
            // Load validations
            if (study.studyIMSSValidation.imssValidationList && study.studyIMSSValidation.imssValidationList.length > 0) {
                let temp = [...study.studyIMSSValidation.imssValidationList].map(validation => {
                    let tempValidation = { ...validation }
                    let date = new Date(tempValidation.startDate)
                    tempValidation.startDate = date.getFullYear() + "-" + pad(date.getMonth() + 1) + "-" + pad(date.getDate())

                    date = new Date(tempValidation.endDate)
                    tempValidation.endDate = date.getFullYear() + "-" + pad(date.getMonth() + 1) + "-" + pad(date.getDate())

                    return tempValidation
                })
                setValidations(temp)
            }

            // Fill infonavit data
            let date = new Date(study.studyIMSSValidation.grantDate)
            let tempInfonavit = {
                creditNumber: study.studyIMSSValidation.creditNumber,
                creditStatus: study.studyIMSSValidation.creditStatus,
                grantDate: date.getFullYear() + "-" + pad(date.getMonth() + 1) + "-" + pad(date.getDate())
            }
            setInfonavit(tempInfonavit)

            // Legal claims
            setLegalValidation(study.studyIMSSValidation.conciliationClaimsSummary)
        }
    }

    // Get Study
    useEffect(() => {
        if (studyId) {
            getStudy()
        }
    }, [studyId])

    // Submit data
    const submit = async () => {
        // If IMSS Validation exists update
        if (study.studyIMSSValidation) {
            try {
                // Create IMSS Validation object
                let IMSSValidation = {
                    id: study.studyIMSSValidation.id,
                    studyId: study.id,
                    creditNumber: infonavit.creditNumber,
                    creditStatus: infonavit.creditStatus,
                    grantDate: infonavit.grantDate,
                    conciliationClaimsSummary: legalValidation
                }

                // Update core IMSS validation object
                await IMSSValidationService.update(IMSSValidation)

                // Update existing validations
                let existingValidations = validations.filter(validation => validation.studyIMSSValidationId)
                for (const validation of existingValidations) {
                    await IMSSValidationService.updateValidation(validation)
                }

                // Submit new validations
                let newValidations = validations.filter(validation => !validation.studyIMSSValidationId).map(validation => {
                    let temp = { ...validation }
                    temp.studyIMSSValidationId = study.studyIMSSValidation.id
                    return temp
                })

                if (newValidations.length > 0) {
                    await IMSSValidationService.createValidation(newValidations)
                }

                // Refresh data
                getStudy()
            } catch (error) {
                console.log(error)
            }
        } else {
            try {
                // Create IMSS Validation object
                let IMSSValidation = {
                    studyId: study.id,
                    creditNumber: infonavit.creditNumber,
                    creditStatus: infonavit.creditStatus,
                    grantDate: infonavit.grantDate,
                    conciliationClaimsSummary: legalValidation,
                    imssValidationList: validations
                }

                // If validations are empty assign null
                if (validations.length === 1 && validations[0].startDate === "" && validations[0].endDate === "") {
                    IMSSValidation.imssValidationList = null
                }

                await IMSSValidationService.create(IMSSValidation)

                // Refresh data
                getStudy()
            } catch (error) {
                console.log(error)
            }
        }
    }

    const addValidation = () => {
        let validation = { companyBusinessName: "", startDate: "", endDate: "", result: "" }
        setValidations([...validations, validation])
    }

    const removeValidation = async (index, input) => {
        try {
            if (input.id) {
                await IMSSValidationService.deleteValidation(input.id)
            }

            let temp = [...validations]
            temp.splice(index, 1)
            setValidations(temp)
        } catch (error) {
            console.log(error)
        }
    }

    const handleValidation = (index, event) => {
        let temp = [...validations]
        temp[index][event.target.name] = event.target.value
        setValidations(temp)
    }

    const handleInfonavit = (event) => {
        let temp = { ...infonavit }
        temp[event.target.name] = event.target.value
        setInfonavit(temp)
    }

    return (
        <div className={"imss-validation"}>
            <h2>Validación IMSS</h2>
            <div className={"imss-validation-section"}>
                <h3>Validación IMSS</h3>
                <div className={"imss-validation-grid"}>
                    <div className={"imss-validation-header"}>
                        <label>Razón Social</label>
                        <label>Fecha de ingreso</label>
                        <label>Fecha de baja</label>
                        <label>Resultado</label>
                    </div>
                    {
                        validations.map((input, index) => {
                            return (
                                <div key={`validations-${index}`} className={"imss-validation-item"}>
                                    <input type={"text"} name={"companyBusinessName"} placeholder={"Agregar..."}
                                        value={input.companyBusinessName}
                                        onChange={event => handleValidation(index, event)} disabled={disableForm} />
                                    <input type={"date"} name={"startDate"} placeholder={"Agregar..."}
                                        value={input.startDate} onChange={event => handleValidation(index, event)} disabled={disableForm} />
                                    <input type={"date"} name={"endDate"} placeholder={"Agregar..."}
                                        value={input.endDate} onChange={event => handleValidation(index, event)} disabled={disableForm} />
                                    <input type={"text"} name={"result"} placeholder={"Agregar..."} value={input.result}
                                        onChange={event => handleValidation(index, event)} disabled={disableForm} />
                                    {
                                        index !== 0 ?
                                            <button className={"remove-item-button"}
                                                onClick={() => removeValidation(index, input)} disabled={disableForm}>
                                                <img src={"/images/trash-icon.png"} alt={""} />
                                            </button> : null
                                    }
                                </div>
                            )
                        })
                    }
                    <div className={"imss-validation-footer"}>
                        <button className={"add-item-button"} onClick={addValidation} disabled={disableForm}>
                            <img src={"/images/add-icon.svg"} alt={""} />
                        </button>
                    </div>
                </div>
            </div>
            <div className={"infonavit"}>
                <h3>Validación Infonavit</h3>
                <div className={"infonavit-grid"}>
                    <div className={"infonavit-header"}>
                        <label>Número de crédito</label>
                        <label>Estatus</label>
                        <label>Fecha de otorgamiento</label>
                    </div>
                    <div className={"infonavit-item"}>
                        <input type={"text"} name={"creditNumber"} placeholder={"Agregar..."}
                            value={infonavit.creditNumber} onChange={event => handleInfonavit(event)} disabled={disableForm} />
                        <input type={"text"} name={"creditStatus"} placeholder={"Agregar..."}
                            value={infonavit.creditStatus}
                            onChange={event => handleInfonavit(event)} disabled={disableForm} />
                        <input type={"date"} name={"grantDate"} placeholder={"Agregar..."} value={infonavit.grantDate}
                            onChange={event => handleInfonavit(event)} disabled={disableForm} />
                    </div>
                </div>
            </div>
            <div className={"legal-validation"}>
                <h3>Verificación por demandas ante la junta de conciliación y arbitraje</h3>
                <textarea placeholder={"Agregar comentario"} value={legalValidation}
                    onChange={event => setLegalValidation(event.target.value)} disabled={disableForm} />
            </div>
            <div className={"result-socioeconomic-save"}>
                <button className={"form-button-primary save-step"} onClick={submit} disabled={disableForm}>Guardar</button>
            </div>
            v
        </div>
    )
}

export default ImssValidation