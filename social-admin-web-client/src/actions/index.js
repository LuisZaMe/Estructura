// Admin
export const showRegisterAdmin = () => {
    return {
        type: "registerAdmin/show"
    }
}

export const hideRegisterAdmin = () => {
    return {
        type: "registerAdmin/hide"
    }
}

export const setAdminId = (id) => {
    return {
        type: "admin/setId",
        payload: id
    }
}

export const removeAdminId = () => {
    return {
        type: "admin/removeId"
    }
}
// Visit
export const showRegisterVisit = () => {
    return {
        type: "registerVisit/show"
    }
}

export const hideRegisterVisit = () => {
    return {
        type: "registerVisit/hide"
    }
}
export const showEditVisit = () => {
    return {
        type: "editVisit/show"
    }
}

export const showEditProfile = () => {
    return {
        type: "editProfile/show"
    }
}

export const hideEditProfile = () => {
    return {
        type: "editProfile/hide"
    }
}

export const hideEditVisit = () => {
    return {
        type: "editVisit/hide"
    }
}

export const setVisitId = (id) => {
    return {
        type: "visit/setId",
        payload: id
    }
}

export const setProfileId = (id) => {
    return {
        type: "profile/setId",
        payload: id
    }
}

export const removeVisitId = () => {
    return {
        type: "visit/removeId"
    }
}

// Analyst
export const showRegisterAnalyst = () => {
    return {
        type: "registerAnalyst/show"
    }
}

export const hideRegisterAnalyst = () => {
    return {
        type: "registerAnalyst/hide"
    }
}

export const setAnalystId = (id) => {
    return {
        type: "analyst/setId",
        payload: id
    }
}

export const removeAnalystId = () => {
    return {
        type: "analyst/removeId"
    }
}

export const showAssignStudy = () => {
    return {
        type: "assignStudy/show"
    }
}

export const hideAssignStudy = () => {
    return {
        type: "assignStudy/hide"
    }
}

export const showAssignedStudies = () => {
    return {
        type: "assignedStudies/show"
    }
}

export const hideAssignedStudies = () => {
    return {
        type: "assignedStudies/hide"
    }
}

// Candidate
export const showRegisterCandidate = ()  => {
    return {
        type: "registerCandidate/show"
    }
}

export const hideRegisterCandidate = () => {
    return {
        type: "registerCandidate/hide"
    }
}

export const registerCandidateFromClient = () => {
    return {
        type: "registerCandidateFromClient"
    }
}

export const setCandidateId = (id) => {
    return {
        type: "candidate/setId",
        payload: id
    }
}

export const removeCandidateId = () => {
    return {
        type: "candidate/removeId"
    }
}

// Client
export const showRegisterClient = () => {
    return {
        type: "registerClient/show"
    }
}

export const hideRegisterClient = () => {
    return {
        type: "registerClient/hide"
    }
}

export const setClientId = (id) => {
    return {
        type: "client/setId",
        payload: id
    }
}

export const removeClientId = () => {
    return {
        type: "client/removeId"
    }
}

// Interviewer
export const showRegisterInterviewer = () => {
    return {
        type: "registerInterviewer/show"
    }
}

export const hideRegisterInterviewer = () => {
    return {
        type: "registerInterviewer/hide"
    }
}

export const setInterviewerId = (id) => {
    return {
        type: "interviewer/setId",
        payload: id
    }
}

export const removeInterviewerId = () => {
    return {
        type: "interviewer/removeId"
    }
}

// Study
export const setStudyId = (id) => {
    return {
        type: "study/setId",
        payload: id
    }
}

export const removeStudyId = () => {
    return {
        type: "study/removeId"
    }
}