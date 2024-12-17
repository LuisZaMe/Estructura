import SocialAdmin from "./SocialAdmin";
import authHeader from "./AuthHeader";

class VehicleService {
    create = (vehicle) => {
        return SocialAdmin.post("/StudyEconomicSituation/Vehicle", vehicle, {
            headers: authHeader()
        })
    }

    update = (vehicle) => {
        return SocialAdmin.put("/StudyEconomicSituation/Vehicle", vehicle, {
            headers: authHeader()
        })
    }

    delete = (id) => {
        return SocialAdmin.delete("/StudyEconomicSituation/Vehicle", {
            params: {
                id: id
            },
            headers: authHeader()
        })
    }
}

export default new VehicleService()