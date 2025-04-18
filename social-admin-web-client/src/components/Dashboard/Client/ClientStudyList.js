import React, { useEffect, useState } from "react";
import { useHistory } from "react-router-dom";
import { useDispatch, useSelector } from "react-redux";
import AuthService from "../../../services/AuthService";

// Actions
import { setStudyId } from "../../../actions";

// Components
import ActionDropdown from "../study/ActionDropdown";
import Pagination from "../common/Pagination";

// Services
import StudyService from "../../../services/StudyService";

// MUI
import Box from '@mui/material/Box';
import LinearProgress from '@mui/material/LinearProgress';
import { purple } from '@mui/material/colors';

const ClientStudyList = () => {
    const dispatch = useDispatch();
    const history = useHistory();

    const study = useSelector(state => state.study);

    const [studies, setStudies] = useState([]);
    const [searchTerm, setSearchTerm] = useState("");
    const [page, setPage] = useState(0);
    const [pages, setPages] = useState(0);
    const [isLoading, setIsLoading] = useState(false);

    const getStudies = async () => {
        setIsLoading(true);
        try {
            var identity = AuthService.getIdentity();
            const response = await StudyService.getStudies(page, 1, identity.id, 2, 1, 1, 4, 7);
            setStudies(response.data.response);
            setIsLoading(false);
        } catch (error) {
            console.log(error);
            setIsLoading(false);
        }
    };

    const getPages = async () => {
        try {
            var identity = AuthService.getIdentity();
            const response = await StudyService.getPages(0, 0, 0, identity.id, 0, 4, 7);
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
        const date = new Date(value);
        return date.toLocaleDateString();
    };

    const onClickView = (id) => {
        dispatch(setStudyId(id));
        history.push("/dashboard/validaciones/ver");
    };

    const renderActions = studies.map((study) => {
        return (
            <ActionDropdown key={`action-${study.id}`} onClickView={onClickView} studyId={study.id} />
        );
    });

    const filteredStudies = studies.filter(study => {
        const searchLower = searchTerm.toLowerCase();
        const candidateName = study.candidate ? study.candidate.name.toLowerCase() : '';
        const position = study.candidate ? study.candidate.position.toLowerCase() : '';
        const companyName = study.candidate && study.candidate.client && study.candidate.client.companyInformation 
                            ? study.candidate.client.companyInformation.companyName.toLowerCase() : '';
        const analystName = study.analyst ? study.analyst.name.toLowerCase() : '';
        const interviewerName = study.interviewer ? study.interviewer.name.toLowerCase() : '';
        const serviceType = study.serviceType === 1 ? "Estudio Socioeconómico" : "Estudio Laboral";

        return (
            candidateName.includes(searchLower) ||
            position.includes(searchLower) ||
            companyName.includes(searchLower) ||
            analystName.includes(searchLower) ||
            interviewerName.includes(searchLower) ||
            serviceType.toLowerCase().includes(searchLower)
        );
    });

    const renderStudies = filteredStudies.map((study) => {
        const progress = study.studyStatus === 3 || study.studyStatus === 2 ? 100 : study.studyStatus === 1 ? 25 : 50;
        const color = study.studyStatus === 2 ? 'error' : study.studyStatus === 3 ? 'success' : 'info';
        return (
            <div key={study.id} className={"table-row"}>
                <label className={"table-cell"}>{study.candidate ? `${study.candidate.name} ${study.candidate.lastName}` : "-"}</label>
                <label className={"table-cell"}>{study.candidate ? study.candidate.position : "-"}</label>
                <label className={"table-cell"}>{study.candidate ? study.candidate.client.companyInformation.companyName : "-"}</label>
                <label className={"table-cell"}>{study.analyst ? `${study.analyst.name} ${study.analyst.lastName}` : "-"}</label>
                <label className={"table-cell"}>{study.interviewer ? `${study.interviewer.name} ${study.interviewer.lastName}` : "-"}</label>
                <label className={"table-cell"}>{study.serviceType === 1 ? "Estudio Socioeconómico" : "Estudio Laboral"}</label>
                <label className={"table-cell"}>{getDate(study.createdAt)}</label>
                <label className={"table-cell"}>{getDate(study.updatedAt)}</label>
                <label className={"table-cell"}>
                    <LinearProgress
                        style={{ height: 5, width: '80%', marginTop: '8%', marginLeft: '10%' }}
                        variant="determinate"
                        value={progress}
                        color={color}
                    />
                </label>
                <ActionDropdown key={`action-${study.id}`} onClickView={onClickView} studyId={study.id} />
            </div>
        );
    });

    return (
        <div className={"container"}>
            <div className={"content studies"}>
                <div className={"main-section studies-list shadow"}>
                    <div className={"studies-list-top"}>
                        <div className={"search-form"}>
                            <input
                                className={"search-field"}
                                type={"search"}
                                placeholder={"Buscar..."}
                                value={searchTerm}
                                onChange={(event) => setSearchTerm(event.target.value)}
                            />
                            <button className={"search-button"}>
                                <img src={"/images/search.png"} alt={""} />
                            </button>
                        </div>
                        <div className={"list-actions"}>
                            <button className={"reload"} onClick={getStudies}>
                                <img src={"/images/refresh-icon.png"} alt={""} />
                            </button>
                        </div>
                    </div>

                    <div className={"table studies fixed-scrollbar"}>
                        <div className={"table-row"}>
                            <label className={"table-cell-header"}>Candidato</label>
                            <label className={"table-cell-header"}>Puesto</label>
                            <label className={"table-cell-header"}>Empresa</label>
                            <label className={"table-cell-header"}>Analista</label>
                            <label className={"table-cell-header"}>Entrevistador</label>
                            <label className={"table-cell-header"}>Tipo de Servicio</label>
                            <label className={"table-cell-header"}>Fecha de solicitud</label>
                            <label className={"table-cell-header"}>Fecha de entrega</label>
                            <label className={"table-cell-header"}>Progreso</label>
                        </div>
                        {renderStudies}
                    </div>
                </div>
                <div className={"pagination"}>
                    <Pagination page={page} setPage={setPage} pages={pages} isLoading={isLoading} />
                </div>
            </div>
        </div>
    );
};

export default ClientStudyList;