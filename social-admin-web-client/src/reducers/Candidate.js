const Candidate = (state = null, action) => {
    switch (action.type) {
        case "candidate/setId":
            return action.payload
        case "candidate/removeId":
            return null
        default:
            return state
    }
}

export default Candidate