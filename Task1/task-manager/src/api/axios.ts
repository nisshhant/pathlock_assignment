import axios from "axios";

export default axios.create({
  baseURL: "http://localhost:5114/api", // your backend port
});
