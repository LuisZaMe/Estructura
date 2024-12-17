import SocialAdmin from "./SocialAdmin";
import authHeader from "./AuthHeader";

class IMSSValidationService {
    create = (validation) => {
        return SocialAdmin.post("/StudyIMSSValidation", validation, {
            headers: authHeader()
        })
    }

    update = (validation) => {
        return SocialAdmin.put("/StudyIMSSValidation", validation, {
            headers: authHeader()
        })
    }

    // Validation
    createValidation = (validations) => {
        return SocialAdmin.post("/StudyIMSSValidation/IMSSValidation", validations, {
            headers: authHeader()
        })
    }

    updateValidation = (validation) => {
        return SocialAdmin.put("/StudyIMSSValidation/IMSSValidation", validation, {
            headers: authHeader()
        })
    }

    deleteValidation = (id) => {
        return SocialAdmin.delete("/StudyIMSSValidation/IMSSValidation", {
            params: {
                id: id
            },
            headers: authHeader()
        })
    }
}

export default new IMSSValidationService()