import SocialAdmin from "./SocialAdmin";
import authHeader from "./AuthHeader";

class CreditService {
    create = (credit) => {
        return SocialAdmin.post("/StudyEconomicSituation/Credit", credit, {
            headers: authHeader()
        })
    }

    update = (credit) => {
        return SocialAdmin.put("/StudyEconomicSituation/Credit", credit, {
            headers: authHeader()
        })
    }

    delete = (id) => {
        return SocialAdmin.delete("/StudyEconomicSituation/Credit", {
            params: {
                id: id
            },
            headers: authHeader()
        })
    }
}

export default new CreditService()