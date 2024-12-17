const Study = (state = null, action) => {
    switch (action.type) {
        case "study/setId":
            return action.payload
        case "admin/removeId":
            return null
        default:
            return state
    }
}

export default Study