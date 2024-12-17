const registerClient = (state = false, action) => {
    switch (action.type) {
        case "registerClient/show":
            return true
        case "registerClient/hide":
            return false
        default:
            return state
    }
}

export default registerClient