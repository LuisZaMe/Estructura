import "../styles/App.css";

import React from "react";
import Landing from "./Landing/Landing";
import Dashboard from "./Dashboard/Dashboard";
import { BrowserRouter, Route, Switch } from "react-router-dom";
import ViewPdf from "./PDFs/ViewPdf";

const App = () => {
    return (
        <BrowserRouter>
            <Switch>
                <Route path={"/dashboard"}>
                    <Dashboard />
                </Route>
                <Route path={"/"}>
                    <Landing />
                </Route>
                <Route path={"/test"}>
                    <ViewPdf />
                </Route>
            </Switch>
        </BrowserRouter>
    )
}

export default App;