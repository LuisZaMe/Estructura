const Interviewer = (state = null, action) => {
    switch (action.type) {
        case "interviewer/setId":
            return action.payload
        case "interviewer/removeId":
            return null
        default:
            return state
    }
}

export default Interviewer