import SocialAdmin from "./SocialAdmin";
import authHeader from "./AuthHeader";

class StudyPicturesService {
    create = (pictures) => {
        return SocialAdmin.post("/StudyPictures", pictures, {
            headers: authHeader()
        })
    }

    update = (pictures) => {
        return SocialAdmin.put("/StudyPictures", pictures, {
            headers: authHeader()
        })
    }
}

export default new StudyPicturesService()