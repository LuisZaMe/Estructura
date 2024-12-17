import React, {useState} from "react";

// Components
import Economy from "./steps/Economy";
import Education from "./steps/Education";
import Family from "./steps/Family";
import GeneralData from "./steps/GeneralData";
import ImssValidation from "./steps/ImssValidation";
import PersonalReferences from "./steps/PersonalReferences";
import Pictures from "./steps/Pictures";
import ResultSocioeconomic from "./steps/ResultSocioeconomic";
import Social from "./steps/Social";
import StudyPanel from "./StudyPanel";
import WorkHistory from "./steps/WorkHistory";
import FinalStep from "./steps/FinalStep";

const StudyView = () => {
    const [step, setStep] = useState(0)

    return (
        <div className={"container"}>
            <div className={"content view-study"}>
                <div className={"content view-study"}>
                    <StudyPanel step={step} setStep={setStep}/>
                </div>
                <div className={"study-body shadow"}>
                    {step === 0 ? <ResultSocioeconomic/> : null}
                    {step === 1 ? <GeneralData/> : null}
                    {step === 2 ? <Education/> : null}
                    {step === 3 ? <Family/> : null}
                    {step === 4 ? <Economy/> : null}
                    {step === 5 ? <Social/> : null}
                    {step === 6 ? <WorkHistory/> : null}
                    {step === 7 ? <ImssValidation/> : null}
                    {step === 8 ? <PersonalReferences/> : null}
                    {step === 9 ? <Pictures/> : null}
                    {step === 10 ? <FinalStep/> : null}
                </div>
            </div>
        </div>
    )
}

export default StudyView