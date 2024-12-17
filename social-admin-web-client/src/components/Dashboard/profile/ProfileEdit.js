import React, {useEffect, useState} from "react"
import {useHistory} from "react-router-dom";
import {useDispatch, useSelector} from "react-redux";

// Services
import AccountService from "../../../services/AccountService";

const ProfileEdit = () => {
    const dispatch = useDispatch()
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

    const saveChanges = async () => {
        try {
            if (user.role === 5) {
                await AccountService.update(client)
            } else {
                await AccountService.update(user)
            }
            history.push("/dashboard/profile")
        } catch (error) {
            await AccountService.update(client)
            history.push("/dashboard/profile")
            console.log(error)
        }
    }

    const onClickView = () => {
        history.push("/dashboard/profile")
    }

    return (
        <div className={"container"}>
            <div className={"content client"}>
                {
                    client ? 
                        <div className={"top-section"}>
                            <div className={"client-header"}>
                                <div className={"company-name"}>
                                    <label className={"company-name-title"}>Nombre de empresa</label>
                                    <input className={"client-section-input"} type={"text"}
                                        value={client ? client.companyInformation.companyName : ""} disabled/>
                                </div>
                                <button className={"edit-client"} onClick={onClickView}>
                                    <img src={"/images/actions-dropdown/edit.svg"} alt={""}/>
                                    Editar
                                </button>
                            </div>
                        </div>
                    : <div></div>
                }
                <div className={"main-section client shadow"}>
                    <div className={"client-edit no-scrollbar"}>
                        <div className={"client-main-info"}>
                            <label className={"client-section-title"}>Datos principales</label>
                            <div className={"client-section-item"}>
                                <label className={"property"}>Nombre</label>
                                {
                                    client ? 
                                    <input className={"client-section-input"} type={"text"}
                                            value={client ? client.name : user ? user.name : ""}
                                            onChange={event => setClient({
                                                ...client,
                                                name: event.target.value
                                            })}/>
                                    :
                                    <input className={"client-section-input"} type={"text"}
                                    value={user ? user.name : ""}
                                    onChange={event => setUser({
                                        ...user,
                                        name: event.target.value
                                    })}/>
                                }
                            </div>
                            <div className={"client-section-item"}>
                                <label className={"property"}>Telefono</label>
                                {
                                    client ? 
                                    <input className={"client-section-input"} type={"text"}
                                    value={client ? client.phone : user ? user.phone : ""}
                                    onChange={event => setClient({
                                        ...client,
                                        phone: event.target.value
                                    })}/>
                                    :
                                    <input className={"client-section-input"} type={"text"}
                                    value={client ? client.phone : user ? user.phone : ""}
                                    onChange={event => setUser({
                                        ...user,
                                        phone: event.target.value
                                    })}/>
                                }
                            </div>
                            <div className={"client-section-item"}>
                                <label className={"property"}>Correo</label>
                                {
                                    client ? 
                                    <input className={"client-section-input"} type={"email"}
                                        value={client ? client.email : user ? user.email : ""}
                                        onChange={event => setClient({
                                        ...client,
                                        email: event.target.value
                                    })}/>
                                    :
                                    <input className={"client-section-input"} type={"email"}
                                        value={client ? client.email : user ? user.email : ""}
                                        onChange={event => setUser({
                                        ...user,
                                        email: event.target.value
                                    })}/>
                                }
                            </div>
                        </div>
                    </div>
                    <button className={"client-view-accept"} onClick={saveChanges}>Guardar</button>
                </div>
            </div>
        </div>
    )
}

export default ProfileEdit