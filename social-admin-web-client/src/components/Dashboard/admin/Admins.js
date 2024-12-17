import React from "react";
import {Route, Switch} from "react-router-dom";

// Components
import AdminList from "./AdminList";
import AdminView from "./AdminView";
import AdminEdit from "./AdminEdit";

const Admins = () => {
    return (
        <Switch>
            <Route path={"/dashboard/administrador"} exact>
                <AdminList/>
            </Route>
            <Route path={"/dashboard/administrador/ver"} exact>
                <AdminView/>
            </Route>
            <Route path={"/dashboard/administrador/editar"} exact>
                <AdminEdit/>
            </Route>
        </Switch>
    )
}

export default Admins