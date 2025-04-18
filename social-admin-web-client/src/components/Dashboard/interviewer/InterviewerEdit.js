import React, { useEffect, useState } from "react";
import { useHistory } from "react-router-dom";
import { useSelector } from "react-redux";

// Services
import AccountService from "../../../services/AccountService";
import LocationService from "../../../services/LocationService";

const InterviewerEdit = () => {
	const history = useHistory()

	const id = useSelector(state => state.interviewer)

	const [interviewer, setInterviewer] = useState(null);
	const [state, setState] = useState(0)
	const [states, setStates] = useState([])
	const [cities, setCities] = useState([])
	const [city, setCity] = useState(0)

	useEffect(() => {
		const getInterviewer = async () => {
			try {
				const response = await AccountService.getAccount(id)
				setInterviewer(response.data.response[0])
			} catch (error) {
				console.log(error)
			}
		}
		getInterviewer()
	}, [id])

	useEffect(() => {
		if(interviewer && interviewer.stateId && interviewer.cityId){
			setState(interviewer.stateId);
			setCity(interviewer.cityId);
		}
	}, [interviewer])

	// Request states
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

	const renderCities = cities.map(city => {
		return (
			<option key={`city-${city.id}`} value={city.id}>{city.name}</option>
		)
	})

	const renderStates = states.map(state => {
		return (
			<option key={`state-${state.id}`} value={state.id}>{state.name}</option>
		)
	})

	const saveChanges = async () => {
		try {
			await AccountService.update(interviewer)
			history.push("/dashboard/entrevistadores/ver")
		} catch (error) {
			console.log(error)
		}
	}

	const onClickView = () => {
		history.push("/dashboard/entrevistadores/ver")
	}

	return (
		<div className={"container"}>
			<div className={"content interviewer"}>
			<div className={"top-section"}>
			<div className={"interviewer-header"}>
				<div className={"interviewer-name"}>
					<label className={"interviewer-name-title"}>Nombre del entrevistador</label>
					<input
						className={"interviewer-section-input"}
						type={"text"}
						value={`${interviewer?.name || ""} ${interviewer?.lastname || ""}`}
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

							setInterviewer({
								...interviewer,
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

							setInterviewer({
								...interviewer,
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

								setInterviewer({
									...interviewer,
									name: newName,
									lastname: newLastname
								});
							}
						}}
					/>
				</div>
				<button className={"edit-interviewer"} onClick={onClickView}>
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
							<input
								className={"interviewer-section-input"}
								type={"tel"}
								value={interviewer ? interviewer.phone : ""}
								onChange={event => {
								const newPhone = event.target.value.replace(/[^0-9]/g, '');
								if (newPhone.length <= 10) {
									setInterviewer({
									...interviewer,
									phone: newPhone
									});
								}
								}}
							/>
							</div>
							<div className={"interviewer-section-item"}>
								<label className={"property"}>Correo</label>
								<input className={"interviewer-section-input"} type={"email"} value={interviewer ? interviewer.email : ""}
									disabled />
							</div>
						</div>
						<label className={"interviewer-section-title"}>Estado y Municipio</label>
						<div className={"interviewer-main-info"}>
							<div className={"interviewer-section-item"}>
								<select name={"state"} className={"interviewer-section-input"} value={state} onChange={event => {
									setState(event.target.value);
									setInterviewer({
										...interviewer,
										stateId: parseInt(event.target.value)
									});
								}}>
									<option value={0}>Seleccionar</option>
									{renderStates}
								</select>
							</div>
							<div className={"interviewer-section-item"}>
								<select name={"city"} className={"interviewer-section-input"} value={city} onChange={event => {
									setCity(event.target.value);
									setInterviewer({
										...interviewer,
										cityId: parseInt(event.target.value)
									});
								}}>
									<option value={0}>Seleccionar</option>
									{renderCities}
								</select>
							</div>
						</div>
					</div>
					<button className={"interviewer-view-accept"} onClick={saveChanges}>Guardar</button>
				</div>
			</div>
		</div>
	)
}

export default InterviewerEdit