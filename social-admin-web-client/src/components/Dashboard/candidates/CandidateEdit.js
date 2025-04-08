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
                            <label className={"property"}>Nombre(s)</label>
                            <input 
                                className={"candidate-section-input"} 
                                value={candidate ? candidate.name : ""}
                                onChange={event => {
                                    const input = event.target.value;
                                    const cleanedInput = input.replace(/[^a-zA-ZáéíóúÁÉÍÓÚñÑ\s]/g, '');
                                    const validInput = cleanedInput.split(' ').filter(Boolean);
                                    if (validInput.length <= 2) {
                                        setCandidate({...candidate, name: cleanedInput});
                                    }
                                }}
                                onBlur={event => {
                                    const trimmedValue = candidate.name.trim();
                                    setCandidate({...candidate, name: trimmedValue});
                                }}
                                onKeyDown={event => {
                                    if (event.key === 'Enter') {
                                        const trimmedValue = candidate.name.trim();
                                        setCandidate({...candidate, name: trimmedValue});
                                    }
                                }}
                            />
                        </div>
                            <div className={"candidate-section-item"}>
                                <label className={"property"}>Telefono</label>
                                <input 
                                    className={"candidate-section-input"} 
                                    value={candidate ? candidate.phone : ""}
                                    onChange={event => {
                                        const input = event.target.value.replace(/\D/g, '');
                                        if (input.length <= 10) {
                                            setCandidate({...candidate, phone: input});
                                        }
                                    }}
                                />
                            </div>
                            <div className={"candidate-section-item"}>
                                <label className={"property"}>Correo</label>
                                <input 
                                    className={"candidate-section-input"} 
                                    value={candidate ? candidate.email : ""} 
                                    onChange={event => setCandidate({...candidate, email: event.target.value})}
                                    onBlur={event => {
                                        const emailPattern = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9-]+(\.[a-zA-Z]{2,})+$/;
                                        const doubleDotPattern = /\.{2,}/;
                                        const input = event.target.value;

                                        if ((!emailPattern.test(input) || doubleDotPattern.test(input)) && input !== '') {
                                            alert('Por favor, ingresa un correo válido.');
                                        }
                                    }}
                                />
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
                        <label className={"property"}>Apellido(s)</label>
                            <input 
                                className={"candidate-section-input"} 
                                value={candidate ? candidate.lastname : ""}
                                onChange={event => {
                                    const input = event.target.value;
                                    const cleanedInput = input.replace(/[^a-zA-ZáéíóúÁÉÍÓÚñÑ\s]/g, '');
                                    const validInput = cleanedInput.split(' ').filter(Boolean);
                                    if (validInput.length <= 2) {
                                        setCandidate({...candidate, lastname: cleanedInput});
                                    }
                                }}
                                onBlur={event => {
                                    const trimmedValue = candidate.lastname.trim();
                                    setCandidate({...candidate, lastname: trimmedValue});
                                }}
                                onKeyDown={event => {
                                    if (event.key === 'Enter') {
                                        const trimmedValue = candidate.lastname.trim();
                                        setCandidate({...candidate, lastname: trimmedValue});
                                    }
                                }}/>
                            </div>
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