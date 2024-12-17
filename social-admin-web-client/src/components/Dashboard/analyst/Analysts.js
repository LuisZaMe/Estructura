import React from "react";
import {Route, Switch} from "react-router-dom";

// Components
import AnalystList from "./AnalystList";
import AnalystView from "./AnalystView";
import AnalystEdit from "./AnalystEdit";

const Analysts = () => {
    return (
        <Switch>
            <Route path={"/dashboard/analistas"} exact>
                <AnalystList/>
            </Route>
            <Route path={"/dashboard/analistas/ver"} exact>
                <AnalystView/>
            </Route>
            <Route path={"/dashboard/analistas/editar"} exact>
                <AnalystEdit/>
            </Route>
            <Route path={"/dashboard/analistas/ver-estudios"} exact>
                <AnalystEdit/>
            </Route>
        </Switch>
    )
}

export default Analysts