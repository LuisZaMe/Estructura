import React, { useEffect, useState } from "react";
import { useHistory } from "react-router-dom";
import { useDispatch, useSelector } from "react-redux";

// Services
import AccountService from "../../../services/AccountService";

const ProfileEdit = () => {
    const dispatch = useDispatch();
    const history = useHistory();
    const id = useSelector((state) => state.client);

    const [data, setData] = useState(null); // Se unifica client y user en un solo estado

    useEffect(() => {
        const user = JSON.parse(localStorage.getItem("user"));
        const getClient = async () => {
            try {
                const response = await AccountService.getAccount(user.identity.id);
                setData(response.data.response[0]); // Guarda los datos directamente
            } catch (error) {
                console.log(error);
            }
        };
        getClient();
    }, [id]);

    const saveChanges = async () => {
        try {
            if (data) {
                await AccountService.update(data);
            }
            history.push("/dashboard/profile");
        } catch (error) {
            console.log(error);
        }
    };

    const onChangeHandler = (event) => {
        const { name, value } = event.target;
        setData((prev) => ({
            ...prev,
            [name]: value,
        }));
    };

    return (
        <div className="container">
            <div className="content client">
                {data && data.role === 5 && (
                    <div className="top-section">
                        <div className="client-header">
                            <div className="company-name">
                                <label className="company-name-title">Nombre de Analista</label>
                                <input
                                    className="client-section-input"
                                    type="text"
                                    value={data.companyInformation?.companyName || ""}
                                    disabled
                                />
                            </div>
                            <button className="edit-client" onClick={() => history.push("/dashboard/profile")}>
                                <img src="/images/actions-dropdown/edit.svg" alt="Editar" />
                                Editar
                            </button>
                        </div>
                    </div>
                )}
                <div className="main-section client shadow">
                    <div className="client-edit no-scrollbar">
                        <div className="client-main-info">
                            <label className="client-section-title">Datos principales</label>
                            
                            {/* Nombre */}
                            <div className="client-section-item">
                                <label className="property">Nombre(s)</label>
                                <input
                                    className="client-section-input"
                                    type="text"
                                    name="name"
                                    value={data?.name || ""}
                                    onChange={onChangeHandler}
                                />
                            </div>

                            {/* Apellido */}
                            <div className="client-section-item">
                                <label className="property">Apellido(s)</label>
                                <input
                                    className="client-section-input"
                                    type="text"
                                    name="lastname"
                                    value={data?.lastname || ""}
                                    onChange={onChangeHandler}
                                />
                            </div>

                            {/* Teléfono */}
                            <div className="client-section-item">
                                <label className="property">Teléfono</label>
                                <input
                                    className="client-section-input"
                                    type="text"
                                    name="phone"
                                    value={data?.phone || ""}
                                    onChange={onChangeHandler}
                                />
                            </div>

                            {/* Correo */}
                            <div className="client-section-item">
                                <label className="property">Correo</label>
                                <input
                                    className="client-section-input"
                                    type="email"
                                    name="email"
                                    value={data?.email || ""}
                                    onChange={onChangeHandler}
                                />
                            </div>
                        </div>
                    </div>
                    <button className="client-view-accept" onClick={saveChanges}>
                        Guardar
                    </button>
                </div>
            </div>
        </div>
    );
};

export default ProfileEdit;
