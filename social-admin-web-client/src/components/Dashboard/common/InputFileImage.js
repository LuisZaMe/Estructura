import React, { useEffect, useRef, useState } from "react";
import AuthService from "../../../services/AuthService";

const InputFileImage = ({ setFile, imageUrl, setImageUrl }) => {
    const disableForm = AuthService.getIdentity().role === 4;
    const fileRef = useRef()
    const [image, setImage] = useState("")
    const [show, setShow] = useState(false)

    useEffect(() => {
        setImage(imageUrl)
    }, [imageUrl])

    const handleFile = event => {
        setFile(event.target.files[0])
        setImage(URL.createObjectURL(event.target.files[0]))
        setImageUrl(URL.createObjectURL(event.target.files[0]))
    }

    const removeFile = () => {
        setFile(null)
        setImage("")
        setImageUrl("")
    }

    const showHideModal = show ? "modal display-block" : "modal display-none"
    if (image === "") {
        return (
            <div className={"input-file-image"} onClick={() => fileRef.current.click()}>
                <img className={"image-icon"} src={"/images/image-icon.png"} alt={""} />
                <label className={"image-label"}>Subir imagen</label>
                <img className={"image-add"} src={"/images/add-icon.png"} alt={""} />
                <input ref={fileRef} type={"file"} onChange={handleFile} hidden accept={".jpg, .jpeg"} disabled={disableForm} />
            </div>
        )
    } else {
        return (
            <div className={"input-file-image loaded"}>
                <img className={"uploaded-image"} src={image} alt={""} />
                <div className={"overlay"}>
                    <button className={"preview-button"} onClick={() => setShow(true)} >
                        <img src={"/images/actions-dropdown/eye.svg"} alt={""} />
                    </button>
                </div>
                <button className={"remove-file"} onClick={removeFile} disabled={disableForm}>
                    <img src={"/images/close-icon.svg"} alt={""} />
                </button>
                <div className={showHideModal}>
                    <div className={"preview-modal"}>
                        <button className={"remove-file"} onClick={() => setShow(false)}>
                            <img src={"/images/close-icon.svg"} alt={""} />
                        </button>
                        <img className={"file-preview"} src={image} alt={""} />
                    </div>
                </div>
            </div>
        )
    }
}

export default InputFileImage
