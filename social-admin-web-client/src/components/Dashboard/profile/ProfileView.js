import React, {useEffect, useState} from "react";
import {useHistory} from "react-router-dom";
import {useDispatch, useSelector} from "react-redux";

// Services
import AccountService from "../../../services/AccountService";

const ProfileView = () => {
    const history = useHistory()

    const id = useSelector(state => state.client)

    const [client, setClient] = useState(null)
    const [user, setUser] = useState(null)

    useEffect(() => {
        const user = JSON.parse(localStorage.getItem('user'));
        const getClient = async () => {
            try {
                const response = await AccountService.getAccount(user.identity.id)
                if (response.data.response[0].role === 5) {
                    setClient(response.data.response[0])
                } else {
                    const userData = response.data.response[0]
                    setUser(userData)
                }
            } catch (error) {
                console.log(error)
            }
        }
        getClient()
    }, [id])

    const onClickAccept = () => {
        history.push("/dashboard")
    }

    const onClickEdit = () => {
        history.push("/dashboard/profile/editar")
    }

    return (
        <div className={"container"}>
            <div className={"content client"}>
                <div className={"top-section"}>
                    <div className={"client-header"}>
                        <div className={"company-name"}>
                            <label className={"company-name-title"}>Nombre de empresa</label>
                            <label
                                className={"company-name-value"}>{client ? client.companyInformation.companyName : user ? user.name : ""}
                            </label>
                        </div>
                        <button className={"edit-client"} onClick={onClickEdit}>
                            <img src={"/images/actions-dropdown/edit.svg"} alt={""}/>
                            Editar
                        </button>
                    </div>
                </div>
                <div className={"main-section client shadow"}>
                    <div className={"client-view no-scrollbar"}>
                        <div className={"client-main-info"}>
                            <label className={"client-section-title"}>Datos principales</label>
                            <div className={"client-section-item"}>
                                <label className={"property"}>Nombre</label>
                                <label className={"value"}>{client ? client.name : user ? user.name : ""}</label>
                            </div>
                            <div className={"client-section-item"}>
                                <label className={"property"}>Telefono</label>
                                <label className={"value"}>{client ? client.phone : user ? user.phone : ""}</label>
                            </div>
                            <div className={"client-section-item"}>
                                <label className={"property"}>Correo</label>
                                <label className={"value"}>{client ? client.email : user ? user.email : ""}</label>
                            </div>
                        </div>
                    </div>
                    <button className={"client-view-accept"} onClick={onClickAccept}>Aceptar</button>
                </div>
            </div>
        </div>
    )
}

export default ProfileView