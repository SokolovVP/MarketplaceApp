import http from "../http-common";

class authorizationCRUD {
  getToken(data) {
    return http.post("/login", data);
  }
}

export default new authorizationCRUD();
