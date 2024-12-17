import React, {useEffect, useState} from "react";
import {useHistory} from "react-router-dom";

const DashboardLogo = () => {
    const [path, setPathname] = useState(window.location.pathname)
    const history = useHistory()

    useEffect(() => {
        history.listen(() => {
            setPathname(window.location.pathname)
        })
    })

    const renderContent = () => {
        if (path === "/dashboard") {
            return (
                <div className={"dashboard-logo-home"}>
                    <img src="/images/dashboard-puzzle.png" alt="Header Icon"/>
                </div>
            )
        } else {
            return (
                <div className={"dashboard-logo-icon"}>
                    <img src="/images/dashboard-logo.png" alt="Logo Icon"/>
                </div>
            )
        }
    }

    return (
        renderContent()
    )

}

export default DashboardLogo