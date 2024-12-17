import React from "react";

const DeleteWorkHistory = ({show, handleClose, handleDelete}) => {
    const showHideModal = show ? "modal display-block" : "modal display-none"

    return (
        <div className={showHideModal}>
            <div className={"form-remove-work-history"}>
                <h2>¿Seguro de querer eliminar<br/>trayectoria laboral?</h2>
                <div className={"confirm-actions"}>
                    <button onClick={() => handleClose(false)}>No</button>
                    <button onClick={() => {
                        handleClose(false)
                        handleDelete()
                    }}>Si</button>
                </div>
            </div>
        </div>
    )
}

export default DeleteWorkHistory