import React, { useEffect, useState } from "react";
import { useSelector } from "react-redux";

// Components
import DeleteReference from "./DeleteReference";
import { PDFDownloadLink } from "@react-pdf/renderer";

// Services
import StudyService from "../../../../services/StudyService";
import PersonalReferenceService from "../../../../services/PersonalReferenceService";
import AuthService from "../../../../services/AuthService";
import DocuPDF from "../../../../services/CreatePdfService";

import { v4 as uuidv4 } from 'uuid';

const PersonalReferences = () => {
    // Form data

    const disableForm = AuthService.getIdentity().role === 4;
    const [references, setReferences] = useState([
        {
            referenceTitle: "",
            name: "",
            currentJob: "",
            address: "",
            phone: "",
            yearsKnowingEachOther: "",
            knowAddress: "",
            yearsOnCurrentResidence: "",
            knowledgeAboutPreviousJobs: "",
            opinionAboutTheCandidate: "",
            politicalActivity: "",
            wouldYouRecommendIt: ""
        }
    ])
    const [currentTab, setCurrentTab] = useState(0)
    const [showDeleteForm, setShowDeleteForm] = useState(false)

    // Get Study id from Redux
    const studyId = useSelector(state => state.study)
    // Add study to state
    const [study, setStudy] = useState({})

    const user = JSON.parse(localStorage.getItem('user'));
    // Make API request to Study Service and search for the study
    const getStudy = async () => {
        let response = await StudyService.getStudy(studyId)
        let study = response.data.response[0]

        setStudy(study)

        // If there is a studyPersonalReferenceList, pass the values to the form data
        if (study.studyPersonalReferenceList && study.studyPersonalReferenceList.length > 0) {
            setReferences([...study.studyPersonalReferenceList])
        } else {
            // If there are no References created, add studyId to new Reference object
            let tempReferences = references.map(reference => {
                let temp = { ...reference }
                temp.studyId = study.id
                return temp
            })
            setReferences(tempReferences)
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
        // If personalReferences list exists update
        if (study.studyPersonalReferenceList && study.studyPersonalReferenceList.length > 0) {
            try {
                // Update references
                let existingReferences = references.filter(reference => reference.id)
                for (const reference of existingReferences) {
                    // Update reference
                    await PersonalReferenceService.update(reference)
                }

                // Submit new references
                let newReferences = references.filter(reference => !reference.id)
                if (newReferences.length > 0) {
                    await PersonalReferenceService.create(newReferences)
                }

                // Refresh data
                getStudy()
            } catch (error) {
                console.log(error)
            }
        } else {
            // Submit personal references list
            try {
                await PersonalReferenceService.create(references)

                // Refresh data
                getStudy()
            } catch (error) {
                console.log(error)
            }
        }
    }

    const handleReference = (index, event) => {
        let temp = [...references]
        temp[index][event.target.name] = event.target.value
        setReferences(temp)
    }

    const addReference = () => {
        let reference = {
            referenceTitle: "",
            name: "",
            currentJob: "",
            address: "",
            phone: "",
            yearsKnowingEachOther: "",
            knowAddress: "",
            yearsOnCurrentResidence: "",
            knowledgeAboutPreviousJobs: "",
            opinionAboutTheCandidate: "",
            politicalActivity: "",
            wouldYouRecommendIt: "",
            studyId: study.id
        }
        setReferences([...references, reference])

        // Change active reference
        setCurrentTab(references.length)

        // Scroll top
        scrollToTop()
    }

    const removeReference = async (index) => {
        try {
            if (references[index].id) {
                await PersonalReferenceService.delete(references[index].id)
            }

            // Change active reference
            setCurrentTab(index - 1)

            let temp = [...references]
            temp.splice(index, 1)
            setReferences(temp)

            // Scroll to top
            scrollToTop()
        } catch (error) {
            console.log(error)
        }
    }

    const scrollToTop = () => {
        document.getElementsByClassName("container")[0].scrollTo({ top: 0, behavior: "smooth" })
    }

    return (
        <div className={"personal-references"}>
            <h2>Referencia personal</h2>
            <div className={"personal-references-container"}>
                <div className={"tabs"}>
                    {
                        references.map((input, index) => {
                            return (
                                <div key={`tab-${index}`} className={`tab ${index === currentTab ? "active" : ""}`}
                                    onClick={() => setCurrentTab(index)}>
                                    <label>{`Referencia ${index + 1}`}</label>
                                </div>
                            )
                        })
                    }
                </div>
                <div className={"reference"}>
                    <label>Nombre</label>
                    <input type={"text"} name={"name"} placeholder={"Agregar"} value={references[currentTab].name}
                        onChange={event => handleReference(currentTab, event)} disabled={disableForm} />
                    <label>Ocupación actual</label>
                    <input type={"text"} name={"currentJob"} placeholder={"Agregar..."}
                        value={references[currentTab].currentJob}
                        onChange={event => handleReference(currentTab, event)} disabled={disableForm} />
                    <label>Dirección</label>
                    <input type={"text"} name={"address"} placeholder={"Agregar..."}
                        value={references[currentTab].address}
                        onChange={event => handleReference(currentTab, event)} disabled={disableForm} />
                    <label>Teléfono</label>
                    <input type={"text"} name={"phone"} placeholder={"Agregar..."} value={references[currentTab].phone}
                        onChange={event => handleReference(currentTab, event)} disabled={disableForm} />
                    <label>1. ¿Cuánto tiempo tiene de conocer a nuestra investigada?</label>
                    <input type={"text"} name={"yearsKnowingEachOther"} placeholder={"Agregar..."}
                        value={references[currentTab].yearsKnowingEachOther}
                        onChange={event => handleReference(currentTab, event)} disabled={disableForm} />
                    <label>2. ¿Sabe usted en dónde vive nuestra investigada?</label>
                    <input type={"text"} name={"knowAddress"} placeholder={"Agregar..."}
                        value={references[currentTab].knowAddress}
                        onChange={event => handleReference(currentTab, event)} disabled={disableForm} />
                    <label>3. ¿Sabe usted cuánto tiempo tiene radicando en su domicilio actual?</label>
                    <input type={"text"} name={"yearsOnCurrentResidence"} placeholder={"Agregar..."}
                        value={references[currentTab].yearsOnCurrentResidence}
                        onChange={event => handleReference(currentTab, event)} disabled={disableForm} />
                    <label>4. ¿Sabe dónde ha laborado?</label>
                    <input type={"text"} name={"knowledgeAboutPreviousJobs"} placeholder={"Agregar..."}
                        value={references[currentTab].knowledgeAboutPreviousJobs}
                        onChange={event => handleReference(currentTab, event)} disabled={disableForm} />
                    <label>5. ¿Qué opinión tiene de nuestra investigada?</label>
                    <input type={"text"} name={"opinionAboutTheCandidate"} placeholder={"Agregar..."}
                        value={references[currentTab].opinionAboutTheCandidate}
                        onChange={event => handleReference(currentTab, event)} disabled={disableForm} />
                    <label>6. ¿Sabe usted si nuestra investigada ha participado en alguna actividad política?</label>
                    <input type={"text"} name={"politicalActivity"} placeholder={"Agregar..."}
                        value={references[currentTab].politicalActivity}
                        onChange={event => handleReference(currentTab, event)} disabled={disableForm} />
                    <label>7. ¿Usted la recomienda?</label>
                    <input type={"text"} name={"wouldYouRecommendIt"} placeholder={"Agregar..."}
                        value={references[currentTab].wouldYouRecommendIt}
                        onChange={event => handleReference(currentTab, event)} disabled={disableForm} />
                </div>
                <div className={"reference-actions"}>
                    {
                        currentTab !== 0 ?
                            <button className={"remove-reference"} onClick={() => setShowDeleteForm(true)} disabled={disableForm}>
                                <img src={"/images/trash-icon.svg"} alt={""} />
                                <span>Eliminar referencia</span>
                            </button> : null
                    }
                    <DeleteReference show={showDeleteForm} handleClose={setShowDeleteForm}
                        handleDelete={() => removeReference(currentTab)} disabled={disableForm} />
                    <button className={"add-reference"} onClick={addReference} disabled={disableForm}>
                        <img src={"/images/add-icon.svg"} alt={""} />
                        <span>Agregar otra referencia</span>
                    </button>
                </div>
            </div>
            <div className={"result-socioeconomic-save"}>
                {user.identity.role == 4 && study.workStudy != null ?
                    study.id ?

                        <PDFDownloadLink document={<DocuPDF study={study} />} fileName={`${uuidv4()}.pdf`} className={"form-button-primary save-step"}>
                            <button className={"form-button-primary save-step"}>
                                Descargar PDF
                            </button>
                        </PDFDownloadLink> :

                        <button className={"form-button-primary save-step"}>
                            Cargando
                        </button> :
                    <>
                        <button className={"form-button-primary save-step"} onClick={submit} disabled={disableForm}>Guardar</button>
                    </>
                }
            </div>
        </div>
    )
}

export default PersonalReferences