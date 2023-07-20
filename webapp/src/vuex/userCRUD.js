import http from "../../src/http-common";

class userCRUD {
  getAll() {
    http.get("/Users");
  }

  get(id) {
    return http.get(`/Users/${id}`);
  }
  create(data) {
    return http.post("/Users", data);
  }
  update(id, data) {
    return http.put(`/Users/${id}`, data);
  }
  delete(id) {
    return http.delete(`/Users/${id}`);
  }
  deleteAll() {
    return http.delete("/Users");
  }
}

export default new userCRUD();
