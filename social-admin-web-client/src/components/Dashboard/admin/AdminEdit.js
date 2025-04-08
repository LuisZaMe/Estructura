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
                    <input
                        className={"admin-section-input"}
                        type={"text"}
                        value={`${admin?.name || ""} ${admin?.lastname || ""}`}
                        onChange={event => {
                            const input = event.target.value;
                            // Filtrando solo letras y espacios
                            const cleanedInput = input.replace(/[^a-zA-ZáéíóúÁÉÍÓÚñÑ\s]/g, '');
                            const words = cleanedInput.split(" ");
                            let newName, newLastname;

                            if (words.length >= 4) {
                                newName = words.slice(0, words.length - 2).join(" ");
                                newLastname = words.slice(-2).join(" ");
                            } else if (words.length === 3) {
                                newName = words[0];
                                newLastname = words.slice(1).join(" ");
                            } else {
                                newName = words.slice(0, words.length - 1).join(" ");
                                newLastname = words.length > 1 ? words[words.length - 1] : "";
                            }

                            setAdmin({
                                ...admin,
                                name: newName,
                                lastname: newLastname
                            });
                        }}
                        onBlur={(event) => {
                            const trimmedValue = event.target.value.trim();
                            const words = trimmedValue.split(" ");
                            let newName, newLastname;

                            if (words.length >= 4) {
                                newName = words.slice(0, words.length - 2).join(" ");
                                newLastname = words.slice(-2).join(" ");
                            } else if (words.length === 3) {
                                newName = words[0];
                                newLastname = words.slice(1).join(" ");
                            } else {
                                newName = words.slice(0, words.length - 1).join(" ");
                                newLastname = words.length > 1 ? words[words.length - 1] : "";
                            }

                            setAdmin({
                                ...admin,
                                name: newName,
                                lastname: newLastname
                            });
                        }}
                        onKeyDown={(event) => {
                            if (event.key === 'Enter') {
                                const trimmedValue = event.target.value.trim();
                                const words = trimmedValue.split(" ");
                                let newName, newLastname;

                                if (words.length >= 4) {
                                    newName = words.slice(0, words.length - 2).join(" ");
                                    newLastname = words.slice(-2).join(" ");
                                } else if (words.length === 3) {
                                    newName = words[0];
                                    newLastname = words.slice(1).join(" ");
                                } else {
                                    newName = words.slice(0, words.length - 1).join(" ");
                                    newLastname = words.length > 1 ? words[words.length - 1] : "";
                                }

                                setAdmin({
                                    ...admin,
                                    name: newName,
                                    lastname: newLastname
                                });
                            }
                        }}
                    />
                </div>

                <button className={"edit-admin"} onClick={onClickView}>
                    <img src={"/images/actions-dropdown/edit.svg"} alt={""} />
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
                            <input
                                className={"admin-section-input"}
                                type={"tel"}
                                value={admin ? admin.phone : ""}
                                onChange={event => {
                                    const input = event.target.value;
                                    const cleanedInput = input.replace(/[^0-9]/g, '');
                                    if (cleanedInput.length <= 10) {
                                        setAdmin({
                                            ...admin,
                                            phone: cleanedInput
                                        });
                                    }
                                }}
                            />
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