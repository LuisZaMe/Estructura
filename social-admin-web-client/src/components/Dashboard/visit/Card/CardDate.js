import React from 'react';
import './CardDate.css';
import moment from 'moment';
import { removeVisitId, setVisitId, showEditVisit } from "../../../../actions/index.js";
import { useDispatch, useSelector } from 'react-redux';
import _ from 'lodash';

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
const DIAS = [
    "DOMINGO",
    "LUNES",
    "MARTES",
    "MIÉRCOLES",
    "JUEVES",
    "VIERNES",
    "SABADO"
];
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
const users = [
    {
        nameUser: 'Eric',
        hora: '12hrs'
    },
    {
        nameUser: 'Jesus',
        hora: '12hrs'
    },
    {
        nameUser: 'Juan',
        hora: '12hrs'
    },
    {
        nameUser: 'Jose',
        hora: '12hrs'
    },
]

const colors = [
    "#00A987", "#1BB9EF", "#0F92DB", "#07DDA5"
]
export const CardDate = (props) => {
    const dispatch = useDispatch();
    const { date, visits, setVisitToEdit } = props;
    let today = new Date();

    const visit = useSelector(state => state)
    return (
        <div className='card-content'>
            <div className='header-card'>
                <h2 className={format(date, "dd/MM/yy") === format(today, "dd/MM/yy") ? '' : 'today-class'}>
                    {
                        format(date, "dd/MM/yy") === format(today, "dd/MM/yy") ? 'HOY' : 'OTRO'
                    }
                </h2>
                <div className='date-container-card'>
                    <div className='number'>
                        <h2 className='number-text'>{format(date, "dd")}</h2>
                    </div>
                    <div className='date'>
                        <p>{DIAS[date.getDay()]}</p>
                        <p>{MESES[date.getMonth()]} DEL {getYear(date)}</p>
                    </div>
                </div>
            </div>
            <div className='body-card'>
                <h2>
                    Visitas Agendadas
                </h2>
                {
                    visits.map((visit, index) => {
                        let visitDate = moment.utc(_.get(visit, 'visitDate', '')).local().format("DD/MM/YYYY");
                        let dateSelected = moment.utc(date).local().format("DD/MM/YYYY");
                        if (visitDate === dateSelected) {
                            return (

                                <div className='user-subcard' style={{ backgroundColor: visit.notationColor }} onClick={() => (dispatch(showEditVisit()), setVisitToEdit(visit))}>
                                    <p>{_.get(visit, 'study.candidate.name', '')}</p>
                                    <p>{moment.utc(_.get(visit, 'visitDate', '')).local().format("HH:mm")}</p>
                                </div>
                            )
                        }
                    })
                }
            </div>


        </div>
    )
}
