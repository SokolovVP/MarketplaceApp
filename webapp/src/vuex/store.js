// import Vuex from "vuex";
// import axios from "axios";

// let store = new Vuex.Store({
//   state: {
//     tasks: [],
//   },
//   mutations: {
//     SET_TASKS_TO_STATE: (state, tasks) => {
//       state.tasks = tasks;
//     },
//   },
//   actions: {
//     GET_TASKS_FROM_API({ commit }) {
//       return axios("https://localhost:7122/Quests", {
//         method: "GET",
//       })
//         .then((tasks) => {
//           commit("SET_TASKS_TO_STATE", tasks.data);
//           console.log(tasks.data);
//           console.log(tasks);
//           return tasks;
//         })
//         .catch((error) => {
//           console.log(error);
//           return error;
//         });
//     },
//   },
//   getters: {
//     TASKS(state) {
//       return state.tasks;
//     },
//   },
// });

// export default store;
