import React, { useEffect, useState } from "react";
import { useSelector } from "react-redux";

import { PDFDownloadLink } from "@react-pdf/renderer";

// Component
import InputFileImage from "../../common/InputFileImage";

// Services
import StudyService from "../../../../services/StudyService";
import StudyPicturesService from "../../../../services/StudyPicturesService";
import DocuPDF from "../../../../services/CreatePdfService";

import { v4 as uuidv4 } from 'uuid';

const Pictures = () => {
    const [pictures, setPictures] = useState({
        media1: { base64Image: null },
        media2: { base64Image: null },
        media3: { base64Image: null },
        media4: { base64Image: null },
        media5: { base64Image: null },
        media6: { base64Image: null }
    })

    // Get Study id from Redux
    const studyId = useSelector(state => state.study)
    // Add study to state
    const [study, setStudy] = useState({});

    const user = JSON.parse(localStorage.getItem('user'));
    // Make API request to Study Service and search for the study
    const getStudy = async () => {
        let response = await StudyService.getStudy(studyId)
        let study = response.data.response[0]
        setStudy(study)
        // If there is a studyPictures, pass the values to the form data
        if (study.studyPictures !== null) {
            setPictures({ ...study.studyPictures })
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
        if (study.studyPictures) {
            // Update
            try {
                await StudyPicturesService.update(pictures)

                // Refresh data
                getStudy()
            } catch (error) {
                console.log(error)
            }
        } else {
            // Create
            try {
                let data = { ...pictures }
                data.studyId = study.id

                await StudyPicturesService.create(data)

                // Refresh data
                getStudy()
            } catch (error) {
                console.log(error)
            }
        }
    }

    const handleFile = (file, type) => {
        let temp = { ...pictures }

        if (file) {
            let reader = new FileReader()
            reader.readAsDataURL(file)

            reader.onload = (e) => {
                temp[type] = {
                    base64Image: e.target.result.replace(e.target.result.split(",")[0] + ",", "")
                }
                setPictures(temp)
            }
        } else {
            temp[type] = {
                base64Image: null
            }
            setPictures(temp)
        }
    }

    return (
        <div className={"pictures"}>
            <h2>Fotografías</h2>
            <div className={"pictures-grid"}>
                <InputFileImage setFile={(file) => handleFile(file, "media1")}
                    imageUrl={pictures.media1.mediaURL ? pictures.media1.mediaURL : ""}
                    setImageUrl={() => null} />
                <InputFileImage setFile={(file) => handleFile(file, "media2")}
                    imageUrl={pictures.media2.mediaURL ? pictures.media2.mediaURL : ""}
                    setImageUrl={() => null} />
                <InputFileImage setFile={(file) => handleFile(file, "media3")}
                    imageUrl={pictures.media3.mediaURL ? pictures.media3.mediaURL : ""}
                    setImageUrl={() => null} />
                <InputFileImage setFile={(file) => handleFile(file, "media4")}
                    imageUrl={pictures.media4.mediaURL ? pictures.media4.mediaURL : ""}
                    setImageUrl={() => null} />
                <InputFileImage setFile={(file) => handleFile(file, "media5")}
                    imageUrl={pictures.media5.mediaURL ? pictures.media5.mediaURL : ""}
                    setImageUrl={() => null} />
                <InputFileImage setFile={(file) => handleFile(file, "media6")}
                    imageUrl={pictures.media6.mediaURL ? pictures.media6.mediaURL : ""}
                    setImageUrl={() => null} />
            </div>
            <div className={"result-socioeconomic-save"}>
                {user.identity.role == 4 ?
                    study.id ?

                        <PDFDownloadLink document={<DocuPDF study={study} />} fileName={`${uuidv4()}.pdf`} className={"form-button-primary save-step"}>
                            <button className={"form-button-primary save-step"}>
                                Descargar PDF
                            </button>
                        </PDFDownloadLink> :

                        <button className={"form-button-primary save-step"}>
                            Cargando
                        </button> :

                    <button className={"form-button-primary save-step"} onClick={submit}>
                        Guardar
                    </button>
                }
            </div>
        </div>
    )
}

export default Pictures