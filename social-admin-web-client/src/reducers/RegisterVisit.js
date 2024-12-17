const registerVisit = (state = false, action) => {
    switch (action.type) {
        case "registerVisit/show":
            return true
        case "registerVisit/hide":
            return false
        default:
            return state
    }
}

export default registerVisit