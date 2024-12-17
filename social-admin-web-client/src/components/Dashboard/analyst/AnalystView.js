import React, {useEffect, useState} from "react";
import {useHistory} from "react-router-dom";
import {useDispatch, useSelector} from "react-redux";

// Actions
import {removeAnalystId, showAssignStudy, showAssignedStudies} from "../../../actions";

// Components
import AssignedStudies from "./AssignedStudies";
import AssignStudy from "./AssignStudy";
import AssignStudy2 from "./AssignStudy2";

// Services
import AccountService from "../../../services/AccountService";

const AnalystView = () => {
    const dispatch = useDispatch()
    const history = useHistory()

    const id = useSelector(state => state.analyst)

    const [analyst, setAnalyst] = useState(null)

    useEffect(() => {
        const getAnalyst = async () => {
            try {
                const response = await AccountService.getAccount(id)
                setAnalyst(response.data.response[0])
            } catch (error) {
                console.log(error)
            }
        }
        getAnalyst()
    }, [id])

    const onClickAccept = () => {
        dispatch(removeAnalystId())
        history.push("/dashboard/analistas")
    }

    const onClickEdit = () => {
        history.push("/dashboard/analistas/editar")
    }

    return (
        <div className={"container"}>
            <div className={"content analyst"}>
                <div className={"top-section"}>
                    <div className={"analyst-header"}>
                        <div className={"analyst-name"}>
                            <label className={"analyst-name-title"}>Nombre del analista</label>
                            <label className={"analyst-name-value"}>{analyst ? analyst.name : null}</label>
                        </div>
                        <button className={"edit-analyst"} onClick={onClickEdit}>
                            <img src={"/images/actions-dropdown/edit.svg"} alt={""}/>
                            Editar
                        </button>
                    </div>
                </div>
                <div className={"main-section analyst shadow"}>
                    <div className={"analyst-view"}>
                        <label className={"analyst-section-title"}>Datos principales</label>
                        <div className={"analyst-main-info"}>
                            <div className={"analyst-section-item"}>
                                <label className={"property"}>Telefono</label>
                                <label className={"value"}>{analyst ? analyst.phone : null}</label>
                            </div>
                            <div className={"analyst-section-item"}>
                                <label className={"property"}>Correo</label>
                                <label className={"value"}>{analyst ? analyst.email : null}</label>
                            </div>
                            <div className={"analyst-section-study"}>
                                {/*<button onClick={() => dispatch(showAssignStudy())}>Asignar estudio</button>*/}
                                {/*<AssignStudy/>*/}

                                <button onClick={() => dispatch(showAssignStudy())}>Asignar estudio</button>
                                <AssignStudy2 />
                                <button onClick={() => dispatch(showAssignedStudies())}>Estudios asignados</button>
                                <AssignedStudies/>
                            </div>
                        </div>
                    </div>
                    <button className={"analyst-view-accept"} onClick={onClickAccept}>Aceptar</button>
                </div>
            </div>
        </div>
    )
}

export default AnalystView