import SocialAdmin from "./SocialAdmin";
import authHeader from "./AuthHeader";

class StudyGeneralInformationService {
    create = (data) => {
        return SocialAdmin.post("/StudyGeneralInformation", data, {
            headers: authHeader()
        })
    }

    update = (data) => {
        return SocialAdmin.put("/StudyGeneralInformation", data, {
            headers: authHeader()
        })
    }
}

export default new StudyGeneralInformationService()