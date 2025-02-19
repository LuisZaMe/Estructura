import React, { useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { useHistory } from "react-router-dom";

// Actions
import {
    removeInterviewerId,
    setInterviewerId,
    showRegisterInterviewer,
    removeAnalystId,
} from "../../../actions";

// Components
import RegisterInterviewer from "./RegisterInterviewer";
import ActionDropdown from "../common/ActionDropdown";
import DeleteAccountForm from "../common/DeleteAccountForm";
import Pagination from "../common/Pagination";

// Models
import Account from "../../../model/Account";

// Services
import AccountService from "../../../services/AccountService";

const InterviewerList = () => {
    const dispatch = useDispatch()
    const history = useHistory()

    const interviewer = useSelector(state => state.interviewer)

    const [interviewers, setInterviewers] = useState([])
    const [searchTerm, setSearchTerm] = useState("")
    const [page, setPage] = useState(0)
    const [pages, setPages] = useState(0)
    const [showDeleteInterviewer, setShowDeleteInterviewer] = useState(false)
    const [isLoading, setIsLoading] = useState(false);

    const getInterviewers = async () => {
        setIsLoading(true);
        try {
            const response = await AccountService.getAccounts(Account.INTERVIEWER, searchTerm, page)
            setInterviewers(response.data.response)
            setIsLoading(false);
        } catch (error) {
            console.log(error)
            setIsLoading(false);
        }
    }

    const getPages = async () => {
        try {
            const response = await AccountService.getPages(Account.INTERVIEWER, searchTerm)
            setPages(response.data.response)
        } catch (error) {
            console.log(error)
        }
    }

    useEffect(() => {
        getInterviewers()
        getPages()
    }, [page, interviewer, searchTerm])

    const handleDelete = () => {
        dispatch(removeInterviewerId())
        history.push("/dashboard/entrevistadores")
    }

    const onClickView = (id) => {
        dispatch(removeAnalystId());
        dispatch(setInterviewerId(id))
        history.push("/dashboard/entrevistadores/ver")
    }

    const onClickEdit = (id) => {
        dispatch(removeAnalystId());
        dispatch(setInterviewerId(id))
        history.push("/dashboard/entrevistadores/editar")
    }

    const onClickDelete = (id) => {
        dispatch(removeAnalystId());
        dispatch(setInterviewerId(id))
        setShowDeleteInterviewer(true)
    }

    const renderActions = interviewers.map(interviewer => {
        return (
            <ActionDropdown key={`action-${interviewer.id}`} onClickView={onClickView} onClickEdit={onClickEdit}
                onClickDelete={onClickDelete} userId={interviewer.id} />
        )
    })

    const renderInterviewers = interviewers.map(interviewer => {
        return (
            <div key={interviewer.id} className={"table-row"}>
                <label className={"table-cell"}>{`${interviewer.name} ${interviewer.lastname || ""}`.trim()}</label>
                <label className={"table-cell"}>{interviewer.phone}</label>
                <label className={"table-cell"}>{interviewer.email}</label>
                <ActionDropdown key={`action-${interviewer.id}`} onClickView={onClickView} onClickEdit={onClickEdit}
                    onClickDelete={onClickDelete} userId={interviewer.id} />
            </div>
        )
    })

    return (
        <div className={"container"}>
            <div className={"content interviewers"}>
                <div className={"main-section interviewers-list shadow"}>
                    <div className={"interviewers-list-top"}>
                        <div className={"search-form"}>
                            <input className={"search-field"} type={"search"} placeholder={"Buscar..."}
                                value={searchTerm} onChange={event => setSearchTerm(event.target.value)} />
                            <button className={"search-button"}>
                                <img src={"/images/search.png"} alt={""} />
                            </button>
                        </div>
                        <div className={"list-actions"}>
                            <button className={"reload"} onClick={getInterviewers}>
                                <img src={"/images/refresh-icon.png"} alt={""} />
                            </button>
                            <button className={"register"} onClick={() => dispatch(showRegisterInterviewer())}>Registrar
                            </button>
                            <RegisterInterviewer />
                            <DeleteAccountForm show={showDeleteInterviewer} handleClose={setShowDeleteInterviewer}
                                handleDelete={handleDelete} id={interviewer} userType={"interviewer"} />
                        </div>
                    </div>
                    <div className={"table interviewers no-scrollbar"}>
                        <div className={"table-row"}>
                            <label className={"table-cell-header"}>Nombre</label>
                            <label className={"table-cell-header"}>Telefono</label>
                            <label className={"table-cell-header"}>Correo</label>
                        </div>
                        {renderInterviewers}
                    </div>
                    {/* <div className={"table-actions"}>
                        {renderActions}
                    </div> */}
                </div>
                <div className={"pagination"}>
                    <Pagination page={page} setPage={setPage} pages={pages} isLoading={isLoading} />
                </div>
            </div>
        </div>
    )
}

export default InterviewerList