import React, {useEffect, useState} from "react";
import {useHistory} from "react-router-dom";
import {useDispatch, useSelector} from "react-redux";

// Actions
import {removeCandidateId} from "../../../actions";

// Services
import CandidateService from "../../../services/CandidateService";

const CandidateView = () => {
    const dispatch = useDispatch()
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

    const onClickAccept = () => {
        dispatch(removeCandidateId())
        history.push("/dashboard/candidatos")
    }

    const onClickEdit = () => {
        history.push("/dashboard/candidatos/editar")
    }

    return (
        <div className={"container"}>
            <div className={"content candidate"}>
                <div className={"top-section"}>
                    <div className={"candidate-header"}>
                        <div className={"company-name"}>
                            <label className={"company-name-title"}>Nombre del cliente</label>
                            <label className={"company-name-value"}>{candidate ? candidate.client.companyInformation ? candidate.client.companyInformation.companyName : "" : ""}</label>
                        </div>
                        <button className={"edit-candidate"} onClick={onClickEdit}>
                            <img src={"/images/actions-dropdown/edit.svg"} alt={""}/>
                            Editar
                        </button>
                    </div>
                </div>
                <div className={"main-section candidate shadow"}>
                    <div className={"candidate-view"}>
                        <div>
                            <div className={"candidate-section-item"}>
                                <label className={"property"}>Nombre(s) del candidato</label>
                                <label className={"value"}>{candidate ? candidate.name : null}</label>
                            </div>
                            <div className={"candidate-section-item"}>
                                <label className={"property"}>Telefono</label>
                                <label className={"value"}>{candidate ? candidate.phone : null}</label>
                            </div>
                            <div className={"candidate-section-item"}>
                                <label className={"property"}>Correo</label>
                                <label className={"value"}>{candidate ? candidate.email : null}</label>
                            </div>
                            <div className={"candidate-section-item"}>
                                <label className={"property"}>CURP</label>
                                <label className={"value"}>{candidate ? candidate.curp : null}</label>
                            </div>
                            <div className={"candidate-section-item"}>
                                <label className={"property"}>NSS</label>
                                <label className={"value"}>{candidate ? candidate.nss : null}</label>
                            </div>
                        </div>
                        <div>
                        <div className={"candidate-section-item"}>
                                <label className={"property"}>Apellido(s) del candidato</label>
                                <label className={"value"}>{candidate ? candidate.lastname : null}</label>
                            </div>
                            <div className={"candidate-section-item"}>
                                <label className={"property"}>Estado</label>
                                <label
                                    className={"value"}>{candidate ? (candidate.state ? candidate.state.name : null) : null}</label>
                            </div>
                            <div className={"candidate-section-item"}>
                                <label className={"property"}>Ciudad</label>
                                <label
                                    className={"value"}>{candidate ? (candidate.city ? candidate.city.name : null) : null}</label>
                            </div>
                            <div className={"candidate-section-item"}>
                                <label className={"property"}>Domicilio</label>
                                <label className={"value"}>{candidate ? candidate.address : null}</label>
                            </div>
                            <div className={"candidate-section-item"}>
                                <label className={"property"}>Puesto</label>
                                <label className={"value"}>{candidate ? candidate.position : null}</label>
                            </div>
                        </div>
                    </div>
                    <button className={"candidate-view-accept"} onClick={onClickAccept}>Aceptar</button>
                </div>
            </div>
        </div>
    )
}

export default CandidateView