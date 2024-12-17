import React, {useEffect, useState} from "react";
import {useHistory} from "react-router-dom";
import {useDispatch, useSelector} from "react-redux";

// Actions
import {removeCandidateId} from "../../../actions";

// Services
import CandidateService from "../../../services/CandidateService";
import VisitService from "../../../services/VisitService";
import moment from "moment";


const VisitView = (props) => {

  const dispatch = useDispatch()
  const history = useHistory()

  const id = useSelector(state => state.visit)
  const [visit, setVisit] = useState(null)
  const [color, setColor] = useState(["FFFFF", ''])
  const [visitDateSelected, setVisitDateSelected] = useState("")
  const [visitHour, setVisitHour] = useState("")

  const serviceType =[
    "None",
    "Estudio socioeconomico",
    "Estudio Laboral"
  ] 
  useEffect(() => {
    if(visit!==null){
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

  const onClickAccept = () => {
      dispatch(removeCandidateId())
      history.push("/dashboard/visitas")
  }

  const onClickEdit = () => {
      history.push("/dashboard/visitas/editar")
  }

  return (
    <div className={"container"}>
            <div className={"content candidate"}>
                <div className={"top-section"}>
                    <div className={"candidate-header"}>
                        <div className={"company-name"}>
                            <label className={"company-name-title"}>Nombre del cliente</label>
                            <label className={"company-name-value"}>{visit ? visit.study.candidate.name : ""}</label>
                        </div>
                        <button className={"edit-candidate"} onClick={onClickEdit}>
                            <img src={"/images/actions-dropdown/edit.svg"} alt={""}/>
                            Editar
                        </button>
                    </div>
                </div>
                <div className={"main-section candidate shadow"}>
                    <div className={"candidate-view"} style={{overflowY: "auto"}}>
                        <div>
                            <div className={"candidate-section-item"}>
                                <label className={"property"}>Cliente</label>
                                <label className={"value"}>{visit ? visit.study.candidate.client.name : null}</label>
                            </div>
                            <div className={"candidate-section-item"}>
                                <label className={"property"}>Entrevistador</label>
                                <label className={"value"}>{visit ? visit.study.interviewer.name : null}</label>
                            </div>
                            <div className={"candidate-section-item"}>
                                <label className={"property"}>Fecha de visita</label>
                                <label className={"value"}>{visitDateSelected ? visitDateSelected : null}</label>
                            </div>
                            
                            <div className={"candidate-section-item"}>
                                <label className={"property"}>Tipo de servicio</label>
                                <label className={"value"}>{visit ? serviceType[visit.study.serviceType] : null}</label>
                            </div>
                            <div className={"candidate-section-item"}>
                                <label className={"property"}>Direccion</label>
                                <label className={"value"}>{visit ? visit.address : null}</label>
                            </div>
                            <div className={"candidate-section-item"}>
                                <label className={"property"}>Ciudad</label>
                                <label className={"value"}>{visit ? visit.city.name : null}</label>
                            </div>
                        </div>
                        <div>
                        <div className={"candidate-section-item"}>
                                <label className={"property"}>Candidato</label>
                                <label className={"value"}>{visit ? visit.study.candidate.name : null}</label>
                            </div>
                            <div className={"candidate-section-item"}>
                                <label className={"property"}>Color</label>
                                <div className="color-picker-container">
                                <div className="color-picker" style={{backgroundColor: "#07DDA5"}}> <img className={color[1]==='color1'? 'color1': ''} src='/images/checkWhite.png'></img> </div>
                                <div className="color-picker" style={{backgroundColor: "#0F92DB"}}> <img className={color[1]==='color2'? 'color2': ''} src='/images/checkWhite.png'></img></div>
                                <div className="color-picker" style={{backgroundColor: "#1BB9EF"}}> <img className={color[1]==='color3'? 'color3': ''} src='/images/checkWhite.png'></img></div>
                                <div className="color-picker" style={{backgroundColor: "#00A987"}}> <img className={color[1]==='color4'? 'color4': ''} src='/images/checkWhite.png'></img></div>
                                </div>
                            </div>
                            <div className={"candidate-section-item"}>
                                <label className={"property"}>Hora de visita</label>
                                <label className={"value"}>{visitHour ? visitHour : null}</label>
                            </div>
                            <div className={"candidate-section-item"}>
                                <label className={"property"}>Medio de reunion</label>
                                <label className={"value"}>{visit ? visit.appointmentMedium : null}</label>
                            </div>
                            <div className={"candidate-section-item"}>
                                <label className={"property"}></label>
                                <label className={"value"}></label>
                            </div>
                            <div className={"candidate-section-item"}>
                                <label className={"property"}>Estado</label>
                                <label className={"value"}>{visit ? visit.state.name : null}</label>
                            </div>
                        </div>
                    </div>
                    <button className={"candidate-view-accept"} onClick={onClickAccept}>Aceptar</button>
                </div>
            </div>
        </div>
  )
}

export default VisitView