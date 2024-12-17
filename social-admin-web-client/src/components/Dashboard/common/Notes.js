import React, {useEffect, useState} from "react";
import CandidateService from "../../../services/CandidateService";
import Note from "../../../model/Note";

const Notes = ({id, show, setShowNotes}) => {
    const [text, setText] = useState("")

    useEffect(() => {
    }, [])

    const renderUserText = () => {
    }

    const onSubmit = async (event) => {
        event.preventDefault()
        try {
            // ADD CODE TO SUBMIT NOTE
            setShowNotes(false)
            const note = new Note(1, text)
            await CandidateService.createNote(note)
        } catch (error) {
            console.log(error)
        }
    }

    const showHideModal = show ? "modal display-block" : "modal display-none"

    return (
        <div className={showHideModal}>
            <div className={"notes"}>
                <div className={"close-modal"}>
                    <img src={"/images/icon-close.png"} alt={""} onClick={() => setShowNotes(false)}/>
                </div>
                <h2>Agregar nota sobre<br/>el candidato{renderUserText()}</h2>
                <form onSubmit={onSubmit}>
                    <textarea placeholder={"Agregar notas..."} value={text} onChange={event => setText(event.target.value)}></textarea>
                    <button className={"form-button-primary"}>Aceptar</button>
                </form>
            </div>
        </div>
    )
}

export default Notes