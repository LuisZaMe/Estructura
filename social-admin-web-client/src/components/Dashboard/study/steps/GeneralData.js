import React, {useEffect, useState} from "react";
import InputFileImage from "../../common/InputFileImage";
import {useSelector} from "react-redux";
import StudyService from "../../../../services/StudyService";
import RecommendationLetterService from "../../../../services/RecommendationLetterService";
import LocationService from "../../../../services/LocationService";
import StudyGeneralInformationService from "../../../../services/StudyGeneralInformationService";
import AuthService from "../../../../services/AuthService";

const GeneralData = () => {
    const disableForm = AuthService.getIdentity().role === 4;
    const [data, setData] = useState({
        "name": "",
        "email": "",
        "timeOnComany": "",
        "employeeNumber": "",
        "bornCity": {
            "id": 0
        },
        "bornState": {
            "id": 0
        },
        "countryName": "",
        "bornDate": "",
        "age": "",
        "maritalStatus": 0,
        "taxRegime": "",
        "address": "",
        "postalCode": "",
        "suburb": "",
        "homePhone": "",
        "city": {
            "id": 0
        },
        "state": {
            "id": 0
        },
        "mobilPhone": "",
        "idCardOriginal": false,
        "idCardCopy": false,
        "idCardRecord": "",
        "idCardExpeditionPlace": "",
        "idCardObservations": "",
        "addressProofOriginal": false,
        "addressProofCopy": false,
        "addressProofRecord": "",
        "addressProofExpeditionPlace": "",
        "addressProofObservations": "",
        "birthCertificateOriginal": false,
        "birthCertificateCopy": false,
        "birthCertificateRecord": "",
        "birthCertificateExpeditionPlace": "",
        "birthCertificateObservations": "",
        "curpOriginal": false,
        "curpCopy": false,
        "curpRecord": "",
        "curpExpeditionPlace": "",
        "curpObservations": "",
        "studyProofOriginal": false,
        "studyProofCopy": false,
        "studyProofRecord": "",
        "studyProofExpeditionPlace": "",
        "studyProofObservations": "",
        "socialSecurityProofOriginal": false,
        "socialSecurityProofCopy": false,
        "socialSecurityProofRecord": "",
        "socialSecurityProofExpeditionPlace": "",
        "socialSecurityProofObservations": "",
        "militaryLetterOriginal": false,
        "militaryLetterCopy": false,
        "militaryLetterRecord": "",
        "militaryLetterExpeditionPlace": "",
        "militaryLetterObservations": "",
        "rfcOriginal": false,
        "rfcCopy": false,
        "rfcRecord": "",
        "rfcExpeditionPlace": "",
        "rfcObservations": "",
        "criminalRecordLetterOriginal": false,
        "criminalRecordLetterCopy": false,
        "criminalRecordLetterRecord": "",
        "criminalRecordLetterExpeditionPlace": "",
        "criminalRecordLetterObservations": "",
        "ineFrontMedia": {
            "base64Image": ""
        },
        "ineBackMedia": {
            "base64Image": ""
        },
        "addressProofMedia": {
            "base64Image": ""
        },
        "bornCertificateMedia": {
            "base64Image": ""
        },
        "curpMedia": {
            "base64Image": "string"
        },
        "studiesProofMedia": {
            "base64Image": "string"
        },
        "socialSecurityProofMedia": {
            "base64Image": "string"
        },
        "militaryLetterMedia": {
            "base64Image": "string"
        },
        "rfcMedia": {
            "base64Image": "string"
        },
        "criminalRecordMedia": {
            "base64Image": "string"
        },
        "presentedIdentificationMedia": {
            "base64Image": "string"
        },
        "verificationMedia": {
            "base64Image": "string"
        }
    })
    const [recommendationLetters, setRecommendationLetters] = useState([
        {issueCompany: "", workingFrom: "", workingTo: "", timeWorking: ""}
    ])
    const [state, setState] = useState(0)
    const [states, setStates] = useState([])
    const [bornState, setBornState] = useState()
    const [cities, setCities] = useState([])
    const [bornCities, setBornCities] = useState([])

    // Get Study id from Redux
    const studyId = useSelector(state => state.study)
    // Add study to state
    const [study, setStudy] = useState({})

    const [isLoading, setIsLoading] = useState(true)

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

        // If there is a studyGeneralInformation, pass the values to the form data
        if (study.studyGeneralInformation) {
            let temp = {...study.studyGeneralInformation}

            // Parse Born Date
            let date = new Date(temp.bornDate)
            temp.bornDate = date.getFullYear() + "-" + pad(date.getMonth() + 1) + "-" + pad(date.getDate())

            // Set State and Born State
            setState(temp.state.id)
            setBornState(temp.bornState.id)

            setData(temp)

            // Load recommendationCards (letters)
            if (study.studyGeneralInformation.recommendationCards && study.studyGeneralInformation.recommendationCards.length > 0) {
                let tempLetters = [...study.studyGeneralInformation.recommendationCards].map(letter => {
                    let temp = {...letter}

                    let date1 = new Date(temp.workingFrom)
                    temp.workingFrom = date1.getFullYear() + "-" + pad(date1.getMonth() + 1) + "-" + pad(date1.getDate())

                    let date2 = new Date(temp.workingTo)
                    temp.workingTo = date2.getFullYear() + "-" + pad(date2.getMonth() + 1) + "-" + pad(date2.getDate())

                    return temp
                })
                setRecommendationLetters(tempLetters)
            }
        }
    }

    // Get Study
    useEffect(() => {
        if (studyId) {
            getStudy()
        }
    }, [studyId])

    // Request states
    useEffect(() => {
        const getStates = async () => {
            try {
                const response = await LocationService.getStates()
                setStates(response.data.response)
            } catch (error) {
                console.log(error)
            }
        }
        getStates()
    }, [])

    // Request cities
    useEffect(() => {
        const getCities = async () => {
            try {
                const response = await LocationService.getCities(state)
                setCities(response.data.response)
            } catch (error) {
                console.log(error)
            }
        }
        if (state) {
            getCities()
        }
    }, [state])

    // Request born cities
    useEffect(() => {
        const getCities = async () => {
            try {
                const response = await LocationService.getCities(bornState)
                setBornCities(response.data.response)
            } catch (error) {
                console.log(error)
            }
        }
        if (bornState) {
            getCities()
        }
    }, [bornState])

    // Submit data
    const submit = async () => {
        if (study.studyGeneralInformation) {
            // Update
            try {
                await StudyGeneralInformationService.update(data)

                // Update existing letters
                let existingLetters = recommendationLetters.filter(letter => letter.studyGeneralInformationId)
                for (const letter of existingLetters) {
                    await RecommendationLetterService.update(letter)
                }

                // Submit new letters
                let newLetters = recommendationLetters.filter(letter => !letter.studyGeneralInformationId).map(letter => {
                    let temp = {...letter}
                    temp.studyGeneralInformationId = study.studyGeneralInformation.id
                    return temp
                })
                if (newLetters.length > 0) {
                    await RecommendationLetterService.create(newLetters)
                }

                // Refresh data
                getStudy()
            } catch (error) {
                console.log(error)
            }
        } else {
            // Create
            try {
                let tempData = {...data}
                tempData.studyId = study.id
                tempData.recommendationCards = recommendationLetters

                // If recommendation letters was not filled (not required) don't include it in the POST object
                if (recommendationLetters.length === 1 && recommendationLetters[0].workingFrom === "" && recommendationLetters[0].workingTo) {
                    tempData.recommendationCards = null
                }

                await StudyGeneralInformationService.create(tempData)

                // Refresh data
                getStudy()
            } catch (error) {
                console.log(error)
            }
        }
    }

    const handleRecommendationLetter = (index, event) => {
        let data = [...recommendationLetters]
        data[index][event.target.name] = event.target.value

        // Calculate time worked
        try {
            const date1 = new Date(data[index]["workingFrom"])
            const date2 = new Date(data[index]["workingTo"])

            const diff = new Date(date2.getTime() - date1.getTime())
            if (diff > 0) {
                const years = Math.abs(diff.getFullYear() - 1970)
                const months = diff.getMonth()
                const days = diff.getDate()
                data[index]["timeWorking"] = (years > 0 ? years + " año(s)" : "") +
                    (months > 0 ? " " + months + " mes(es)" : "") +
                    (days > 0 ? " " + days + " día(s)" : "")
            } else {
                data[index]["timeWorking"] = ""
            }
        } catch (error) {
            data[index]["timeWorking"] = ""
        }

        setRecommendationLetters(data)
    }

    const addRecommendationLetter = () => {
        let newLetter = {issueCompany: "", workingFrom: "", workingTo: "", timeWorking: ""}
        setRecommendationLetters([...recommendationLetters, newLetter])
    }

    const removeRecommendationLetter = async (index, input) => {
        try {
            if (input.id) {
                await RecommendationLetterService.delete(input.id)
            }

            let data = [...recommendationLetters]
            data.splice(index, 1)
            setRecommendationLetters(data)
        } catch (error) {
            console.log(error)
        }
    }

    const handleData = (event) => {
        let temp = {...data}

        switch (event.target.name) {
            case "bornCity":
            case "bornState":
            case "city":
            case "state":
                temp[event.target.name].id = parseInt(event.target.value)
                if (event.target.name === "bornState") {
                    setBornState(event.target.value)
                }
                if (event.target.name === "state") {
                    setState(event.target.value)
                }
                break

            case "maritalStatus":
                temp[event.target.name] = parseInt(event.target.value)
                break

            case "idCardOriginal":
            case "idCardCopy":
            case "addressProofOriginal":
            case "addressProofCopy":
            case "birthCertificateOriginal":
            case "birthCertificateCopy":
            case "curpOriginal":
            case "curpCopy":
            case "studyProofOriginal":
            case "studyProofCopy":
            case "socialSecurityProofOriginal":
            case "socialSecurityProofCopy":
            case "militaryLetterOriginal":
            case "militaryLetterCopy":
            case "rfcOriginal":
            case "rfcCopy":
            case "criminalRecordLetterOriginal":
            case "criminalRecordLetterCopy":
                temp[event.target.name] = event.target.checked
                break

            default:
                temp[event.target.name] = event.target.value
                break
        }

        setData(temp)
    }

    const handleImage = (file, type) => {
        let temp = {...data}
        if (file) {
            let reader = new FileReader()
            reader.readAsDataURL(file)

            reader.onload = (e) => {
                temp[type] = {
                    base64Image: e.target.result.replace(e.target.result.split(",")[0] + ",", "")
                }
                setData(temp)
            }
        } else {
            temp[type] = {
                base64Image: null
            }
            setData(temp)
        }
    }

    const renderCities = cities.map(city => {
        return (
            <option key={`city-${city.id}`} value={city.id}>{city.name}</option>
        )
    })

    const renderStates = states.map(state => {
        return (
            <option key={`state-${state.id}`} value={state.id}>{state.name}</option>
        )
    })

    const renderBornCities = bornCities.map(city => {
        return (
            <option key={`bornCity-${city.id}`} value={city.id}>{city.name}</option>
        )
    })

    const renderBornStates = states.map(state => {
        return (
            <option key={`bornState-${state.id}`} value={state.id}>{state.name}</option>
        )
    })

    return (
        <div className={"general-data"}>
            <div className={"general-data-section"}>
                <h2>Datos generales</h2>
                <div className={"general-data-item"}>
                    <label>Nombre</label>
                    <input type={"text"} name={"name"} placeholder={"Agregar"} value={data.name}
                           onChange={event => handleData(event)} disabled={disableForm}/>
                </div>
                <div className={"general-data-item"}>
                    <label>Correo</label>
                    <input type={"email"} name={"email"} placeholder={"Agregar"} value={data.email}
                           onChange={event => handleData(event)} disabled={disableForm}/>
                </div>
                <div className={"general-data-item"}>
                    <label>Antigüedad en la institución</label>
                    <input type={"text"} name={"timeOnComany"} placeholder={"Agregar"} value={data.timeOnComany}
                           onChange={event => handleData(event)} disabled={disableForm}/>
                </div>
                <div className={"general-data-item"}>
                    <label>Número de empleado</label>
                    <input type={"text"} name={"employeeNumber"} placeholder={"Agregar"} value={data.employeeNumber}
                           onChange={event => handleData(event)} disabled={disableForm}/>
                </div>
                <div className={"general-data-item"}>
                    <label>Lugar de Nacimiento</label>
                    <div className={"general-data-item-double"}>
                        <select name={"bornState"} value={data.bornState.id} onChange={event => handleData(event)}
                                disabled={disableForm}>
                            <option value={0}>Seleccionar</option>
                            {renderBornStates}
                        </select>
                        <select name={"bornCity"} value={data.bornCity.id} onChange={event => handleData(event)}
                                disabled={disableForm}>
                            <option value={0}>Seleccionar</option>
                            {renderBornCities}
                        </select>
                    </div>
                </div>
                <div className={"general-data-item"}>
                    <label>Nacionalidad</label>
                    <input type={"text"} name={"countryName"} placeholder={"Agregar"} value={data.countryName}
                           onChange={event => handleData(event)} disabled={disableForm}/>
                </div>
                <div className={"general-data-item"}>
                    <label>Fecha de nacimiento</label>
                    <input type={"date"} name={"bornDate"} value={data.bornDate} onChange={event => handleData(event)}
                           disabled={disableForm}/>
                </div>
                <div className={"general-data-item"}>
                    <label>Edad</label>
                    <input type={"text"} name={"age"} placeholder={"Agregar"} value={data.age}
                           onChange={event => handleData(event)} disabled={disableForm}/>
                </div>
                <div className={"general-data-item"}>
                    <label>Estado Civil</label>
                    <select name={"maritalStatus"} value={data.maritalStatus} onChange={event => handleData(event)}
                            disabled={disableForm}>
                        <option value={0}>Seleccionar</option>
                        <option value={1}>Soltero(a)</option>
                        <option value={2}>Casado(a)</option>
                        <option value={3}>Divorciado(a)</option>
                        <option value={4}>Separación en proceso judicial</option>
                        <option value={5}>Viudo(a)</option>
                        <option value={6}>Concubinato</option>
                    </select>
                </div>
                <div className={"general-data-item"}>
                    <label>Régimen</label>
                    <input type={"text"} name={"taxRegime"} placeholder={"Agregar"} value={data.taxRegime}
                           onChange={event => handleData(event)} disabled={disableForm}/>
                </div>
                <div className={"general-data-item"}>
                    <label>Domicilio</label>
                    <input type={"text"} name={"address"} placeholder={"Agregar"} value={data.address}
                           onChange={event => handleData(event)} disabled={disableForm}/>
                </div>
                <div className={"general-data-item"}>
                    <label>Código Postal</label>
                    <input type={"type"} name={"postalCode"} placeholder={"Agregar"} value={data.postalCode}
                           onChange={event => handleData(event)} disabled={disableForm}/>
                </div>
                <div className={"general-data-item"}>
                    <label>Colonia</label>
                    <input type={"text"} name={"suburb"} placeholder={"Agregar"} value={data.suburb}
                           onChange={event => handleData(event)} disabled={disableForm}/>
                </div>
                <div className={"general-data-item"}>
                    <label>Tel. Casa/Recados</label>
                    <input type={"tel"} name={"homePhone"} placeholder={"Agregar"} value={data.homePhone}
                           onChange={event => handleData(event)} disabled={disableForm}/>
                </div>
                <div className={"general-data-item"}>
                    <label>Estado y Municipio</label>
                    <div className={"general-data-item-double"}>
                        <select name={"state"} value={data.state.id} onChange={event => handleData(event)}
                                disabled={disableForm}>
                            <option value={0}>Seleccionar</option>
                            {renderStates}
                        </select>
                        <select name={"city"} value={data.city.id} onChange={event => handleData(event)}
                                disabled={disableForm}>
                            <option value={0}>Seleccionar</option>
                            {renderCities}
                        </select>
                    </div>
                </div>
                <div className={"general-data-item"}>
                    <label>Teléfono Celular</label>
                    <input type={"tel"} name={"mobilPhone"} placeholder={"Agregar"} value={data.mobilPhone}
                           onChange={event => handleData(event)} disabled={disableForm}/>
                </div>
            </div>

            <div className={"official-documents"}>
                <h2>Documentación oficial</h2>
                <div className={"official-documents-fields"}>
                    <div className={"official-documents-fields-header"}>
                        <label className={"official-documents-column-document"}>Documento</label>
                        <label className={"official-documents-column-original"}>Original</label>
                        <label className={"official-documents-column-copy"}>Copia</label>
                        <label className={"official-documents-column-doc-number"}>No. de acta o folio</label>
                        <label className={"official-documents-column-issue-place"}>Lugar de expedición</label>
                        <label className={"official-documents-column-comments"}>Observaciones</label>
                    </div>
                    <div className={"official-documents-fields-row"}>
                        <span>Credencial INE</span>
                        <input type={"checkbox"} name={"idCardOriginal"} value={data.idCardOriginal.value}
                               onChange={event => handleData(event)} checked={data.idCardOriginal}
                               disabled={disableForm}/>
                        <input type={"checkbox"} name={"idCardCopy"} value={data.idCardCopy.value}
                               onChange={event => handleData(event)} checked={data.idCardCopy} disabled={disableForm}/>
                        <input type={"text"} name={"idCardRecord"} placeholder={"Agregar"} value={data.idCardRecord}
                               onChange={event => handleData(event)} disabled={disableForm}/>
                        <input type={"text"} name={"idCardExpeditionPlace"} placeholder={"Agregar"}
                               value={data.idCardExpeditionPlace} onChange={event => handleData(event)}
                               disabled={disableForm}/>
                        <input type={"text"} name={"idCardObservations"} placeholder={"Agregar"}
                               value={data.idCardObservations} onChange={event => handleData(event)}
                               disabled={disableForm}/>
                    </div>
                    <div className={"official-documents-fields-row"}>
                        <span>Comprobante de domicilio</span>
                        <input type={"checkbox"} name={"addressProofOriginal"} value={data.addressProofOriginal.value}
                               onChange={event => handleData(event)} checked={data.addressProofOriginal}
                               disabled={disableForm}/>
                        <input type={"checkbox"} name={"addressProofCopy"} value={data.addressProofCopy.value}
                               onChange={event => handleData(event)} checked={data.addressProofCopy}
                               disabled={disableForm}/>
                        <input type={"text"} name={"addressProofRecord"} placeholder={"Agregar"}
                               value={data.addressProofRecord} onChange={event => handleData(event)}
                               disabled={disableForm}/>
                        <input type={"text"} name={"addressProofExpeditionPlace"} placeholder={"Agregar"}
                               value={data.addressProofExpeditionPlace} onChange={event => handleData(event)}
                               disabled={disableForm}/>
                        <input type={"text"} name={"addressProofObservations"} placeholder={"Agregar"}
                               value={data.addressProofObservations} onChange={event => handleData(event)}
                               disabled={disableForm}/>
                    </div>
                    <div className={"official-documents-fields-row"}>
                        <span>Acta de nacimiento</span>
                        <input type={"checkbox"} name={"birthCertificateOriginal"}
                               value={data.birthCertificateOriginal.value} onChange={event => handleData(event)}
                               checked={data.birthCertificateOriginal} disabled={disableForm}/>
                        <input type={"checkbox"} name={"birthCertificateCopy"} value={data.birthCertificateCopy.value}
                               onChange={event => handleData(event)} checked={data.birthCertificateCopy}
                               disabled={disableForm}/>
                        <input type={"text"} name={"birthCertificateRecord"} placeholder={"Agregar"}
                               value={data.birthCertificateRecord} onChange={event => handleData(event)}
                               disabled={disableForm}/>
                        <input type={"text"} name={"birthCertificateExpeditionPlace"} placeholder={"Agregar"}
                               value={data.birthCertificateExpeditionPlace} onChange={event => handleData(event)}
                               disabled={disableForm}/>
                        <input type={"text"} name={"birthCertificateObservations"} placeholder={"Agregar"}
                               value={data.birthCertificateObservations} onChange={event => handleData(event)}
                               disabled={disableForm}/>
                    </div>
                    <div className={"official-documents-fields-row"}>
                        <span>CURP</span>
                        <input type={"checkbox"} name={"curpOriginal"} value={data.curpOriginal.value}
                               onChange={event => handleData(event)} checked={data.curpOriginal}
                               disabled={disableForm}/>
                        <input type={"checkbox"} name={"curpCopy"} value={data.curpCopy.value}
                               onChange={event => handleData(event)} checked={data.curpCopy} disabled={disableForm}/>
                        <input type={"text"} name={"curpRecord"} placeholder={"Agregar"} value={data.curpRecord}
                               onChange={event => handleData(event)} disabled={disableForm}/>
                        <input type={"text"} name={"curpExpeditionPlace"} placeholder={"Agregar"}
                               value={data.curpExpeditionPlace} onChange={event => handleData(event)}
                               disabled={disableForm}/>
                        <input type={"text"} name={"curpObservations"} placeholder={"Agregar"}
                               value={data.curpObservations}
                               onChange={event => handleData(event)} disabled={disableForm}/>
                    </div>
                    <div className={"official-documents-fields-row"}>
                        <span>Comprobante de estudios</span>
                        <input type={"checkbox"} name={"studyProofOriginal"} value={data.studyProofOriginal.value}
                               onChange={event => handleData(event)} checked={data.studyProofOriginal}
                               disabled={disableForm}/>
                        <input type={"checkbox"} name={"studyProofCopy"} value={data.studyProofCopy.value}
                               onChange={event => handleData(event)} checked={data.studyProofCopy}
                               disabled={disableForm}/>
                        <input type={"text"} name={"studyProofRecord"} placeholder={"Agregar"}
                               value={data.studyProofRecord}
                               onChange={event => handleData(event)} disabled={disableForm}/>
                        <input type={"text"} name={"studyProofExpeditionPlace"} placeholder={"Agregar"}
                               value={data.studyProofExpeditionPlace} onChange={event => handleData(event)}
                               disabled={disableForm}/>
                        <input type={"text"} name={"studyProofObservations"} placeholder={"Agregar"}
                               value={data.studyProofObservations} onChange={event => handleData(event)}
                               disabled={disableForm}/>
                    </div>
                    <div className={"official-documents-fields-row"}>
                        <span>Número de IMSS</span>
                        <input type={"checkbox"} name={"socialSecurityProofOriginal"}
                               value={data.socialSecurityProofOriginal.value} onChange={event => handleData(event)}
                               checked={data.socialSecurityProofOriginal} disabled={disableForm}/>
                        <input type={"checkbox"} name={"socialSecurityProofCopy"}
                               value={data.socialSecurityProofCopy.value} onChange={event => handleData(event)}
                               checked={data.socialSecurityProofCopy} disabled={disableForm}/>
                        <input type={"text"} name={"socialSecurityProofRecord"} placeholder={"Agregar"}
                               value={data.socialSecurityProofRecord} onChange={event => handleData(event)}
                               disabled={disableForm}/>
                        <input type={"text"} name={"socialSecurityProofExpeditionPlace"} placeholder={"Agregar"}
                               value={data.socialSecurityProofExpeditionPlace} onChange={event => handleData(event)}
                               disabled={disableForm}/>
                        <input type={"text"} name={"socialSecurityProofObservations"} placeholder={"Agregar"}
                               value={data.socialSecurityProofObservations} onChange={event => handleData(event)}
                               disabled={disableForm}/>
                    </div>
                    <div className={"official-documents-fields-row"}>
                        <span>Cartilla Militar</span>
                        <input type={"checkbox"} name={"militaryLetterOriginal"}
                               value={data.militaryLetterOriginal.value} onChange={event => handleData(event)}
                               checked={data.militaryLetterOriginal} disabled={disableForm}/>
                        <input type={"checkbox"} name={"militaryLetterCopy"} value={data.militaryLetterCopy.value}
                               onChange={event => handleData(event)} checked={data.militaryLetterCopy}
                               disabled={disableForm}/>
                        <input type={"text"} name={"militaryLetterRecord"} placeholder={"Agregar"}
                               value={data.militaryLetterRecord} onChange={event => handleData(event)}
                               disabled={disableForm}/>
                        <input type={"text"} name={"militaryLetterExpeditionPlace"} placeholder={"Agregar"}
                               value={data.militaryLetterExpeditionPlace} onChange={event => handleData(event)}
                               disabled={disableForm}/>
                        <input type={"text"} name={"militaryLetterObservations"} placeholder={"Agregar"}
                               value={data.militaryLetterObservations} onChange={event => handleData(event)}
                               disabled={disableForm}/>
                    </div>
                    <div className={"official-documents-fields-row"}>
                        <span>RFC</span>
                        <input type={"checkbox"} name={"rfcOriginal"} value={data.rfcOriginal.value}
                               onChange={event => handleData(event)} checked={data.rfcOriginal} disabled={disableForm}/>
                        <input type={"checkbox"} name={"rfcCopy"} value={data.rfcCopy.value}
                               onChange={event => handleData(event)} checked={data.rfcCopy} disabled={disableForm}/>
                        <input type={"text"} name={"rfcRecord"} placeholder={"Agregar"} value={data.rfcRecord}
                               onChange={event => handleData(event)} disabled={disableForm}/>
                        <input type={"text"} name={"rfcExpeditionPlace"} placeholder={"Agregar"}
                               value={data.rfcExpeditionPlace} onChange={event => handleData(event)}
                               disabled={disableForm}/>
                        <input type={"text"} name={"rfcObservations"} placeholder={"Agregar"}
                               value={data.rfcObservations}
                               onChange={event => handleData(event)} disabled={disableForm}/>
                    </div>
                    <div className={"official-documents-fields-row"}>
                        <span>Carta de Antecedentes Penales</span>
                        <input type={"checkbox"} name={"criminalRecordLetterOriginal"}
                               value={data.criminalRecordLetterOriginal.value} onChange={event => handleData(event)}
                               checked={data.criminalRecordLetterOriginal} disabled={disableForm}/>
                        <input type={"checkbox"} name={"criminalRecordLetterCopy"}
                               value={data.criminalRecordLetterCopy.value} onChange={event => handleData(event)}
                               checked={data.criminalRecordLetterCopy} disabled={disableForm}/>
                        <input type={"text"} name={"criminalRecordLetterRecord"} placeholder={"Agregar"}
                               value={data.criminalRecordLetterRecord} onChange={event => handleData(event)}
                               disabled={disableForm}/>
                        <input type={"text"} name={"criminalRecordLetterExpeditionPlace"} placeholder={"Agregar"}
                               value={data.criminalRecordLetterExpeditionPlace} onChange={event => handleData(event)}
                               disabled={disableForm}/>
                        <input type={"text"} name={"criminalRecordLetterObservations"} placeholder={"Agregar"}
                               value={data.criminalRecordLetterObservations} onChange={event => handleData(event)}
                               disabled={disableForm}/>
                    </div>
                </div>
                <div className={"official-documents-images"}>
                    <div className={"official-document"}>
                        <label>INE Frente</label>
                        <InputFileImage
                            imageUrl={data.ineFrontMedia.mediaURL ? data.ineFrontMedia.mediaURL : ""}
                            setImageUrl={() => null}
                            setFile={(file) => handleImage(file, "ineFrontMedia")}
                        />
                    </div>
                    <div className={"official-document"}>
                        <label>INE Reverso</label>
                        <InputFileImage
                            imageUrl={data.ineBackMedia.mediaURL ? data.ineBackMedia.mediaURL : ""}
                            setImageUrl={() => null}
                            setFile={(file) => handleImage(file, "ineBackMedia")}
                        />
                    </div>
                    <div className={"official-document"}>
                        <label>Comprobante de domicilio</label>
                        <InputFileImage
                            imageUrl={data.addressProofMedia.mediaURL ? data.addressProofMedia.mediaURL : ""}
                            setImageUrl={() => null}
                            setFile={(file) => handleImage(file, "addressProofMedia")}
                        />
                    </div>
                    <div className={"official-document"}>
                        <label>Acta de nacimiento</label>
                        <InputFileImage
                            imageUrl={data.bornCertificateMedia.mediaURL ? data.bornCertificateMedia.mediaURL : ""}
                            setImageUrl={() => null}
                            setFile={(file) => handleImage(file, "bornCertificateMedia")}
                        />
                    </div>
                    <div className={"official-document"}>
                        <label>CURP</label>
                        <InputFileImage
                            imageUrl={data.curpMedia.mediaURL ? data.curpMedia.mediaURL : ""}
                            setImageUrl={() => null}
                            setFile={(file) => handleImage(file, "curpMedia")}
                        />
                    </div>
                    <div className={"official-document"}>
                        <label>Comprobante de estudios</label>
                        <InputFileImage
                            imageUrl={data.studiesProofMedia.mediaURL ? data.studiesProofMedia.mediaURL : ""}
                            setImageUrl={() => null}
                            setFile={(file) => handleImage(file, "studiesProofMedia")}
                        />
                    </div>
                    <div className={"official-document"}>
                        <label>Número de imss</label>
                        <InputFileImage
                            imageUrl={data.socialSecurityProofMedia.mediaURL ? data.socialSecurityProofMedia.mediaURL : ""}
                            setImageUrl={() => null}
                            setFile={(file) => handleImage(file, "socialSecurityProofMedia")}
                        />
                    </div>
                    <div className={"official-document"}>
                        <label>Cartilla militar</label>
                        <InputFileImage
                            imageUrl={data.militaryLetterMedia.mediaURL ? data.militaryLetterMedia.mediaURL : ""}
                            setImageUrl={() => null}
                            setFile={(file) => handleImage(file, "militaryLetterMedia")}
                        />
                    </div>
                    <div className={"official-document"}>
                        <label>RFC</label>
                        <InputFileImage
                            imageUrl={data.rfcMedia.mediaURL ? data.rfcMedia.mediaURL : ""}
                            setImageUrl={() => null}
                            setFile={(file) => handleImage(file, "rfcMedia")}
                        />
                    </div>
                    <div className={"official-document"}>
                        <label>Carta de antecedentes penales</label>
                        <InputFileImage
                            imageUrl={data.criminalRecordMedia.mediaURL ? data.criminalRecordMedia.mediaURL : ""}
                            setImageUrl={() => null}
                            setFile={(file) => handleImage(file, "criminalRecordMedia")}
                        />
                    </div>
                </div>
            </div>

            <div className={"recommendation-letter-section"}>
                <h2>Cartas de recomendación y/o constancias laborales</h2>
                <div className={"recommendation-letter-grid"}>
                    <div className={"recommendation-letter-grid-header"}>
                        <label>Empresa que expide</label>
                        <label>De</label>
                        <label>A</label>
                        <label>Años laborados</label>
                    </div>
                    <div className={"recommendation-letter-grid-body"}>
                        {recommendationLetters.map((input, index) => {
                            return (
                                <div key={index} className={"recommendation-letter-grid-row"}>
                                    <input type={"text"} name={"issueCompany"} value={input.issueCompany}
                                           onChange={event => handleRecommendationLetter(index, event)}
                                           disabled={disableForm}/>
                                    <input type={"date"} name={"workingFrom"} value={input.workingFrom}
                                           onChange={event => handleRecommendationLetter(index, event)}
                                           disabled={disableForm}/>
                                    <input type={"date"} name={"workingTo"} value={input.workingTo}
                                           onChange={event => handleRecommendationLetter(index, event)}
                                           disabled={disableForm}/>
                                    <label>{input.timeWorking}</label>
                                    {index !== 0 ?
                                        <button className={"remove-item-button"}
                                                onClick={() => removeRecommendationLetter(index, input)}
                                                disabled={disableForm}>
                                            <img src={"/images/trash-icon.png"} alt={""}/>
                                        </button> : null
                                    }
                                </div>
                            )
                        })}
                    </div>
                    <div className={"recommendation-letter-grid-footer"}>
                        <button className={"add-item-button"} onClick={addRecommendationLetter} disabled={disableForm}>
                            <img src={"/images/add-icon.svg"} alt={""}/>
                        </button>
                    </div>
                </div>
            </div>

            <div className={"ine-validation-section"}>
                <h2>Verificación identificación oficial ine</h2>
                <div className={"official-document"}>
                    <label>Identificación presentada</label>
                    <InputFileImage
                        imageUrl={data.presentedIdentificationMedia.mediaURL ? data.presentedIdentificationMedia.mediaURL : ""}
                        setImageUrl={() => null}
                        setFile={(file) => handleImage(file, "presentedIdentificationMedia")}
                    />
                </div>
                <div className={"official-document"}>
                    <label>Verificación</label>
                    <InputFileImage
                        imageUrl={data.verificationMedia.mediaURL ? data.verificationMedia.mediaURL : ""}
                        setImageUrl={() => null}
                        setFile={(file) => handleImage(file, "verificationMedia")}
                    />
                </div>
            </div>

            <div className={"result-socioeconomic-save"}>
                <button className={"form-button-primary save-step"} onClick={submit} disabled={disableForm}>Guardar
                </button>
                <button className={"next-step"} disabled={disableForm}>
                    <img src={"/images/next-arrow.png"} alt={""}/>
                </button>
            </div>
        </div>
    );
}

export default GeneralData