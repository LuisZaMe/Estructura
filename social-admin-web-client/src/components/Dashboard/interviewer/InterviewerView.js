import React, { useEffect, useState } from "react";
import { useHistory } from "react-router-dom";
import { useDispatch, useSelector } from "react-redux";
import _ from 'lodash';

// Actions
import {
	removeInterviewerId,
	showAssignedStudies,
	showAssignStudy,
} from "../../../actions";

// Services
import AccountService from "../../../services/AccountService";
import AssignedStudies from "../analyst/AssignedStudies";
import LocationService from "../../../services/LocationService";
import AssignStudy2 from "./../analyst/AssignStudy2";

const InterviewerView = () => {
	const dispatch = useDispatch()
	const history = useHistory()

	const id = useSelector(state => state.interviewer)

	const [interviewer, setInterviewer] = useState(null)

	const [state, setState] = useState(0);
	const [states, setStates] = useState([]);
	const [cities, setCities] = useState([]);
	const [city, setCity] = useState(0);

	// Request states
	useEffect(() => {
		const getStates = async () => {
			try {
				const response = await LocationService.getStates();
				setStates(response.data.response);
			} catch (error) {
				console.log(error);
			}
		}
		getStates()
	}, [])

	useEffect(() => {
		const getInterviewer = async () => {
			try {
				const response = await AccountService.getAccount(id);
				setInterviewer(response.data.response[0]);

				setState(response.data.response[0].stateId - 1);
			} catch (error) {
				console.log(error);
			}
		}
		getInterviewer()
	}, [id])

		// Request cities
		useEffect(() => {
			const getCities = async () => {
				try {
					const response = await LocationService.getCities(state + 1);
					setCities(response.data.response);
					if(interviewer && interviewer.stateId && interviewer.cityId){
						const index = response.data.response.map(function(e) { return e.id; }).indexOf(interviewer.cityId);
						setCity(index);
					}
				} catch (error) {
					console.log(error)
				}
			}
			if (state) {
				getCities();
			}
		}, [state])

	const onClickAccept = () => {
		dispatch(removeInterviewerId())
		history.push("/dashboard/entrevistadores");
	}

	const onClickEdit = () => {
		history.push("/dashboard/entrevistadores/editar");
	}

	return (
		<div className={"container"}>
			<div className={"content interviewer"}>
				<div className={"top-section"}>
					<div className={"interviewer-header"}>
						<div className={"interviewer-name"}>
							<label className={"interviewer-name-title"}>Nombre del entrevistador</label>
							<label className={"interviewer-name-value"}>{interviewer ? interviewer.name : null}</label>
						</div>
						<button className={"edit-interviewer"} onClick={onClickEdit}>
							<img src={"/images/actions-dropdown/edit.svg"} alt={""} />
							Editar
						</button>
					</div>
				</div>
				<div className={"main-section interviewer shadow"}>
					<div className={"interviewer-view"}>
						<label className={"interviewer-section-title"}>Datos principales</label>
						<div className={"interviewer-main-info"}>
							<div className={"interviewer-section-item"}>
								<label className={"property"}>Telefono</label>
								<label className={"value"}>{interviewer ? interviewer.phone : null}</label>
							</div>
							<div className={"interviewer-section-item"}>
								<label className={"property"}>Correo</label>
								<label className={"value"}>{interviewer ? interviewer.email : null}</label>
							</div>
						</div>
						<label className={"interviewer-section-title"}>Estado y Municipio</label>
						<div className={"interviewer-main-info"}>
							<div className={"interviewer-section-item"}>
								<label className={"value"}>{interviewer && interviewer.stateId && states ? _.get(states[state],'name', '--') : null}</label>
							</div>
							<div className={"interviewer-section-item"}>
								<label className={"value"}>{interviewer && interviewer.cityId && cities.length > 0 ? _.get(cities[city], 'name', '--') : null}</label>
							</div>
							<div className={"interviewer-section-study"}>
								<button onClick={() => dispatch(showAssignStudy())}>Asignar estudio</button>
                                <AssignStudy2 />
								<button onClick={() => dispatch(showAssignedStudies())}>Estudios asignados</button>
								<AssignedStudies />
							</div>
						</div>
					</div>
					<button className={"interviewer-view-accept"} onClick={onClickAccept}>Aceptar</button>
				</div>
			</div>
		</div>
	)
}

export default InterviewerView