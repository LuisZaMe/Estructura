import React, { useEffect, useState } from "react";
import InputFileDocument from "../../common/InputFileDocument";
import InputFileImage from "../../common/InputFileImage";
import { useSelector } from "react-redux";
import StudyService from "../../../../services/StudyService";
import EducationListService from "../../../../services/EducationListService";
import EducationService from "../../../../services/EducationService";
import ExtracurricularActivities from "../../../../services/ExtracurricularActivities";
import AuthService from "../../../../services/AuthService";

const Education = () => {
    // Form data
    const disableForm = AuthService.getIdentity().role === 4;
    const [education, setEducation] = useState([
        {
            schoolarLevel: 0,
            career: "",
            period: "",
            timeOnCareer: "",
            institution: "",
            doccument: { base64Doccument: null }
        }
    ])
    const [courses, setCourses] = useState([
        { name: "", instituution: "", knowledgeLevel: 0, period: "" }
    ])

    // const [languages, setLanguages] = useState([
    //     {language: "", academy: "", level: 0, years: ""}
    // ])
    const [schoolValidationFile, setSchoolValidationFile] = useState({ base64Image: null })
    //const [image, setImage] = useState("")
    const [schoolValidationComment, setSchoolValidationComment] = useState("")

    // Get Study id from Redux
    const studyId = useSelector(state => state.study)
    // Add study to state
    const [study, setStudy] = useState({})

    // Make API request to Study Service and search for the study
    const getStudy = async () => {
        let response = await StudyService.getStudy(studyId)
        let study = response.data.response[0]

        setStudy(study)

        // If there is a studySchoolarity object, pass the values to the form data
        if (study.studySchoolarity) {
            // Load Education list
            if (study.studySchoolarity.scholarityList && study.studySchoolarity.scholarityList.length > 0) {
                setEducation(study.studySchoolarity.scholarityList)
            }

            // Load extracurricular activities
            if (study.studySchoolarity.extracurricularActivitiesList && study.studySchoolarity.extracurricularActivitiesList.length > 0) {
                setCourses(study.studySchoolarity.extracurricularActivitiesList)
            }

            // Load School Validation
            setSchoolValidationComment(study.studySchoolarity.scholarVerificationSummary)

            // Load School Validation image
            if (study.studySchoolarity.scholarVerificationMedia && study.studySchoolarity.scholarVerificationMedia.mediaURL) {
                setSchoolValidationFile(study.studySchoolarity.scholarVerificationMedia)
            }
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
        // If education exists update
        if (study.studySchoolarity) {
            try {
                // Update core Education object
                let educationStudy = {
                    id: study.studySchoolarity.id,
                    studyId: study.id,
                    scholarVerificationSummary: schoolValidationComment,
                    scholarVerificationMedia: schoolValidationFile
                }

                await EducationService.update(educationStudy)

                // Update existing education
                let existingEducation = education.filter(education => education.studySchoolarityId)
                for (const education of existingEducation) {
                    await EducationListService.update(education)
                }

                // Submit new education
                let newEducation = education.filter(education => !education.studySchoolarityId).map(education => {
                    let temp = { ...education }
                    temp.studySchoolarityId = study.studySchoolarity.id
                    return temp
                })
                if (newEducation.length > 0) {
                    await EducationListService.create(newEducation)
                }

                // Update existing extra activities
                let existingExtraActivities = courses.filter(course => course.studySchoolarityId)
                for (const activiy of existingExtraActivities) {
                    await ExtracurricularActivities.update(activiy)
                }

                // Submit new extra activities
                let newActivities = courses.filter(course => !course.studySchoolarityId).map(course => {
                    let temp = { ...course }
                    temp.studySchoolarityId = study.studySchoolarity.id
                    return temp
                })
                if (newActivities.length > 0) {
                    await ExtracurricularActivities.create(newActivities)
                }

                // Refresh data
                getStudy()
            } catch (error) {
                console.log(error)
            }
        } else {
            try {
                // Create Education study
                let educationStudy = {
                    studyId: study.id,
                    scholarVerificationSummary: schoolValidationComment,
                    scholarVerificationMedia: schoolValidationFile,
                    scholarityList: education,
                    extracurricularActivitiesList: courses
                }

                await EducationService.create(educationStudy)

                // Refresh data
                getStudy()
            } catch (error) {
                console.log(error)
            }
        }
    }

    const handleEducation = (index, event) => {
        let data = [...education]

        if (event.target.name === "schoolarLevel") {
            data[index][event.target.name] = parseInt(event.target.value)
        } else {
            data[index][event.target.name] = event.target.value
        }
        setEducation(data)
    }

    const handleDocument = (index, file) => {
        let data = [...education]

        if (file) {
            let reader = new FileReader()
            reader.readAsDataURL(file)

            reader.onload = (e) => {
                data[index].doccument = {
                    base64Doccument: e.target.result.replace(e.target.result.split(",")[0] + ",", ""),
                    storeFileType: 1,
                    doccumentName: file.name
                }
                setEducation(data)
            }
        } else {
            data[index].doccument = {
                base64Doccument: null
            }
            setEducation(data)
        }
    }

    const addEducationEntry = () => {
        let newEntry = {
            schoolarLevel: 0,
            career: "",
            period: "",
            timeOnCareer: "",
            institution: "",
            doccument: { base64Doccument: null }
        }
        setEducation([...education, newEntry])
    }

    const removeEducationEntry = async (index, input) => {
        try {
            if (input.id) {
                await EducationListService.delete(input.id)
            }

            let data = [...education]
            data.splice(index, 1)
            setEducation(data)
        } catch (error) {
            console.log(error)
        }
    }

    const handleCourses = (index, event) => {
        let data = [...courses]
        if (event.target.name === "knowledgeLevel") {
            data[index][event.target.name] = parseInt(event.target.value)
        } else {
            data[index][event.target.name] = event.target.value
        }
        setCourses(data)
    }

    const addCourse = () => {
        let course = { name: "", instituution: "", knowledgeLevel: 0, period: "" }
        setCourses([...courses, course])
    }

    const removeCourse = async (index, input) => {
        try {
            if (input.id) {
                await ExtracurricularActivities.delete(input.id)
            }

            let data = [...courses]
            data.splice(index, 1)
            setCourses(data)
        } catch (error) {
            console.log(error)
        }
    }

    const handleImage = (file) => {
        if (file) {
            let reader = new FileReader()
            reader.readAsDataURL(file)

            reader.onload = (e) => {
                let data = {
                    base64Image: e.target.result.replace(e.target.result.split(",")[0] + ",", "")
                }
                setSchoolValidationFile(data)
            }
        } else {
            let data = {
                base64Image: null,
                //mediaURL: ""
            }
            setSchoolValidationFile(data)
        }
    }

    // const handleLanguages = (index, event) => {
    //     let data = [...languages]
    //     data[index][event.target.name] = event.target.value
    //     setLanguages(data)
    // }
    //
    // const addLanguage = () => {
    //     let language = {language: "", academy: "", level: "", years: ""}
    //     setLanguages([...languages, language])
    // }
    //
    // const removeLanguage = (index) => {
    //     let data = [...languages]
    //     data.splice(index, 1)
    //     setLanguages(data)
    // }

    return (
        <div className={"education"}>
            <div className={"education-section"}>
                <h2>Escolaridad</h2>
                <div className={"education-grid"}>
                    {education.map((input, index) => {
                        return (
                            <div key={`education-${index}`} className={"education-grid-item"}>
                                <div className={"education-grid-header-1"}>
                                    <label>Nivel escolar</label>
                                    <label>Carrera</label>
                                    <label>Periodo</label>
                                    <label>Años cursados</label>
                                </div>
                                <div className={"education-grid-row-1"}>
                                    <select name={"schoolarLevel"} value={input.schoolarLevel}
                                        onChange={event => handleEducation(index, event)} disabled={disableForm}>
                                        <option value={0}>Seleccionar</option>
                                        <option value={1}>Primaria</option>
                                        <option value={2}>Secundaria</option>
                                        <option value={3}>Bachillerato</option>
                                        <option value={4}>Licenciatura</option>
                                        <option value={5}>Posgrado</option>
                                    </select>
                                    <input type={"text"} name={"career"} placeholder={"Agregar..."}
                                        value={input.career}
                                        onChange={event => handleEducation(index, event)} disabled={disableForm} />
                                    <input type={"text"} name={"period"} placeholder={"Agregar..."}
                                        value={input.period}
                                        onChange={event => handleEducation(index, event)} disabled={disableForm} />
                                    <input type={"text"} name={"timeOnCareer"} placeholder={"Agregar..."}
                                        value={input.timeOnCareer}
                                        onChange={event => handleEducation(index, event)} disabled={disableForm} />
                                </div>
                                <div className={"education-grid-header-2"}>
                                    <label>Institución</label>
                                    <label>Lugar</label>
                                    <label>Documento</label>
                                </div>
                                <div className={"education-grid-row-2"}>
                                    <input type={"text"} name={"institution"} placeholder={"Agregar..."}
                                        value={input.institution}
                                        onChange={event => handleEducation(index, event)} disabled={disableForm} />
                                    <input type={"text"} name={"place"} placeholder={"Agregar..."}
                                        value={input.place}
                                        onChange={event => handleEducation(index, event)} disabled={disableForm} />
                                    <InputFileDocument file={input.doccument.doccumentName}
                                        setFile={(file) => handleDocument(index, file)} />
                                    {index !== 0 ?
                                        <button className={"remove-item-button"}
                                            onClick={() => removeEducationEntry(index, input)} disabled={disableForm}>
                                            <img src={"/images/trash-icon.png"} alt={""} />
                                        </button> : null
                                    }
                                </div>
                            </div>
                        )
                    })}
                    <div className={"education-grid-footer"}>
                        <button className={"add-item-button"} onClick={addEducationEntry} disabled={disableForm}>
                            <img src={"/images/add-icon.svg"} alt={""} />
                        </button>
                    </div>
                </div>
            </div>
            <div className={"extracurricular-section"}>
                <h2>Actividades extracurriculares</h2>
                <div className={"extracurricular-grid"}>
                    <div className={"extracurricular-header"}>
                        <label>Nombre del curso o idioma</label>
                        <label>Institución</label>
                        <label>Nivel</label>
                        <label>Periodo</label>
                    </div>
                    {courses.map((input, index) => {
                        return (
                            <div key={`course-${index}`} className={"extracurricular-row"}>
                                <input type={"text"} name={"name"} placeholder={"Agregar..."} value={input.name}
                                    onChange={event => handleCourses(index, event)} disabled={disableForm} />
                                <input type={"text"} name={"instituution"} placeholder={"Agregar..."}
                                    value={input.instituution}
                                    onChange={event => handleCourses(index, event)} disabled={disableForm} />
                                <select name={"knowledgeLevel"} value={input.knowledgeLevel}
                                    onChange={event => handleCourses(index, event)} disabled={disableForm}>
                                    <option value={0}>Seleccionar</option>
                                    <option value={1}>Básico</option>
                                    <option value={2}>Intermedio</option>
                                    <option value={3}>Avanzado</option>
                                </select>
                                <input type={"text"} name={"period"} placeholder={"Agregar"} value={input.period}
                                    onChange={event => handleCourses(index, event)} disabled={disableForm} />
                                {index > 0 ?
                                    <button className={"remove-item-button"} onClick={() => removeCourse(index, input)} disabled={disableForm}>
                                        <img src={"/images/trash-icon.png"} alt={""} />
                                    </button>
                                    : null
                                }
                            </div>
                        )
                    })}
                    <div className={"extracurricular-footer"}>
                        <button className={"add-item-button"} onClick={addCourse} disabled={disableForm}>
                            <img src={"/images/add-icon.svg"} alt={""} />
                        </button>
                    </div>
                </div>
                {/*<div className={"extracurricular-grid"}>*/}
                {/*    <div className={"extracurricular-header"}>*/}
                {/*        <label>Nombre del curso</label>*/}
                {/*        <label>Institución</label>*/}
                {/*        <label>Nivel</label>*/}
                {/*        <label>Periodo</label>*/}
                {/*    </div>*/}
                {/*    {languages.map((input, index) => {*/}
                {/*        return (*/}
                {/*            <div key={`course-${index}`} className={"extracurricular-row"}>*/}
                {/*                <input type={"text"} name={"name"} placeholder={"Agregar..."} value={input.name}*/}
                {/*                       onChange={event => handleLanguages(index, event)}/>*/}
                {/*                <input type={"text"} name={"academy"} placeholder={"Agregar..."} value={input.academy}*/}
                {/*                       onChange={event => handleLanguages(index, event)}/>*/}
                {/*                <select name={"level"} value={input.level}*/}
                {/*                        onChange={event => handleLanguages(index, event)}>*/}
                {/*                    <option value={0}>Seleccionar</option>*/}
                {/*                    <option value={1}>Básico</option>*/}
                {/*                    <option value={2}>Intermedio</option>*/}
                {/*                    <option value={3}>Avanzado</option>*/}
                {/*                </select>*/}
                {/*                <input type={"text"} name={"years"} placeholder={"Agregar"} value={input.years}*/}
                {/*                       onChange={event => handleLanguages(index, event)}/>*/}
                {/*                {index > 0 ?*/}
                {/*                    <button className={"remove-item-button"} onClick={() => removeLanguage(index)}>*/}
                {/*                        <img src={"/images/trash-icon.png"} alt={""}/>*/}
                {/*                    </button>*/}
                {/*                    : null*/}
                {/*                }*/}
                {/*            </div>*/}
                {/*        )*/}
                {/*    })}*/}
                {/*    <div className={"extracurricular-footer"}>*/}
                {/*        <button className={"add-item-button"} onClick={addLanguage}>*/}
                {/*            <img src={"/images/add-icon.svg"} alt={""}/>*/}
                {/*        </button>*/}
                {/*    </div>*/}
                {/*</div>*/}
            </div>
            <div className={"school-validation-section"}>
                <h2>Verificación escolar</h2>
                <textarea placeholder={"Agregar comentario..."} value={schoolValidationComment}
                    onChange={event => setSchoolValidationComment(event.target.value)} disabled={disableForm}></textarea>
                <div className={"school-validation-file"}>
                    <InputFileImage
                        imageUrl={schoolValidationFile.mediaURL ? schoolValidationFile.mediaURL : ""}
                        setImageUrl={() => null}
                        setFile={(file) => handleImage(file)}
                    />
                </div>
            </div>
            <div className={"result-socioeconomic-save"}>
                <button className={"form-button-primary save-step"} onClick={submit} disabled={disableForm}>Guardar</button>
            </div>
        </div>
    )
}

export default Education