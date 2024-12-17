import SocialAdmin from "./SocialAdmin";
import authHeader from "./AuthHeader";

class PersonalReferenceService {
    create = (references) => {
        return SocialAdmin.post("/StudyPersonalReference", references, {
            headers: authHeader()
        })
    }

    update = (reference) => {
        return SocialAdmin.put("/StudyPersonalReference", reference, {
            headers: authHeader()
        })
    }

    delete = (id) => {
        return SocialAdmin.delete("/StudyPersonalReference", {
            params: {
                id
            },
            headers: authHeader()
        })
    }
}

export default new PersonalReferenceService()