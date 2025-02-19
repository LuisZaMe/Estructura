import React, { useEffect, useState } from "react";
import { useHistory } from "react-router-dom";
import { useDispatch, useSelector } from "react-redux";

// Actions
import { hideRegisterVisit, setVisitId } from "../../../actions";


// Services
import VisitService from "../../../services/VisitService";
import LocationService from "../../../services/LocationService";
import StudyService from "../../../services/StudyService";
import moment from "moment";
import _ from 'lodash';
export const RegisterVisit = () => {

    const dispatch = useDispatch()
    const history = useHistory()

    const show = useSelector(state => state.registerVisit)
    const [visitDate, setVisitDate] = useState("")
    const [visitHour, setVisitHour] = useState("")
    const [dateTime, setDateTime] = useState("")

    const [states, setStates] = useState([])
    const [cities, setCities] = useState([])
    const [state, setState] = useState(0)
    const [city, setCity] = useState(0)
    const [address, setAddress] = useState("")
    const [studies, setStudies] = useState([])
    const [studySelectedId, setStudySelectedId] = useState(null)
    const [studySelected, setStudySelected] = useState(null)
    const [appointmentMedium, setAppointmentMedium] = useState(null)
    const [color, setColor] = useState(["FFFFF", ''])

    const serviceType = [
        "None",
        "Estudio socioeconomico",
        "Estudio Laboral"
    ]
    useEffect(() => {
        if ((visitDate && visitHour) !== '') {
            let date = moment(visitDate);
            let time = moment(visitHour, 'HH:mm');
            date.set({
                hour: time.get('hour'),
                minute: time.get('minute'),
                second: time.get('second')
            });
            setDateTime(moment.utc(date))
        }
    }, [visitDate, visitHour])

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
                console.log('StudyService: ', response);
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

    const onSubmit = async () => {
        try {
            const data = {
                address: address,
                city: { Id: city },
                state: { Id: state },
                notationColor: color[0],
                study: { Id: studySelectedId },
                candidate: { Id: _.get(studySelected, 'candidate.id' , null) },
                interviewer: { Id: _.get(studySelected, 'interviewer.id', null) },
                visitDate: dateTime,
                appointmentMedium: appointmentMedium,
            }
            const response = await VisitService.create(data)
            // dispatch(setAdminId(response.data.response.id))
            if (response) {
                dispatch(hideRegisterVisit())
                history.push("/dashboard/visitas")
            }
        } catch (error) {
            console.log(error)
        }
    }

    const showHideModal = show ? "modal display-block" : "modal display-none"
    return (
        <div className={showHideModal}>
            <div className={"form-register-admin"}>
                <div className={"close-modal"}>
                    <img src={"/images/icon-close.png"} alt={""} onClick={() => dispatch(hideRegisterVisit())} />
                </div>
                <h2>Registro de visitas</h2>
                <form className={"form-section"}>
                    <h3 className={"form-subsection-header"}>Datos Principales</h3>
                    <div className={"form-item"}>
                        <label htmlFor={"nameClient"}>Seleccionar servicio*</label>
                        <select style={{ width: "500px" }} className='study-dropdown select-study' value={studySelectedId} onChange={(e) => {
                            const data = studies.find(ele => ele.id === parseInt(e.target.value));
                            setStudySelectedId(parseInt(e.target.value));
                            setStudySelected(data);
                        }}>
                            <option className='option-selected' value={0} selected hidden> {"Seleccionar estudio"}</option>
                            {
                                studies
                                .filter(ele => ele.serviceType !== undefined && ele.candidate) // Filtra los vacíos
                                .map(ele => (
                                    <option key={ele.id} value={ele.id}>
                                        {serviceType[ele.serviceType] || "Desconocido"} - {`${_.get(ele, 'candidate.name', 'Sin nombre')} ${_.get(ele, 'candidate.lastname', 'Sin apellido')}`.trim()}
                                    </option>
                                ))
                            }
                        </select >
                    </div>
                    <div className={"form-item"}>
                        <label htmlFor={"nameClient"}>Nombre del cliente*</label>
                        <label htmlFor={"nameClient"} className='labelFormInput'>{studySelected ? `${studySelected.candidate.client.name} ${studySelected.candidate.client.lastname}` : ''}</label>
                    </div>
                    <div className={"form-item"}>
                        <label htmlFor={"nameCandidate"}>Nombre del candidato*</label>
                        <label htmlFor={"nameClient"} className='labelFormInput'>{studySelected ? `${studySelected.candidate.name} ${studySelected.candidate.lastname}` : ''}</label>
                    </div>
                    <div className={"form-item"}>
                        <label htmlFor={"nameInterviewer"}>Nombre del entrevistador*</label>
                        <label htmlFor={"nameClient"} className='labelFormInput'>{`${_.get(studySelected, 'interviewer.name', '--')} ${_.get(studySelected, 'interviewer.lastname', '--')}`}</label>
                    </div>
                    <div style={{ display: "flex", justifyContent: "space-between", width: "100%" }}>
                        <div className={"form-item-column"}>
                            <label htmlFor={"visitDate"}>Fecha de visita*</label>
                            <input type={"date"} name={"visitDate"} required={true} value={visitDate}
                                onChange={event => setVisitDate(event.target.value)} />
                        </div>
                        <div className={"form-item-column"}>
                            <label htmlFor={"visitHour"}>Hora de visita*</label>
                            <input type={"time"} name={"visitHour"} required={true} value={visitHour}
                                onChange={event => setVisitHour(event.target.value)} />
                        </div>
                    </div>
                    <div style={{ display: "flex", justifyContent: "space-between", width: "100%" }}>
                        <div className={"form-item-column"}>
                            <label htmlFor={"name"} style={{ marginBottom: "13px" }} >Color*</label>
                            <div className="color-picker-container">
                                <div className="color-picker" style={{ backgroundColor: "#07DDA5" }} onClick={() => setColor(['#07DDA5', "color1"])}> <img className={color[1] === 'color1' ? 'color1' : ''} src='/images/checkWhite.png'></img> </div>
                                <div className="color-picker" style={{ backgroundColor: "#0F92DB" }} onClick={() => setColor(['#0F92DB', "color2"])}> <img className={color[1] === 'color2' ? 'color2' : ''} src='/images/checkWhite.png'></img></div>
                                <div className="color-picker" style={{ backgroundColor: "#1BB9EF" }} onClick={() => setColor(['#1BB9EF', "color3"])}> <img className={color[1] === 'color3' ? 'color3' : ''} src='/images/checkWhite.png'></img></div>
                                <div className="color-picker" style={{ backgroundColor: "#00A987" }} onClick={() => setColor(['#00A987', "color4"])}> <img className={color[1] === 'color4' ? 'color4' : ''} src='/images/checkWhite.png'></img></div>
                            </div>
                        </div>
                    </div>
                    <div className={"form-item"}>
                        <label htmlFor={"address"}>Direccion*</label>
                        <input type={"text"} name={"address"} placeholder={"Agregar dirección"} required={true} value={address}
                            onChange={event => setAddress(event.target.value)} />
                    </div>
                    <div style={{ display: "flex", justifyContent: "space-between", width: "100%" }}>
                        <div className={"form-item-column"}>
                            <label htmlFor={"state"}>Estado</label>
                            <select name={"state"} value={state} onChange={event => setState(parseInt(event.target.value))}>
                                <option value={0}>Seleccionar</option>
                                {stateOptions}
                            </select>
                        </div>
                        <div className={"form-item-column"}>
                            <label htmlFor={"city"}>Ciudad</label>
                            <select name={"city"} value={city} onChange={event => setCity(parseInt(event.target.value))}>
                                <option value={0}>Seleccionar</option>
                                {cityOptions}
                            </select>
                        </div>
                    </div>

                    <div className={"form-action"}>
                        <button  className={"form-button-primary"} onClick={onSubmit} >Registrar</button>
                    </div>
                </form>
            </div>
        </div>
    )
}
