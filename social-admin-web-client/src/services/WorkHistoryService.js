import SocialAdmin from "./SocialAdmin";
import authHeader from "./AuthHeader";

class WorkHistoryService {
    create = (jobs) => {
        return SocialAdmin.post("/StudyLaboralTrayectory", jobs, {
            headers: authHeader()
        })
    }

    update = (job) => {
        return SocialAdmin.put("/StudyLaboralTrayectory", job, {
            headers: authHeader()
        })
    }

    delete = (id) => {
        return SocialAdmin.delete("/StudyLaboralTrayectory", {
            params: {
                id: id
            },
            headers: authHeader()
        })
    }
}

export default new WorkHistoryService()