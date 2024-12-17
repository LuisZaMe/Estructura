import React from 'react';
import {Route, Switch} from "react-router-dom";

// Components
import VisitList from "./VisitList";
import VisitView from "./VisitView";
import VisitEdit from "./VisitEdit";

const Visits = () => {
    return (
        <Switch>
            <Route path={"/dashboard/visitas"} exact>
                <VisitList/>
            </Route>
            <Route path={"/dashboard/visitas/ver"} exact>
                <VisitView/>
            </Route>
            <Route path={"/dashboard/visitas/editar"} exact>
                <VisitEdit/>
            </Route>
        </Switch>
    )
}
export default Visits
