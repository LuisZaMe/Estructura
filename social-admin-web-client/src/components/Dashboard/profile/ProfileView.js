import React, { useEffect, useState } from "react";
import { useHistory } from "react-router-dom";
import { useSelector } from "react-redux";

// Services
import AccountService from "../../../services/AccountService";

const ProfileView = () => {
    const history = useHistory();
    const id = useSelector(state => state.client);

    const [client, setClient] = useState(null);
    const [user, setUser] = useState(null);

    useEffect(() => {
        const storedUser = localStorage.getItem('user');
        if (!storedUser) {
            console.error("No hay usuario en localStorage");
            return;
        }
    
        const user = JSON.parse(storedUser);
        console.log("ID del usuario que se buscará:", user.identity.id);
    
        if (!user.identity || !user.identity.id) {
            console.error("El usuario en localStorage no tiene un ID válido", user);
            return;
        }
    
        const getClient = async () => {
            try {
                const response = await AccountService.getAccount(user.identity.id);
                console.log("Respuesta de la API:", response.data);
                if (!response.data || !response.data.response || response.data.response.length === 0) {
                    console.error("La API no devolvió datos válidos.");
                    return;
                }
    
                if (response.data.response[0].role === 5) {
                    setClient(response.data.response[0]);
                } else {
                    setUser(response.data.response[0]);
                }
            } catch (error) {
                console.error("Error al obtener la cuenta:", error);
            }
        };
    
        getClient();
    }, [id]);    

    const onClickAccept = () => {
        history.push("/dashboard");
    };

    const onClickEdit = () => {
        history.push("/dashboard/profile/editar");
    };

    if (!client && !user) {
        return <div className="container">No se encontraron datos del perfil.</div>;
    }

    return (
        <div className={"container"}>
            <div className={"content client"}>
                <div className={"top-section"}>
                    <div className={"client-header"}>
                        <div className={"company-name"}>
                            <label className={"company-name-title"}>Nombre</label>
                            <label className={"company-name-value"}>
                                {client?.companyInformation?.companyName || 
                                 `${client?.name || user?.name || "Sin nombre"} ${client?.lastname || user?.lastname || ""}`.trim()}
                            </label>
                        </div>
                        <button className={"edit-client"} onClick={onClickEdit}>
                            <img src={"/images/actions-dropdown/edit.svg"} alt={"Editar"}/>
                            Editar
                        </button>
                    </div>
                </div>
                <div className={"main-section client shadow"}>
                    <div className={"client-view no-scrollbar"}>
                        <div className={"client-main-info"}>
                            <label className={"client-section-title"}>Datos principales</label>
                            <div className={"client-section-item"}>
                                <label className={"property"}>Nombre(s)</label>
                                <label className={"value"}>{client?.name || user?.name || "Sin datos"}</label>
                            </div>
                            <div className={"client-section-item"}>
                                <label className={"property"}>Apellido(s)</label>
                                <label className={"value"}>{client?.lastname || user?.lastname || "Sin datos"}</label>
                            </div>
                            <div className={"client-section-item"}>
                                <label className={"property"}>Teléfono</label>
                                <label className={"value"}>{client?.phone || user?.phone || "Sin datos"}</label>
                            </div>
                            <div className={"client-section-item"}>
                                <label className={"property"}>Correo</label>
                                <label className={"value"}>{client?.email || user?.email || "Sin datos"}</label>
                            </div>
                        </div>
                    </div>
                    <button className={"client-view-accept"} onClick={onClickAccept}>Aceptar</button>
                </div>
            </div>
        </div>
    );
};

export default ProfileView;
