import SocialAdmin from "./SocialAdmin"
import authHeader from "./AuthHeader";

class RecommendationLetterService {
    create = (letters) => {
        return SocialAdmin.post("/StudyGeneralInformation/RecommendationCard", letters, {
            headers: authHeader()
        })
    }

    update = (letter) => {
        return SocialAdmin.put("/StudyGeneralInformation/RecommendationCard", letter, {
            headers: authHeader()
        })
    }

    delete = (id) => {
        return SocialAdmin.delete("/StudyGeneralInformation/RecommendationCard", {
            params: {
                id: id
            },
            headers: authHeader()
        })
    }
}

export default new RecommendationLetterService()