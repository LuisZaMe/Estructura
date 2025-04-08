import React, { useState, useEffect, useRef } from "react";
import { useHistory } from "react-router-dom";
import { useDispatch, useSelector } from "react-redux";

// Actions
import { hideRegisterInterviewer, setInterviewerId } from "../../../actions";

// Models
import Account from "../../../model/Account";

// Services
import AccountService from "../../../services/AccountService";
import LocationService from "../../../services/LocationService";

//css
import "./RegisterInterviewer.css";

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
	const [showSuccessMessage, setShowSuccessMessage] = useState(false)
	const [isSubmitting, setIsSubmitting] = useState(false);
	const timeoutRef = useRef()

	useEffect(() => {
		return () => {
			if (timeoutRef.current) {
				clearTimeout(timeoutRef.current)
			}
		}
	}, [])

	const onSubmit = async (event) => {
		event.preventDefault()
		if (isSubmitting) return;
    
        setIsSubmitting(true);

		try {
			const interviewer = new Account(email, name, lastname, Account.INTERVIEWER, phone, null, parseInt(state), parseInt(city))

			const response = await AccountService.create(interviewer)
			dispatch(setInterviewerId(response.data.response.id))
			
			setShowSuccessMessage(true)
            timeoutRef.current = setTimeout(() => {
				dispatch(hideRegisterInterviewer())
				history.push("/dashboard/entrevistadores/ver")
		}, 3000)
		} catch (error) {
			console.log(error)
			setIsSubmitting(false)
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
				{showSuccessMessage && (
							<div className="confirmation-message">
								¡Cargando Entrevistador!
							</div>
						)}
				<div className={"close-modal"}>
					<img src={"/images/icon-close.png"} alt={""} onClick={() => dispatch(hideRegisterInterviewer())} />
				</div>
				<h2>Registro de Entrevistador</h2>
				<form className={"form-section"} onSubmit={onSubmit}>
					<div className="form-item">
						<label htmlFor="name">Nombre(s)</label>
						<input
							type="text"
							name="name"
							placeholder="Agregar nombre(s)"
							required
							value={name}
							maxLength={40}
							onChange={(event) => {
								const input = event.target.value;
								const cleanedInput = input.replace(/[^a-zA-ZáéíóúÁÉÍÓÚñÑ\s]/g, '');
								const validInput = cleanedInput.split(' ').filter(Boolean);
								if (validInput.length <= 2) {
									setName(cleanedInput);
								}
							}}
							onBlur={() => setName(name.trim())}
							onKeyDown={(event) => {
								if (event.key === 'Enter') {
									setName(name.trim());
								}
							}}
						/>
					</div>
					<div className="form-item">
						<label htmlFor="lastname">Apellido(s)</label>
						<input
							type="text"
							name="lastname"
							placeholder="Agregar apellido(s)"
							required
							value={lastname}
							maxLength={40}
							onChange={(event) => {
								const input = event.target.value;
								const cleanedInput = input.replace(/[^a-zA-ZáéíóúÁÉÍÓÚñÑ\s]/g, '');
								const validInput = cleanedInput.split(' ').filter(Boolean);
								if (validInput.length <= 2) {
									setLastname(cleanedInput);
								}
							}}
							onBlur={() => setLastname(lastname.trim())}
							onKeyDown={(event) => {
								if (event.key === 'Enter') {
									setLastname(lastname.trim());
								}
							}}
						/>
					</div>
					<div className="form-item">
						<label htmlFor="phone">Teléfono</label>
						<input
							type="tel"
							name="phone"
							placeholder="Agregar teléfono"
							required
							value={phone}
							maxLength={10}
							onChange={(event) => {
								const input = event.target.value.replace(/\D/g, '');
								if (input.length <= 10) {
									setPhone(input);
								}
							}}
							onBlur={() => {
								if (phone.length !== 10) {
									alert("El número debe tener exactamente 10 dígitos.");
									setPhone("");
								}
							}}
							onKeyDown={(event) => {
								if (event.key === 'Enter' && phone.length !== 10) {
									alert("El número debe tener exactamente 10 dígitos.");
									setPhone("");
								}
							}}
						/>
					</div>
					<div className={"form-item"}>
					<label htmlFor={"email"}>Correo</label>
					<input
						type={"email"}
						name={"email"}
						placeholder={"Agregar correo"}
						required
						value={email}
						onChange={event => setEmail(event.target.value)}
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
						{!isSubmitting ? (
								<button className={"form-button-primary"}>Registrar</button>
							) : (
								<div className="processing-message">Procesando solicitud...</div>
							)}
					</div>
				</form>
			</div>
		</div>
	)
}

export default RegisterInterviewer