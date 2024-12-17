import SocialAdmin from "./SocialAdmin";
import authHeader from "./AuthHeader";

class AdditionalIncomeService {
    create = (income) => {
        return SocialAdmin.post("/StudyEconomicSituation/AdditionalIncoming", income, {
            headers: authHeader()
        })
    }

    update = (income) => {
        return SocialAdmin.put("/StudyEconomicSituation/AdditionalIncoming", income, {
            headers: authHeader()
        })
    }

    delete = (id) => {
        return SocialAdmin.delete("/StudyEconomicSituation/AdditionalIncoming", {
            params: {
                id: id
            },
            headers: authHeader()
        })
    }
}

export default new AdditionalIncomeService()