import React, { useEffect, useState } from "react";
import _ from "lodash";
import { useHistory } from "react-router-dom";
import { useDispatch, useSelector } from "react-redux";

// Actions
import { setStudyId } from "../../../actions";

// Components
import ActionDropdown from "./ActionDropdown";
import Pagination from "../common/Pagination";

// Services
import StudyService from "../../../services/StudyService";

// MUI
import LinearProgress from "@mui/material/LinearProgress";

const StudyList = () => {
    const dispatch = useDispatch();
    const history = useHistory();

    const study = useSelector((state) => state.study);

    const [studies, setStudies] = useState([]);
    const [searchTerm, setSearchTerm] = useState("");
    const [page, setPage] = useState(0);
    const [pages, setPages] = useState(0);

    const getStudies = async () => {
        const user = JSON.parse(localStorage.getItem("user"));
        try {
            const response = await StudyService.getStudies(page, 0, 0, 0, user.identity.id, 0, 0, 0);
            setStudies(response.data.response);
        } catch (error) {
            console.log(error);
        }
    };

    const getPages = async () => {
        try {
            const response = await StudyService.getPages(0, 0, 0, 0, 0, 0);
            setPages(response.data.response);
        } catch (error) {
            console.log(error);
        }
    };

    useEffect(() => {
        getStudies();
        getPages();
    }, [page, study]);

    const getDate = (value) => {
        if (!value) return "";
        const date = new Date(value);
        return date.toLocaleDateString();
    };

    const onClickView = (id) => {
        dispatch(setStudyId(id));
        history.push("/dashboard/validaciones/ver");
    };

    const renderStudies = () => {
        const filteredStudies = studies.filter((study) => {
            const candidateName = `${study?.candidate?.name ?? ""} ${study?.candidate?.lastname ?? ""}`.toLowerCase();
            const position = study?.candidate?.position?.toLowerCase() ?? "";
            const company = study?.candidate?.client?.companyInformation?.companyName?.toLowerCase() ?? "";

            return (
                candidateName.includes(searchTerm.toLowerCase()) ||
                position.includes(searchTerm.toLowerCase()) ||
                company.includes(searchTerm.toLowerCase())
            );
        });

        return filteredStudies.map((study) => {
            const progress = study.studyStatus === 3 || study.studyStatus === 2 ? 100 : study.studyStatus === 1 ? 25 : 50;
            const color = study.studyStatus === 2 ? "error" : study.studyStatus === 3 ? "success" : "primary";

            return (
                <div key={study.id} className="table-row">
                    {/* <label className="table-cell">{study?.id ?? "---"}</label> */}
                    <label className="table-cell">
                        {`${study?.candidate?.name ?? ""} ${study?.candidate?.lastname ?? ""}`.trim()}
                    </label>
                    <label className="table-cell">{study?.candidate?.position ?? ""}</label>
                    <label className="table-cell">{study?.candidate?.client?.companyInformation?.companyName ?? ""}</label>
                    <label className="table-cell">
                        {`${study?.analyst?.name ?? ""} ${study?.analyst?.lastname ?? ""}`.trim()}
                    </label>
                    <label className="table-cell">
                        {`${study?.interviewer?.name ?? ""} ${study?.interviewer?.lastname ?? ""}`.trim()}
                    </label>
                    <label className="table-cell">
                        {study.serviceType === 1 ? "Estudio Socioeconómico" : "Estudio Laboral"}
                    </label>
                    <label className="table-cell">{getDate(study.studyFinalResult?.applicationDate)}</label>
                    <label className="table-cell">{getDate(study.studyFinalResult?.visitDate)}</label>
                    <label className="table-cell">
                        <LinearProgress
                            style={{ height: 5, width: "80%", margin: "8% 10%" }}
                            variant="determinate"
                            value={progress}
                            color={color}
                        />
                    </label>
                    <ActionDropdown key={`action-${study.id}`} onClickView={onClickView} studyId={study.id} />
                </div>
            );
        });
    };

    return (
        <div className="container">
            <div className="content studies">
                <div className="main-section studies-list shadow">
                    <div className="studies-list-top">
                        <div className="search-form">
                            <input
                                className="search-field"
                                type="search"
                                placeholder="Buscar..."
                                value={searchTerm}
                                onChange={(event) => setSearchTerm(event.target.value)}
                            />
                            <button className="search-button">
                                <img src="/images/search.png" alt="" />
                            </button>
                        </div>
                        <div className="list-actions">
                            <button className="reload" onClick={getStudies}>
                                <img src="/images/refresh-icon.png" alt="" />
                            </button>
                        </div>
                    </div>
                    <div className="table studies">
                        <div className="table-row">
                            {/* <label className="table-cell-header">Id</label> */}
                            <label className="table-cell-header">Candidato</label>
                            <label className="table-cell-header">Puesto</label>
                            <label className="table-cell-header">Empresa</label>
                            <label className="table-cell-header">Analista</label>
                            <label className="table-cell-header">Entrevistador</label>
                            <label className="table-cell-header">Tipo de Servicio</label>
                            <label className="table-cell-header">Fecha de solicitud</label>
                            <label className="table-cell-header">Fecha de entrega</label>
                            <label className="table-cell-header">Progreso</label>
                        </div>
                        {renderStudies()}
                    </div>
                </div>
                <div className="pagination">
                    <Pagination page={page} setPage={setPage} pages={pages} />
                </div>
            </div>
        </div>
    );
};

export default StudyList;