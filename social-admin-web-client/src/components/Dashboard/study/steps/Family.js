import React, { useEffect, useState } from "react";
import { useSelector } from "react-redux";
import StudyService from "../../../../services/StudyService";
import LivingFamilyService from "../../../../services/LivingFamilyService";
import NoLivingFamilyService from "../../../../services/NoLivingFamilyService";
import FamilyService from "../../../../services/FamilyService";
import AuthService from "../../../../services/AuthService";

const Family = () => {
    const disableForm = AuthService.getIdentity().role === 4;
    // Form data
    const [household, setHousehold] = useState([
        { name: "", relationship: "", age: "", maritalStatus: 0, schoolarity: "", address: "", phone: "", occupation: "" }
    ])
    const [relativesNotInHousehold, setRelativesNotInHousehold] = useState([
        { name: "", relationship: "", age: "", maritalStatus: 0, schoolarity: "", address: "", phone: "", occupation: "" }
    ])
    const [note, setNote] = useState("")
    const [familyArea, setFamilyArea] = useState("")

    // Get Study id from Redux
    const studyId = useSelector(state => state.study)
    // Add study to state
    const [study, setStudy] = useState({})

    // Make API request to Study Service and search for the study
    const getStudy = async () => {
        let response = await StudyService.getStudy(studyId)
        let study = response.data.response[0]

        setStudy(study)

        // If there is an studyFamily object, pass the values to the form data
        if (study.studyFamily) {
            // Load livingFamily
            if (study.studyFamily.livingFamilyList && study.studyFamily.livingFamilyList.length > 0) {
                setHousehold(study.studyFamily.livingFamilyList)
            }

            // Load noLivingFamilyList
            if (study.studyFamily.noLivingFamilyList && study.studyFamily.noLivingFamilyList.length > 0) {
                setRelativesNotInHousehold(study.studyFamily.noLivingFamilyList)
            }

            // Load notes
            setNote(study.studyFamily.notes)

            // Load familiarArea
            setFamilyArea(study.studyFamily.familiarArea)
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
        // If family study exists update
        if (study.studyFamily) {
            try {
                // Update study
                let family = {
                    id: study.studyFamily.id,
                    studyId: study.id,
                    notes: note,
                    familiarArea: familyArea
                }

                await FamilyService.update(family)

                // Submit new livingFamily
                let newLivingFamilyList = household.filter(household => !household.studyFamilyId).map(household => {
                    let temp = { ...household }
                    temp.studyFamilyId = study.studyFamily.id
                    return temp
                })
                if (newLivingFamilyList.length > 0) {
                    await LivingFamilyService.create(newLivingFamilyList)
                }

                // Update existing livingFamily
                let existingLivingFamilyList = household.filter(household => household.studyFamilyId)
                for (const livingFamily of existingLivingFamilyList) {
                    await LivingFamilyService.update(livingFamily)
                }

                // Submit new NoLivingFamily
                let newNoLivingFamilyList = relativesNotInHousehold.filter(relative => !relative.studyFamilyId).map(relative => {
                    let temp = { ...relative }
                    temp.studyFamilyId = study.studyFamily.id
                    return temp
                })
                if (newNoLivingFamilyList.length > 0) {
                    await NoLivingFamilyService.create(newNoLivingFamilyList)
                }

                // Update existing noLivingFamily
                let existingNoLivingFamilyList = relativesNotInHousehold.filter(relative => relative.studyFamilyId)
                for (const noLivingFamily of existingNoLivingFamilyList) {
                    await NoLivingFamilyService.update(noLivingFamily)
                }

                // Refresh data
                getStudy()
            } catch (error) {
                console.log(error)
            }
        } else {
            try {
                // Create new StudyFamily
                let family = {
                    studyId: study.id,
                    notes: note,
                    familiarArea: familyArea,
                    livingFamilyList: household,
                    noLivingFamilyList: relativesNotInHousehold
                }

                await FamilyService.create(family)

                // Refresh data
                getStudy()
            } catch (error) {
                console.log(error)
            }
        }
    }

    const handleHousehold = (index, event) => {
        let data = [...household]
        if (event.target.name !== "maritalStatus") {
            data[index][event.target.name] = event.target.value
        } else {
            data[index][event.target.name] = parseInt(event.target.value)
        }
        setHousehold(data)
    }

    const addHousehold = () => {
        let item = {
            name: "",
            relationship: "",
            age: "",
            maritalStatus: 0,
            schoolarity: "",
            address: "",
            phone: "",
            occupation: ""
        }
        setHousehold([...household, item])
    }

    const removeHousehold = async (index, input) => {
        try {
            if (input.id) {
                await LivingFamilyService.delete(input.id)
            }

            let data = [...household]
            data.splice(index, 1)
            setHousehold(data)
        } catch (error) {
            console.log(error)
        }
    }

    const handleRelativeNotInHousehold = (index, event) => {
        let data = [...relativesNotInHousehold]
        if (event.target.name !== "maritalStatus") {
            data[index][event.target.name] = event.target.value
        } else {
            data[index][event.target.name] = parseInt(event.target.value)
        }
        setRelativesNotInHousehold(data)
    }

    const addRelativeNotInHousehold = () => {
        let item = {
            name: "",
            relationship: "",
            age: "",
            maritalStatus: 0,
            schoolarity: "",
            address: "",
            phone: "",
            occupation: ""
        }
        setRelativesNotInHousehold([...relativesNotInHousehold, item])
    }

    const removeRelativeNotInHouseHold = async (index, input) => {
        try {
            if (input.id) {
                await NoLivingFamilyService.delete(input.id)
            }

            let data = [...relativesNotInHousehold]
            data.splice(index, 1)
            setRelativesNotInHousehold(data)
        } catch (error) {
            console.log(error)
        }
    }

    return (
        <div className={"family"}>
            <div className={"family-section"}>
                <h2>Familia conviviente</h2>
                <div className={"family-grid"}>
                    {household.map((input, index) => {
                        return (
                            <div key={`family-${index}`} className={"family-item"}>
                                <div className={"family-header-1"}>
                                    <label>Nombre</label>
                                    <label>Parentesco</label>
                                    <label>Edad</label>
                                    <label>Estado Civil</label>
                                    <label>Escolaridad</label>
                                </div>
                                <div className={"family-row-1"}>
                                    <input type={"text"} name={"name"} placeholder={"Agregar..."} value={input.name}
                                        onChange={event => handleHousehold(index, event)} disabled={disableForm} />
                                    <input type={"text"} name={"relationship"} placeholder={"Agregar..."}
                                        value={input.relationship}
                                        onChange={event => handleHousehold(index, event)} disabled={disableForm} />
                                    <input type={"text"} name={"age"} placeholder={"Agregar..."} value={input.age}
                                        onChange={event => handleHousehold(index, event)} disabled={disableForm} />
                                    <select name={"maritalStatus"} value={input.maritalStatus}
                                        onChange={event => handleHousehold(index, event)} disabled={disableForm}>
                                        <option value={0}>Seleccionar</option>
                                        <option value={1}>Soltero</option>
                                        <option value={2}>Casado</option>
                                        <option value={3}>Divorciado</option>
                                        <option value={4}>Separado en proceso judicial</option>
                                        <option value={5}>Viudo</option>
                                        <option value={6}>Concubitano</option>
                                    </select>
                                    <input type={"text"} name={"schoolarity"} placeholder={"Agregar..."}
                                        value={input.schoolarity} onChange={event => handleHousehold(index, event)} disabled={disableForm} />
                                </div>
                                <div className={"family-header-2"}>
                                    <label>Domicilio</label>
                                    <label>Teléfono</label>
                                    <label>Ocupación</label>
                                </div>
                                <div className={"family-row-2"}>
                                    <input type={"text"} name={"address"} placeholder={"Agregar..."}
                                        value={input.address} onChange={event => handleHousehold(index, event)} disabled={disableForm} />
                                    <input type={"tel"} name={"phone"} placeholder={"Agregar..."} value={input.phone}
                                        onChange={event => handleHousehold(index, event)} disabled={disableForm} />
                                    <input type={"text"} name={"occupation"} placeholder={"Agregar..."}
                                        value={input.occupation} onChange={event => handleHousehold(index, event)} disabled={disableForm} />
                                    {index !== 0 ?
                                        <button className={"remove-item-button"} onClick={() => removeHousehold(index, input)} disabled={disableForm}>
                                            <img src={"/images/trash-icon.png"} alt={""} />
                                        </button> : null
                                    }
                                </div>
                            </div>
                        )
                    })}
                    <div className={"family-grid-footer"}>
                        <button className={"add-item-button"} onClick={addHousehold} disabled={disableForm}>
                            <img src={"/images/add-icon.svg"} alt={""} />
                        </button>
                    </div>
                    <div className={"family-note"}>
                        <label>Nota:</label>
                        <textarea placeholder={"Agregar comentario..."} value={note}
                            onChange={event => setNote(event.target.value)} disabled={disableForm} />
                    </div>
                </div>
            </div>
            <div className={"family-section"}>
                <h2>Familia no conviviente</h2>
                <div className={"family-grid"}>
                    {
                        relativesNotInHousehold.map((input, index) => {
                            return (
                                <div key={`not-household-${index}`} className={"family-item"}>
                                    <div className={"family-header-1"}>
                                        <label>Nombre</label>
                                        <label>Parentesco</label>
                                        <label>Edad</label>
                                        <label>Estado Civil</label>
                                        <label>Escolaridad</label>
                                    </div>
                                    <div className={"family-row-1"}>
                                        <input type={"text"} name={"name"} placeholder={"Agregar..."} value={input.name}
                                            onChange={event => handleRelativeNotInHousehold(index, event)} disabled={disableForm} />
                                        <input type={"text"} name={"relationship"} placeholder={"Agregar..."}
                                            value={input.relationship}
                                            onChange={event => handleRelativeNotInHousehold(index, event)} disabled={disableForm} />
                                        <input type={"text"} name={"age"} placeholder={"Agregar..."} value={input.age}
                                            onChange={event => handleRelativeNotInHousehold(index, event)} disabled={disableForm} />
                                        <select name={"maritalStatus"} value={input.maritalStatus}
                                            onChange={event => handleRelativeNotInHousehold(index, event)} disabled={disableForm}>
                                            <option value={0}>Seleccionar</option>
                                            <option value={1}>Soltero</option>
                                            <option value={2}>Casado</option>
                                            <option value={3}>Divorciado</option>
                                            <option value={4}>Separado en proceso judicial</option>
                                            <option value={5}>Viudo</option>
                                            <option value={6}>Concubinato</option>
                                        </select>
                                        <input type={"text"} name={"schoolarity"} placeholder={"Agregar..."}
                                            value={input.schoolarity}
                                            onChange={event => handleRelativeNotInHousehold(index, event)} disabled={disableForm} />
                                    </div>
                                    <div className={"family-header-2"}>
                                        <label>Domicilio</label>
                                        <label>Teléfono</label>
                                        <label>Ocupación</label>
                                    </div>
                                    <div className={"family-row-2"}>
                                        <input type={"text"} name={"address"} placeholder={"Agregar..."}
                                            value={input.address}
                                            onChange={event => handleRelativeNotInHousehold(index, event)} disabled={disableForm} />
                                        <input type={"tel"} name={"phone"} placeholder={"Agregar..."}
                                            value={input.phone}
                                            onChange={event => handleRelativeNotInHousehold(index, event)} disabled={disableForm} />
                                        <input type={"text"} name={"occupation"} placeholder={"Agregar..."}
                                            value={input.occupation}
                                            onChange={event => handleRelativeNotInHousehold(index, event)} disabled={disableForm} />
                                        {index !== 0 ?
                                            <button className={"remove-item-button"}
                                                onClick={() => removeRelativeNotInHouseHold(index, input)} disabled={disableForm}>
                                                <img src={"/images/trash-icon.png"} alt={""} />
                                            </button> : null
                                        }
                                    </div>
                                </div>
                            )
                        })
                    }
                    <div className={"family-grid-footer"}>
                        <button className={"add-item-button"} onClick={addRelativeNotInHousehold} disabled={disableForm}>
                            <img src={"/images/add-icon.svg"} alt={""} />
                        </button>
                    </div>
                    <div className={"family-note"}>
                        <label>Area familiar:</label>
                        <textarea placeholder={"Agregar comentario..."} value={familyArea}
                            onChange={event => setFamilyArea(event.target.value)} disabled={disableForm} />
                    </div>
                </div>
            </div>
            <div className={"result-socioeconomic-save"}>
                <button className={"form-button-primary save-step"} onClick={submit} disabled={disableForm}>Guardar</button>
            </div>
        </div>
    )
}

export default Family