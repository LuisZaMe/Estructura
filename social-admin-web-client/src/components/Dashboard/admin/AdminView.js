import React, {useEffect, useState} from "react";
import {useHistory} from "react-router-dom";
import {useDispatch, useSelector} from "react-redux";

// Actions
import {removeAdminId} from "../../../actions";

// Services
import AccountService from "../../../services/AccountService";

const AdminView = () => {
    const dispatch = useDispatch()
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

    const onClickAccept = () => {
        dispatch(removeAdminId())
        history.push("/dashboard/administrador")
    }

    const onClickEdit = () => {
        history.push("/dashboard/administrador/editar")
    }

    return (
        <div className={"container"}>
            <div className={"content admin"}>
                <div className={"top-section"}>
                    <div className={"admin-header"}>
                        <div className={"admin-name"}>
                            <label className={"admin-name-title"}>Nombre del administrador</label>
                            <label className={"admin-name-value"}>{admin ? admin.name : null}</label>
                        </div>
                        <button className={"edit-admin"} onClick={onClickEdit}>
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
                                <label className={"value"}>{admin ? admin.phone : null}</label>
                            </div>
                            <div className={"admin-section-item"}>
                                <label className={"property"}>Correo</label>
                                <label className={"value"}>{admin ? admin.email : null}</label>
                            </div>
                        </div>
                    </div>
                    <button className={"admin-view-accept"} onClick={onClickAccept}>Aceptar</button>
                </div>
            </div>
        </div>
    )
}

export default AdminView