import React, {useEffect, useState} from "react";
import {useHistory} from "react-router-dom";
import {useSelector} from "react-redux";
import moment from "moment";
// Services
import CandidateService from "../../../services/CandidateService";
import VisitService from "../../../services/VisitService";
import LocationService from "../../../services/LocationService";

const VisitEdit = (props) => {

  const history = useHistory()

  const id = useSelector(state => state.visit)
  const estado =useSelector(state => state)
  const [visit, setVisit] = useState(null)

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

  const serviceType =[
    "None",
    "Estudio socioeconomico",
    "Estudio Laboral"
  ] 

  useEffect(() => {
    if((visitDateSelected && visitHour)!== ''){
      let date = moment(visitDateSelected);
      let time = moment(visitHour, 'HH:mm');
      date.set({
          hour:   time.get('hour'),
          minute: time.get('minute'),
          second: time.get('second')
      });
      setDateTime(moment.utc(date))
    }
  }, [visitDateSelected,visitHour])
  useEffect(() => {
    if(visit){
      let classColor;
        if(visit.notationColor ==="#07DDA5"){
            classColor = "color1";
        }else
        if(visit.notationColor ==="#0F92DB"){
            classColor = "color2";
        }else
        if(visit.notationColor ==="#1BB9EF"){
            classColor = "color3";
        }else
        if(visit.notationColor ==="#00A987"){
            classColor = "color4";
        }
        setColor([visit.notationColor, classColor])
        let date = moment.utc(visit.visitDate).local().format('yyyy-MM-DD')
        let hourDate = moment.utc(visit.visitDate).local().format('hh:mm')
        setVisitDateSelected(date)
        setVisitHour(hourDate)
        setState(visit.state.id)
        setCity(visit.city.id)
        setAddress(visit.address)
        setAppointmentMedium(visit.appointmentMedium)
    }
  }, [visit])
  

  useEffect(() => {
      const getCandidate = async () => {
          try {
              const response = await VisitService.getVisit(id)
              setVisit(response.data.response[0])
          } catch (error) {
              console.log(error)
          }
      }
      getCandidate()
  }, [id])

  const saveChanges = async () => {
    try {
      const data = {
          id: visit.id,
          address : address,
          city : { Id: city },
          state : { Id : state },
          notationColor : color[0],
          visitDate : dateTime,
          appointmentMedium: appointmentMedium,
      }
      const response = await VisitService.update(data);
      history.push("/dashboard/visitas");
  } catch (error) {
      console.log(error)
  }
  }

  const onClickView = () => {
      history.push("/dashboard/visitas/ver")
  }

  useEffect(() => {
    const getStates = async () => {
        try {
            const response = await LocationService.getStates()
            setStates(response.data.response)
        } catch (error) {
            console.log(error)
        }
    }
        getStates()
}, [])
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
  return (
    <div className={"container"}>
        <div className={"content candidate"}>
            <div className={"top-section"}>
                <div className={"candidate-header"}>
                    <div className={"company-name"}>
                        <label className={"company-name-title"}>Nombre Cliente/Empresa</label>
                        <input className={"client-section-input"} value={visit ? visit.study.candidate.client.companyInformation.companyName : ""} disabled/>
                    </div>
                    <button className={"edit-candidate"} onClick={onClickView}>
                        <img src={"/images/actions-dropdown/edit.svg"} alt={""}/>
                        Editar
                    </button>
                </div>
            </div>
            <div className={"main-section candidate shadow"}>
                <div className={"candidate-view"} style={{overflowY: "auto"}}>
                    <div>
                        <div className={"candidate-section-item"}>
                            <label className={"property"}>Nombre de Responable</label>
                            <input className={"candidate-section-input"} style={{background : "transparent"}} value={visit ? `${visit.study.candidate.client.name} ${visit.study.candidate.client.lastname}` : ""}  disabled/>
                        </div>
                        <div className={"candidate-section-item"}>
                            <label className={"property"}>Nombre del entrevistador</label>
                            <input className={"candidate-section-input"} style={{background : "transparent"}} value={visit ? `${visit.study.interviewer.name} ${visit.study.interviewer.lastname}` : null} disabled/>
                        </div>
                        <div className={"candidate-section-item"}>
                            <label className={"property"}>Fecha de visita</label>
                            <input type="date" className={"candidate-section-input"} value={visitDateSelected ? visitDateSelected : null} onChange={event => setVisitDateSelected(event.target.value)}/>
                        </div>
                        <div className={"candidate-section-item"}>
                            <label className={"property"}>Direccion</label>
                            <input className={"candidate-section-input"} value={address ? address : null} onChange={event => setAddress(event.target.value)}/>
                        </div>
                        <div className={"candidate-section-item"}>
                            <label className={"property"}>Estado</label>
                            <select className={"candidate-section-input"} name={"state"} value={state} required={true} onChange={event => setState(parseInt(event.target.value))}>
                            <option value={0}>Seleccionar</option>
                            {stateOptions}
                        </select>
                        </div>
                    </div>
                    <div>
                        <div className={"candidate-section-item"}>
                            <label className={"property"}>Candidato</label>
                            <input className={"candidate-section-input"} style={{background : "transparent"}}
                                  value={visit ? `${visit.study.candidate.name} ${visit.study.candidate.lastname}` : null} disabled/>
                        </div>
                        <div className={"candidate-section-item"}>
                            <label className={"property"}>Color</label>
                            <div className="color-picker-container">
                            <div className="color-picker" style={{backgroundColor: "#07DDA5"}} onClick={()=> {setColor(['#07DDA5', "color1"]) }}> <img className={color[1]==='color1'? 'color1': ''} src='/images/checkWhite.png'></img> </div>
                            <div className="color-picker" style={{backgroundColor: "#0F92DB"}} onClick={()=> {setColor(['#0F92DB', "color2"]) }}> <img className={color[1]==='color2'? 'color2': ''} src='/images/checkWhite.png'></img></div>
                            <div className="color-picker" style={{backgroundColor: "#1BB9EF"}} onClick={()=> {setColor(['#1BB9EF', "color3"]) }}> <img className={color[1]==='color3'? 'color3': ''} src='/images/checkWhite.png'></img></div>
                            <div className="color-picker" style={{backgroundColor: "#00A987"}} onClick={()=> {setColor(['#00A987', "color4"]) }}> <img className={color[1]==='color4'? 'color4': ''} src='/images/checkWhite.png'></img></div>
                            </div>
                        </div>
                        <div className={"candidate-section-item"}>
                            <label className={"property"}>Hora de visita</label>
                            <input type={"time"} className={"candidate-section-input"} value={visitHour ? visitHour : null} onChange={event => setVisitHour(event.target.value)}/>
                        </div>
                        <div className={"candidate-section-item"}>
                            <label className={"property"}></label>
                            <label className={"property"}></label>
                        </div>
                        <div className={"candidate-section-item"}>
                            <label className={"property"}>Ciudad</label>
                            <select className={"candidate-section-input"} name={"city"} value={city} required={true} onChange={event => setCity(parseInt(event.target.value))}>
                            <option value={0}>Seleccionar</option>
                            {cityOptions}
                        </select>
                        </div>
                    </div>
                </div>
                <button className={"candidate-view-accept"} onClick={saveChanges}>Guardar</button>
            </div>
        </div>
    </div>
)
}
export default VisitEdit
