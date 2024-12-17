import React, {useEffect, useState} from "react";
import {useDispatch, useSelector} from "react-redux";
import {useHistory} from "react-router-dom";
import _ from 'lodash';

// Actions
import {hideAssignedStudies, removeAnalystId, removeInterviewerId, setStudyId} from "../../../actions";
import StudyService from "../../../services/StudyService";

// Services

const AssignedStudies = () => {
    const dispatch = useDispatch()
    const history = useHistory()

    const analystId = useSelector(state => state.analyst)
    const interviewerId = useSelector(state => state.interviewer)
    const show = useSelector(state => state.assignedStudies)

    const [studies, setStudies] = useState([])
    const [page, setPage] = useState(0)
    const [search, setSearch] = useState("")

    useEffect(() => {
        const getStudies = async () => {
            try {
                if (analystId) {
                    const response = await StudyService.getStudies(page, null, null, 0, null, null, null, analystId)
                    setStudies([...studies, ...response.data.response])
                } else if (interviewerId) {
                    const response = await StudyService.getStudies(page, null, interviewerId)
                    setStudies([...studies, ...response.data.response])
                }
            } catch (error) {
                console.log("error")
            }
        }

        if (show) {
            getStudies()
        }
    }, [show, analystId, interviewerId, page])

    useEffect(() => {
        return () => {
            setPage(0)
            setStudies([])
        }
    }, [show])

    const handleScroll = (event) => {
        const newPage = Math.floor(event.target.scrollTop / 40)

        if (newPage > page) {
            setPage(newPage)
        }
    }

    const viewStudy = (id) => {
        // Assign study
        dispatch(setStudyId(id))

        // Remove users ids
        dispatch(removeAnalystId())
        dispatch(removeInterviewerId())

        // Close Form
        dispatch(hideAssignedStudies())

        // Navigate to View Study
        history.push("/dashboard/validaciones/ver")
    }

    const renderStudies = studies.map((option, index) => {
            return (
                <div className={"assigned-study"} key={`study-${option.id}`}>
                    <label>{_.get(option,"candidate.name", "---")}</label>
                    <button onClick={() => viewStudy(option.id)}>
                        <img src={"/images/actions-dropdown/eye.svg"} alt={""}/>
                    </button>
                </div>
            )
        }
    )

    const showHideModal = show ? "modal display-block" : "modal display-none"

    return (
        <div className={showHideModal}>
            <div className={"assigned-studies"}>
                <div className={"close-modal"}>
                    <img src={"/images/icon-close.png"} alt={""} onClick={() => dispatch(hideAssignedStudies())}/>
                </div>
                <h2>Estudios {( !_.isNil(interviewerId) ? "socioeconomicos" : "laborales")} asignados</h2>
                <div className={"search-form"}>
                    <input className={"search-field"} type={"search"} placeholder={"Buscar candidato..."} value={search}
                           onChange={event => setSearch(event.target.value)}/>
                    <button className={"search-button"}>
                        <img src={"/images/search.png"} alt={""}/>
                    </button>
                </div>
                <div className={"assigned-studies-list"} onScroll={handleScroll}>
                    {renderStudies}
                </div>
            </div>
        </div>
    )
}

export default AssignedStudies