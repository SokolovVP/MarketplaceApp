// import http from "../../src/http-common";

// class questCRUD {
//   getAll() {
//     return http.get("/Quests");
//   }
//   get(id) {
//     return http.get(`/Quests/${id}`);
//   }
//   create(data) {
//     return http.post("/Quests", data);
//   }
//   update(id, data) {
//     return http.put(`/Quests/${id}`, data);
//   }
//   delete(id) {
//     return http.delete(`/Quests/${id}`);
//   }
//   deleteAll() {
//     return http.delete("/Quests");
//   }
// }

// export default new questCRUD();

import http from "../http-common";
import authHeader from "../services/auth-header";

class questCRUD {
  getAll() {
    return http.get("/Quests", { headers: authHeader() });
  }
  get(id) {
    return http.get(`/Quests/${id}`, { headers: authHeader() });
  }
  create(data) {
    return http.post("/Quests", data, { headers: authHeader() });
  }
  update(id, data) {
    return http.put(`/Quests/${id}`, data, { headers: authHeader() });
  }
  delete(id) {
    return http.delete(`/Quests/${id}`, { headers: authHeader() });
  }
  deleteAll() {
    return http.delete("/Quests", { headers: authHeader() });
  }
}

export default new questCRUD();
