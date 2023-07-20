import http from "../http-common";
import authHeader from "../services/auth-header";

class solutionCRUD {
  getAll() {
    return http.get("/Solutions", { headers: authHeader() });
  }

  getBy(questId) {
    return http.get(`/Solutions/Solutions/questId=${questId}`, {
      headers: authHeader(),
    });
  }

  get(id) {
    return http.get(`/Solutions/${id}`, { headers: authHeader() });
  }
  create(data) {
    return http.post("/Solutions", data, { headers: authHeader() });
  }
  update(id, data) {
    return http.put(`/Solutions/${id}`, data, { headers: authHeader() });
  }
  delete(id) {
    return http.delete(`/Solutions/${id}`, { headers: authHeader() });
  }
  deleteAll() {
    return http.delete("/Solutions", { headers: authHeader() });
  }
}

export default new solutionCRUD();
