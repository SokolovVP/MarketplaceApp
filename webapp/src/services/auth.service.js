import axios from "axios";

//const API_URL = "https://localhost:7122";
const API_URL = process.env.VUE_APP_API_URL;

class AuthService {
  login(user) {
    return axios
      .post(API_URL + "/login", {
        email: user.email,
        password: user.password,
      })
      .then((response) => {
        if (response.data.access_token) {
          localStorage.setItem("user", JSON.stringify(response.data));
        }
        return response.data;
      });
  }
  logout() {
    localStorage.removeItem("user");
  }
}

export default new AuthService();
