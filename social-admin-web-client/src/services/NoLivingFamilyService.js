import SocialAdmin from "./SocialAdmin";
import authHeader from "./AuthHeader";

class NoLivingFamilyService {
    create = (noLivingFamily) => {
        return SocialAdmin.post("/StudyFamily/NoLivingFamily", noLivingFamily, {
            headers: authHeader()
        })
    }

    update = (noLivingFamily) => {
        return SocialAdmin.put("/StudyFamily/NoLivingFamily", noLivingFamily, {
            headers: authHeader()
        })
    }

    delete = (id) => {
        return SocialAdmin.delete("/StudyFamily/NoLivingFamily", {
            params: {
                id: id
            },
            headers: authHeader()
        })
    }
}

export default new NoLivingFamilyService()