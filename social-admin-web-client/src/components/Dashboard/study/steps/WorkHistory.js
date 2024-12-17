import React, { cloneElement, useEffect, useState } from "react";
import { useSelector } from "react-redux";

// Components
import InputFileImage from "../../common/InputFileImage";
import DeleteWorkHistory from "./DeleteWorkHistory";

// Services
import StudyService from "../../../../services/StudyService";
import WorkHistoryService from "../../../../services/WorkHistoryService";
import AuthService from "../../../../services/AuthService";

const WorkHistory = () => {
    // Form data
    const disableForm = AuthService.getIdentity().role === 4;
    const [jobs, setJobs] = useState([
        {
            companyName: "",
            candidateBusinessName: "",
            companyBusinessName: "",
            candidateRole: "",
            companyRole: "",
            candidatePhone: "",
            companyPhone: "",
            candidateAddress: "",
            companyAddress: "",
            candidateStartDate: "",
            companyStartDate: "",
            candidateEndDate: "",
            companyEndDate: "",
            candidateInitialRole: "",
            companyInitialRole: "",
            candidateFinalRole: "",
            companyFinalRole: "",
            candidateStartSalary: 0,
            companyStartSalary: 0,
            candidateEndSalary: 0,
            companyEndSalary: 0,
            candidateBenefits: "",
            companyBenefits: "",
            candidateResignationReason: "",
            companyResignationReason: "",
            candidateDirectBoss: "",
            companyDirectBoss: "",
            candidateLaborUnion: "",
            companyLaborUnion: "",
            recommended: "",
            recommendedReasonWhy: "",
            rehire: "",
            rehireReason: "",
            observations: "",
            notes: "",
            media1: {
                base64Image: null
            },
            media2: {
                base64Image: null
            }
        }
    ])
    const [currentTab, setCurrentTab] = useState(0)
    const [showDeleteForm, setShowDeleteForm] = useState(false)
    const [images, setImages] = useState([{ image1: "", image2: "" }])

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

        // If there is a studyLaboralTrayectoryList, pass the values to the form data
        if (study.studyLaboralTrayectoryList && study.studyLaboralTrayectoryList.length > 0) {
            let tempJobs = [...study.studyLaboralTrayectoryList].map(job => {
                let tempJob = { ...job }

                let date = new Date(tempJob.candidateStartDate)
                tempJob.candidateStartDate = date.getFullYear() + "-" + pad(date.getMonth() + 1) + "-" + pad(date.getDate())

                date = new Date(tempJob.companyStartDate)
                tempJob.companyStartDate = date.getFullYear() + "-" + pad(date.getMonth() + 1) + "-" + pad(date.getDate())

                date = new Date(tempJob.candidateEndDate)
                tempJob.candidateEndDate = date.getFullYear() + "-" + pad(date.getMonth() + 1) + "-" + pad(date.getDate())

                date = new Date(tempJob.companyEndDate)
                tempJob.companyEndDate = date.getFullYear() + "-" + pad(date.getMonth() + 1) + "-" + pad(date.getDate())

                return tempJob
            })
            setJobs(tempJobs)

            let tempImages = [...study.studyLaboralTrayectoryList].map(job => {
                return {
                    image1: job.media1.mediaURL ? job.media1.mediaURL : "",
                    image2: job.media2.mediaURL ? job.media2.mediaURL : ""
                }
            })
            setImages(tempImages)
        } else {
            let tempJobs = jobs.map(job => {
                let temp = { ...job }
                temp.studyId = study.id
                return temp
            })
            setJobs(tempJobs)
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
        // If workHistory list exists update
        if (study.studyLaboralTrayectoryList && study.studyLaboralTrayectoryList.length > 0) {
            try {
                // Update jobs
                let existingJobs = jobs.filter(job => job.id)
                for (const job of existingJobs) {
                    // Update job
                    await WorkHistoryService.update(job)
                }

                // Submit new jobs
                let newJobs = jobs.filter(job => !job.id)
                if (newJobs.length > 0) {
                    await WorkHistoryService.create(newJobs)
                }

                // Refresh data
                getStudy()
            } catch (error) {
                console.log(error)
            }
        } else {
            // Submit work history list
            try {
                await WorkHistoryService.create(jobs)

                // Refresh data
                getStudy()
            } catch (error) {
                console.log(error)
            }
        }
    }

    const handleJob = (index, event) => {
        let data = [...jobs]

        if (event.target.name !== "candidateStartSalary" && event.target.name !== "companyStartSalary" && event.target.name !== "candidateEndSalary" && event.target.name !== "companyEndSalary") {
            data[index][event.target.name] = event.target.value
        } else {
            data[index][event.target.name] = event.target.value ? parseFloat(event.target.value) : 0
        }
        setJobs(data)
    }

    const handleFile = (index, name, file) => {
        let data = [...jobs]

        if (file) {
            let reader = new FileReader();
            reader.readAsDataURL(file);

            reader.onload = (e) => {
                data[index][name] = { base64Image: e.target.result.replace(e.target.result.split(",")[0] + ",", "") }
                setJobs(data)
            }
        } else {
            data[index][name] = { base64Image: null }
            setJobs(data)
        }
    }

    const addJob = () => {
        let job = {
            companyName: "",
            candidateBusinessName: "",
            companyBusinessName: "",
            candidateRole: "",
            companyRole: "",
            candidatePhone: "",
            companyPhone: "",
            candidateAddress: "",
            companyAddress: "",
            candidateStartDate: "",
            companyStartDate: "",
            candidateEndDate: "",
            companyEndDate: "",
            candidateInitialRole: "",
            companyInitialRole: "",
            candidateFinalRole: "",
            companyFinalRole: "",
            candidateStartSalary: 0,
            companyStartSalary: 0,
            candidateEndSalary: 0,
            companyEndSalary: 0,
            candidateBenefits: "",
            companyBenefits: "",
            candidateResignationReason: "",
            companyResignationReason: "",
            candidateDirectBoss: "",
            companyDirectBoss: "",
            candidateLaborUnion: "",
            companyLaborUnion: "",
            recommended: "",
            recommendedReasonWhy: "",
            rehire: "",
            rehireReason: "",
            observations: "",
            notes: "",
            media1: {
                base64Image: null
            },
            media2: {
                base64Image: null
            },
            studyId: studyId
        }
        setJobs([...jobs, job])

        // Add temp images
        let tempImages = { image1: "", image2: "" }
        setImages([...images, tempImages])

        // Change active job
        setCurrentTab(jobs.length)

        // Scroll to Top
        scrollToTop()
    }

    const removeJob = async (index) => {
        try {
            if (jobs[index].id) {
                await WorkHistoryService.delete(jobs[index].id)
            }

            setCurrentTab(index - 1)

            let data = [...jobs]
            data.splice(index, 1)
            setJobs(data)

            // Delete temp image
            let tempImages = [...images]
            tempImages.splice(index, 1)
            setImages(tempImages)

            // Scroll to Top
            scrollToTop()
        } catch (error) {
            console.log(error)
        }
    }

    const scrollToTop = () => {
        document.getElementsByClassName("container")[0].scrollTo({ top: 0, behavior: "smooth" })
    }

    return (
        <div className={"work-history"}>
            <h2>Trayectorial laboral</h2>
            <div className={"work-history-container"}>
                <div className={"tabs"}>
                    {
                        jobs.map((input, index) => {
                            return (
                                <div key={`tab-${index}`} className={`tab ${index === currentTab ? "active" : ""}`}
                                    onClick={() => setCurrentTab(index)}>
                                    <label>{`Trayectoria ${index + 1}`}</label>
                                </div>
                            )
                        })
                    }
                </div>
                <div className={"job"}>
                    <div className={"job-company"}>
                        <label>Nombre de la empresa</label>
                        <input type={"text"} name={"companyName"} placeholder={"Agregar..."}
                            value={jobs[currentTab].companyName}
                            onChange={event => handleJob(currentTab, event)} disabled={disableForm} />
                    </div>
                    <div className={"job-details"}>
                        <h3>Información proporcionada por</h3>
                        <div className={"job-details-grid"}>
                            <div className={"job-details-header"}>
                                <span />
                                <label>Candidato</label>
                                <label>Empresa</label>
                            </div>
                            <div className={"job-details-item"}>
                                <label>Razón Social</label>
                                <input type={"text"} name={"candidateBusinessName"}
                                    placeholder={"Agregar..."}
                                    value={jobs[currentTab].candidateBusinessName}
                                    onChange={event => handleJob(currentTab, event)} disabled={disableForm} />
                                <input type={"text"} name={"companyBusinessName"}
                                    placeholder={"Agregar"}
                                    value={jobs[currentTab].companyBusinessName}
                                    onChange={event => handleJob(currentTab, event)} disabled={disableForm} />
                            </div>
                            <div className={"job-details-item"}>
                                <label>Giro</label>
                                <input type={"text"} name={"candidateRole"}
                                    placeholder={"Agregar..."}
                                    value={jobs[currentTab].candidateRole}
                                    onChange={event => handleJob(currentTab, event)} disabled={disableForm} />
                                <input type={"text"} name={"companyRole"}
                                    placeholder={"Agregar"}
                                    value={jobs[currentTab].companyRole}
                                    onChange={event => handleJob(currentTab, event)} disabled={disableForm} />
                            </div>
                            <div className={"job-details-item"}>
                                <label>Teléfono</label>
                                <input type={"tel"} name={"candidatePhone"}
                                    placeholder={"Agregar..."}
                                    value={jobs[currentTab].candidatePhone}
                                    onChange={event => handleJob(currentTab, event)} disabled={disableForm} />
                                <input type={"tel"} name={"companyPhone"}
                                    placeholder={"Agreaar..."}
                                    value={jobs[currentTab].companyPhone}
                                    onChange={event => handleJob(currentTab, event)} disabled={disableForm} />
                            </div>
                            <div className={"job-details-item"}>
                                <label>Domicilio</label>
                                <input type={"text"} name={"candidateAddress"}
                                    placeholder={"Agregar..."}
                                    value={jobs[currentTab].candidateAddress}
                                    onChange={event => handleJob(currentTab, event)} disabled={disableForm} />
                                <input type={"text"} name={"companyAddress"}
                                    placeholder={"Agregar..."}
                                    value={jobs[currentTab].companyAddress}
                                    onChange={event => handleJob(currentTab, event)} disabled={disableForm} />
                            </div>
                            <div className={"job-details-item"}>
                                <label>Fecha de ingreso</label>
                                <input type={"date"} name={"candidateStartDate"}
                                    placeholder={"Agregar"}
                                    value={jobs[currentTab].candidateStartDate}
                                    onChange={event => handleJob(currentTab, event)} disabled={disableForm} />
                                <input type={"date"} name={"companyStartDate"}
                                    placeholder={"Agregar"}
                                    value={jobs[currentTab].companyStartDate}
                                    onChange={event => handleJob(currentTab, event)} disabled={disableForm} />
                            </div>
                            <div className={"job-details-item"}>
                                <label>Fecha de egreso</label>
                                <input type={"date"} name={"candidateEndDate"}
                                    placeholder={"Agregar..."}
                                    value={jobs[currentTab].candidateEndDate}
                                    onChange={event => handleJob(currentTab, event)} disabled={disableForm} />
                                <input type={"date"} name={"companyEndDate"}
                                    placeholder={"Agregar..."}
                                    value={jobs[currentTab].companyEndDate}
                                    onChange={event => handleJob(currentTab, event)} disabled={disableForm} />
                            </div>
                            <div className={"job-details-item"}>
                                <label>Puesto inicial</label>
                                <input type={"text"} name={"candidateInitialRole"}
                                    placeholder={"Agregar..."}
                                    value={jobs[currentTab].candidateInitialRole}
                                    onChange={event => handleJob(currentTab, event)} disabled={disableForm} />
                                <input type={"text"} name={"companyInitialRole"}
                                    placeholder={"Agregar..."}
                                    value={jobs[currentTab].companyInitialRole}
                                    onChange={event => handleJob(currentTab, event)} disabled={disableForm} />
                            </div>
                            <div className={"job-details-item"}>
                                <label>Puesto final</label>
                                <input type={"text"} name={"candidateFinalRole"}
                                    placeholder={"Agregar..."}
                                    value={jobs[currentTab].candidateFinalRole}
                                    onChange={event => handleJob(currentTab, event)} disabled={disableForm} />
                                <input type={"text"} name={"companyFinalRole"}
                                    placeholder={"Agregar..."}
                                    value={jobs[currentTab].companyFinalRole}
                                    onChange={event => handleJob(currentTab, event)} disabled={disableForm} />
                            </div>
                            <div className={"job-details-item"}>
                                <label>Salario inicial</label>
                                <input type={"text"} name={"candidateStartSalary"}
                                    placeholder={"Agregar..."}
                                    value={jobs[currentTab].candidateStartSalary}
                                    onChange={event => handleJob(currentTab, event)} disabled={disableForm} />
                                <input type={"text"} name={"companyStartSalary"}
                                    placeholder={"Agregar..."}
                                    value={jobs[currentTab].companyStartSalary}
                                    onChange={event => handleJob(currentTab, event)} disabled={disableForm} />
                            </div>
                            <div className={"job-details-item"}>
                                <label>Salario final</label>
                                <input type={"text"} name={"candidateEndSalary"}
                                    placeholder={"Agregar..."}
                                    value={jobs[currentTab].candidateEndSalary}
                                    onChange={event => handleJob(currentTab, event)} disabled={disableForm} />
                                <input type={"text"} name={"companyEndSalary"}
                                    placeholder={"Agregar..."}
                                    value={jobs[currentTab].companyEndSalary}
                                    onChange={event => handleJob(currentTab, event)} disabled={disableForm} />
                            </div>
                            <div className={"job-details-item"}>
                                <label>Prestaciones</label>
                                <input type={"text"} name={"candidateBenefits"}
                                    placeholder={"Agregar..."}
                                    value={jobs[currentTab].candidateBenefits}
                                    onChange={event => handleJob(currentTab, event)} disabled={disableForm} />
                                <input type={"text"} name={"companyBenefits"}
                                    placeholder={"Agregar..."}
                                    value={jobs[currentTab].companyBenefits}
                                    onChange={event => handleJob(currentTab, event)} disabled={disableForm} />
                            </div>
                            <div className={"job-details-item"}>
                                <label>Motivo de salida</label>
                                <input type={"text"} name={"candidateResignationReason"}
                                    placeholder={"Agregar..."}
                                    value={jobs[currentTab].candidateResignationReason}
                                    onChange={event => handleJob(currentTab, event)} disabled={disableForm} />
                                <input type={"text"} name={"companyResignationReason"}
                                    placeholder={"Agregar..."}
                                    value={jobs[currentTab].companyResignationReason}
                                    onChange={event => handleJob(currentTab, event)} disabled={disableForm} />
                            </div>
                            <div className={"job-details-item"}>
                                <label>Jefe inmediato y puesto</label>
                                <input type={"text"} name={"candidateDirectBoss"}
                                    placeholder={"Agregar..."}
                                    value={jobs[currentTab].candidateDirectBoss}
                                    onChange={event => handleJob(currentTab, event)} disabled={disableForm} />
                                <input type={"text"} name={"companyDirectBoss"}
                                    placeholder={"Agregar..."}
                                    value={jobs[currentTab].companyDirectBoss}
                                    onChange={event => handleJob(currentTab, event)} disabled={disableForm} />
                            </div>
                            <div className={"job-details-item"}>
                                <label>Sindicalizado</label>
                                <input type={"text"} name={"candidateLaborUnion"}
                                    placeholder={"Agregar..."}
                                    value={jobs[currentTab].candidateLaborUnion}
                                    onChange={event => handleJob(currentTab, event)} disabled={disableForm} />
                                <input type={"text"} name={"companyLaborUnion"}
                                    placeholder={"Agregar..."}
                                    value={jobs[currentTab].companyLaborUnion}
                                    onChange={event => handleJob(currentTab, event)} disabled={disableForm} />
                            </div>
                        </div>
                    </div>
                    <div className={"job-questions"}>
                        <div className={"job-questions-item"}>
                            <label>Recomendable</label>
                            <input type={"text"} name={"recommended"} placeholder={"Agregar"}
                                value={jobs[currentTab].recommended}
                                onChange={event => handleJob(currentTab, event)} disabled={disableForm} />
                            <label>¿Por qué?</label>
                            <input type={"text"} name={"recommendedReasonWhy"} placeholder={"Agregar..."}
                                value={jobs[currentTab].recommendedReasonWhy}
                                onChange={event => handleJob(currentTab, event)} disabled={disableForm} />
                        </div>
                        <div className={"job-questions-item"}>
                            <label>Recontratable</label>
                            <input type={"text"} name={"rehire"} placeholder={"Agregar..."}
                                value={jobs[currentTab].rehire}
                                onChange={event => handleJob(currentTab, event)} disabled={disableForm} />
                            <label>¿Por qué?</label>
                            <input type={"text"} name={"rehireReason"} placeholder={"Agregar..."}
                                value={jobs[currentTab].rehireReason}
                                onChange={event => handleJob(currentTab, event)} disabled={disableForm} />
                        </div>
                    </div>
                    <div className={"job-comments"}>
                        <div className={"job-observations"}>
                            <label>Observaciones</label>
                            <textarea name={"observations"} placeholder={"Agregar comentario..."}
                                value={jobs[currentTab].observations}
                                onChange={event => handleJob(currentTab, event)} disabled={disableForm} />
                        </div>
                        <div className={"job-notes"}>
                            <label>Notas</label>
                            <textarea name={"notes"} placeholder={"Agregar comentario..."}
                                value={jobs[currentTab].notes}
                                onChange={event => handleJob(currentTab, event)} disabled={disableForm} />
                        </div>
                    </div>
                    <div className={"job-images"}>
                        <div className={"job-images-grid"}>
                            <InputFileImage setFile={(file) => handleFile(currentTab, "media1", file)}
                                imageUrl={images[currentTab].image1} setImageUrl={(imageUrl) => {
                                    let temp = [...images]
                                    temp[currentTab].image1 = imageUrl
                                    setImages(temp)
                                }} />
                            <InputFileImage setFile={(file) => handleFile(currentTab, "media2", file)}
                                imageUrl={images[currentTab].image2} setImageUrl={(imageUrl) => {
                                    let temp = [...images]
                                    temp[currentTab].image2 = imageUrl
                                    setImages(temp)
                                }} />
                        </div>
                    </div>
                </div>
                <div className={"job-actions"}>
                    {
                        currentTab !== 0 ?
                            <button className={"remove-job"} onClick={() => setShowDeleteForm(true)} disabled={disableForm}>
                                <img src={"/images/trash-icon.svg"} alt={""} />
                                <span>Eliminar trayectoria</span>
                            </button> : null
                    }
                    <DeleteWorkHistory show={showDeleteForm} handleClose={setShowDeleteForm}
                        handleDelete={() => removeJob(currentTab)} />
                    <button className={"add-job"} onClick={addJob} disabled={disableForm}>
                        <img src={"/images/add-icon.svg"} alt={""} />
                        <span>Agregar otra trayectoria</span>
                    </button>
                </div>
            </div>
            <div className={"result-socioeconomic-save"}>
                <button className={"form-button-primary save-step"} onClick={submit} disabled={disableForm}>Guardar</button>
            </div>
        </div>
    )
}

export default WorkHistory