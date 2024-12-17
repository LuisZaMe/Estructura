import React, {useEffect, useRef, useState} from "react";

const ActionDropdown = ({onClickView, studyId}) => {
    const [active, setActive] = useState(false)
    const ref = useRef()

    useEffect(() => {
        const onBodyClick = (event) => {
            if (ref.current && ref.current.contains(event.target)) {
                return
            }
            setActive(false)
        }
        document.body.addEventListener("click", onBodyClick)

        return () => {
            document.body.removeEventListener("click", onBodyClick)
        }
    }, [])

    return (
        <div className={"action-dropdown"} ref={ref}>
            <div onClick={() => setActive(!active)} className={"action-icon"}>
                <img src={"/images/actions-dropdown/dot.png"} alt={""}/>
                <img src={"/images/actions-dropdown/dot.png"} alt={""}/>
                <img src={"/images/actions-dropdown/dot.png"} alt={""}/>
            </div>
            <div className={`dropdown-content ${active ? "show" : ""}`}>
                <div className={"dropdown-option"} onClick={() => onClickView(studyId)}>
                    <img src={"/images/actions-dropdown/eye.svg"} alt={""}/>
                    <span>Ver estudio</span>
                </div>
            </div>
        </div>
    )
}

export default ActionDropdown