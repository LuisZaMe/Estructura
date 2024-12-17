import React, { useEffect, useState } from "react";
import { useHistory } from "react-router-dom";
import { useDispatch, useSelector } from "react-redux";

// Actions
import { hideEditVisit, setVisitId } from "../../../actions";


// Services
import VisitService from "../../../services/VisitService";
import LocationService from "../../../services/LocationService";
import StudyService from "../../../services/StudyService";
import moment from "moment";

export const EditVisit = ({ visitToEdit }) => {

    const dispatch = useDispatch()
    const history = useHistory()

    const show = useSelector(state => state.editVisit)
    const [visitDateSelected, setVisitDateSelected] = useState("")
    const [visitHour, setVisitHour] = useState("")
    const [dateTime, setDateTime] = useState("")
    const [states, setStates] = useState([])
    const [cities, setCities] = useState([])
    const [state, setState] = useState(0)
    const [city, setCity] = useState(0)
    const [address, setAddress] = useState("")
    const [appointmentMedium, setAppointmentMedium] = useState(null)
    const [color, setColor] = useState(["FFFFF", ''])
    const [disable, setDisable] = useState(true)
    const [studies, setStudies] = useState([])
    //   const [studySelectedId, setStudySelectedId] = useState(null)
    //   const [studySelected, setStudySelected] = useState(null)

    const serviceType = [
        "None",
        "Estudio socioeconomico",
        "Estudio Laboral"
    ]
    useEffect(() => {
        if ((visitDateSelected && visitHour) !== '') {
            let date = moment(visitDateSelected);
            let time = moment(visitHour, 'HH:mm');
            date.set({
                hour: time.get('hour'),
                minute: time.get('minute'),
                second: time.get('second')
            });
            setDateTime(moment.utc(date))
        }
    }, [visitDateSelected, visitHour])

    useEffect(() => {
        if (visitToEdit) {
            let classColor;
            if (visitToEdit.notationColor === "#07DDA5") {
                classColor = "color1";
            } else
                if (visitToEdit.notationColor === "#0F92DB") {
                    classColor = "color2";
                } else
                    if (visitToEdit.notationColor === "#1BB9EF") {
                        classColor = "color3";
                    } else
                        if (visitToEdit.notationColor === "#00A987") {
                            classColor = "color4";
                        }
            setColor([visitToEdit.notationColor, classColor])
            let date = moment.utc(visitToEdit.visitDate).local().format('yyyy-MM-DD')
            let hourDate = moment.utc(visitToEdit.visitDate).local().format('hh:mm')
            setVisitDateSelected(date)
            setVisitHour(hourDate)
            setState(visitToEdit.state.id)
            setCity(visitToEdit.city.id)
            setAddress(visitToEdit.address)
            setAppointmentMedium(visitToEdit.appointmentMedium)
        }
    }, [visitToEdit])

    useEffect(() => {
        const getStates = async () => {
            try {
                const response = await LocationService.getStates()
                setStates(response.data.response)
            } catch (error) {
                console.log(error)
            }
        }
        const getStudies = async () => {
            try {
                const response = await StudyService.getAllStudies()
                setStudies(response.data.response)
            } catch (error) {
                console.log(error)
            }
        }
        if (show) {
            getStates()
            getStudies()
        }
    }, [show])

    // Request cities
    useEffect(() => {
        const getCities = async () => {
            try {
                const response = await LocationService.getCities(state)
                setCities(response.data.response)
            } catch (error) {
                console.log(error)
            }
        }

        if (state) {
            getCities()
        }
    }, [state])
    const stateOptions = states.map(state => {
        return (
            <option key={"state-" + state.id} value={state.id}>{state.name}</option>
        )
    })

    const cityOptions = cities.map(city => {
        return (
            <option key={"city-" + city.id} value={city.id}>{city.name}</option>
        )
    })

    const sendData = async () => {
        try {
            const data = {
                id: visitToEdit.id,
                address: address,
                city: { Id: city },
                state: { Id: state },
                notationColor: color[0],
                visitDate: dateTime,
                appointmentMedium: appointmentMedium,
            }
            const response = await VisitService.update(data);
            setDisable(true);
            dispatch(hideEditVisit());
            history.push("/dashboard/visitas");
        } catch (error) {
            console.log(error)
        }
    }

    const showHideModal = show ? "modal display-block" : "modal display-none"
    return (
        <div className={showHideModal}>
            <div className={"form-register-admin"}>
                <div className={"close-modal"}>
                    <img src={"/images/icon-close.png"} alt={""} onClick={() => { setDisable(true); dispatch(hideEditVisit()) }} />
                </div>
                <h2>visita</h2>
                <div className={"form-section"} >
                    <div style={{ display: "flex", justifyContent: "space-between", width: "100%" }}>
                        <div className={"form-item-column"}>
                            <label htmlFor={"nameClient"}>cliente</label>
                            <label htmlFor={"nameClient"} className='labelFormInput'>{visitToEdit ? visitToEdit.study.candidate.client.name : ''}</label>
                        </div>
                        <div className={"form-item-column"}>
                            <label htmlFor={"nameCandidate"}>candidato</label>
                            <label htmlFor={"nameClient"} className='labelFormInput'>{visitToEdit ? visitToEdit.study.candidate.name : ''}</label>
                        </div>
                    </div>
                    <div style={{ display: "flex", justifyContent: "space-between", width: "100%" }}>
                        <div className={"form-item-column"}>
                            <label htmlFor={"nameInterviewer"}>entrevistador</label>
                            <label htmlFor={"nameClient"} className='labelFormInput'>{visitToEdit ? visitToEdit.study.interviewer.name : ''}</label>
                        </div>
                        <div className={"form-item-column"}>
                            <label htmlFor={"name"} style={{ marginBottom: "13px" }} >Color</label>
                            <div className="color-picker-container">
                                <div className="color-picker" style={{ backgroundColor: "#07DDA5" }} onClick={() => (disable ? '' : setColor(['#07DDA5', "color1"]))}> <img className={color[1] === 'color1' ? 'color1' : ''} src='/images/checkWhite.png'></img> </div>
                                <div className="color-picker" style={{ backgroundColor: "#0F92DB" }} onClick={() => (disable ? '' : setColor(['#0F92DB', "color2"]))}> <img className={color[1] === 'color2' ? 'color2' : ''} src='/images/checkWhite.png'></img></div>
                                <div className="color-picker" style={{ backgroundColor: "#1BB9EF" }} onClick={() => (disable ? '' : setColor(['#1BB9EF', "color3"]))}> <img className={color[1] === 'color3' ? 'color3' : ''} src='/images/checkWhite.png'></img></div>
                                <div className="color-picker" style={{ backgroundColor: "#00A987" }} onClick={() => (disable ? '' : setColor(['#00A987', "color4"]))}> <img className={color[1] === 'color4' ? 'color4' : ''} src='/images/checkWhite.png'></img></div>
                            </div>
                        </div>

                    </div>
                    <div style={{ display: "flex", justifyContent: "space-between", width: "100%" }}>
                        <div className={"form-item-column"}>
                            <label htmlFor={"visitDateSelected"}>Fecha de visita</label>
                            <input type={"date"} name={"visitDateSelected"} required={true} disabled={disable} value={visitDateSelected}
                                onChange={event => setVisitDateSelected(event.target.value)} />
                        </div>
                        <div className={"form-item-column"}>
                            <label htmlFor={"visitHour"}>Hora de visita</label>
                            <input type={"time"} name={"visitHour"} required={true} disabled={disable} value={visitHour}
                                onChange={event => setVisitHour(event.target.value)} />
                        </div>
                    </div>
                    <div style={{ display: "flex", justifyContent: "space-between", width: "100%" }}>
                        <div className={"form-item-column"}>
                            <label htmlFor={"study"}>Tipo de servicio</label>
                            <label htmlFor={"nameClient"} className='labelFormInput'>{visitToEdit ? serviceType[visitToEdit.study.serviceType] : ''}</label>
                        </div>

                    </div>
                    <div className={"form-item"} style={{ width: "100%" }}>
                        <label htmlFor={"address"}>Direccion</label>
                        <input type={"text"} name={"address"} placeholder={"Agregar dirección"} required={true} disabled={disable} value={address}
                            onChange={event => setAddress(event.target.value)} />
                    </div>
                    <div style={{ display: "flex", justifyContent: "space-between", width: "100%" }}>
                        <div className={"form-item-column"}>
                            <label htmlFor={"state"}>Estado</label>
                            <select name={"state"} value={state} required={true} disabled={disable} onChange={event => setState(parseInt(event.target.value))}>
                                <option value={0}>Seleccionar</option>
                                {stateOptions}
                            </select>
                        </div>
                        <div className={"form-item-column"}>
                            <label htmlFor={"city"}>Ciudad</label>
                            <select name={"city"} value={city} required={true} disabled={disable} onChange={event => setCity(parseInt(event.target.value))}>
                                <option value={0}>Seleccionar</option>
                                {cityOptions}
                            </select>
                        </div>
                    </div>
                    <div className={"form-item"}>
                        <label htmlFor={"appointmentMedium"}>Medio de reunión</label>
                        <input type={"text"} name={"appointmentMedium"} placeholder={"Medio de reunión"} required={true} disabled={disable} value={appointmentMedium}
                            onChange={event => setAppointmentMedium(event.target.value)} />
                    </div>
                    <div style={{ display: "flex", justifyContent: "space-between", width: "100%" }}>
                        <div className={"form-item-column"}>
                            <div className={"form-action"}>
                                <div className={"set-edit-form-button"} onClick={() => setDisable(!disable)}></div>
                            </div>
                        </div>
                        <div className={"form-item-column"}>
                            <div className={"form-action"}>
                                <button className={"form-button-primary"} onClick={() => sendData()} >Registrar</button>
                                {/* <button className={"form-button-primary"} onClick={disable ? '' : sendData} >Registrar</button> */}
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    )
}
