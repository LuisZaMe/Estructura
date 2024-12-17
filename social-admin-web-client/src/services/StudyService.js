import SocialAdmin from "./SocialAdmin";
import authHeader from "./AuthHeader";

class StudyService {
    create = (study) => {
        return SocialAdmin.post("/Study", study, {
            headers: authHeader()
        })
    }

    update = (study) => {
        return SocialAdmin.put("/Study", study, {
            headers: authHeader()
        })
    }

    getStudy = (id) => {
        return SocialAdmin.get("/Study", {
            params: {
                id: id
            },
            headers: authHeader()
        })
    }

    getStudies = (
        page,
        serviceType,
        interviewerId,
        studyStatus,
        clientId,
        candidateId,
        studyProgressStatus,
        analystId,
        bringStudiesOnlyOwn = true,
        bringStudiesOnly = 0
    ) => {
        return SocialAdmin.get("/Study", {
            params: {
                serviceType: serviceType,
                interviewerId: interviewerId,
                studyStatus: studyStatus,
                clientId: clientId,
                candidateId: candidateId,
                currentPage: page,
                studyProgressStatus: studyProgressStatus,
                analystId: analystId,
                offset: 9,
                bringStudiesOnlyOwn: bringStudiesOnlyOwn,
                bringStudiesOnly: bringStudiesOnly,
            },
            headers: authHeader()
        })
    }

    getAllStudies = () => {
        return SocialAdmin.get("/Study", {
            params: {
                // currentPage: 1,
                offset: 100000000
            },
            headers: authHeader()
        })
    }

    getPages = (serviceType, interviewerId, studyStatus, clientId, candidateId, studyProgressStatus, analystId) => {
        return SocialAdmin.get("/Study/Pagination", {
            params: {
                serviceType: serviceType,
                interviewerId: interviewerId,
                studyStatus: studyStatus,
                clientId: clientId,
                candidateId: candidateId,
                studyProgressStatus: studyProgressStatus,
                analystId: analystId,
                splitBy: 9
            },
            headers: authHeader()
        })
    }
}

export default new StudyService()