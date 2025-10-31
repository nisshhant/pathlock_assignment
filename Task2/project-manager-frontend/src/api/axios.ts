import axios from "axios";

const api = axios.create({
  baseURL: "http://localhost:5057/api", // change port if different
});

// Add JWT token automatically if present
api.interceptors.request.use((config) => {
  const token = localStorage.getItem("token");
  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
  }
  return config;
});

export default api;
