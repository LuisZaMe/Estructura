const assignStudy = (state = false, action) => {
    switch (action.type) {
        case "assignStudy/show":
            return true
        case "assignStudy/hide":
            return false
        default:
            return state
    }
}

export default assignStudy