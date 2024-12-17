import SocialAdmin from "./SocialAdmin";
import authHeader from "./AuthHeader";

class VisitService {
    create = (visit) => {
        return SocialAdmin.post("/Visit", visit, {
            headers: authHeader()
        })
    }

    update = (visit) => {
        return SocialAdmin.put("/Visit", visit, {
            headers: authHeader()
        })
    }

    delete = (id) => {
        return SocialAdmin.delete("/Visit", {
            params: {
                id: id.visit
            },
            headers: authHeader()
        })
    }

    getVisit = (id) => {
        return SocialAdmin.get("/Visit", {
            params:  id!=='' ? {
                id,
                currentPage: 0,
                offset: 1
            } : 
            {
                currentPage: 0,
                offset: 9
            },
            headers: authHeader()
        })
    }

    getVisits = (search, page) => {
        return SocialAdmin.get("/Visit/Search", {
            params: {
                key: search,
                currentPage: page,
                offset: 9
            },
            headers: authHeader()
        })
    }

    getPages = () => {
        return SocialAdmin.get("/Visit/Pagination", {
            params: {
                splitBy: 9
            },
            headers: authHeader()
        })
    }
}

export default new VisitService()