import React, { useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { useHistory } from "react-router-dom";
import _ from "lodash";

// Actions
import { removeCandidateId, setCandidateId, showRegisterCandidate } from "../../../actions";

// Components
import ActionDropdown from "../common/ActionDropdown";
import DeleteAccountForm from "../common/DeleteAccountForm";
import Notes from "../common/Notes";
import Pagination from "../common/Pagination";
import RegisterCandidate from "./RegisterCandidate";

// Services
import CandidateService from "../../../services/CandidateService";

const CandidateList = () => {
    const dispatch = useDispatch();
    const history = useHistory();

    const candidate = useSelector((state) => state.candidate);

    const [candidates, setCandidates] = useState([]);
    const [filteredCandidates, setFilteredCandidates] = useState([]); // Nuevo estado para los filtrados
    const [searchTerm, setSearchTerm] = useState("");
    const [page, setPage] = useState(0);
    const [pages, setPages] = useState(0);
    const [showDeleteCandidate, setShowDeleteCandidate] = useState(false);
    const [showNotes, setShowNotes] = useState(false);
    const [isLoading, setIsLoading] = useState(false);

    const getCandidates = async () => {
        setIsLoading(true);
        try {
            const response = await CandidateService.getCandidates("", page); // No enviar `searchTerm`
            setCandidates(response.data.response);
            setIsLoading(false);
        } catch (error) {
            console.log(error);
            setIsLoading(false);
        }
    };

    const getPages = async () => {
        try {
            const response = await CandidateService.getPages();
            setPages(response.data.response);
        } catch (error) {
            console.log(error);
        }
    };

    // Obtener candidatos y total de páginas al cambiar de página
    useEffect(() => {
        getCandidates();
        getPages();
    }, [page]);

    // Filtrar localmente cuando cambia `searchTerm`
    useEffect(() => {
        const filtered = candidates.filter((candidate) => {
            return (
                _.get(candidate, "client.companyInformation.companyName", "").toLowerCase().includes(searchTerm.toLowerCase()) ||
                candidate.name.toLowerCase().includes(searchTerm.toLowerCase()) ||
                candidate.lastname.toLowerCase().includes(searchTerm.toLowerCase()) ||
                candidate.phone.toLowerCase().includes(searchTerm.toLowerCase()) ||
                candidate.email.toLowerCase().includes(searchTerm.toLowerCase()) ||
                candidate.curp.toLowerCase().includes(searchTerm.toLowerCase()) ||
                candidate.nss.toLowerCase().includes(searchTerm.toLowerCase()) ||
                candidate.address.toLowerCase().includes(searchTerm.toLowerCase()) ||
                candidate.position.toLowerCase().includes(searchTerm.toLowerCase())
            );
        });

        setFilteredCandidates(filtered);
    }, [searchTerm, candidates]);

    const handleDelete = () => {
        dispatch(removeCandidateId());
        history.push("/dashboard/candidatos");
    };

    const onClickView = (id) => {
        dispatch(setCandidateId(id));
        history.push("/dashboard/candidatos/ver");
    };

    const onClickEdit = (id) => {
        dispatch(setCandidateId(id));
        history.push("/dashboard/candidatos/editar");
    };

    const onClickDelete = (id) => {
        dispatch(setCandidateId(id));
        setShowDeleteCandidate(true);
    };

    const renderCandidates = filteredCandidates.map((candidate) => {
        return (
            <div key={candidate.id} className="table-row">
                <label className="table-cell">{_.get(candidate, "client.companyInformation.companyName", "")}</label>
                <label className="table-cell">{candidate.name}</label>
                <label className="table-cell">{candidate.lastname}</label>
                <label className="table-cell">{candidate.phone}</label>
                <label className="table-cell">{candidate.email}</label>
                <label className="table-cell">{candidate.curp}</label>
                <label className="table-cell">{candidate.nss}</label>
                <label className="table-cell">{candidate.address}</label>
                <label className="table-cell">{candidate.position}</label>
                <ActionDropdown
                    key={`action-${candidate.id}`}
                    onClickView={onClickView}
                    onClickEdit={onClickEdit}
                    onClickDelete={onClickDelete}
                    userId={candidate.id}
                    notes={true}
                    showNotes={setShowNotes}
                />
            </div>
        );
    });

    return (
        <div className="container">
            <div className="content candidates">
                <div className="main-section candidates-list shadow">
                    <div className="candidates-list-top">
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
                            <button className="reload" onClick={getCandidates}>
                                <img src="/images/refresh-icon.png" alt="" />
                            </button>
                            <button className="register" onClick={() => dispatch(showRegisterCandidate())}>
                                Registrar
                            </button>
                            <RegisterCandidate />
                            <DeleteAccountForm
                                show={showDeleteCandidate}
                                handleClose={setShowDeleteCandidate}
                                handleDelete={handleDelete}
                                id={candidate}
                                userType="candidate"
                            />
                            <Notes id={candidate} show={showNotes} setShowNotes={setShowNotes} />
                        </div>
                    </div>
                    <div className="table candidates">
                        <div className="table-row">
                            <label className="table-cell-header">Cliente</label>
                            <label className="table-cell-header">Nombre(s)</label>
                            <label className="table-cell-header">Apellido(s)</label>
                            <label className="table-cell-header">Teléfono</label>
                            <label className="table-cell-header">Correo</label>
                            <label className="table-cell-header">CURP</label>
                            <label className="table-cell-header">NSS</label>
                            <label className="table-cell-header">Domicilio</label>
                            <label className="table-cell-header">Puesto</label>
                        </div>
                        {renderCandidates}
                    </div>
                </div>
                <Pagination page={page} setPage={setPage} pages={pages} isLoading={isLoading} />
            </div>
        </div>
    );
};

export default CandidateList;