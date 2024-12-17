import SocialAdmin from "./SocialAdmin";
import authHeader from "./AuthHeader";

class IncomeService {
    create = (income) => {
        return SocialAdmin.post("/StudyEconomicSituation/Incoming", income, {
            headers: authHeader()
        })
    }

    update = (income) => {
        return SocialAdmin.put("/StudyEconomicSituation/Incoming", income, {
            headers: authHeader()
        })
    }

    delete = (id) => {
        return SocialAdmin.delete("/StudyEconomicSituation/Incoming", {
            params: {
                id: id
            },
            headers: authHeader()
        })
    }
}

export default new IncomeService()