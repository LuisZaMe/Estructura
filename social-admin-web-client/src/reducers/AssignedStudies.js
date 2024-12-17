const assignedStudies = (state = false, action) => {
    switch (action.type) {
        case "assignedStudies/show":
            return true
        case "assignedStudies/hide":
            return false
        default:
            return state
    }
}

export default assignedStudies