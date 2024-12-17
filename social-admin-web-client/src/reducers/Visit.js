const Visit = (state = null, action) => {
    switch (action.type) {
        case "visit/setId":
            return action.payload
        case "visit/removeId":
            return null
        default:
            return state
    }
}

export default Visit