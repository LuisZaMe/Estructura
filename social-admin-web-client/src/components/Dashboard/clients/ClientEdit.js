import React, {useEffect, useState} from "react"
import {useHistory} from "react-router-dom";
import {useDispatch, useSelector} from "react-redux";

// Services
import AccountService from "../../../services/AccountService";
// Actions
import {removeClientId} from "../../../actions";

const ClientEdit = () => {
    const dispatch = useDispatch()
    const history = useHistory()

    const id = useSelector(state => state.client)

    const [client, setClient] = useState(null)

    useEffect(() => {
        const getClient = async () => {
            try {
                const response = await AccountService.getAccount(id)
                setClient(response.data.response[0])
            } catch (error) {
                console.log(error)
            }
        }
        getClient()
    }, [id])

    const saveChanges = async () => {
        try {
            await AccountService.update(client)
            history.push("/dashboard/clientes/ver")
        } catch (error) {
            console.log(error)
        }
    }

    const onClickView = () => {
        dispatch(removeClientId())
        history.push("/dashboard/clientes/ver")
    }

    
    return (
        <div className={"container"}>
            <div className={"content client"}>
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
                <div className={"main-section client shadow"}>
                    <div className={"client-edit no-scrollbar"}>
                        <div className={"client-main-info"}>
                            <label className={"client-section-title"}>Datos principales</label>
                            <div className={"client-section-item"}>
                                <label className={"property"}>Telefono empresa</label>
                                <input
                                    className={"client-section-input"}
                                    type={"tel"}
                                    value={client ? client.companyInformation.companyPhone : ""}
                                    maxLength={10}
                                    onChange={event => {
                                        const input = event.target.value.replace(/\D/g, '');
                                        setClient(prevClient => ({
                                            ...prevClient,
                                            companyInformation: {
                                                ...prevClient.companyInformation,
                                                companyPhone: input
                                            }
                                        }));
                                    }}
                                />
                            </div>
                            <div className={"client-section-item"}>
                                <label className={"property"}>Nombre(s) responsable</label>
                                <input
                                    className={"client-section-input"}
                                    type={"text"}
                                    value={client ? client.name : ""}
                                    onChange={event => {
                                        const input = event.target.value;
                                        const cleanedInput = input.replace(/[^a-zA-ZáéíóúÁÉÍÓÚñÑ\s]/g, '');
                                        const validInput = cleanedInput.split(' ').filter(Boolean);
                                        if (validInput.length <= 2) {
                                            setClient({
                                                ...client,
                                                name: cleanedInput
                                            });
                                        }
                                    }}
                                    onBlur={event => {
                                        const trimmedValue = client.name.trim();
                                        setClient({
                                            ...client,
                                            name: trimmedValue
                                        });
                                    }}
                                    onKeyDown={event => {
                                        if (event.key === 'Enter') {
                                            const trimmedValue = client.name.trim();
                                            setClient({
                                                ...client,
                                                name: trimmedValue
                                            });
                                        }
                                    }}
                                />
                            </div>
                            <div className={"client-section-item"}>
                                <label className={"property"}>Apellido(s) responsable</label>
                                <input
                                    className={"client-section-input"}
                                    type={"text"}
                                    value={client ? client.lastname : ""}
                                    onChange={event => {
                                        const input = event.target.value;
                                        const cleanedInput = input.replace(/[^a-zA-ZáéíóúÁÉÍÓÚñÑ\s]/g, '');
                                        const validInput = cleanedInput.split(' ').filter(Boolean);
                                        setClient({
                                            ...client,
                                            lastname: cleanedInput
                                        });
                                    }}
                                    onBlur={event => {
                                        const trimmedValue = client.lastname.trim();
                                        setClient({
                                            ...client,
                                            lastname: trimmedValue
                                        });
                                    }}
                                    onKeyDown={event => {
                                        if (event.key === 'Enter') {
                                            const trimmedValue = client.lastname.trim();
                                            setClient({
                                                ...client,
                                                lastname: trimmedValue
                                            });
                                        }
                                    }}
                                />
                            </div>

                            <div className={"client-section-item"}>
                                <label className={"property"}>Telefono responsable</label>
                                <input
                                    className={"client-section-input"}
                                    type={"text"}
                                    value={client ? client.phone : ""}
                                    maxLength={10}
                                    onChange={event => {
                                        const input = event.target.value.replace(/\D/g, '');
                                        if (input.length <= 10) {
                                            setClient({
                                                ...client,
                                                phone: input
                                            });
                                        }
                                    }}
                                />
                            </div>
                            <div className={"client-section-item"}>
                                <label className={"property"}>Correo</label>
                                <input
                                    className={"client-section-input"}
                                    type={"email"}
                                    value={client ? client.email : ""}
                                    onChange={event => setClient({
                                        ...client,
                                        email: event.target.value
                                    })}
                                    onBlur={event => {
                                        const emailPattern = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9-]+(\.[a-zA-Z]{2,})+$/;
                                        const doubleDotPattern = /\.{2,}/; 
                                        const input = event.target.value;
                                        if ((!emailPattern.test(input) || doubleDotPattern.test(input)) && input !== '') {
                                            alert('Por favor, ingresa un correo válido.');
                                        }
                                    }}
                                />
                            </div>
                        </div>
                        <div className={"client-tax-info"}>
                            <label className={"client-section-title"}>Datos fiscales</label>
                            <div className={"client-section-item"}>
                                <label className={"property"}>Razon social</label>
                                <input className={"client-section-input"} type={"text"}
                                        value={client ? client.companyInformation.razonSocial : ""}
                                        onChange={event => setClient({
                                        ...client,
                                        companyInformation: {
                                            ...client.companyInformation,
                                            razonSocial: event.target.value
                                        } 
                                    })}
                                    />
                            </div>
                            <div className={"client-section-item"}>
                                <label className={"property"}>RFC</label>
                                <input className={"client-section-input"} type={"text"}
                                        value={client ? client.companyInformation.rfc : ""}
                                        onChange={event => setClient({
                                        ...client,
                                        companyInformation: {
                                            ...client.companyInformation,
                                            rfc: event.target.value
                                        } 
                                    })}/>
                            </div>
                            <div className={"client-section-item"}>
                                <label className={"property"}>Direccion fiscal</label>
                                <input className={"client-section-input"} type={"text"}
                                        value={client ? client.companyInformation.direccionFiscal : ""}
                                        onChange={event => setClient({
                                        ...client,
                                        companyInformation: {
                                            ...client.companyInformation,
                                            direccionFiscal: event.target.value
                                        } 
                                    })}/>
                            </div>
                            <div className={"client-section-item"}>
                                <label className={"property"}>Regimen fiscal</label>
                                <select
                                    className={"client-section-input"}
                                    value={client ? client.companyInformation.regimenFiscal : 0}
                                    onChange={event => setClient({
                                        ...client,
                                        companyInformation: {
                                            ...client.companyInformation,
                                            regimenFiscal: event.target.value
                                        }
                                    })}>
                                    <option value={0}>Seleccionar</option> 
                                    <option value={1}>Régimen General de Ley</option>
                                    <option value={2}>Régimen de Personas Morales con Fines no Lucrativos</option>
                                    <option value={3}>Régimen de Incorporación Fiscal (RIF)</option>
                                    <option value={4}>Régimen de Actividades Agrícolas, Ganaderas, Silvícolas y Pesqueras</option>
                                    <option value={5}>Régimen de Coordinados</option>
                                    <option value={6}>Régimen de Empresas en Liquidación</option>
                                    <option value={7}>Régimen de Pequeños Contribuyentes (REPECO)</option>
                                    <option value={8}>Régimen de Arrendamiento</option>
                                    <option value={9}>Régimen de Distribuidores de Energía</option>
                                    <option value={10}>Régimen de Servicios Profesionales</option>
                                    <option value={11}>Régimen de Cooperativas</option>
                                    <option value={12}>Régimen de Actividades Empresariales con Ingresos Menores a 2 millones de pesos</option>
                                    <option value={13}>Régimen de Honorarios Profesionales</option>
                                    <option value={14}>Régimen de Empresas Familiares</option>
                                    <option value={15}>Régimen de Sociedad de Inversión de Capitales</option>
                                </select>
                            </div>
                            <div className={"client-section-item"}>
                                <label className={"property"}>Metodo de pago</label>
                                <select className={"client-section-input"}
                                        value={client ? client.companyInformation.payment.id : 0} 
                                        // onChange={event => setClient({
                                        //     ...client,
                                        //     companyInformation: {
                                        //         ...client.companyInformation,
                                        //         payment: {
                                        //             ...client.companyInformation.payment,
                                        //             id: event.target.value
                                        //         }
                                        //         } 
                                        //     })}
                                        >
                                    <option value={0}>Seleccionar</option>
                                    <option value={1}>Transferencia Bancaria</option>
                                    <option value={2}>Cheque</option>
                                    <option value={3}>Efectivo</option>
                                    <option value={4}>Por definir</option>
                                </select>
                            </div>
                        </div>
                    </div>
                    <button className={"client-view-accept"} onClick={saveChanges}>Guardar</button>
                </div>
            </div>
        </div>
    )
}

export default ClientEdit