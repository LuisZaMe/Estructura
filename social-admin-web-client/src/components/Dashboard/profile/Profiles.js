import React from "react"
import {Route, Switch} from "react-router-dom";

// Components
import ProfileView from "./ProfileView";
import ProfileEdit from "./ProfileEdit";

const Profiles = () => {
    return (
        <Switch>
            <Route path="/dashboard/profile" exact>
                <ProfileView/>
            </Route>
            <Route path="/dashboard/profile/editar" exact>
                <ProfileEdit/>
            </Route>
        </Switch>
    )
}

export default Profiles