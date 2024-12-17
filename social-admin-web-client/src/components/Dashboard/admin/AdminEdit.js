import React, {useEffect, useState} from "react";
import {useHistory} from "react-router-dom";
import {useSelector} from "react-redux";

// Services
import AccountService from "../../../services/AccountService";

const AdminEdit = () => {
    const history = useHistory()

    const id = useSelector(state => state.admin)

    const [admin, setAdmin] = useState(null)

    useEffect(() => {
        const getAdmin = async () => {
            try {
                const response = await AccountService.getAccount(id)
                setAdmin(response.data.response[0])
            } catch (error) {
                console.log(error)
            }
        }
        getAdmin()
    }, [id])
    
    const saveChanges = async () => {
        try {
            await AccountService.update(admin)
            history.push("/dashboard/administrador/ver")
        } catch (error) {
            console.log(error)
        }
    }

    const onClickView = () => {
        history.push("/dashboard/administrador/ver")
    }

    return (
        <div className={"container"}>
            <div className={"content admin"}>
                <div className={"top-section"}>
                    <div className={"admin-header"}>
                        <div className={"admin-name"}>
                            <label className={"admin-name-title"}>Nombre del administrador</label>
                            <input className={"admin-section-input"} type={"text"} value={admin ? admin.name : ""}
                                   onChange={event => setAdmin({
                                       ...admin,
                                       name: event.target.value
                                   })}/>
                        </div>
                        <button className={"edit-admin"} onClick={onClickView}>
                            <img src={"/images/actions-dropdown/edit.svg"} alt={""}/>
                            Editar
                        </button>
                    </div>
                </div>
                <div className={"main-section admin shadow"}>
                    <div className={"admin-view"}>
                        <label className={"admin-section-title"}>Datos principales</label>
                        <div className={"admin-main-info"}>
                            <div className={"admin-section-item"}>
                                <label className={"property"}>Telefono</label>
                                <input className={"admin-section-input"} type={"tel"} value={admin ? admin.phone : ""}
                                       onChange={event => setAdmin({
                                           ...admin,
                                           phone: event.target.value
                                       })}/>
                            </div>
                            <div className={"admin-section-item"}>
                                <label className={"property"}>Correo</label>
                                <input className={"admin-section-input"} type={"email"} value={ admin ? admin.email : ""}
                                       disabled/>
                            </div>
                        </div>
                    </div>
                    <button className={"admin-view-accept"} onClick={saveChanges}>Guardar</button>
                </div>
            </div>
        </div>
    )
}

export default AdminEdit