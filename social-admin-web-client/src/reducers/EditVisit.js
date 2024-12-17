const EditVisit = (state = false, action) => {
    switch (action.type) {
        case "editVisit/show":
            return true
        case "editVisit/hide":
            return false
        default:
            return state
    }
}

export default EditVisit