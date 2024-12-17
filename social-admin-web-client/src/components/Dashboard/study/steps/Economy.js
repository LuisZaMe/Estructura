import React, { useEffect, useState } from "react";
import { useSelector } from "react-redux";

// Services
import StudyService from "../../../../services/StudyService";
import EconomicSituationService from "../../../../services/EconomicSituationService";
import IncomeService from "../../../../services/IncomeService";
import AdditionalIncomeService from "../../../../services/AdditionalIncomeService";
import CreditService from "../../../../services/CreditService";
import EstateService from "../../../../services/EstateService";
import VehicleService from "../../../../services/VehicleService";
import AuthService from "../../../../services/AuthService";

const Economy = () => {
    // Form data
    const disableForm = AuthService.getIdentity().role === 4;
    const [services, setServices] = useState({
        electricity: {
            label: "Luz",
            amount: 0
        },
        rent: {
            label: "Renta",
            amount: 0
        },
        gas: {
            label: "Gas",
            amount: 0
        },
        infonavit: {
            label: "Infonavit",
            amount: 0
        },
        water: {
            label: "Agua",
            amount: 0
        },
        credits: {
            label: "Créditos",
            amount: 0
        },
        propertyTax: {
            label: "Predio",
            amount: 0
        },
        maintenance: {
            label: "Mantenimiento",
            amount: 0
        },
        internet: {
            label: "Internet",
            amount: 0
        },
        cable: {
            label: "Cable",
            amount: 0
        },
        food: {
            label: "Alimentación",
            amount: 0
        },
        cellphone: {
            label: "Celulares",
            amount: 0
        },
        gasoline: {
            label: "Gasolina",
            amount: 0
        },
        entertainment: {
            label: "Entretenimiento",
            amount: 0
        },
        clothing: {
            label: "Ropa",
            amount: 0
        },
        miscellaneous: {
            label: "Otros (Pañales & Leche)",
            amount: 0
        },
        schoolar: {
            label: "Gastos escolares",
            amount: 0
        }
    })
    const [total, setTotal] = useState(0)
    const [incomes, setIncomes] = useState([
        { name: "", relationship: "", amount: 0 }
    ])
    const [totalIncome, setTotalIncome] = useState(0)
    const [additionalIncomes, setAdditionalIncomes] = useState([
        { activity: "", timeFrame: "", amount: 0 }
    ])
    const [totalAdditionalIncome, setTotalAdditionalIncome] = useState(0)
    const [credits, setCredits] = useState([
        { bank: "", accountNumber: "", creditLimit: 0, debt: 0 }
    ])
    const [totalDebt, setTotalDebt] = useState(0)
    const [estates, setEstates] = useState([
        { concept: "", acquisitionMethod: "", acquisitionDate: "", owner: "", purchaseValue: 0, currentValue: 0 }
    ])
    const [cars, setCars] = useState([
        { plates: "", serialNumber: "", brandAndModel: "", owner: "", purchaseValue: 0, currentValue: 0 }
    ])
    const [economicSituationSummary, setEconomicSituationSummary] = useState("")

    // Get Study id from Redux
    const studyId = useSelector(state => state.study)
    // Add study to state
    const [study, setStudy] = useState({})

    // Date helper
    const pad = (number) => {
        if (number < 10) {
            return '0' + number;
        }
        return number;
    }

    // Make API request to Study Service and search for the study
    const getStudy = async () => {
        let response = await StudyService.getStudy(studyId)
        let study = response.data.response[0]

        setStudy(study)

        // If there is an Economic situation object, pass the values to the form data
        if (study.studyEconomicSituation) {
            // Load services
            let temp = { ...services }
            for (const property in services) {
                temp[property].amount = study.studyEconomicSituation[property]
                setServices(temp)
            }

            let total = 0.0
            for (let property in services) {
                if (services[property].amount) {
                    total = total + parseFloat(services[property].amount)
                }
            }
            setTotal(total)

            // Load incomes
            if (study.studyEconomicSituation.incomingList && study.studyEconomicSituation.incomingList.length > 0) {
                setIncomes(study.studyEconomicSituation.incomingList)
                calculateTotalIncome(study.studyEconomicSituation.incomingList)
            }

            // Load additional incomes
            if (study.studyEconomicSituation.additionalIncomingList && study.studyEconomicSituation.additionalIncomingList.length > 0) {
                setAdditionalIncomes([...study.studyEconomicSituation.additionalIncomingList])
                calculateTotalAdditionalIncome(study.studyEconomicSituation.additionalIncomingList)
            }

            // Load credits
            if (study.studyEconomicSituation.creditList && study.studyEconomicSituation.creditList.length > 0) {
                setCredits([...study.studyEconomicSituation.creditList])
                calculateDebt(study.studyEconomicSituation.creditList)
            }

            // Load estates
            if (study.studyEconomicSituation.estateList && study.studyEconomicSituation.estateList.length > 0) {
                let temp = [...study.studyEconomicSituation.estateList].map(estate => {
                    let tempEstate = { ...estate }
                    // Format date to a valida state
                    let date = new Date(tempEstate.acquisitionDate)
                    tempEstate.acquisitionDate = date.getFullYear() + "-" + pad(date.getMonth() + 1) + "-" + pad(date.getDate())
                    return tempEstate
                })
                setEstates(temp)
            }

            // Load vehicles
            if (study.studyEconomicSituation.vehicleList && study.studyEconomicSituation.vehicleList.length > 0) {
                setCars([...study.studyEconomicSituation.vehicleList])
            }

            // Load Economic Situation Summary
            setEconomicSituationSummary(study.studyEconomicSituation.economicSituationSummary)
        }
    }

    // Get Study
    useEffect(() => {
        if (studyId) {
            getStudy()
        }
    }, [studyId])

    // Submit Data
    const submit = async () => {
        // If economic situation exists update
        if (study.studyEconomicSituation) {
            try {
                // Update Economic situation
                let economicSituation = {
                    "id": study.studyEconomicSituation.id,
                    "studyId": study.id,
                    "electricity": services.electricity.amount,
                    "rent": services.rent.amount,
                    "gas": services.gas.amount,
                    "infonavit": services.infonavit.amount,
                    "water": services.water.amount,
                    "credits": services.credits.amount,
                    "propertyTax": services.propertyTax.amount,
                    "maintenance": services.maintenance.amount,
                    "internet": services.internet.amount,
                    "cable": services.cable.amount,
                    "food": services.food.amount,
                    "cellphone": services.cellphone.amount,
                    "gasoline": services.gasoline.amount,
                    "entertainment": services.entertainment.amount,
                    "clothing": services.clothing.amount,
                    "miscellaneous": services.miscellaneous.amount,
                    "schoolar": services.schoolar.amount,
                    "economicSituationSummary": economicSituationSummary
                }

                // Update core EconomicSituation object
                await EconomicSituationService.update(economicSituation)

                // Submit new incomes
                let newIncomes = incomes.filter(income => !income.studyEconomicSituationId).map(option => {
                    let temp = { ...option }
                    temp.studyEconomicSituationId = study.studyEconomicSituation.id
                    return temp
                })
                if (newIncomes.length > 0) {
                    await IncomeService.create(newIncomes)
                }

                // Update existing incomes
                let existingIncomes = incomes.filter(income => income.studyEconomicSituationId)
                for (const income of existingIncomes) {
                    await IncomeService.update(income)
                }

                // Submit new additional incomes
                let newAdditionalIncomes = additionalIncomes.filter(income => !income.studyEconomicSituationId).map(option => {
                    let temp = { ...option }
                    temp.studyEconomicSituationId = study.studyEconomicSituation.id
                    return temp
                })
                if (newAdditionalIncomes.length > 0) {
                    await AdditionalIncomeService.create(newAdditionalIncomes)
                }

                // Update existing additional incomes
                let existingAdditionalIncomes = additionalIncomes.filter(income => income.studyEconomicSituationId)
                for (const income of existingAdditionalIncomes) {
                    await AdditionalIncomeService.update(income)
                }

                // Submit new credits
                let newCredits = credits.filter(credit => !credit.studyEconomicSituationId).map(option => {
                    let temp = { ...option }
                    temp.studyEconomicSituationId = study.studyEconomicSituation.id
                    return temp
                })
                if (newCredits.length > 0) {
                    await CreditService.create(newCredits)
                }

                // Update existing credits
                let existingCredits = credits.filter(credit => credit.studyEconomicSituationId)
                for (const credit of existingCredits) {
                    await CreditService.update(credit)
                }

                // Submit new estates
                let newEstates = estates.filter(estate => !estate.studyEconomicSituationId).map(option => {
                    let temp = { ...option }
                    temp.studyEconomicSituationId = study.studyEconomicSituation.id
                    return temp
                })
                if (newEstates.length > 0) {
                    await EstateService.create(newEstates)
                }

                // Update existing estates
                let existingEstates = estates.filter(estate => estate.studyEconomicSituationId)
                for (const estate of existingEstates) {
                    await EstateService.update(estate)
                }

                // Submit new vehicles
                let newVehicles = cars.filter(car => !car.studyEconomicSituationId).map(option => {
                    let temp = { ...option }
                    temp.studyEconomicSituationId = study.studyEconomicSituation.id
                    return temp
                })
                if (newVehicles.length > 0) {
                    await VehicleService.create(newVehicles)
                }

                // Update existing vehicles
                let existingVehicles = cars.filter(car => car.studyEconomicSituationId)
                for (const vehicle of existingVehicles) {
                    await VehicleService.update(vehicle)
                }

                // Refresh data
                getStudy()
            } catch (error) {
                console.log(error)
            }
        } else {
            try {
                // Create a new Economic Situation object
                let economicSituation = {
                    "studyId": study.id,
                    "electricity": services.electricity.amount,
                    "rent": services.rent.amount,
                    "gas": services.gas.amount,
                    "infonavit": services.infonavit.amount,
                    "water": services.water.amount,
                    "credits": services.credits.amount,
                    "propertyTax": services.propertyTax.amount,
                    "maintenance": services.maintenance.amount,
                    "internet": services.internet.amount,
                    "cable": services.cable.amount,
                    "food": services.food.amount,
                    "cellphone": services.cellphone.amount,
                    "gasoline": services.gasoline.amount,
                    "entertainment": services.entertainment.amount,
                    "clothing": services.clothing.amount,
                    "miscellaneous": services.miscellaneous.amount,
                    "schoolar": services.schoolar.amount,
                    "economicSituationSummary": economicSituationSummary,
                    "incomingList": [...incomes],
                    "additionalIncomingList": [...additionalIncomes],
                    "creditList": [...credits],
                    "estateList": [...estates],
                    "vehicleList": [...cars]
                }

                // If estates are empty assign null
                if (estates.length === 1 && estates[0].acquisitionDate === "" && estates[0].concept === "") {
                    economicSituation.estateList = null
                }

                await EconomicSituationService.create(economicSituation)

                // Refresh data
                getStudy()
            } catch (error) {
                console.log(error)
            }
        }
    }

    const handleService = (event) => {
        let data = { ...services }
        data[event.target.name].amount = event.target.value ? parseFloat(event.target.value) : 0
        setServices(data)

        // Calculate total of services
        let temp = 0.0
        for (let property in services) {
            if (services[property].amount) {
                temp = temp + parseFloat(services[property].amount)
            }
        }
        setTotal(temp)
    }

    const renderServices = () => {
        const renderedServices = []

        for (const property in services) {
            let item = (
                <div key={`service-${property}`} className={"outcome-item"}>
                    <label>{services[property].label}</label>
                    <input type={"text"} name={property} value={services[property].amount}
                        onChange={event => handleService(event)} placeholder={"Agregar..."} disabled={disableForm} />
                </div>
            )
            renderedServices.push(item)
        }

        return renderedServices
    }

    const handleIncome = (index, event) => {
        let data = [...incomes]
        if (event.target.name !== "amount") {
            data[index][event.target.name] = event.target.value
        } else {
            data[index][event.target.name] = event.target.value ? parseFloat(event.target.value) : 0
        }
        setIncomes(data)

        // Calculate total income
        calculateTotalIncome(data)
    }

    const addIncome = () => {
        let income = { name: "", relationship: "", amount: "" }
        setIncomes([...incomes, income])
    }

    const removeIncome = async (index, input) => {
        try {
            if (input.id) {
                await IncomeService.delete(input.id)
            }

            let data = [...incomes]
            data.splice(index, 1)
            setIncomes(data)

            // Calculate total income
            calculateTotalIncome(data)
        } catch (error) {
            console.log(error)
        }
    }

    const calculateTotalIncome = (data) => {
        let temp = 0
        data.forEach(income => {
            if (income.amount) {
                if (!isNaN(income.amount)) {
                    temp = temp + parseFloat(income.amount)
                } else {
                    temp = temp + income.amount
                }
            }
        })
        setTotalIncome(temp)
    }

    const handleAdditionalIncome = (index, event) => {
        let data = [...additionalIncomes]
        if (event.target.name !== "amount") {
            data[index][event.target.name] = event.target.value
        } else {
            data[index][event.target.name] = event.target.value ? parseFloat(event.target.value) : 0
        }
        setAdditionalIncomes(data)

        // Calculate total additional income
        calculateTotalAdditionalIncome(data)
    }

    const addAdditionalIncome = () => {
        let income = { activity: "", timeFrame: "", amount: "" }
        setAdditionalIncomes([...additionalIncomes, income])
    }

    const removeAdditionalIncome = async (index, input) => {
        try {
            if (input.id) {
                await AdditionalIncomeService.delete(input.id)
            }

            let data = [...additionalIncomes]
            data.splice(index, 1)
            setAdditionalIncomes(data)

            // Calculate total additional income
            calculateTotalAdditionalIncome(data)
        } catch (error) {
            console.log(error)
        }
    }

    const calculateTotalAdditionalIncome = (data) => {
        let temp = 0
        data.forEach(income => {
            if (income.amount) {
                if (!isNaN(income.amount)) {
                    temp = temp + parseFloat(income.amount)
                } else {
                    temp = temp + income.amount
                }
            }
        })
        setTotalAdditionalIncome(temp)
    }

    const handleCredit = (index, event) => {
        let data = [...credits]
        if (event.target.name !== "debt" && event.target.name !== "creditLimit") {
            data[index][event.target.name] = event.target.value
        } else {
            data[index][event.target.name] = event.target.value ? parseFloat(event.target.value) : 0
        }
        setCredits(data)

        // Calculate debt
        calculateDebt(data)
    }

    const addCredit = () => {
        let credit = { bank: "", accountNumber: "", creditLimit: "", debt: "" }
        setCredits([...credits, credit])
    }

    const removeCredit = async (index, input) => {
        try {
            if (input.id) {
                await CreditService.delete(input.id)
            }

            let data = [...credits]
            data.splice(index, 1)
            setCredits(data)

            // Calculate debt
            calculateDebt(data)
        } catch (error) {
            console.log(error)
        }
    }

    const calculateDebt = (data) => {
        let temp = 0
        data.forEach(credit => {
            if (credit.debt) {
                if (!isNaN(credit.debt)) {
                    temp = temp + parseFloat(credit.debt)
                } else {
                    temp = temp + credit.debt
                }
            }
        })
        setTotalDebt(temp)
    }

    const handleEstate = (index, event) => {
        let data = [...estates]
        if (event.target.name !== "purchaseValue" && event.target.name !== "currentValue") {
            data[index][event.target.name] = event.target.value
        } else {
            data[index][event.target.name] = event.target.value ? parseFloat(event.target.value) : 0
        }
        setEstates(data)
    }

    const addEstate = () => {
        let estate = {
            concept: "",
            acquisitionMethod: "",
            acquisitionDate: "",
            owner: "",
            purchaseValue: "",
            currentValue: ""
        }
        setEstates([...estates, estate])
    }

    const removeEstate = async (index, input) => {
        try {
            if (input.id) {
                await EstateService.delete(input.id)
            }

            let data = [...estates]
            data.splice(index, 1)
            setEstates(data)
        } catch (error) {
            console.log(error)
        }
    }

    const handleCar = (index, event) => {
        let data = [...cars]
        if (event.target.name !== "purchaseValue" && event.target.name !== "currentValue") {
            data[index][event.target.name] = event.target.value
        } else {
            data[index][event.target.name] = event.target.value ? parseFloat(event.target.value) : 0
        }
        setCars(data)
    }

    const addCar = () => {
        let car = {
            plates: "",
            serialNumber: "",
            brandAndModel: "",
            owner: "",
            purchaseValue: "",
            currentValue: ""
        }
        setCars([...cars, car])
    }

    const removeCar = async (index, input) => {
        try {
            if (input.id) {
                await VehicleService.delete(input.id)
            }

            let data = [...cars]
            data.splice(index, 1)
            setCars(data)
        } catch (error) {
            console.log(error)
        }
    }

    // Display form only if Economic situation is required
    if (study && study.fieldsToFill && study.fieldsToFill.economicSituation) {
        return (
            <div className={"economy"}>
                <h2>Economía</h2>
                <div className={"outcomes"}>
                    <h2>Situación económica</h2>
                    <h3>Egresos</h3>
                    <div className={"outcomes-grid"}>
                        <div className={"outcome-grid-header"}>
                            <div className={"outcome-header-item"}>
                                <label>Servicios</label>
                                <label>Importe mensual</label>
                            </div>
                            <div className={"outcome-header-item"}>
                                <label>Servicios</label>
                                <label>Importe mensual</label>
                            </div>
                        </div>
                        <div className={"outcome-grid-body"}>
                            {renderServices()}
                            <div className={"outcome-item outcome-total"}>
                                <label>Total</label>
                                <span>{"$" + total}</span>
                            </div>
                        </div>
                    </div>
                </div>
                <div className={"incomes"}>
                    <h2>Ingresos</h2>
                    <div className={"incomes-grid"}>
                        <div className={"incomes-header"}>
                            <label>Nombre</label>
                            <label>Parentesco</label>
                            <label>Monto (Aportaciones)</label>
                        </div>
                        {
                            incomes.map((input, index) => {
                                return (
                                    <div key={`income-${index}`} className={"incomes-item"}>
                                        <input type={"text"} name={"name"} placeholder={"Agregar..."} value={input.name}
                                            onChange={event => handleIncome(index, event)} disabled={disableForm} />
                                        <input type={"text"} name={"relationship"} placeholder={"Agregar..."}
                                            value={input.relationship}
                                            onChange={event => handleIncome(index, event)} disabled={disableForm} />
                                        <input type={"text"} name={"amount"} placeholder={"Agregar"}
                                            value={input.amount}
                                            onChange={event => handleIncome(index, event)} disabled={disableForm} />
                                        {
                                            index !== 0 ?
                                                <button className={"remove-item-button"}
                                                    onClick={() => removeIncome(index, input)} disabled={disableForm}>
                                                    <img src={"/images/trash-icon.png"} alt={""} />
                                                </button> : null
                                        }
                                    </div>
                                )
                            })
                        }
                        <div className={"incomes-total"}>
                            <label>Total</label>
                            <span>${totalIncome}</span>
                        </div>
                        <div className={"incomes-footer"}>
                            <button className={"add-item-button"} onClick={addIncome} disabled={disableForm}>
                                <img src={"/images/add-icon.svg"} alt={""} />
                            </button>
                        </div>
                    </div>
                </div>
                <div className={"incomes"}>
                    <h2>Ingresos adicionales</h2>
                    <div className={"incomes-grid"}>
                        <div className={"incomes-header"}>
                            <label>Actividad</label>
                            <label>Periodo</label>
                            <label>Ingresos</label>
                        </div>
                        {
                            additionalIncomes.map((input, index) => {
                                return (
                                    <div key={`additional-income-${index}`} className={"incomes-item"}>
                                        <input type={"text"} name={"activity"} placeholder={"Agregar..."}
                                            value={input.activity}
                                            onChange={event => handleAdditionalIncome(index, event)} disabled={disableForm} />
                                        <input type={"text"} name={"timeFrame"} placeholder={"Agregar..."}
                                            value={input.timeFrame}
                                            onChange={event => handleAdditionalIncome(index, event)} disabled={disableForm} />
                                        <input type={"text"} name={"amount"} placeholder={"Agregar..."}
                                            value={input.amount}
                                            onChange={event => handleAdditionalIncome(index, event)} disabled={disableForm} />
                                        {index !== 0 ?
                                            <button className={"remove-item-button"}
                                                onClick={() => removeAdditionalIncome(index, input)} disabled={disableForm}>
                                                <img src={"/images/trash-icon.png"} alt={""} />
                                            </button> : null
                                        }
                                    </div>
                                )
                            })
                        }
                        <div className={"incomes-total"}>
                            <label>Total</label>
                            <span>${totalAdditionalIncome}</span>
                        </div>
                        <div className={"incomes-footer"}>
                            <button className={"add-item-button"} onClick={addAdditionalIncome} disabled={disableForm}>
                                <img src={"/images/add-icon.svg"} alt={""} />
                            </button>
                        </div>
                    </div>
                </div>
                <div className={"credits"}>
                    <h2>Créditos</h2>
                    <div className={"credits-grid"}>
                        <div className={"credits-header"}>
                            <label>Banco/Institución</label>
                            <label>No. de Cuenta</label>
                            <label>Límite de crédito</label>
                            <label>Adeudo</label>
                        </div>
                        {
                            credits.map((input, index) => {
                                return (
                                    <div key={`credit-${index}`} className={"credits-item"}>
                                        <input type={"text"} name={"bank"} placeholder={"Agregar..."}
                                            value={input.bank}
                                            onChange={event => handleCredit(index, event)} disabled={disableForm} />
                                        <input type={"text"} name={"accountNumber"} placeholder={"Agregar..."}
                                            value={input.accountNumber}
                                            onChange={event => handleCredit(index, event)} disabled={disableForm} />
                                        <input type={"text"} name={"creditLimit"} placeholder={"Agregar..."}
                                            value={input.creditLimit}
                                            onChange={event => handleCredit(index, event)} disabled={disableForm} />
                                        <input type={"text"} name={"debt"} placeholder={"Agregar..."} value={input.debt}
                                            onChange={event => handleCredit(index, event)} disabled={disableForm} />
                                        {
                                            index !== 0 ?
                                                <button className={"remove-item-button"}
                                                    onClick={() => removeCredit(index, input)} disabled={disableForm}>
                                                    <img src={"/images/trash-icon.png"} alt={""} />
                                                </button> : null
                                        }
                                    </div>
                                )
                            })
                        }
                        <div className={"credits-total"}>
                            <label>Total</label>
                            <span>${totalDebt}</span>
                        </div>
                        <div className={"credits-footer"}>
                            <button className={"add-item-button"} onClick={addCredit} disabled={disableForm}>
                                <img src={"/images/add-icon.svg"} alt={""} />
                            </button>
                        </div>
                    </div>
                </div>
                <div className={"estates"}>
                    <h2>Bienes</h2>
                    <div className={"estates-grid"}>
                        <div className={"estates-header"}>
                            <label>Concepto</label>
                            <label>Forma de adquisición</label>
                            <label>Año de adquisición</label>
                            <label>Titular</label>
                            <label>Valor de compra</label>
                            <label>Valor actual</label>
                        </div>
                        {
                            estates.map((input, index) => {
                                return (
                                    <div key={`estate-${index}`} className={"estates-item"}>
                                        <input type={"text"} name={"concept"} placeholder={"Agregar..."}
                                            value={input.concept}
                                            onChange={event => handleEstate(index, event)} disabled={disableForm} />
                                        <input type={"text"} name={"acquisitionMethod"} placeholder={"Agregar..."}
                                            value={input.acquisitionMethod}
                                            onChange={event => handleEstate(index, event)} disabled={disableForm} />
                                        <input type={"date"} name={"acquisitionDate"} placeholder={"Agregar..."}
                                            value={input.acquisitionDate}
                                            onChange={event => handleEstate(index, event)} disabled={disableForm} />
                                        <input type={"text"} name={"owner"} placeholder={"Agregar..."}
                                            value={input.owner}
                                            onChange={event => handleEstate(index, event)} disabled={disableForm} />
                                        <input type={"text"} name={"purchaseValue"} placeholder={"Agregar..."}
                                            value={input.purchaseValue}
                                            onChange={event => handleEstate(index, event)} disabled={disableForm} />
                                        <input type={"text"} name={"currentValue"} placeholder={"Agregar..."}
                                            value={input.currentValue}
                                            onChange={event => handleEstate(index, event)} disabled={disableForm} />
                                        {
                                            index !== 0 ?
                                                <button className={"remove-item-button"}
                                                    onClick={() => removeEstate(index, input)}>
                                                    <img src={"/images/trash-icon.png"} alt={""} disabled={disableForm} />
                                                </button> : null
                                        }
                                    </div>
                                )
                            })
                        }
                        <div className={"estates-footer"}>
                            <button className={"add-item-button"} onClick={addEstate} disabled={disableForm}>
                                <img src={"/images/add-icon.svg"} alt={""} />
                            </button>
                        </div>
                    </div>
                </div>
                <div className={"cars"}>
                    <h2>Vehículo</h2>
                    <div className={"cars-grid"}>
                        <div className={"cars-header"}>
                            <label>Placas</label>
                            <label>No. de serie</label>
                            <label>Marca y modelo</label>
                            <label>Titular</label>
                            <label>Valor compra</label>
                            <label>Valor actual</label>
                        </div>
                        {
                            cars.map((input, index) => {
                                return (
                                    <div key={`cars-${index}`} className={"cars-item"}>
                                        <input type={"text"} name={"plates"} placeholder={"Agregar..."}
                                            value={input.plates}
                                            onChange={event => handleCar(index, event)} disabled={disableForm} />
                                        <input type={"text"} name={"serialNumber"} placeholder={"Agregar..."}
                                            value={input.serialNumber} onChange={event => handleCar(index, event)} disabled={disableForm} />
                                        <input type={"text"} name={"brandAndModel"} placeholder={"Agregar..."}
                                            value={input.brandAndModel}
                                            onChange={event => handleCar(index, event)} disabled={disableForm} />
                                        <input type={"text"} name={"owner"} placeholder={"Agregar..."}
                                            value={input.owner}
                                            onChange={event => handleCar(index, event)} disabled={disableForm} />
                                        <input type={"text"} name={"purchaseValue"} placeholder={"Agregar..."}
                                            value={input.purchaseValue} onChange={event => handleCar(index, event)} disabled={disableForm} />
                                        <input type={"text"} name={"currentValue"} placeholder={"Agregar..."}
                                            value={input.currentValue} onChange={event => handleCar(index, event)} disabled={disableForm} />
                                        {
                                            index !== 0 ?
                                                <button className={"remove-item-button"}
                                                    onClick={() => removeCar(index, input)} disabled={disableForm}>
                                                    <img src={"/images/trash-icon.png"} alt={""} />
                                                </button> : null
                                        }
                                    </div>
                                )
                            })
                        }
                        <div className={"cars-footer"}>
                            <button className={"add-item-button"} onClick={addCar} disabled={disableForm}>
                                <img src={"/images/add-icon.svg"} alt={""} />
                            </button>
                        </div>
                    </div>
                </div>
                <div className={"economic-analysis"}>
                    <label>Análisis económico:</label>
                    <textarea placeholder={"Agregar comentario"} value={economicSituationSummary}
                        onChange={event => setEconomicSituationSummary(event.target.value)} disabled={disableForm} />
                </div>
                <div className={"result-socioeconomic-save"}>
                    <button className={"form-button-primary save-step"} onClick={submit} disabled={disableForm}>Guardar</button>
                </div>
            </div>
        )
    } else {
        return null
    }
}

export default Economy