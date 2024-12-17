import React from "react";
import BrandPanel from "./BrandPanel";
import Footer from "../Footer";
import Login from "./Login";
import RecoverPassword from "./RecoverPassword";
import CreateAccount from "./CreateAccount";
import {Route, Switch} from "react-router-dom";
import ValidateAccount from "./ValidateAccount";
import ResetPassword from "./ResetPassword";

const Landing = () => {
    return (
        <div className={"landing recovery"}>
            <BrandPanel/>
            <Switch>
                <Route path={"/"} exact>
                    <Login/>
                </Route>
                <Route path={"/recuperar"} exact>
                    <RecoverPassword/>
                </Route>
                <Route path={"/registrar"} exact>
                    <CreateAccount/>
                </Route>
                <Route path={"/validar-cuenta"}>
                    <ValidateAccount/>
                </Route>
                <Route path={"/password/reset"}>
                    <ResetPassword/>
                </Route>
            </Switch>
            <Footer/>
        </div>
    )
}

export default Landing;