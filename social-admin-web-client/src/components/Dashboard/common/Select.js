import React, {useEffect, useRef, useState} from "react";

const Select = ({options, selectedOption, onChange, page, setPage}) => {
    const [expanded, setExpanded] = useState(false)
    const ref = useRef()

    useEffect(() => {
        // Listener to catch click outside component and close dropdown
        const onBodyClick = (event) => {
            if (ref.current && ref.current.contains(event.target)) {
                return
            }
            setExpanded(false)
        }
        document.body.addEventListener("click", onBodyClick)

        return () => {
            document.body.removeEventListener("click", onBodyClick)
        }
    }, [])

    const handleScroll = (event) => {
        const newPage = Math.floor(event.target.scrollTop / 175)
        debugger;

        if (newPage > page) {
            setPage(newPage)
        }
    }

    const renderOptions = options.map((option, index) => {
        return (
            <li key={index} onClick={() => onChange(option)}>
                <span>{option.value}</span>
            </li>
        )
    })

    return (
        <div className={"select"} onClick={() => setExpanded(!expanded)} ref={ref}>
            <div className={"input"}>
                <span>{selectedOption ? selectedOption : "Seleccionar"}</span>
                <img src={"/images/arrow-down.png"} alt={""}/>
            </div>
            <div className={`body ${expanded ? "show" : ""} no-scrollbar`} onScroll={handleScroll}>
                <ul>{renderOptions}</ul>
            </div>
        </div>
    )
}

export default Select