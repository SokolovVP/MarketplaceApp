import { createApp } from "vue";
import App from "./App.vue";
import "bootstrap/dist/css/bootstrap.min.css";
import store from "./store/index";
import Vuex from "vuex";
import router from "./router/router";
import appAuthorization from "./components/appAuthorization.vue";

//import appAuthorization from "./components/appAuthorization.vue";
import vue3GoogleLogin from "vue3-google-login";

let gauthClientId =
  "22574001-o5crnt8tirol6mhsvappq3ncddrinn5n.apps.googleusercontent.com";
console.log(gauthClientId);

const app = createApp(App);
// const app = createApp(appAuthorization);

app.use(vue3GoogleLogin, {
  clientId: gauthClientId,
});
app.use(appAuthorization);
app.use(Vuex);
app.use(store);
app.use(router);
app.mount("#app");
