import axios from "axios";

export default axios.create({
  //baseURL: "https://localhost:7122",
  baseURL: process.env.VUE_APP_API_URL,
  headers: {
    "Content-Type": "application/json",
  },
});
