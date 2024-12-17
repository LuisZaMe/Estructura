import React from "react";
import {Switch} from "react-router-dom";
//import Route from "react-router-dom/es/Route";
import {Route} from "react-router-dom";

// Components
import StudyView from "./StudyView";
import StudyList from "./StudyList";
import AnalystAssigned from "./steps/AnalystAssigned";
import AdminAssigned from "./steps/AdminAssigned";
import ApprovedStudy from "./steps/ApprovedStudy";
import RejectedStudy from "./steps/RejectedStudy";

const Studies = () => {
    return (
        <Switch>
            <Route path={"/dashboard/validaciones/ver"} exact>
                <StudyView/>
            </Route>
            <Route path={"/dashboard/validaciones"} exact>
                <StudyList/>
            </Route>
            <Route path={"/dashboard/validaciones/analista-asignado"} exact>
                <AnalystAssigned/>
            </Route>
            <Route path={"/dashboard/validaciones/administrador-asignado"} exact>
                <AdminAssigned/>
            </Route>
            <Route path={"/dashboard/validaciones/estudio-aprobado"} exact>
                <ApprovedStudy/>
            </Route>
            <Route path={"/dashboard/validaciones/estudio-rechazado"} exact>
                <RejectedStudy/>
            </Route>
        </Switch>
    )
}

export default Studies