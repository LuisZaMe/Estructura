import { combineReducers } from "redux";

// Admin
import admin from "./Admin";
import registerAdmin from "./RegisterAdmin";

// Analyst
import analyst from "./Analyst";
import assignStudy from "./AssignStudy";
import assignedStudies from "./AssignedStudies";
import registerAnalyst from "./RegisterAnalyst";

// Candidate
import candidate from "./Candidate";
import registerCandidate from "./RegisterCandidate";
import registerCandidateFromClient from "./registerCandidateFromClient";

// Client
import client from "./Client";
import registerClient from "./RegisterClient";

// Interviewer
import interviewer from "./Interviewer";
import registerInterviewer from "./RegisterInterviewer";
import visit from "./Visit";
import registerVisit from "./RegisterVisit";
import editVisit from "./EditVisit";

// Study
import study from "./Study";

const reducers = combineReducers({
    // Admin
    admin: admin,
    registerAdmin: registerAdmin,

    // Analyst
    analyst: analyst,
    assignStudy: assignStudy,
    assignedStudies: assignedStudies,
    registerAnalyst: registerAnalyst,

    // Candidate
    candidate: candidate,
    registerCandidate: registerCandidate,
    registerCandidateFromClient: registerCandidateFromClient,

    // Client
    client: client,
    registerClient: registerClient,

    // Interviewer
    interviewer: interviewer,
    registerInterviewer: registerInterviewer,

    // Visits
    visit: visit,
    registerVisit: registerVisit,
    editVisit: editVisit,

    // Study
    study: study
})

export default reducers