import SocialAdmin from "./SocialAdmin";
import authHeader from "./AuthHeader";

class EducationService {
    create = (education) => {
        return SocialAdmin.post("/StudySchoolarity", education, {
            headers: authHeader()
        })
    }

    update = (education) => {
        return SocialAdmin.put("/StudySchoolarity", education, {
            headers: authHeader()
        })
    }
}

export default new EducationService()