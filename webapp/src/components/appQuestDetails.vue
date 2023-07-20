<template>
  <div class="questDetailsContainer">
    <div class="questDetails">
      <div class="appText text-primary" id="questTitle">Quest:</div>
      <div class="isSolution" v-show="questDetails.isSolved">SOLVED</div>
      <div class="detailText">{{ questDetails.title }}</div>
      <div class="appText text-primary">Description:</div>
      <div class="detailText">{{ questDetails.description }}</div>
      <div class="appText text-primary">Published on:</div>
      <div class="detailText">
        {{ new Date(questDetails.publishingDate).toLocaleDateString() }}
      </div>
      <div class="appText text-primary">Available until:</div>
      <div class="detailText">
        {{ new Date(questDetails.endDate).toLocaleDateString() }}
      </div>
      <div class="appText text-primary">Published by:</div>
      <div class="detailText">{{ userDetails.userName }}</div>
    </div>
    <div class="appText text-primary">Solutions:</div>
    <ul>
      <li
        v-for="solution in questSolutions"
        :key="solution.solutionId"
        @click="solutionClicked(solution.solutionId)"
        id="solutionElement"
        v-bind:class="{ isSolution: solution.isSelected }"
        v-show="isAdmin || solution.userId === curUserId"
      >
        {{ solution.description }}
      </li>
    </ul>
    <p v-show="!questSolutions.length" class="text-danger">
      There are no solutions yet
    </p>
    <button
      class="btn"
      id="createSolutionBtn"
      @click="createSolutionClicked"
      v-show="
        questDetails.isAvailable &&
        new Date(questDetails.endDate) >= new Date() &&
        !questDetails.isSolved
      "
    >
      Add a solution
    </button>
    <appBackBtn />
  </div>
</template>

<script>
import router from "@/router/router";
import questCRUD from "../vuex/questCRUD";
import solutionCRUD from "../vuex/solutionCRUD";
import appBackBtn from "./appBackBtn.vue";
// import axios from "axios";

export default {
  name: "appQuestDetails",
  data() {
    return {
      questDetails: Object,
      userDetails: Object,
      questSolutions: [],
      isAdmin:
        JSON.parse(localStorage.getItem("user")).user.category === "Admin",
      curUserId: JSON.parse(localStorage.getItem("user")).user.userId,
    };
  },
  components: {
    appBackBtn,
  },
  methods: {
    retrieveQuest(id) {
      questCRUD
        .get(id)
        .then((response) => {
          this.questDetails = response.data;
          this.userDetails = response.data.user;
          solutionCRUD
            .getBy(id)
            .then((solutionResponse) => {
              this.questSolutions = solutionResponse.data;
            })
            .catch((error) => {
              console.log(error);
            });
        })
        .catch((error) => {
          console.log(error);
        });
    },
    solutionClicked(id) {
      this.$router.push({ name: "solution", query: { solution: id } });
      // alert(`solution ${id} selected!`);
    },
    questDetailsBackBtnClick() {
      router.go(-1);
    },
    refreshQuest() {
      this.retrieveQuest(this.$route.query.quest);
    },
    createSolutionClicked() {
      this.$router.push({
        name: "createSolution",
        query: { questId: this.questDetails.questId },
      });
    },
  },
  mounted() {
    this.retrieveQuest(this.$route.query.quest);
  },
};
</script>

<style>
.questDetailsContainer {
  display: flex;
  flex-direction: column;
  margin-left: 145px;
  margin-right: 145px;
  flex-wrap: wrap;
  justify-content: center;
  align-items: center;
}

.appText {
  margin-top: 7px;
  margin-bottom: 7px;
}

.detailText {
  align-items: start;
  overflow-wrap: break-word;
  width: 500px;
  justify-content: space-between;
}

#detailSolutions {
  align-items: center !important;
  justify-content: space-between !important;
}

#solutionElement:hover {
  cursor: pointer;
  text-decoration: underline;
}

#createSolutionBtn:hover {
  border: 1px solid;
}

#createSolutionBtn {
  border: 1px solid;
}

#questTitle {
  margin-top: 0px !important;
}

.isSolution {
  color: rgb(13, 195, 13) !important;
}
</style>
