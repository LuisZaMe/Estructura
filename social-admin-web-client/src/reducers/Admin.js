const Admin = (state = null, action) => {
    switch (action.type) {
        case "admin/setId":
            return action.payload
        case "admin/removeId":
            return null
        default:
            return state
    }
}

export default Admin