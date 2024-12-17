const registerAdmin = (state = false, action) => {
    switch (action.type) {
        case "registerAdmin/show":
            return true
        case "registerAdmin/hide":
            return false
        default:
            return state
    }
}

export default registerAdmin