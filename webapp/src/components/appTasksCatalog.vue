<template>
  <div class="appTasksCatalog">
    <p style="justify-content: start">Catalog</p>
    <div class="appTasksCatalogList">
      <appCardItem
        v-for="_task in TASKS"
        :key="_task.id"
        v-bind:card_data="_task"
        @cardDetailsClick="cardDetailsClick(_task.questId)"
      ></appCardItem>
    </div>
  </div>
</template>

<!-- <script>
import appCardItem from "./appCardItem.vue";
import { mapActions, mapGetters } from "vuex";
export default {
  name: "appTasksCatalog",
  components: {
    appCardItem,
  },
  props: {},
  data() {
    return {};
  },
  computed: {
    ...mapGetters(["TASKS"]),
  },
  methods: {
    ...mapActions(["GET_TASKS_FROM_API"]),
    cardDetailsClick(questId) {
      // alert(questId);
      this.$router.push({ name: "quest", query: { quest: questId } });
    },
  },
  mounted() {
    this.GET_TASKS_FROM_API().then((response) => {
      if (response.data) {
        console.log("data arrived!");
      }
    });
  },
};
</script> -->

<script>
import appCardItem from "./appCardItem.vue";
import questCRUD from "../vuex/questCRUD";

export default {
  name: "appTasksCatalog",
  components: {
    appCardItem,
  },
  data() {
    return {
      TASKS: [],
    };
  },
  methods: {
    retrieveQuests() {
      questCRUD
        .getAll()
        .then((response) => {
          this.TASKS = response.data;
        })
        .catch((error) => {
          console.log(error);
        });
    },
    refreshQuests() {
      this.retrieveQuests();
    },
    cardDetailsClick(questId) {
      this.$router.push({ name: "quest", query: { quest: questId } });
    },
  },
  mounted() {
    if (!this.$store.state.auth.user) {
      this.$router.push({ name: "authorization" });
    }
    this.retrieveQuests();
    if (!this.$store.state.auth) {
      this.$router.push({ name: "authorization" });
    }
  },
};
</script>

<style>
.appTasksCatalog {
  display: flex;
  flex-wrap: wrap;
  align-items: center;
  flex-direction: column;

  margin-left: 145px;
  margin-right: 145px;
}

.appTasksCatalogList {
  display: flex;
  flex-wrap: wrap;
  justify-content: center;
  align-items: center;
  max-width: inherit;
}
</style>
