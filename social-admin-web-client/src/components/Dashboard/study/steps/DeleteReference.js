import React from "react";

const DeleteReference = ({show, handleClose, handleDelete}) => {
    const showHideModal = show ? "modal display-block" : "modal display-none"

    return (
        <div className={showHideModal}>
            <div className={"form-remove-reference"}>
                <h2>¿Seguro de querer eliminar<br/>referencia?</h2>
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

export default DeleteReference