const registerInterviewer = (state = false, action) => {
    switch (action.type) {
        case "registerInterviewer/show":
            return true
        case "registerInterviewer/hide":
            return false
        default:
            return state
    }
}

export default registerInterviewer