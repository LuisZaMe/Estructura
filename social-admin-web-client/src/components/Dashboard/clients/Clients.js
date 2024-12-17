import React from "react"
import {Route, Switch} from "react-router-dom";

// Components
import ClientList from "./ClientList";
import ClientView from "./ClientView";
import ClientEdit from "./ClientEdit";

const Clients = () => {
    return (
        <Switch>
            <Route path={"/dashboard/clientes"} exact>
                <ClientList/>
            </Route>
            <Route path={"/dashboard/clientes/ver"} exact>
                <ClientView/>
            </Route>
            <Route path={"/dashboard/clientes/editar"} exact>
                <ClientEdit/>
            </Route>
        </Switch>
    )
}

export default Clients