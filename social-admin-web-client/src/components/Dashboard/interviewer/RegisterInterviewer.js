import React, { useState, useEffect } from "react";
import { useHistory } from "react-router-dom";
import { useDispatch, useSelector } from "react-redux";

// Actions
import { hideRegisterInterviewer, setInterviewerId } from "../../../actions";

// Models
import Account from "../../../model/Account";

// Services
import AccountService from "../../../services/AccountService";
import LocationService from "../../../services/LocationService";

const RegisterInterviewer = () => {
	const dispatch = useDispatch();
	const history = useHistory();

	const show = useSelector(state => state.registerInterviewer);

	const [name, setName] = useState("");
	const [lastname, setLastname] = useState("");
	const [phone, setPhone] = useState("");
	const [email, setEmail] = useState("");

	const [state, setState] = useState(0);
	const [states, setStates] = useState([]);
	const [cities, setCities] = useState([]);
	const [city, setCity] = useState(0);

	const onSubmit = async (event) => {
		event.preventDefault()

		try {
			const interviewer = new Account(email, name, lastname, Account.INTERVIEWER, phone, null, parseInt(state), parseInt(city))

			const response = await AccountService.create(interviewer)
			dispatch(setInterviewerId(response.data.response.id))

			dispatch(hideRegisterInterviewer())
			history.push("/dashboard/entrevistadores/ver")
		} catch (error) {
			console.log(error)
		}
	}

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

	const showHideModal = show ? "modal display-block" : "modal display-none"

	return (
		<div className={showHideModal}>
			<div className={"form-register-interviewer"}>
				<div className={"close-modal"}>
					<img src={"/images/icon-close.png"} alt={""} onClick={() => dispatch(hideRegisterInterviewer())} />
				</div>
				<h2>Registro de Entrevistador</h2>
				<form className={"form-section"} onSubmit={onSubmit}>
					<div className={"form-item"}>
						<label htmlFor={"name"}>Nombre(s)</label>
						<input type={"text"} name={"name"} placeholder={"Agregar nombre(s)"} required value={name}
							onChange={event => setName(event.target.value)} />
					</div>
					<div className={"form-item"}>
						<label htmlFor={"lastname"}>Apellido(s)</label>
						<input type={"text"} name={"lastname"} placeholder={"Agregar apellido(s)"} required value={lastname}
							onChange={event => setLastname(event.target.value)} />
					</div>
					<div className={"form-item"}>
						<label htmlFor={"phone"}>Telefono</label>
						<input type={"tel"} name={"phone"} placeholder={"Agregar telefono"} required value={phone}
							onChange={event => setPhone(event.target.value)} />
					</div>
					<div className={"form-item"}>
						<label htmlFor={"email"}>Correo</label>
						<input type={"email"} name={"email"} placeholder={"Agregar correo"} required value={email}
							onChange={event => setEmail(event.target.value)} />
					</div>
					<div className={"form-item"}>
						<label>Estado y Municipio</label>
						<div className={"general-data-item-double"}>
							<select name={"state"} value={state} onChange={event => setState(event.target.value)}>
								<option value={0}>Seleccionar</option>
								{renderStates}
							</select>
							<select name={"city"} value={city} onChange={event => setCity(event.target.value)}>
								<option value={0}>Seleccionar</option>
								{renderCities}
							</select>
						</div>
					</div>
					<div className={"form-action"}>
						<button className={"form-button-primary"}>Registrar</button>
					</div>
				</form>
			</div>
		</div>
	)
}

export default RegisterInterviewer