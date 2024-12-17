import SocialAdmin from "./SocialAdmin";
import authHeader from "./AuthHeader";

class SocialGoalsService {
    create = (goal) => {
        return SocialAdmin.post("/StudySocial/SocialGoals", goal, {
            headers: authHeader()
        })
    }

    update = (goal) => {
        return SocialAdmin.put("/StudySocial/SocialGoals", goal, {
            headers: authHeader()
        })
    }

    delete = (id) => {
        return SocialAdmin.delete("/StudySocial/SocialGoals", {
            params: {
                id: id
            },
            headers: authHeader()
        })
    }
}

export default new SocialGoalsService()