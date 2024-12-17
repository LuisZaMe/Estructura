const registerCandidate = (state = false, action) => {
    switch (action.type) {
        case "registerCandidate/show":
            return true
        case "registerCandidate/hide":
            return false
        default:
            return state
    }
}

export default registerCandidate