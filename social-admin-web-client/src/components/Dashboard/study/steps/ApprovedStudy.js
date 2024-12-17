import React from "react";
import {useHistory} from "react-router-dom";
import {useDispatch} from "react-redux";

// Actions
import {removeStudyId} from "../../../../actions";

const ApprovedStudy = () => {
    const dispatch = useDispatch()
    const history = useHistory()

    return (
        <div className={"container"}>
            <div className={"study-generated"}>
                <img src={"/images/study-generated.png"} alt={""}/>
                <h2>Estudio autorizado exitosamente</h2>
                <button className={"study-button"} onClick={() => {
                    // Remove study id
                    dispatch(removeStudyId())
                    history.push("/dashboard/validaciones")
                }}>Publicar al cliente
                </button>
            </div>
        </div>
    )
}

export default ApprovedStudy