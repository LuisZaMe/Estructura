const registerCandidateFromClient = (state = false, action) => {
    if (action.type === "registerCandidateFromClient") {
        return !state
    }
    return state
}

export default registerCandidateFromClient