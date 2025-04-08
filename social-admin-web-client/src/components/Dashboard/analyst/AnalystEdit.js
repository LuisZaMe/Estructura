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
                    <input
                        className={"analyst-section-input"}
                        type={"text"}
                        value={`${analyst?.name || ""} ${analyst?.lastname || ""}`}
                        onChange={event => {
                            const input = event.target.value;
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

                            setAnalyst({
                                ...analyst,
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

                            setAnalyst({
                                ...analyst,
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

                                setAnalyst({
                                    ...analyst,
                                    name: newName,
                                    lastname: newLastname
                                });
                            }
                        }}
                    />
                </div>
                <button className={"edit-analyst"} onClick={onClickView}>
                    <img src={"/images/actions-dropdown/edit.svg"} alt={""} />
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
                        <input
                            className={"analyst-section-input"}
                            type={"tel"}
                            value={analyst ? analyst.phone : ""}
                            maxLength={10}
                            onChange={event => {
                            const newPhone = event.target.value.replace(/[^0-9]/g, '');
                            if (newPhone.length <= 10) {
                                setAnalyst({
                                ...analyst,
                                phone: newPhone
                                });
                            }
                            }}
                        />
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