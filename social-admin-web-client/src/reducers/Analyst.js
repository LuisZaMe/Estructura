const Analyst = (state = null, action) => {
    switch (action.type) {
        case "analyst/setId":
            return action.payload
        case "analyst/removeId":
            return null
        default:
            return state
    }
}

export default Analyst