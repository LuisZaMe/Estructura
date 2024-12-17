import SocialAdmin from "./SocialAdmin";
import authHeader from "./AuthHeader";

class FamilyService {
    create = (studyFamily) => {
        return SocialAdmin.post("/StudyFamily", studyFamily, {
            headers: authHeader()
        })
    }

    update = (studyFamily) => {
        return SocialAdmin.put("/StudyFamily", studyFamily, {
            headers: authHeader()
        })
    }
}

export default new FamilyService()