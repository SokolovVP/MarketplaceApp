import appTasksCatalog from "../components/appTasksCatalog.vue";
import appQuestDetails from "../components/appQuestDetails.vue";
import appCreateQuest from "../components/appCreateQuest.vue";
import appSolution from "../components/appSolution.vue";
import appCreateSolution from "../components/appCreateSolution.vue";
import appAuthorization from "../components/appAuthorization.vue";
import { createRouter, createWebHashHistory } from "vue-router";

const router = createRouter({
  history: createWebHashHistory(),
  routes: [
    {
      path: "/",
      name: "catalog",
      component: appTasksCatalog,
    },
    {
      path: "/login",
      name: "authorization",
      component: appAuthorization,
    },
    // {
    //   path: "/catalog",
    //   name: "catalog",
    //   component: appTasksCatalog,
    // },
    {
      path: "/quest",
      name: "quest",
      component: appQuestDetails,
      props: true,
    },
    {
      path: "/createQuest",
      name: "createQuest",
      component: appCreateQuest,
    },
    {
      path: "/solution",
      name: "solution",
      component: appSolution,
      props: true,
    },
    {
      path: "/createSolution",
      name: "createSolution",
      component: appCreateSolution,
      props: true,
    },
  ],
});

export default router;
