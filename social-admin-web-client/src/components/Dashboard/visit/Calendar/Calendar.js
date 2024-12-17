import React, { useEffect, useState } from 'react';
import moment from 'moment';
import "./Calendars.scss";
import './Calendar.scss';
import './CalendarCoupons.scss';



const {
  format,
  isSameMonth,

  startOfMonth,
  startOfWeek,
  startOfDay,

  endOfMonth,
  endOfWeek,

  addDays,
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

function takeWeek(start) {
  let date = startOfWeek(startOfDay(start));

  return function () {
    const week = [...Array(7)].map((_, i) => addDays(date, i));
    date = addDays(week[6], 1);
    return week;
  };
}


export const Calendar = (props) => {
  const [globalDate, setGlobalDate] = useState((props.date ? props.date : new Date()));
  const moment = require('moment');

  useEffect(() => {
    setGlobalDate(props.date)
  }, [props.date]);


  // -----------------------------------------------
  function takeMonth(start = globalDate) {
    let month = [];
    let date = start;

    function lastDayOfRange(range) {
      return range[range.length - 1][6];
    }
    return function () {
      const weekGen = takeWeek(startOfMonth(date));
      const endDate = startOfDay(endOfWeek(endOfMonth(date)));
      month.push(weekGen());


      while (lastDayOfRange(month) < endDate) {
        month.push(weekGen());
      }

      const range = month;
      month = [];
      date = addDays(lastDayOfRange(range), 1);
      return range;
    };
  }
  const data = takeMonth()();

  const Month = () => {
    const diaPrueba = '2022-07-08T00:00:00.000Z'
    return (
      <div className={"admin-schedule-content-box calendar"}>
        <div className={"admin-schedule-calendar"}>
          <div className="weekdays">
            <span>Domingo</span>
            <span>Lunes</span>
            <span>Martes</span>
            <span>Miércoles</span>
            <span>Jueves</span>
            <span>Viernes</span>
            <span>Sábado</span>
          </div>

          <div className="days">
            {data.map((week) => (
              <>
                {week.map((day, index) => (
                  <div className='div-month'
                    key={index}
                    onClick={() =>
                      (!isSameMonth(day, globalDate) ? "" : props.setGlobalDate(day))
                    }
                    style={moment(props.selected).format('YYYY-MM-DD') === (moment(day).format('YYYY-MM-DD')) ? { background: '#BEC3D0', color: 'black' } : {}}
                  >
                    <span className={[(!isSameMonth(day, globalDate) ? "dissapear-text" : "")].join(' ')}>
                      {format(day, "dd")}
                    </span>
                    <span style={{ display: "flex", flexWrap: "wrap" }} className={[(!isSameMonth(day, globalDate) ? "dissapear-text" : "")].join(' ')}>
                      {props.visits.map((element) => {
                        let dayMap = moment.utc(element.visitDate).local();
                        if (moment(dayMap).format("DD MM YYYY") === moment(day).format("DD MM YYYY")) {

                          return (
                            <>
                              <div style={{ width: "15px", height: "15px", borderRadius: "50%", margin: "2px", backgroundColor: element.notationColor }} >

                              </div>
                              {/* <p>{moment(dayMap).format("DD MM YYYY")}</p>
                                 <p>{moment(day).format("DD MM YYYY")}</p> */}
                            </>
                          );
                        }


                      })
                      }
                    </span>

                    <span className={[(!isSameMonth(day, globalDate) ? "dissapear-text" : "")].join(' ')}>
                    </span>
                  </div>
                ))}
              </>
            ))}
          </div>
        </div>
      </div>
    );
  }





  return <Month />
};