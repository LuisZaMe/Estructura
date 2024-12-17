import SocialAdmin from "./SocialAdmin";

class AuthService {
    login = async (email, password) => {
        try {
            const { data } = await SocialAdmin.post("/Auth/Login", {
                email: btoa(email),
                password: btoa(password),
                appBuildVersion: 1
            })
            localStorage.setItem("user", JSON.stringify(data))
            return data.statusCode
        } catch (error) {
            return error.response.status
        }
    }

    logout = () => {
        localStorage.removeItem("user")
    }

    getUser = () => {
        return JSON.parse(localStorage.getItem("user"))
    }

    getIdentity = () => {
        return JSON.parse(localStorage.getItem("user")).identity;
    }
}

export default new AuthService()
