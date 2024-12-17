import React, {useEffect, useState} from "react";
import {useHistory} from "react-router-dom";
import {useSelector} from "react-redux";

// Services
import CandidateService from "../../../services/CandidateService";

const CandidateEdit = () => {
    const history = useHistory()

    const id = useSelector(state => state.candidate)

    const [candidate, setCandidate] = useState(null)

    useEffect(() => {
        const getCandidate = async () => {
            try {
                const response = await CandidateService.getCandidate(id)
                setCandidate(response.data.response[0])
            } catch (error) {
                console.log(error)
            }
        }
        getCandidate()
    }, [id])

    const saveChanges = async () => {
        try {
            await CandidateService.update(candidate)
            history.push("/dashboard/candidatos/ver")
        } catch (error) {
            console.log(error)
        }
    }


    useEffect(() => {
    }, [candidate])
    
    const onClickView = () => {
        history.push("/dashboard/candidatos/ver")
    }

    return (
        <div className={"container"}>
            <div className={"content candidate"}>
                <div className={"top-section"}>
                    <div className={"candidate-header"}>
                        <div className={"company-name"}>
                            <label className={"company-name-title"}>Nombre del cliente</label>
                            <input className={"client-section-input"} value={candidate ? candidate.client.companyInformation ? candidate.client.companyInformation.companyName : "" : ""} />
                        </div>
                        <button className={"edit-candidate"} onClick={onClickView}>
                            <img src={"/images/actions-dropdown/edit.svg"} alt={""}/>
                            Editar
                        </button>
                    </div>
                </div>
                <div className={"main-section candidate shadow"}>
                    <div className={"candidate-view"}>
                        <div>
                            <div className={"candidate-section-item"}>
                                <label className={"property"}>Nombre del candidato</label>
                                <input className={"candidate-section-input"} value={candidate ? candidate.name : ""}
                                       onChange={event => setCandidate({...candidate, name: event.target.value})}/>
                            </div>
                            <div className={"candidate-section-item"}>
                                <label className={"property"}>Telefono</label>
                                <input className={"candidate-section-input"} value={candidate ? candidate.phone : ""}
                                       onChange={event => setCandidate({...candidate, phone: event.target.value})}/>
                            </div>
                            <div className={"candidate-section-item"}>
                                <label className={"property"}>Correo</label>
                                <input className={"candidate-section-input"} value={candidate ? candidate.email : ""}
                                    onChange={event => setCandidate({...candidate, email: event.target.value})}/>
                            </div>
                            <div className={"candidate-section-item"}>
                                <label className={"property"}>CURP</label>
                                <input className={"candidate-section-input"} value={candidate ? candidate.curp : ""}
                                       onChange={event => setCandidate({...candidate, curp: event.target.value})}/>
                            </div>
                            <div className={"candidate-section-item"}>
                                <label className={"property"}>NSS</label>
                                <input className={"candidate-section-input"} value={candidate ? candidate.nss : ""}
                                       onChange={event => setCandidate({...candidate, nss: event.target.value})}/>
                            </div>
                        </div>
                        <div>
                            <div className={"candidate-section-item"}>
                                <label className={"property"}>Estado</label>
                                <input className={"candidate-section-input"}
                                       value={candidate ? (candidate.state ? candidate.state.name : "") : ""} 
                                       onChange={event => setCandidate({...candidate, state : {...candidate.state, name: event.target.value} })}/>
                            </div>
                            <div className={"candidate-section-item"}>
                                <label className={"property"}>Ciudad</label>
                                <input className={"candidate-section-input"}
                                       value={candidate ? (candidate.city ? candidate.city.name : "") : ""} 
                                       onChange={event => setCandidate({...candidate, city : {...candidate.city, name: event.target.value} })}/>
                            </div>
                            <div className={"candidate-section-item"}>
                                <label className={"property"}>Domicilio</label>
                                <input className={"candidate-section-input"} value={candidate ? candidate.address : ""}
                                       onChange={event => setCandidate({...candidate, address: event.target.value})}/>
                            </div>
                            <div className={"candidate-section-item"}>
                                <label className={"property"}>Puesto</label>
                                <input className={"candidate-section-input"} value={candidate ? candidate.position : ""}
                                       onChange={event => setCandidate({...candidate, position: event.target.value})}/>
                            </div>
                        </div>
                    </div>
                    <button className={"candidate-view-accept"} onClick={saveChanges}>Guardar</button>
                </div>
            </div>
        </div>
    )
}

export default CandidateEdit