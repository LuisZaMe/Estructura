import SocialAdmin from "./SocialAdmin";
import authHeader from "./AuthHeader";

class ExtracurricularActivities {
    create = (activities) => {
        return SocialAdmin.post("/StudySchoolarity/ExtracurricularActivities", activities, {
            headers: authHeader()
        })
    }

    update = (activity) => {
        return SocialAdmin.put("/StudySchoolarity/ExtracurricularActivities", activity, {
            headers: authHeader()
        })
    }

    delete = (id) => {
        return SocialAdmin.delete("/StudySchoolarity/ExtracurricularActivities", {
            params: {
                id: id
            },
            headers: authHeader()
        })
    }
}

export default new ExtracurricularActivities()