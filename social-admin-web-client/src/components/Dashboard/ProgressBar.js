import React from "react";

const steps = [
    {
        id: 1,
        name: "Step 1",
        description: "Seleccionar Candidato"
    },
    {
        id: 2,
        name: "Step 2",
        description: "Seleccionar Estudio"
    },
    {
        id: 3,
        name: "Step 3",
        description: "Seleccionar Documentos"
    },
    {
        id: 4,
        name: "Step 4",
        description: "Campos a llenar"
    }
]

const ProgressBar = ({progress, onProgressChange}) => {
    const onStepClick = (step) => {
        if (step < progress) {
            onProgressChange(step)
        }
    }

    const renderSteps = () => {
        return steps.map(step => {
            return (
                <div key={step.name} className={`step ${step.id === progress ? "active" : ""}`}>
                    <button className={"step-icon"} type={"button"} onClick={() => onStepClick(step.id)}/>
                    <label className={"description"}>{step.description}</label>
                </div>
            )
        })
    }

    return (
        <div className={"progress-bar"}>
            <div className={"progress-line"}></div>
            <div className={"steps"}>
                {renderSteps()}
            </div>
        </div>
    )
}

export default ProgressBar