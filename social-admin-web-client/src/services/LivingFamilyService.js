import SocialAdmin from "./SocialAdmin"
import authHeader from "./AuthHeader";

class LivingFamilyService {
    create = (livingFamily) => {
        return SocialAdmin.post("/StudyFamily/LivingFamily", livingFamily, {
            headers: authHeader()
        })
    }

    update = (livingFamily) => {
        return SocialAdmin.put("/StudyFamily/LivingFamily", livingFamily, {
            headers: authHeader()
        })
    }

    delete = (id) => {
        return SocialAdmin.delete("/StudyFamily/LivingFamily", {
            params: {
                id: id
            },
            headers: authHeader()
        })
    }
}

export default new LivingFamilyService()