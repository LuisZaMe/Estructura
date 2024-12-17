import SocialAdmin from "./SocialAdmin";
import authHeader from "./AuthHeader";

class EconomicSituationService {
    create = (economicSituation) => {
        return SocialAdmin.post("/StudyEconomicSituation", economicSituation, {
            headers: authHeader()
        })
    }

    update = (economicSituation) => {
        return SocialAdmin.put("/StudyEconomicSituation", economicSituation, {
            headers: authHeader()
        })
    }
}

export default new EconomicSituationService()