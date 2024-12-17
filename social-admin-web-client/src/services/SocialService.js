import SocialAdmin from "./SocialAdmin";
import authHeader from "./AuthHeader";

class SocialService {
    create = (social) => {
        return SocialAdmin.post("/StudySocial", social, {
            headers: authHeader()
        })
    }

    update = (social) => {
        return SocialAdmin.put("/StudySocial", social, {
            headers: authHeader()
        })
    }
}

export default new SocialService()