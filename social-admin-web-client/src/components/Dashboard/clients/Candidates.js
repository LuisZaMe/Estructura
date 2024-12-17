import React, {useEffect, useState} from "react";
import {useDispatch} from "react-redux";
import {useHistory} from "react-router-dom";

// Actions
import {registerCandidateFromClient, showRegisterCandidate} from "../../../actions";
// Services
import CandidateService from "../../../services/CandidateService";

const Candidates = ({show, handleClose}) => {
    const dispatch = useDispatch()
    const history = useHistory()

    const [candidates, setCandidates] = useState([])

    // eslint-disable-next-line react-hooks/exhaustive-deps
    useEffect(async () => {
        try {
            const response = await CandidateService.getCandidates()
            setCandidates(response.data.response)
        } catch (error) {
            console.log(error)
        }
    }, [])

    const renderCandidates = () => {
        return candidates.map((value, index) => {
            return <span key={index} className={"scroll-list-item"}>{value.name}</span>
        })
    }

    const addCandidate = () => {
        handleClose(false)
        dispatch(showRegisterCandidate())
    }

    const showHideModal = show ? "modal display-block" : "modal display-none"

    return (
        <div className={showHideModal}>
            <div className={"candidates-modal"}>
                <div className={"close-modal"}>
                    <img src={"/images/icon-close.png"} alt={""} onClick={() => handleClose(false)}/>
                </div>
                <h2>Candidatos</h2>
                <div className={"modal-content"}>
                    <div className={"scroll-list no-scrollbar"}>
                        {renderCandidates()}
                    </div>
                    <button className={"form-button-primary"} onClick={addCandidate}>Agregar</button>
                </div>
            </div>
        </div>
    )
}

export default Candidates