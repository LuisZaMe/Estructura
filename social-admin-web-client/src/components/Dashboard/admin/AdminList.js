import React, { useEffect, useState } from "react"
import { useHistory } from "react-router-dom";
import { useDispatch, useSelector } from "react-redux";

// Actions
import { removeAdminId, setAdminId, showRegisterAdmin } from "../../../actions";

// Components
import ActionDropdown from "../common/ActionDropdown";
import DeleteAccountForm from "../common/DeleteAccountForm";
import Pagination from "../common/Pagination";
import RegisterAdmin from "./RegisterAdmin";

// Models

import Account from "../../../model/Account";

// Services
import AccountService from "../../../services/AccountService";
import AuthService from "../../../services/AuthService";

const AdminList = () => {
    const dispatch = useDispatch()
    const history = useHistory()

    const admin = useSelector(state => state.admin)

    const [admins, setAdmins] = useState([])
    const [searchTerm, setSearchTerm] = useState("")
    const [page, setPage] = useState(0)
    const [pages, setPages] = useState(0)
    const [showDeleteAdmin, setShowDeleteAdmin] = useState(false)
    const [isLoading, setIsLoading] = useState(false);

    const userIdentity = AuthService.getIdentity();

    const getAdmins = async () => {
        setIsLoading(true);
        try {
            const response = await AccountService.getAccounts(Account.ADMIN, searchTerm, page, null, userIdentity.role === Account.SUPER_ADMINISTRADOR)
            setAdmins(response.data.response)
            setIsLoading(false);
        } catch (error) {
            console.log(error)
            setIsLoading(false);
        }
    }

    const getPages = async () => {
        try {
            const response = await AccountService.getPages(Account.ADMIN, searchTerm, userIdentity.role === Account.SUPER_ADMINISTRADOR)
            setPages(response.data.response)
        } catch (error) {
            console.log(error)
        }
    }

    useEffect(() => {
        getAdmins()
        getPages()
    }, [page, admin, searchTerm])

    const handleDelete = () => {
        dispatch(removeAdminId())
        history.push("/dashboard/administrador")
    }

    const onClickView = (id) => {
        dispatch(setAdminId(id))
        history.push("/dashboard/administrador/ver")
    }

    const onClickEdit = (id) => {
        dispatch(setAdminId(id))
        history.push("/dashboard/administrador/editar")
    }

    const onClickDelete = (id) => {
        dispatch(setAdminId(id))
        setShowDeleteAdmin(true)
    }

    const renderActions = admins.map(admin => {
        return (
            <ActionDropdown
                key={`action-${admin.id}`}
                onClickView={onClickView}
                onClickEdit={onClickEdit}
                onClickDelete={onClickDelete}
                userId={admin.id}
                showDelete={false}
            />
        )
    })

    const RenderRole = ({roleId}) => {
        if (roleId === Account.SUPER_ADMINISTRADOR) {
            return (<span>Super Admin</span>);
        }
        
        return (<span>Administrador</span>);
    }

    const renderAdmins = admins.map(admin => {
        return (
            <div key={admin.id} className={"table-row"}>
                <label className={"table-cell"}>{admin.name}</label>
                <label className={"table-cell"}>{admin.phone}</label>
                <label className={"table-cell"}>{admin.email}</label>
                <label className={"table-cell"}><RenderRole roleId={admin.roleId} /></label>
                <ActionDropdown
                    key={`action-${admin.id}`}
                    onClickView={onClickView}
                    onClickEdit={onClickEdit}
                    onClickDelete={onClickDelete}
                    userId={admin.id}
                    showDelete={userIdentity.role === Account.SUPER_ADMINISTRADOR && userIdentity.id !== admin.id}
                />
            </div>
        )
    })

    return (
        <div className={"container"}>
            <div className={"content admins"}>
                <div className={"main-section admins-list shadow"}>
                    <div className={"admins-list-top"}>
                        <div className={"search-form"}>
                            <input className={"search-field"} type={"search"} placeholder={"Buscar..."}
                                value={searchTerm} onChange={event => setSearchTerm(event.target.value)} />
                            <button className={"search-button"}>
                                <img src={"/images/search.png"} alt={""} />
                            </button>
                        </div>
                        <div className={"list-actions"}>
                            <button className={"reload"} onClick={getAdmins}>
                                <img src={"/images/refresh-icon.png"} alt={""} />
                            </button>
                            <button className={"register"} onClick={() => dispatch(showRegisterAdmin())}>Registrar
                            </button>
                            <RegisterAdmin />
                            <DeleteAccountForm show={showDeleteAdmin} handleClose={setShowDeleteAdmin}
                                handleDelete={handleDelete} id={admin} userType={"admin"} />
                        </div>
                    </div>
                    <div className={"table admins"}>
                        <div className={"table-row"}>
                            <label className={"table-cell-header"}>Nombre</label>
                            <label className={"table-cell-header"}>Telefono</label>
                            <label className={"table-cell-header"}>Correo</label>
                            <label className={"table-cell-header"}>Role</label>
                        </div>
                        {renderAdmins}
                    </div>
                    {/* <div className={"table-actions"}>
                        {renderActions}
                    </div> */}
                </div>
                <div className={"pagination"}>
                    <Pagination page={page} setPage={setPage} pages={pages} isLoading={isLoading}/>
                </div>
            </div>
        </div>
    )
}

export default AdminList