import React from "react";
import AccountService from "../../../services/AccountService";
import CandidateService from "../../../services/CandidateService";
import VisitService from "../../../services/VisitService";
import { useHistory } from "react-router-dom";

const DeleteAccountForm = ({show, handleClose, handleDelete, id, userType}) => {
    const showHideModal = show ? "modal display-block" : "modal display-none"
    const history = useHistory()

    const renderUserText = () => {
        switch (userType) {
            case "admin":
                return "administrador"
            case "analyst":
                return "analista"
            case "visit":
                return "visita"
            case "candidate":
                return "candidato"
            case "client":
                return "cliente"
            case "interviewer":
                return "entrevistador"
            default:
                return "usuario"
        }
    }

    const deleteAccount = async () => {
        try {
            if (userType === "candidate") {
                await CandidateService.delete(id)
            } else if (userType === "visit") {
                await VisitService.delete(id)
            } else {
                await AccountService.delete(id)
            }
            handleDelete()
        } catch (error) {
            handleDelete()
        }
        handleClose(false)
    }

    return (
        <div className={showHideModal}>
            <div className={"form-delete-user"}>
                <h2>¿Seguro de querer<br/>eliminar {renderUserText()}?</h2>
                <div className={"confirm-actions"}>
                    <button onClick={() => handleClose(false)}>No</button>
                    <button onClick={deleteAccount}>Si</button>
                </div>
            </div>
        </div>
    )
}

export default DeleteAccountForm