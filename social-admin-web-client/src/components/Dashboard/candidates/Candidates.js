import React from "react";
import {Route, Switch} from "react-router-dom";

// Components
import CandidateList from "./CandidateList";
import CandidateView from "./CandidateView";
import CandidateEdit from "./CandidateEdit";

const Candidates = () => {
    return (
        <Switch>
            <Route path={"/dashboard/candidatos"} exact>
                <CandidateList/>
            </Route>
            <Route path={"/dashboard/candidatos/ver"} exact>
                <CandidateView/>
            </Route>
            <Route path={"/dashboard/candidatos/editar"} exact>
                <CandidateEdit/>
            </Route>
        </Switch>
    )
}

export default Candidates