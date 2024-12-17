import SocialAdmin from "./SocialAdmin";
import authHeader from "./AuthHeader";

class EstateService {
    create = (estate) => {
        return SocialAdmin.post("/StudyEconomicSituation/Estate", estate, {
            headers: authHeader()
        })
    }

    update = (estate) => {
        return SocialAdmin.put("/StudyEconomicSituation/Estate", estate, {
            headers: authHeader()
        })
    }

    delete = (id) => {
        return SocialAdmin.delete("/StudyEconomicSituation/Estate", {
            params: {
                id: id
            },
            headers: authHeader()
        })
    }
}

export default new EstateService()