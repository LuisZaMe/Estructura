import SocialAdmin from "./SocialAdmin";
import authHeader from "./AuthHeader";

class LocationService {
    getStates = () => {
        return SocialAdmin.get("/Utilities/GetStates", {
            headers: authHeader()
        })
    }

    getCities = (id) => {
        return SocialAdmin.get("/Utilities/GetCities", {
            params: {
                stateId: id
            },
            headers: authHeader()
        })
    }
}

export default new LocationService()