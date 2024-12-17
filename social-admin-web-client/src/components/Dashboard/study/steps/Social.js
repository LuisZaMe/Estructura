import React, { useEffect, useState } from "react";
import { useSelector } from "react-redux";
import StudyService from "../../../../services/StudyService";
import SocialGoalsService from "../../../../services/SocialGoalsService";
import SocialService from "../../../../services/SocialService";
import AuthService from "../../../../services/AuthService";

const Social = () => {
    // Form data
    const disableForm = AuthService.getIdentity().role === 4;
    const [socialGoals, setSocialGoals] = useState([
        { coreValue: "", lifeGoal: "", nextGoal: "" }
    ])
    const [socialArea, setSocialArea] = useState("")
    const [hobbies, setHobbies] = useState("")
    const [healthInformation, setHealthInformation] = useState("")

    // Get Study id from Redux
    const studyId = useSelector(state => state.study)
    // Add study to state
    const [study, setStudy] = useState({})

    // Make API request to Study Service and search for the study
    const getStudy = async () => {
        let response = await StudyService.getStudy(studyId)
        let study = response.data.response[0]

        setStudy(study)
        // If there is a StudySocial object, pass the values to the form data
        if (study.studySocial) {
            // Load goals
            if (study.studySocial.socialGoalsList && study.studySocial.socialGoalsList.length > 0) {
                setSocialGoals([...study.studySocial.socialGoalsList])
            }

            // Load social area
            setSocialArea(study.studySocial.socialArea)

            // Load hobbies
            setHobbies(study.studySocial.hobbies)

            // Load Health Information
            setHealthInformation(study.studySocial.healthInformation)
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
        // If social exists update
        if (study.studySocial) {
            try {
                let social = {
                    "id": study.studySocial.id,
                    "studyId": study.id,
                    "socialArea": socialArea,
                    "hobbies": hobbies,
                    "healthInformation": healthInformation,
                    "socialGoalsList": socialGoals
                }

                // Update social
                await SocialService.update(social)

                // Update existing gaols
                let existingGoals = socialGoals.filter(goal => goal.studySocialId)
                for (const goal of existingGoals) {
                    await SocialGoalsService.update(goal)
                }

                // Submit new goals
                let newGoals = socialGoals.filter(goal => !goal.studySocialId).map(goal => {
                    let temp = { ...goal }
                    temp.studySocialId = study.studySocial.id
                    return temp
                })
                if (newGoals.length > 0) {
                    await SocialGoalsService.create(newGoals)
                }

                // Refresh data
                getStudy()
            } catch (error) {
                console.log(error)
            }
        } else {
            try {
                let social = {
                    "studyId": study.id,
                    "socialArea": socialArea,
                    "hobbies": hobbies,
                    "healthInformation": healthInformation,
                    "socialGoalsList": socialGoals
                }
                await SocialService.create(social)

                // Refresh data
                getStudy()
            } catch (error) {
                console.log(error)
            }
        }
    }

    const handleValue = (index, event) => {
        let data = [...socialGoals]
        data[index][event.target.name] = event.target.value
        setSocialGoals(data)
    }

    const addValue = () => {
        let value = { coreValue: "", lifeGoal: "", nextGoal: "" }
        setSocialGoals([...socialGoals, value])
    }

    const removeValue = async (index, input) => {
        try {
            if (input.id) {
                await SocialGoalsService.delete(input.id)
            }

            let data = [...socialGoals]
            data.splice(index, 1)
            setSocialGoals(data)
        } catch (error) {
            console.log(error)
        }
    }

    // Display form only if Social is required
    if (study && study.fieldsToFill && study.fieldsToFill.social) {
        return (
            <div className={"social"}>
                <h2>Social</h2>
                <div className={"values-section"}>
                    <div className={"values-header"}>
                        <label>Los valores que le inculcaron sus padres y que trata de llevar a cabo en la vida diaria
                            son:</label>
                        <label>Su prioridad en la vida:</label>
                        <label>Meta más próxima:</label>
                    </div>
                    {
                        socialGoals.map((input, index) => {
                            return (
                                <div key={`values-${index}`} className={"values-item"}>
                                    <input type={"text"} name={"coreValue"} placeholder={"Agregar..."}
                                        value={input.coreValue}
                                        onChange={event => handleValue(index, event)} disabled={disableForm} />
                                    <input type={"text"} name={"lifeGoal"} placeholder={"Agregar..."}
                                        value={input.lifeGoal}
                                        onChange={event => handleValue(index, event)} disabled={disableForm} />
                                    <input type={"text"} name={"nextGoal"} placeholder={"Agregar..."}
                                        value={input.nextGoal}
                                        onChange={event => handleValue(index, event)} disabled={disableForm} />
                                    {
                                        index !== 0 ?
                                            <button className={"remove-item-button"}
                                                onClick={() => removeValue(index, input)} disabled={disableForm}>
                                                <img src={"/images/trash-icon.png"} alt={""} />
                                            </button> : null
                                    }
                                </div>
                            )
                        })
                    }
                    <div className={"values-footer"}>
                        <button className={"add-item-button"} onClick={addValue} disabled={disableForm}>
                            <img src={"/images/add-icon.svg"} alt={""} />
                        </button>
                    </div>
                </div>
                <div className={"social-data"}>
                    <div className={"social-environment"}>
                        <label>Área social:</label>
                        <textarea placeholder={"Agregar comentario"} value={socialArea}
                            onChange={event => setSocialArea(event.target.value)} disabled={disableForm} />
                    </div>
                    <div className={"hobbies"}>
                        <label>Intereses y pasatiempos:</label>
                        <textarea placeholder={"Agregar comentario"} value={hobbies}
                            onChange={event => setHobbies(event.target.value)} disabled={disableForm} />
                    </div>
                    <div className={"health-information"}>
                        <label>Datos de salud:</label>
                        <textarea placeholder={"Agregar comentario"} value={healthInformation}
                            onChange={event => setHealthInformation(event.target.value)} disabled={disableForm} />
                    </div>
                </div>
                <div className={"result-socioeconomic-save"}>
                    <button className={"form-button-primary save-step"} onClick={submit} disabled={disableForm}>Guardar</button>
                </div>
            </div>
        )
    } else {
        return null
    }
}

export default Social