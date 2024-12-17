import React from "react";
import {Route, Switch} from "react-router-dom";

// Components
import InterviewerList from "./InterviewerList";
import InterviewerView from "./InterviewerView";
import InterviewerEdit from "./InterviewerEdit";

const Interviewers = () => {
    return (
        <Switch>
            <Route path={"/dashboard/entrevistadores"} exact>
                <InterviewerList/>
            </Route>
            <Route path={"/dashboard/entrevistadores/ver"} exact>
                <InterviewerView/>
            </Route>
            <Route path={"/dashboard/entrevistadores/editar"} exact>
                <InterviewerEdit/>
            </Route>
        </Switch>
    )
}

export default Interviewers