<template>
  <button class="btn btn-primary" @click="backBtnClicked" id="backBtn">
    Back
  </button>
</template>

<script>
import router from "../router/router";
export default {
  name: "appBackBtn",
  methods: {
    backBtnClicked() {
      router.go(-1);
    },
  },
};
</script>

<style>
#backBtn {
  width: fit-content !important;
  margin-top: 10px;
}
#backBtn:hover {
  border: 1px solid;
}
</style>
