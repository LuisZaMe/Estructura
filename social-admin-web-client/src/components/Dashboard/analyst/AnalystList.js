import React, { useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { useHistory } from "react-router-dom";

// Actions
import {
    removeAnalystId,
    setAnalystId,
    showRegisterAnalyst,
    removeInterviewerId,
} from "../../../actions";

// Components
import RegisterAnalyst from "./RegisterAnalyst";
import ActionDropdown from "../common/ActionDropdown";
import DeleteAccountForm from "../common/DeleteAccountForm";
import Pagination from "../common/Pagination";

// Models
import Account from "../../../model/Account";

// Services
import AccountService from "../../../services/AccountService";

const AnalystList = () => {
    const dispatch = useDispatch()
    const history = useHistory()

    const analyst = useSelector(state => state.analyst)

    const [analysts, setAnalysts] = useState([])
    const [searchTerm, setSearchTerm] = useState("")
    const [page, setPage] = useState(0)
    const [pages, setPages] = useState(0)
    const [showDeleteAnalyst, setShowDeleteAnalyst] = useState(false)
    const [isLoading, setIsLoading] = useState(false);

    const getAnalysts = async () => {
        setIsLoading(true);
        try {
            const response = await AccountService.getAccounts(Account.ANALYST, searchTerm, page)
            setAnalysts(response.data.response)
            setIsLoading(false);
        } catch (error) {
            console.log(error)
            setIsLoading(false);
        }
    }

    const getPages = async () => {
        try {
            const response = await AccountService.getPages(Account.ANALYST, searchTerm)
            setPages(response.data.response)
        } catch (error) {
            console.log(error)
        }
    }

    useEffect(() => {
        getAnalysts()
        getPages()
    }, [page, analyst, searchTerm])

    const handleDelete = () => {
        dispatch(removeAnalystId())
        history.push("/dashboard/analistas")
    }

    const onClickView = (id) => {
        dispatch(removeInterviewerId())
        dispatch(setAnalystId(id))
        history.push("/dashboard/analistas/ver")
    }

    const onClickEdit = (id) => {
        dispatch(removeInterviewerId())
        dispatch(setAnalystId(id))
        history.push("/dashboard/analistas/editar")
    }

    const onClickDelete = (id) => {
        dispatch(removeInterviewerId())
        dispatch(setAnalystId(id))
        setShowDeleteAnalyst(true)
    }

    const renderActions = analysts.map(analyst => {
        return (
            <ActionDropdown key={`action-${analyst.id}`} onClickView={onClickView} onClickEdit={onClickEdit}
                onClickDelete={onClickDelete} userId={analyst.id} />
        )
    })

    const renderAnalysts = analysts.map(analyst => {
        return (
            <div key={analyst.id} className={"table-row"}>
                <label className={"table-cell"}>{`${analyst?.name || ""} ${analyst?.lastname || ""}`.trim()}</label>
                <label className={"table-cell"}>{analyst.phone}</label>
                <label className={"table-cell"}>{analyst.email}</label>
                <ActionDropdown
                    key={`action-${analyst.id}`}
                    onClickView={onClickView}
                    onClickEdit={onClickEdit}
                    onClickDelete={onClickDelete}
                    userId={analyst.id}
                />
            </div>
        )
    })

    return (
        <div className={"container"}>
            <div className={"content analysts"}>
                <div className={"main-section analysts-list shadow"}>
                    <div className={"analysts-list-top"}>
                        <div className={"search-form"}>
                            <input className={"search-field"} type={"search"} placeholder={"Buscar..."}
                                value={searchTerm} onChange={event => setSearchTerm(event.target.value)} />
                            <button className={"search-button"}>
                                <img src={"/images/search.png"} alt={""} />
                            </button>
                        </div>
                        <div className={"list-actions"}>
                            <button className={"reload"} onClick={getAnalysts}>
                                <img src={"/images/refresh-icon.png"} alt={""} />
                            </button>
                            <button className={"register"} onClick={() => dispatch(showRegisterAnalyst())}>Registrar
                            </button>
                            <RegisterAnalyst />
                            <DeleteAccountForm show={showDeleteAnalyst} handleClose={setShowDeleteAnalyst}
                                handleDelete={handleDelete} id={analyst} userType={"analyst"} />
                        </div>
                    </div>
                    <div className={"table analysts"}>
                        <div className={"table-row"}>
                            <label className={"table-cell-header"}>Nombre</label>
                            <label className={"table-cell-header"}>Telefono</label>
                            <label className={"table-cell-header"}>Correo</label>
                        </div>
                        {renderAnalysts}
                    </div>
                    {/* <div className={"table-actions"}>
                        {renderActions}
                    </div> */}
                </div>
                <div className={"pagination"}>
                    <Pagination page={page} pages={pages} setPage={setPage} isLoading={isLoading} />
                </div>
            </div>
        </div>
    )
}

export default AnalystList