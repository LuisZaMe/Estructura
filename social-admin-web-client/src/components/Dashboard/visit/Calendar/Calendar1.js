import React, { useState, useRef } from 'react';
import { CalendarCoupons } from './Calendar';
import atras from '../../assets/images/pumpedAssets/Icono_atras.png'
import warning from '../../assets/images/pumpedAssets/calendar/ICONO_WARNING.png'
import sinCupo from '../../assets/images/pumpedAssets/calendar/ICONO_SIN_CUPO.png'
import checkbox from '../../assets/images/pumpedAssets/calendar/ICONO_CHECKBOX.png'
import cancel from '../../assets/images/pumpedAssets/calendar/ICONO_CANCEL.png'
import arrow from '../../assets/images/pumpedAssets/calendar/Arrow_Calendar.png'
import './Calendar.scss'
import { ModalCreate } from '../../components/Sesions/Modals/ModalCreate';
import { ModalEdit } from '../../components/Sesions/Modals/ModalEdit';
import { ModalDelete } from '../../components/Sesions/Modals/ModalDelete';
import { ModalEditSerie } from '../../components/Sesions/Modals/ModalEditSerie';
import { ModalDetalle } from '../../components/Sesions/Modals/ModalDetalle';
import { useDispatch, useSelector } from 'react-redux';
import { upsertSession } from '../../redux/actions/sessionAction';




export const Calendar = () => {
    const [globalDate, setGlobalDate] = useState(new Date());
    const [calendarMode, setCalendarMode] = useState("month");
    const [sessionEdit, setSessionEdit] = useState();
    const [sessionSelected, setSessionSelected] = useState();
    const moment = require('moment');
    const dispatch = useDispatch();
    const day = moment().toDate();
    const MESES = [
        "ENERO",
        "FEBRERO",
        "MARZO",
        "ABRIL",
        "MAYO",
        "JUNIO",
        "JULIO",
        "AGOSTO",
        "SEPTIEMBRE",
        "OCTUBRE",
        "NOVIEMBRE",
        "DICIEMBRE",
    ];

    const {
        format,
        getYear,
        getDay,
        getWeek,
        addWeeks,
        startOfWeek,
        endOfWeek,
        subWeeks,
        addDays,
        subDays,
        addMonths,
        subMonths,

    } = require('date-fns');
    const subDates = () => {
        if (calendarMode === "month") {
            setGlobalDate(subMonths(globalDate, 1))
        } else if (calendarMode === "week") {
            setGlobalDate(subWeeks(globalDate, 1))
        } else if (calendarMode === "day") {
            setGlobalDate(subDays(globalDate, 1))
        }
    }
    const addDates = () => {
        if (calendarMode === "month") {
            setGlobalDate(addMonths(globalDate, 1))
        } else if (calendarMode === "week") {
            setGlobalDate(addWeeks(globalDate, 1))
        } else if (calendarMode === "day") {
            setGlobalDate(addDays(globalDate, 1))
        }
    }

    const [openEliminarModal, setOpenEliminarModal] = useState(false);
    const showModal = () => {
        setOpenEliminarModal(!openEliminarModal);

        setOpenThirdModal(false);

    }
    const [openEliminarModalEdit, setOpenEliminarModalEdit] = useState(false);
    const showModalEdit = () => {
        // setIdModify(id);
        setOpenEliminarModalEdit(!openEliminarModalEdit);

        setOpenEliminarModal(false);
        setOpenThirdModal(false);

    }


    const [openEliminarModalEditSerie, setOpenEliminarModalEditSerie] = useState(false);
    const showModalEditSerie = (data) => {
        console.error("entro");
        console.warn(data);
        setSessionEdit(data);
        // console.warn("SE LLAMO ESTE PEDO")
        setOpenEliminarModalEdit(false);
        setOpenEliminarModalEditSerie(!openEliminarModalEditSerie);

    }
    const [openSecondModal, setOpenSecondModal] = useState(false);
    const showModalTwo = () => {
        setOpenSecondModal(!openSecondModal);
        setOpenThirdModal(false);
    }
    const [openThirdModal, setOpenThirdModal] = useState(false);
    const showModalThree = (session) => {
        setSessionSelected(session);
        setOpenThirdModal(!openThirdModal);
        setOpenEliminarModal(false);
    }

    return (
        <>
            <ModalEdit showModal={showModalEdit} session={sessionSelected} setSessionSelected={setSessionSelected} openEliminarModal={openEliminarModalEdit} setOpenEliminarModal={setOpenEliminarModalEdit} showModalEditSerie={showModalEditSerie} />
            <ModalCreate globalDate={globalDate} showModal={showModal} openEliminarModal={openEliminarModal} setOpenEliminarModal={showModalThree} type={'create'} />
            <ModalDelete globalDate={globalDate} showModal={showModalTwo} sessionSelected={sessionSelected} openEliminarModal={openSecondModal} showModalThree={showModalThree} />
            <ModalDetalle showModal={showModalThree} session={sessionSelected} openEliminarModal={openThirdModal} setOpenEliminarModal={showModalTwo} setOpenEliminarModal2={showModalEdit} />
            <ModalEditSerie globalDate={globalDate} showModal={showModalEditSerie} sessionSelected={sessionEdit} openEliminarModal={openEliminarModalEditSerie} showModalThree={showModalThree} />
            <div>
                <section className='calendar-section'>
                    <div className='calendar-container'>
                        <img className='backbutton' src={atras} onClick={() => history.goBack()} />
                        <div className='body-container-calendar'>
                            <div className='text-container-calendar'>
                                <div className='header-text'>
                                    <h1>CALENDARIO</h1>
                                </div>
                                <div className='body-text'>
                                    <p>Crea sesiones para PUMPED GYM:
                                    </p>
                                </div>
                            </div>
                            <div className='icons-container-calendar'>
                                <div className='div-button'>
                                    <button className='btn-add' onClick={() => showModal()}>AGREGAR</button>
                                </div>
                                <div className='div-icons'>

                                    <div className='icon-container-calendar div-width1 width-calendar'>
                                        <div className='icon'>
                                            <img className='first' src={sinCupo} />
                                        </div>
                                        <div className='text1'>
                                            <h1>Clases sin cupo</h1>
                                        </div>
                                    </div>
                                    <div className='icon-container-calendar div-width2 width-calendar'>
                                        <div className='icon'>
                                            <img src={warning} />
                                        </div>
                                        <div className='text2'>
                                            <h1>Clases casi sin cupo</h1>
                                        </div>
                                    </div>
                                </div>
                                {/* <div className='icon-container-calendar div-width3'>
                                <div className='icon'>
                                    <img src={cancel}/>
                                </div>
                                <div className='text3'>
                                    <h1>Mis clases canceladas</h1>
                                </div>
                                
                            </div>
                            <div className='icon-container-calendar div-width4'>
                                <div className='icon'>
                                    <img src={checkbox}/>
                                </div>
                                <div className='text4'>
                                    <h1>Mis clases RESERVADAS</h1>
                                </div>
                                
                            </div> */}
                            </div>
                            <div className='header-calendar'>
                                <div className='date-container-calendar'>
                                    <h1>
                                        {
                                            (() => {
                                                let date;
                                                if (calendarMode === 'month') {
                                                    date = MESES[globalDate.getMonth()] + " DEL  " + getYear(globalDate)
                                                } else if (calendarMode === 'week') {
                                                    date = format(startOfWeek(globalDate), "dd") + " DE " + MESES[globalDate.getMonth()] + " - " + format(endOfWeek(globalDate), "dd") + " DE " + MESES[globalDate.getMonth()] + " DEL " + getYear(globalDate)
                                                } else if (calendarMode === 'day') {
                                                    date = format(globalDate, "dd") + " DE " + MESES[globalDate.getMonth()] + " DEL " + getYear(globalDate)
                                                }
                                                return date;
                                            })()
                                        }

                                        {/* {MESES[globalDate.getMonth()] + " DEL  " + getYear(globalDate)} */}
                                    </h1>
                                </div>
                                <div className='button-container-calendar'>
                                    <div className='arrows'>
                                        <img src={arrow} onClick={subDates} />
                                        <img className='right' src={arrow} onClick={addDates} />
                                    </div>
                                    <div>
                                        <button className={[(calendarMode === "month" ? "button-calendar-selected" : 'button-calendar')].join(' ')} onClick={() => setCalendarMode("month")}>
                                            MES
                                        </button>
                                    </div>
                                    <div>
                                        <button className={[(calendarMode === "week" ? "button-calendar-selected" : 'button-calendar')].join(' ')} onClick={() => setCalendarMode("week")}>
                                            SEMANA
                                        </button>
                                    </div>
                                    <div>
                                        <button className={[(calendarMode === "day" ? "button-calendar-selected" : 'button-calendar')].join(' ')} onClick={() => setCalendarMode("day")}>
                                            DÍA
                                        </button>
                                    </div>
                                </div>
                            </div>
                            <div className='container-reservation-calendar'>

                                <CalendarCoupons
                                    date={globalDate}
                                    handleDay={day}
                                    name={'travelWindowDateEnds'}
                                    selected={day}
                                    openMode={calendarMode}
                                    showModal={showModal}
                                    showModalEdit={showModalEdit}
                                    showModalTwo={showModalTwo}
                                    showModalThree={showModalThree}
                                    showModalEditSerie={showModalEditSerie}
                                // showModalThree={showModalThree}
                                />
                            </div>

                        </div>
                    </div>

                </section>

            </div>
        </>
    )
};
