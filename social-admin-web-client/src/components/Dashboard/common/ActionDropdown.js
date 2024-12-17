import React, {useEffect, useRef, useState} from "react";

const ActionDropdown = ({
    onClickView,
    onClickEdit,
    onClickDelete,
    userId,
    notes,
    showNotes,
    showDelete = true,
}) => {
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

    const renderNotes = () => {
        if (notes) {
            return (
                <div className={"dropdown-option"} onClick={() => {
                    setActive(false)
                    showNotes(true)
                }}>
                    <img src={"/images/actions-dropdown/note.svg"} alt={""}/>
                    <span>Notas</span>
                </div>
            )
        }
    }

    const RenderDeleteOption = (userId) => {
        if (!showDelete) return null;
        return (
            <div className={"dropdown-option"} onClick={() => {
                setActive(false)
                onClickDelete(userId)
            }}>
                <img src={"/images/actions-dropdown/trash.svg"} alt={""}/>
                <span>Eliminar</span>
            </div>
        );
    }

    return (
        <div className={"action-dropdown"} ref={ref}>
            <div onClick={() => setActive(!active)} className={"action-icon"}>
                <img src={"/images/actions-dropdown/dot.png"} alt={""}/>
                <img src={"/images/actions-dropdown/dot.png"} alt={""}/>
                <img src={"/images/actions-dropdown/dot.png"} alt={""}/>
            </div>
            <div className={`dropdown-content ${active ? "show" : ""}`}>
                <div className={"dropdown-option"} onClick={() => onClickView(userId)}>
                    <img src={"/images/actions-dropdown/eye.svg"} alt={""}/>
                    <span>Ver</span>
                </div>
                <div className={"dropdown-option"} onClick={() => onClickEdit(userId)}>
                    <img src={"/images/actions-dropdown/edit.svg"} alt={""}/>
                    <span>Editar</span>
                </div>
                {RenderDeleteOption(userId)}
                {renderNotes()}
            </div>
        </div>
    )
}

export default ActionDropdown