import SocialAdmin from "./SocialAdmin";
import authHeader from "./AuthHeader";

class EducationListService {
    create = (education) => {
        return SocialAdmin.post("/StudySchoolarity/Schoolarity", education, {
            headers: authHeader()
        })
    }

    update = (education) => {
        return SocialAdmin.put("/StudySchoolarity/Schoolarity", education, {
            headers: authHeader()
        })
    }
    
    delete = (id) => {
        return SocialAdmin.delete("/StudySchoolarity/Schoolarity", {
            params: {
                id: id
            },
            headers: authHeader()
        })
    }
}

export default new EducationListService()