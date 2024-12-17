import React, {useEffect, useState} from "react";
import {useHistory} from "react-router-dom";
import {useSelector} from "react-redux";

// Services
import AccountService from "../../../services/AccountService";

const AnalystEdit = () => {
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

    const saveChanges = async () => {
        try {
            await AccountService.update(analyst)
            history.push("/dashboard/analistas/ver")
        } catch (error) {
            console.log(error)
        }
    }

    const onClickView = () => {
        history.push("/dashboard/analistas/ver")
    }

    return (
        <div className={"container"}>
            <div className={"content analyst"}>
                <div className={"top-section"}>
                    <div className={"analyst-header"}>
                        <div className={"analyst-name"}>
                            <label className={"analyst-name-title"}>Nombre del analista</label>
                            <input className={"analyst-section-input"} type={"text"} value={analyst ? analyst.name : ""}
                                   onChange={event => setAnalyst({
                                       ...analyst,
                                       name: event.target.value
                                   })}/>
                        </div>
                        <button className={"edit-analyst"} onClick={onClickView}>
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
                                <input className={"analyst-section-input"} type={"tel"}
                                       value={analyst ? analyst.phone : ""}
                                       onChange={event => setAnalyst({
                                           ...analyst,
                                           phone: event.target.value
                                       })}/>
                            </div>
                            <div className={"analyst-section-item"}>
                                <label className={"property"}>Correo</label>
                                <input className={"analyst-section-input"} type={"email"}
                                       value={analyst ? analyst.email : ""}
                                       disabled/>
                            </div>
                        </div>
                    </div>
                    <button className={"analyst-view-accept"} onClick={saveChanges}>Guardar</button>
                </div>
            </div>
        </div>
    )
}

export default AnalystEdit