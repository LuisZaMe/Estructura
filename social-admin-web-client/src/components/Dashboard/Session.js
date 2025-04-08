import React, {useEffect, useRef, useState} from "react";
import AuthService from "../../services/AuthService";
import {useHistory} from "react-router-dom";

const Session = () => {
    const [expanded, setExpanded] = useState(false)
    const [user, setUser] = useState({})
    const ref = useRef()
    const history = useHistory()

    useEffect(() => {
        const onBodyClick = (event) => {
            if (ref.current.contains(event.target)) {
                return
            }
            setExpanded(false)
        }
        document.body.addEventListener("click", onBodyClick)

        setUser(AuthService.getUser().identity)

        return () => {
            document.body.removeEventListener("click", onBodyClick)
        }
    },[])

    const logOut = () => {
        AuthService.logout()
        history.push("/")
    }

    const goToProfile = () => {
        history.push("/dashboard/profile")
    }

    return (
        <div className={"session"}>
            <div className={"session-dropdown"} ref={ref}>
                <div className={"session-user"} onClick={() => setExpanded(!expanded)}>
                    <img src={"/images/icon-user.png"} alt={"User icon"}/>
                    <span>{`${user.name} ${user.lastname || ""}`.trim()}</span>
                    <img src={"/images/icon-arrow-down.png"} alt={"dropdown icon"}/>
                </div>
                <div className={`session-actions ${expanded ? "expanded" : ""}`}>
                    <ul>
                        <li onClick={goToProfile}>
                            <span>Mi Perfil</span>
                        </li>
                        <li onClick={logOut}>
                            <span>Cerrar sesion</span>
                        </li>
                    </ul>
                </div>
            </div>
            <img className={"header-right-puzzle"} src={"/images/puzzle-empty.png"} alt={"Header Puzzle icon"}/>
        </div>
    )
}

export default Session