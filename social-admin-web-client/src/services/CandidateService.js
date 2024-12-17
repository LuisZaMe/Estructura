import SocialAdmin from "./SocialAdmin";
import authHeader from "./AuthHeader";

class CandidateService {
    create = (candidate) => {
        return SocialAdmin.post("/Candidate", candidate, {
            headers: authHeader()
        })
    }

    update = (candidate) => {
        return SocialAdmin.put("/Candidate", candidate, {
            headers: authHeader()
        })
    }

    delete = (id) => {
        return SocialAdmin.delete("/Candidate", {
            params: {
                id: id
            },
            headers: authHeader()
        })
    }

    getCandidate = (id) => {
        return SocialAdmin.get("/Candidate", {
            params: {
                id: id,
                currentPage: 0,
                offset: 1
            },
            headers: authHeader()
        })
    }

    getCandidates = (search, page) => {
        return SocialAdmin.get("/Candidate/Search", {
            params: {
                key: search,
                currentPage: page,
                offset: 10
            },
            headers: authHeader()
        })
    }

    getPages = () => {
        return SocialAdmin.get("/Candidate/Pagination", {
            params: {
                splitBy: 9
            },
            headers: authHeader()
        })
    }

    createNote = (note) => {
        return SocialAdmin.post("/Candidate/Note", note, {
            headers: authHeader()
        })
    }
}

export default new CandidateService()