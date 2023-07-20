<template>
  <div class="createSolutionContainer">
    <div class="createSolutionDetails">
      <div class="appText">Leave a link to your solution (required)</div>
      <div class="requiredFieldElement">
        <input
          class="form-control solutionDetailsText"
          placeholder="Link"
          v-model="solutionData.sourceLink"
          id="addSourceLink"
        />
        <div class="emptyFieldWarning" v-if="solutionData.linkEmpty">
          Need a source link!
        </div>
      </div>
      <div class="requiredFieldElement">
        <div class="appText">Leave a description (required)</div>
        <textarea
          class="solutionDetailsText form-control"
          id="createSolutionDescription"
          placeholder="Description"
          v-model="solutionData.description"
        ></textarea>
        <div class="emptyFieldWarning" v-if="solutionData.descriptionEmpty">
          Need a description!
        </div>
      </div>
    </div>
    <button
      class="btn btn-primary"
      @click="saveSolutionClick"
      id="submitSolutionBtn"
    >
      Submit
    </button>
  </div>

  <appBackBtn />
</template>

<script>
import solutionCRUD from "@/vuex/solutionCRUD";
import appBackBtn from "./appBackBtn.vue";
import { ref } from "vue";

export default {
  name: "createSolution",
  components: {
    appBackBtn,
  },
  data() {
    return {
      solutionData: ref({
        userId: ref(null),
        description: ref(null),
        sourceLink: ref(null),
        linkEmpty: false,
        descriptionEmpty: false,
      }),
    };
  },
  methods: {
    saveSolutionClick() {
      if (
        this.solutionData.sourceLink === null ||
        !this.solutionData.sourceLink.length ||
        this.solutionData.description === null ||
        !this.solutionData.description.length
      ) {
        if (
          this.solutionData.sourceLink === null ||
          !this.solutionData.sourceLink.length
        ) {
          this.solutionData.linkEmpty = true;
          setTimeout(() => {
            this.solutionData.linkEmpty = false;
          }, 2000);
        }
        if (
          this.solutionData.description === null ||
          !this.solutionData.description.length
        ) {
          this.solutionData.descriptionEmpty = true;
          setTimeout(() => {
            this.solutionData.descriptionEmpty = false;
          }, 2000);
        }
      } else {
        var newSolutionData = {
          userId: JSON.parse(localStorage.getItem("user")).user.userId,
          questId: this.$route.query.questId,
          description: this.solutionData.description,
          sourceLink: this.solutionData.sourceLink,
          rating: null,
          isSelected: false,
        };
        solutionCRUD
          .create(newSolutionData)
          .then((response) => {
            console.log(response);
            alert("solution created!");
          })
          .catch((error) => {
            console.log(error);
            return error;
          });
      }
    },
  },
};
</script>

<style>
.createSolutionContainer {
  display: flex;
  flex-direction: column;
  align-items: center;
}

.createSolutionDetails {
  justify-content: space-between;
  align-items: center !important;

  color: red !;
}

#addSourceLink {
  width: 500px !important;
}

#createSolutionDescription {
  height: 180px !important;
}

.appText {
  margin-top: 7px !important;
  margin-bottom: 7px !important;
}

#submitSolutionBtn {
  margin-top: 13px;
}

.emptyFieldWarning {
  font-size: 13px;
  margin-top: 3px;
  color: rgba(255, 0, 0, 0.571);
}
</style>
