const registerAnalyst = (state = false, action) => {
    switch (action.type) {
        case "registerAnalyst/show":
            return true
        case "registerAnalyst/hide":
            return false
        default:
            return state
    }
}

export default registerAnalyst