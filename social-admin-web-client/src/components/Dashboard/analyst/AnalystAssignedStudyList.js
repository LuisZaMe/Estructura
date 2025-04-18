import React, { useEffect, useState } from "react";
import { useHistory } from "react-router-dom";
import { useDispatch, useSelector } from "react-redux";

// Actions
import { setStudyId } from "../../../actions";

// Components
import ActionDropdown from "../study/ActionDropdown";
import Pagination from "../common/Pagination";

// Services
import StudyService from "../../../services/StudyService";
import AuthService from "../../../services/AuthService";

const AnalystAssignedStudyList = () => {
    const dispatch = useDispatch()
    const history = useHistory()

    const study = useSelector(state => state.study)

    const [studies, setStudies] = useState([])
    const [filteredStudies, setFilteredStudies] = useState([])
    const [searchTerm, setSearchTerm] = useState("")
    const [page, setPage] = useState(0)
    const [pages, setPages] = useState(0)
    const [isLoading, setIsLoading] = useState(false);

    const getStudies = async () => {
        setIsLoading(true);
        try {
            var identity = AuthService.getIdentity();
            const response = await StudyService.getStudies(page,
                0,
                0,
                0,
                0,
                0,
                3,
                identity.id);
            setStudies(response.data.response)
            setFilteredStudies(response.data.response);
            setIsLoading(false)
        } catch (error) {
            console.log(error)
            setIsLoading(false)
        }
    }

    const getPages = async () => {
        try {
            var identity = AuthService.getIdentity();
            const response = await StudyService.getPages(0, 0, 0, 0, 0, 3, identity.id)
            setPages(response.data.response)
        } catch (error) {
            console.log(error)
        }
    }

    useEffect(() => {
        getStudies()
        getPages()
    }, [page, study])

    const getDate = (value) => {
        const date = new Date(value)
        return date.toLocaleDateString()
    }

    const onClickView = (id) => {
        dispatch(setStudyId(id))
        history.push("/dashboard/validaciones/ver")
    }

    const renderActions = filteredStudies.map(study => {
        return (
            <ActionDropdown key={`action-${study.id}`} onClickView={onClickView} studyId={study.id} />
        )
    })

    const renderStudies = filteredStudies.map(study => {
        return (
            <div key={study.id} className={"table-row"}>
                <label className={"table-cell"}>{`${study.candidate.name} ${study.candidate.lastname || ""}`.trim()}</label>
                <label className={"table-cell"}>{study.candidate.position}</label>
                <label className={"table-cell"}>{study.candidate.client.companyInformation ? study.candidate.client.companyInformation.companyName : "-"}</label>
                <label className={"table-cell"}>{study.analyst ? `${study.analyst.name} ${study.analyst.lastname || ""}`.trim() : "-"}</label>
                <label className={"table-cell"}>{study.interviewer ? `${study.interviewer.name} ${study.interviewer.lastname || ""}`.trim() : "-"}</label>
                <label className={"table-cell"}>{study.serviceType === 1 ? "Estudio Socioeconómico" : "Estudio Laboral"}</label>
                <label className={"table-cell"}>{getDate(study.createdAt)}</label>
                <label className={"table-cell"}>{getDate(study.updatedAt)}</label>
                <ActionDropdown key={`action-${study.id}`} onClickView={onClickView} studyId={study.id} />
            </div>
        )
    })

    const handleSearch = (event) => {
        setSearchTerm(event.target.value);


        const filtered = studies.filter(study => {

            return (
                study.candidate.name.toLowerCase().includes(event.target.value.toLowerCase()) ||
                study.candidate.position.toLowerCase().includes(event.target.value.toLowerCase()) ||
                (study.candidate.client.companyInformation ? study.candidate.client.companyInformation.companyName.toLowerCase().includes(event.target.value.toLowerCase()) : false) ||
                (study.analyst ? study.analyst.name.toLowerCase().includes(event.target.value.toLowerCase()) : false)
            );
        });

        setFilteredStudies(filtered);
    }

    return (
        <div className={"container"}>
            <div className={"content studies"}>
                <div className={"main-section studies-list shadow"}>
                    <div className={"studies-list-top"}>
                        <div className={"search-form"}>
                            <input className={"search-field"} type={"search"} placeholder={"Buscar..."}
                                value={searchTerm} onChange={handleSearch} />  {/* Usar handleSearch */}
                            <button className={"search-button"}>
                                <img src={"/images/search.png"} alt={""} />
                            </button>
                        </div>
                        <div className={"list-actions"} style={{ margin: "0px 10px 0px 0px;", justifyContent: "flex-end" }}>
                            <button className={"reload"} onClick={getStudies}>
                                <img src={"/images/refresh-icon.png"} alt={""} />
                            </button>
                        </div>
                    </div>
                    <div className={"table studies"}>
                        <div className={"table-row"}>
                            <label className={"table-cell-header"}>Candidato</label>
                            <label className={"table-cell-header"}>Puesto</label>
                            <label className={"table-cell-header"}>Empresa</label>
                            <label className={"table-cell-header"}>Analista</label>
                            <label className={"table-cell-header"}>Entrevistador</label>
                            <label className={"table-cell-header"}>Tipo de Servicio</label>
                            <label className={"table-cell-header"}>Fecha de solicitud</label>
                            <label className={"table-cell-header"}>Fecha de entrega</label>
                        </div>
                        {renderStudies}
                    </div>
                </div>
                <div className={"pagination"}>
                    <Pagination page={page} setPage={setPage} pages={pages} isLoading={isLoading} />
                </div>
            </div>
        </div>
    )
}

export default AnalystAssignedStudyList
