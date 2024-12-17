import SocialAdmin from "./SocialAdmin";
import authHeader from "./AuthHeader";

class FinalResultService {
    create = (data) => {
        return SocialAdmin.post("/StudyFinalResult", data, {
            headers: authHeader()
        })
    }

    update = (data) => {
        return SocialAdmin.put("/StudyFinalResult", data, {
            headers: authHeader()
        })
    }
}

export default new FinalResultService()