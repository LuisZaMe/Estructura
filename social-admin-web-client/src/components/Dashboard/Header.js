import React from "react";
import DashboardLogo from "./DashboardLogo";
import Title from "./Title";
import Session from "./Session";

const Header = () => {
    return (
        <div className="main-header">
            <div className={"header"}>
                <DashboardLogo />
                <Title />
                <Session />
            </div>
            <div className="heaer-offset" />
        </div>

    )
}

export default Header