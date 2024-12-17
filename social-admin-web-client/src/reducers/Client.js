const Client = (state = null, action) => {
    switch (action.type) {
        case "client/setId":
            return action.payload
        case "client/removeId":
            return null
        default:
            return state
    }
}

export default Client