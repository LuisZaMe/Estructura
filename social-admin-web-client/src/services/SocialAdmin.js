import axios from "axios";

const isDev =  true;
export default axios.create({
    baseURL: isDev ? "https://localhost:50763/api" : "http://34.68.54.62/api",
})