import React, {useEffect, useState} from "react"
import Content from "../Content"
import {useHistory} from "react-router-dom";

const Title = () => {
    const [content, setContent] = useState("")
    const history = useHistory()

    useEffect(() => {
        const stopListening = history.listen(() => {
            switch (window.location.pathname) {
                case "/dashboard":
                default:
                    setContent("home")
                    break
                case "/dashboard/generacion-de-estudios":
                    setContent("study")
                    break
                case "/dashboard/clientes":
                case "/dashboard/clientes/editar":
                case "/dashboard/clientes/ver":
                    setContent("clients")
                    break
                case "/dashboard/candidatos":
                    setContent("candidates")
                    break
                case "/dashboard/visitas":
                    setContent("visits")
                    break
                case "/dashboard/entrevistadores":
                case "/dashboard/entrevistadores/ver":
                case "/dashboard/entrevistadores/editar":
                    setContent("interviewer")
                    break
                case "/dashboard/analistas":
                case "/dashboard/analistas/ver":
                case "/dashboard/analistas/editar":
                    setContent("analyst")
                    break
                case "/dashboard/validaciones":
                    setContent("validations")
                    break
                case "/dashboard/administrador":
                case "/dashboard/administrador/ver":
                case "/dashboard/administrador/editar":
                    setContent("admin")
                    break
                case "/dashboard/cliente-generacion-de-estudios":
                case "/dashboard/analistas/ver-estudios":
                    setContent("clientStudies")
                    break
                case "/dashboard/analyst-assigned-study-list":
                    setContent("analystStudies")
                    break
                case "/dashboard/analyst-finished-study-list":
                    setContent("analystCompleteStudies")
                    break
                case "/dashboard/interviewer-assigned-studies":
                    setContent("analystStudies")
                    break
            }

            return stopListening
        })
    })

    return (
        <div className={"title"}>
            <h1>{Content[content]}</h1>
        </div>
    )
}

export default Title