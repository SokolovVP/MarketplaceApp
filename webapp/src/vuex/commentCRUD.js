import http from "../http-common";
import authHeader from "../services/auth-header";

class commentCRUD {
  getAll() {
    return http.get("/Comments", { headers: authHeader() });
  }
  get(id) {
    return http.get(`/Comments/${id}`, { headers: authHeader() });
  }
  create(data) {
    return http.post("/Comments", data, { headers: authHeader() });
  }
  update(id, data) {
    return http.put(`/Comments/${id}`, data, { headers: authHeader() });
  }
  delete(id) {
    return http.delete(`/Comments/${id}`, { headers: authHeader() });
  }
  deleteAll() {
    return http.delete("/Comments", { headers: authHeader() });
  }
}

export default new commentCRUD();
