import React, { useEffect, useState } from "react";
import { useSelector } from "react-redux";
import { Link, useHistory } from "react-router-dom";
import AuthService from "../../services/AuthService";
import Account from "../../model/Account";


const Menu = () => {
    const [path, setPath] = useState(window.location.pathname)
    const history = useHistory()
    const rol = useSelector(state => state)

    useEffect(() => {
        history.listen(() => {
            setPath(window.location.pathname)
        })
    }, [])

    const defineClass = (value) => {
        if (path === "/dashboard" && value === "home") {
            return "active"
        }

        if (path === "/dashboard/cliente-generacion-de-estudios" && value === "generated-client-studies") {
            return "active"
        }


        if (path === "/dashboard/analyst-assigned-study-list" && value === "analyst-assigned-study-list") {
            return "active"
        }

        if (path === "/dashboard/analyst-finished-study-list" && value === "analyst-finished-study-list") {
            return "active"
        }

        if (path === "/dashboard/generacion-de-estudios" && value === "study") {
            return "active"
        }

        if (path.includes("/dashboard/clientes") && value === "clients") {
            return "active"
        }

        if (path.includes("/dashboard/interviewer-assigned-studies") && value === "interviewer-studies") {
            return "active"
        }

        if (path === "/dashboard/candidatos" && value === "candidates") {
            return "active"
        }

        if (path === "/dashboard/visitas" && value === "visits") {
            return "active"
        }

        if (path === "/dashboard/entrevistadores" && value === "interviewer") {
            return "active"
        }

        if (path.includes("/dashboard/analistas") && value === "analyst") {
            return "active"
        }

        if (path === "/dashboard/validaciones" && value === "validations") {
            return "active"
        }

        if (path.includes("/dashboard/administrador") && value === "admin") {
            return "active"
        }
    }

    const shouldDisplay = (requiredRole) => {
        var identity = AuthService.getIdentity();
        if (requiredRole.length > 0 && requiredRole.includes(identity.role)) {
            return "";
        }
        return " hide-item";
    }

    return (
        <div className={"menu"}>
            <div className={"menu-top"}>
                <button>{"<"}</button>
            </div>
            <nav>
                <ul className={"menu-list"}>
                    <li className={defineClass("home") + "" + shouldDisplay([
                        Account.ADMIN,
                        Account.INTERVIEWER,
                        Account.ANALYST,
                        Account.CLIENT,
                        Account.SUPER_ADMINISTRADOR
                        ])}>
                        <Link to={"/dashboard"}>
                            <img src={"/images/menu/home.svg"} alt={"Icono Inicio"} />
                            <span>Inicio</span>
                        </Link>
                    </li>

                    <li className={defineClass("generated-client-studies") + "" + shouldDisplay([Account.ANALYST])}>
                        <Link to={"/dashboard/analyst-assigned-study-list"}>
                            <img src={"/images/menu/study.svg"} alt={"Icono generacion de estudio"} />
                            <span>Estudios Generados</span>
                        </Link>
                    </li>

                    <li className={defineClass("generated-client-studies") + "" + shouldDisplay([Account.ANALYST])}>
                        <Link to={"/dashboard/analyst-finished-study-list"}>
                            <img src={"/images/menu/study.svg"} alt={"Icono generacion de estudio"} />
                            <span>Estudios Completados</span>
                        </Link>
                    </li>

                    <li className={defineClass("generated-client-studies") + "" + shouldDisplay([Account.CLIENT])}>
                        <Link to={"/dashboard/cliente-generacion-de-estudios"}>
                            <img src={"/images/menu/study.svg"} alt={"Icono generacion de estudio"} />
                            <span>Estudios Asignados</span>
                        </Link>
                    </li>

                    <li className={defineClass("study") + "" + shouldDisplay([
                            Account.ADMIN, 
                            Account.SUPER_ADMINISTRADOR
                        ])}>
                        <Link to={"/dashboard/generacion-de-estudios"}>
                            <img src={"/images/menu/study.svg"} alt={"Icono generacion de estudio"} />
                            <span>Generacion de estudio</span>
                        </Link>
                    </li>
                    <li className={defineClass("clients") + "" + shouldDisplay([
                            Account.ADMIN,
                            Account.SUPER_ADMINISTRADOR
                        ])}>
                        <Link to={"/dashboard/clientes"}>
                            <img src={"/images/menu/clients.svg"} alt={"Icono clientes"} />
                            <span>Clientes</span>
                        </Link>
                    </li>
                    <li className={defineClass("interviewer-studies") + "" + shouldDisplay([Account.INTERVIEWER])}>
                        <Link to={"/dashboard/interviewer-assigned-studies"}>
                            <img src={"/images/menu/visits.svg"} alt={"Icono visitas"} />
                            <span>Estudios Asignados</span>
                        </Link>
                    </li>
                    <li className={defineClass("candidates") + "" + shouldDisplay([
                            Account.INTERVIEWER,
                            Account.ADMIN,
                            Account.SUPER_ADMINISTRADOR
                        ])}>
                        <Link to={"/dashboard/candidatos"}>
                            <img src={"/images/menu/candidates.svg"} alt={"Icono candidatos"} />
                            <span>Candidatos</span>
                        </Link>
                    </li>
                    <li className={defineClass("visits") + "" + shouldDisplay([
                            Account.INTERVIEWER,
                            Account.ADMIN,
                            Account.SUPER_ADMINISTRADOR
                        ])}>
                        <Link to={"/dashboard/visitas"}>
                            <img src={"/images/menu/visits.svg"} alt={"Icono visitas"} />
                            <span>Visitas</span>
                        </Link>
                    </li>
                    <li className={defineClass("interviewer") + "" + shouldDisplay([
                            Account.ADMIN,
                            Account.SUPER_ADMINISTRADOR
                        ])}>
                        <Link to={"/dashboard/entrevistadores"}>
                            <img src={"/images/menu/interviewer.svg"} alt={"Icono entrevistador"} />
                            <span>Entrevistador</span>
                        </Link>
                    </li>
                    <li className={defineClass("analyst") + "" + shouldDisplay([
                            Account.ADMIN,
                            Account.SUPER_ADMINISTRADOR
                        ])}>
                        <Link to={"/dashboard/analistas"}>
                            <img src={"/images/menu/analyst.svg"} alt={"Icono analista"} />
                            <span>Analista</span>
                        </Link>
                    </li>
                    <li className={defineClass("validations") + "" + shouldDisplay([
                            Account.ADMIN,
                            Account.SUPER_ADMINISTRADOR
                        ])}>
                        <Link to={"/dashboard/validaciones"}>
                            <img src={"/images/menu/validations.svg"} alt={"Icono validaciones"} />
                            <span>Validaciones</span>
                        </Link>
                    </li>
                    <li className={defineClass("admin") + "" + shouldDisplay([
                            Account.ADMIN,
                            Account.SUPER_ADMINISTRADOR
                        ])}>
                        <Link to={"/dashboard/administrador"}>
                            <img src={"/images/menu/admin.svg"} alt={"Icono administrador"} />
                            <span>Administrador</span>
                        </Link>
                    </li>
                </ul>
            </nav>
        </div>
    )
}

export default Menu