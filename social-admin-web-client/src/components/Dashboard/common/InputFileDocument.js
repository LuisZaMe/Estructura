import React, { useEffect, useRef, useState } from "react";
import AuthService from "../../../services/AuthService";

const InputFileDocument = ({ file, setFile }) => {
    const ref = useRef()
    const disableForm = AuthService.getIdentity().role === 4;
    const [fileName, setFileName] = useState()

    useEffect(() => {
        setFileName(file)
    }, [file])

    if (file) {
        return (
            <div className={"upload-file uploaded"} onClick={() => setFile(null)}>
                <label>{fileName}</label>
                <img src={"/images/close-icon.svg"} alt={""} />
            </div>
        )
    } else {
        return (
            <div className={"upload-file"} onClick={() => ref.current.click()}>
                <label>Cargar documento</label>
                <input ref={ref} type={"file"} name={"document"} onChange={event => setFile(event.target.files[0])}
                    hidden accept={".pdf"} disabled={disableForm} />
            </div>
        )
    }
}

export default InputFileDocument