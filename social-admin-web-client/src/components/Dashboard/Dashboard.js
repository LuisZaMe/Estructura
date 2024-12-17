import React from "react"
import { Route, Switch } from "react-router-dom"

import Footer from "../Footer"
import Menu from "./Menu"
import Header from "./Header"
import Study from "./study/Study"
import Clients from "./clients/Clients"
import Candidates from "./candidates/Candidates"
import Admins from "./admin/Admins";
import Analysts from "./analyst/Analysts";
import Interviewers from "./interviewer/Interviewers";
import Studies from "./study/Studies";
import Visits from "./visit/Visits";
import Profiles from "./profile/Profiles";
import ClientStudyList from "./Client/ClientStudyList";
import InterviewerAssignedStudies from "./interviewer/InterviewerAssignedStudies";
import AnalystAssignedStudyList from "./analyst/AnalystAssignedStudyList"
import AnalystFinishedStudyList from "./analyst/AnalystFinishedStudyList"

const Dashboard = () => {

    return (
        <div className={"dashboard"}>
            <Menu />
            <Header />
                <Route path={"/dashboard"}>
                    <Profiles />
                </Route>
            <Switch>
                <Route path={"/dashboard"} exact>
                    <div className={"container"}>
                        <h1>Bienvenido</h1>
                        <img src={"/images/logo.png"} alt={""} />
                    </div>
                </Route>
                <Route path={"/dashboard/cliente-generacion-de-estudios"} exact>
                    <ClientStudyList />
                </Route>

                <Route path={"/dashboard/analyst-assigned-study-list"} exact>
                    <AnalystAssignedStudyList />
                </Route>

                <Route path={"/dashboard/analyst-finished-study-list"} exact>
                    <AnalystFinishedStudyList />
                </Route>

                <Route path={"/dashboard/generacion-de-estudios"} exact>
                    <Study />
                </Route>
                <Route path={"/dashboard/interviewer-assigned-studies"} exact>
                    <InterviewerAssignedStudies />
                </Route>
                <Route path={"/dashboard/clientes"}>
                    <Clients />
                </Route>
                <Route path={"/dashboard/candidatos"}>
                    <Candidates />
                </Route>
                <Route path={"/dashboard/visitas"}>
                    <Visits />
                </Route>
                <Route path={"/dashboard/entrevistadores"}>
                    <Interviewers />
                </Route>
                <Route path={"/dashboard/analistas"}>
                    <Analysts />
                </Route>
                <Route path={"/dashboard/validaciones"}>
                    <Studies />
                </Route>
                <Route path={"/dashboard/administrador"}>
                    <Admins />
                </Route>
            </Switch>
            <Footer />
        </div>
    )
}

export default Dashboard