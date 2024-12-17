import SocialAdmin from "./SocialAdmin"
import authHeader from "./AuthHeader";

class AccountService {
    create = (account) => {
        return SocialAdmin.post("/Account", account, {
            headers: authHeader()
        })
    }

    update = (account) => {
        return SocialAdmin.put("/Account", account, {
            headers: authHeader()
        })
    }

    delete = (id) => {
        return SocialAdmin.delete("/Account", {
            params: {
                id: id
            },
            headers: authHeader()
        })
    }

    getAccount = (id) => {
        return SocialAdmin.get("/Account", {
            params: {
                id: id,
                currentPage: 0,
                offset: 1
            },
            headers: authHeader()
        })
    }

    getAccounts = (role, search, page, underAdminUserId, showSuperAdmin = false,) => {
        return SocialAdmin.get("/Account/Search", {
            params: {
                key: search,
                role: role,
                underAdminUserId: underAdminUserId,
                currentPage: page,
                offset: 9,
                showSuperAdmin,
            },
            headers: authHeader()
        })
    }

    getPages = (role, search, showSuperAdmin = false, ) => {
        return SocialAdmin.get("/Account/Pagination", {
            params: {
                key: search,
                role: role,
                splitBy: 9,
                showSuperAdmin,
            },
            headers: authHeader()
        })
    }

    completeRegistration = (password, token) => {
        return SocialAdmin.put("/Account/CompleteRegistration", {
            password: password,
            token: token
        })
    }
    sendRecoverPassword = (email) => {
        try {
            return SocialAdmin.post("/Account/SendRecoverPasswordMail", {
                email
            })
        } catch (error) {
            return error.response.status
        }
    }
    resetPassword = (password, token) => {
        return SocialAdmin.put("/Account/ChangePassword", {
            password,
            token
        })
    }
}

export default new AccountService()