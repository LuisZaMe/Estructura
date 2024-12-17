import React, { useEffect, useState } from "react";
import { useSelector } from "react-redux";
import StudyService from "../../../services/StudyService";

const StudyPanel = ({ step, setStep }) => {
    const studyId = useSelector(state => state.study)
    const [study, setStudy] = useState({})
    const [varHeight, setVH] = useState({});

    const user = JSON.parse(localStorage.getItem('user'));

    const getStudy = async () => {
        let response = await StudyService.getStudy(studyId);
        let study = response.data.response[0];
        let vh = user.identity.role == 4 ? 0 : 60;
        // if (study.serviceType == 1) { //Socioeconomico

        // }
        // else if (study.serviceType == 2) {//Laboral

        // }
        if (study.fieldsToFill.resume === true) {
            vh += 60;
        }
        if (study.fieldsToFill.generalInformation === true) {
            vh += 60;
        }
        if (study.fieldsToFill.educationalLevel === true) {
            vh += 60;
        }
        if (study.fieldsToFill.family === true) {
            vh += 60;
        }
        if (study.fieldsToFill.economicSituation === true) {
            vh += 60;
        }
        if (study.fieldsToFill.social === true) {
            vh += 60;
        }
        if (study.fieldsToFill.workHistory === true) {
            vh += 60;
        }
        if (study.fieldsToFill.iMSSValidation === true) {
            vh += 60;
        }
        if (study.fieldsToFill.personalReferences === true) {
            vh += 60;
        }
        if (study.fieldsToFill.pictures === true) {
            vh += 60;
        }
        // if (vh > 0)
        //     vh -= 60;
        setVH(vh);
        setStudy(study);
    }

    // Get Study
    useEffect(() => {
        if (studyId) {
            getStudy();
        }
    }, [studyId])
    useEffect(() => {
    }, [step])
    
        return (
            <>
            {
            study && study.fieldsToFill && 
            <div className={"study-panel shadow"}>
                <div className={"study-vertical-line"} style={{ height: varHeight }}></div>
                <div className={"study-stepvbs"}>
                    <div className={"study-step"} style={{ display: (study.fieldsToFill.resume ? "" : "none") }} onClick={() => setStep(0)} >
                        <span className={step!==0 ? "step-icon" : "step-icon step-color"} />
                        <label>Resultados Finales</label>
                    </div>
                    <div className={"study-step"} style={{ display: (study.fieldsToFill.generalInformation ? "" : "none") }} onClick={() => setStep(1)}>
                        <span className={step!==1 ? "step-icon" : "step-icon step-color"} />
                        <label>Datos Generales</label>
                    </div>
                    <div className={"study-step"} style={{ display: (study.fieldsToFill.educationalLevel ? "" : "none") }} onClick={() => setStep(2)}>
                        <span className={step!==2 ? "step-icon" : "step-icon step-color"} />
                        <label>Escolaridad</label>
                    </div>
                    <div className={"study-step"} style={{ display: (study.fieldsToFill.family ? "" : "none") }} onClick={() => setStep(3)}>
                        <span className={step!==3 ? "step-icon" : "step-icon step-color"} />
                        <label>Familia</label>
                    </div>
                    <div className={"study-step"} style={{ display: (study.fieldsToFill.economicSituation ? "" : "none") }} onClick={() => setStep(4)}>
                        <span className={step!==4 ? "step-icon" : "step-icon step-color"} />
                        <label>Economía</label>
                    </div>
                    <div className={"study-step"} style={{ display: (study.fieldsToFill.social ? "" : "none") }} onClick={() => setStep(5)}>
                        <span className={step!==5 ? "step-icon" : "step-icon step-color"} />
                        <label>Social</label>
                    </div>
                    <div className={"study-step"} style={{ display: (study.fieldsToFill.workHistory ? "" : "none") }} onClick={() => setStep(6)}>
                        <span className={step!==6 ? "step-icon" : "step-icon step-color"} />
                        <label>Trayectoría Laboral</label>
                    </div>
                    <div className={"study-step"} style={{ display: (study.fieldsToFill.imssValidation ? "" : "none") }} onClick={() => setStep(7)}>
                        <span className={step!==7 ? "step-icon" : "step-icon step-color"} />
                        <label>Validación IMSS</label>
                    </div>
                    <div className={"study-step"} style={{ display: (study.fieldsToFill.personalReferences ? "" : "none") }} onClick={() => setStep(8)}>
                        <span className={step!==8 ? "step-icon" : "step-icon step-color"} />
                        <label>Referencia Personal</label>
                    </div>
                    <div className={"study-step"} style={{ display: (study.fieldsToFill.pictures ? "" : "none") }} onClick={() => setStep(9)}>
                        <span className={step!==9 ? "step-icon" : "step-icon step-color"} />
                        <label>Fotografías</label>
                    </div>
                    {user.identity.role == 4 ? null :
                    <div className={"study-step"} onClick={() => setStep(10)}>
                        <span className={step!==10 ? "step-icon" : "step-icon step-color"} />
                        <label>Completar</label>
                    </div>
                    }
                </div>
            </div>
                
            }
            </>
        )
}

export default StudyPanel