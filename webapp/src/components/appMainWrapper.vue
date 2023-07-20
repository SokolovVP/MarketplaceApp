<template>
  <div class="appMainWrapper">
    <div class="container">
      <header
        class="d-flex flex-wrap align-items-center justify-content-center justify-content-md-between py-3 mb-4 border-bottom"
      >
        <a
          href="/"
          class="header_logo d-flex align-items-center col-md-3 mb-2 mb-md-0 text-dark text-decoration-none"
          @click="this.$router.push({ name: 'authorization' })"
        >
          Marketplace
        </a>

        <ul class="nav col-12 col-md-auto mb-2 justify-content-center mb-md-0">
          <router-link
            :to="{ name: 'createQuest', params: {} }"
            class="createQuestLink"
          >
            <li>
              <a class="nav-link px-2 link-dark">Создать задание</a>
            </li>
          </router-link>
        </ul>

        <div class="col-md-3 text-end">
          <button type="button" class="btn btn-primary" @click="logOutClicked">
            Log out
          </button>
        </div>
      </header>
    </div>
    <router-view></router-view>
  </div>
</template>

<script>
export default {
  name: "appMainWrapper",
  props: {},
  data() {
    return {
      title: "main wrapper",
    };
  },
  methods: {
    logOutClicked() {
      this.$store.dispatch("auth/logout");
      this.$router.push({ name: "authorization" });
    },
  },
  computed: {},
  mounted() {},
};
</script>

<style>
.header_logo {
  font-size: 22px;
}
.createQuestLink {
  text-decoration: none;
}
</style>
