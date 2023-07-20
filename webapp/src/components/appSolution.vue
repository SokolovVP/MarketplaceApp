<template>
  <div class="solutionDetailsContainer">
    <div class="solutionDetails">
      <div class="appText text-primary solutionHeader">
        {{ user.userName }}'s solution, rating: {{ solution.rating }}
        <div id="notRated" v-show="!solution.rating">not rated yet</div>
        <div
          class="text-success"
          id="markedAsSolution"
          v-show="solution.isSelected"
        >
          Marked as solution!
        </div>
      </div>
      <div class="appText text-primary">Description:</div>
      <div class="detailText">{{ solution.description }}</div>
      <a
        class="sourceLink text-primary"
        id="sourceLink"
        v-bind:href="solution.sourceLink"
        :title="solution.sourceLink"
        >Source link (click it)</a
      >
      <div class="appText text-primary">Leave a comment:</div>
      <textarea
        class="form-control"
        id="commentForm"
        type="text"
        placeholder="Comment"
        v-model="this.commentText"
      />
      <div class="emptyFieldWarning" v-if="this.commentFieldEmpty">
        Comment field should not be empty!
      </div>
      <button
        class="btn btn-primary"
        id="commentSaveBtn"
        @click="submitCommentClick"
      >
        Submit
      </button>
    </div>
  </div>
  <div class="solutionCommentsList">
    <appSolutionComment
      v-for="comment in solution.comments"
      v-bind:_comment="comment"
      :key="comment.commentId"
    ></appSolutionComment>
  </div>
  <button
    class="btn btn-success"
    id="solutionMarkBtn"
    v-show="!solution.isSelected && isAdmin"
    @click="solutionMarkedClick"
  >
    Mark as solution
  </button>
  <button
    class="btn btn-success"
    id="solutionUnmarkBtn"
    v-show="solution.isSelected && isAdmin"
    @click="solutionUnmarkedClick"
  >
    Unmark solution
  </button>
  <div class="ratingsDropdown" v-show="isAdmin">
    <button class="btn ratingsDropdownBtn" @click="dropdownBtnClick">
      Rate this solution
    </button>
    <div class="ratingsDropdownMenu" v-if="showDropdown">
      <div
        v-for="rating in ratings"
        :key="rating"
        class="ratingsDropdownItem"
        @click="ratingClicked(rating)"
      >
        {{ rating }}
      </div>
    </div>
  </div>
  <appBackBtn />
</template>

<script>
import { ref } from "vue";
import solutionCRUD from "../vuex/solutionCRUD";
import userCRUD from "../vuex/userCRUD";
import appBackBtn from "./appBackBtn.vue";
import appSolutionComment from "./appSolutionComment.vue";
import commentCRUD from "@/vuex/commentCRUD";
// import questCRUD from "@/vuex/questCRUD";

export default {
  name: "appSolution",
  data() {
    return {
      solution: Object,
      user: Object,
      commentFieldEmpty: false,
      commentText: ref(""),
      userId: ref(null),
      showDropdown: false,
      ratings: [1, 2, 3, 4, 5],
      isAdmin:
        JSON.parse(localStorage.getItem("user")).user.category === "Admin",
    };
  },
  components: {
    appBackBtn,
    appSolutionComment,
  },
  methods: {
    retrieveSolution(id) {
      solutionCRUD
        .get(id)
        .then((solutionGetResponse) => {
          this.solution = solutionGetResponse.data;
          userCRUD
            .get(solutionGetResponse.data.userId)
            .then((userGetResponse) => {
              this.user = userGetResponse.data;
            });
        })
        .catch((error) => {
          console.log(error);
          return error;
        });
    },
    refreshSolution() {
      this.retrieveSolution(this.$route.query.solution);
    },
    dropdownBtnClick() {
      this.showDropdown = !this.showDropdown;
    },
    ratingClicked(_rating) {
      var updateData = {
        solutionId: this.solution.solutionId,
        userId: this.solution.userId,
        questId: this.solution.questId,
        description: this.solution.description,
        sourceLink: this.solution.sourceLink.toString(),
        rating: _rating,
        isSelected: this.solution.isSelected,
        comments: this.solution.comments,
      };
      solutionCRUD
        .update(this.solution.solutionId, updateData)
        .then((response) => {
          console.log(response);
          this.refreshSolution();
        })
        .catch((error) => {
          console.log(error);
        });
      alert("rated!");
    },
    submitCommentClick() {
      if (!this.commentText.length) {
        this.commentFieldEmpty = true;
        setTimeout(() => {
          this.commentFieldEmpty = false;
        }, 2000);
      } else {
        var commentData = {
          text: this.commentText,
          //userId: this.userId ?? 1,
          userId: JSON.parse(localStorage.getItem("user")).user.userId,
          solutionId: this.solution.solutionId,
        };
      }
      commentCRUD
        .create(commentData)
        .then((response) => {
          console.log(response);
          this.refreshSolution();
        })
        .catch((error) => {
          console.log(error);
          return error;
        });
    },
    solutionMarkedClick() {
      var _solution = this.solution;
      _solution.isSelected = true;
      solutionCRUD
        .update(_solution.solutionId, _solution)
        .then((response) => {
          console.log(response);
        })
        .catch((error) => {
          console.log(error);
        });
    },
    solutionUnmarkedClick() {
      var _solution = this.solution;
      _solution.isSelected = false;
      solutionCRUD
        .update(_solution.solutionId, _solution)
        .then((response) => {
          console.log(response);
        })
        .catch((error) => {
          console.log(error);
        });
    },
  },
  mounted() {
    this.retrieveSolution(this.$route.query.solution);
  },
};
</script>

<style>
.solutionDetailsContainer {
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

.sourceLink {
  margin-top: 14px !important;
}

#commentForm {
  height: 120px !important;
}

#commentSaveBtn {
  margin-top: 15px;
}

.solutionCommentsList {
  position: absolute !important;
  top: 174px !important;
  left: 1050px !important;
  border: 1px solid;
  width: 387px;
  height: 504px;
  border-radius: 5px;
  border-color: rgb(175, 194, 238);
  justify-content: start;
  align-items: center;
  justify-content: center;
  overflow: auto;
  margin-right: 50px;
  /* flex-direction: column; */
}

.emptyFieldWarning {
  font-size: 13px;
  margin-top: 3px;
  color: rgba(255, 0, 0, 0.571);
}

.ratingsDropdown {
  margin-top: 15px;
  display: flex;
  flex-direction: column;
  align-items: center;
}

.ratingsDropdownBtn {
  border: 1px solid !important;
  width: 149px;
}

.ratingsDropdownBtn:hover {
  /* border: 1px solid; */
  color: lightslategrey;
}

.ratingsDropdownMenu {
  border: 1px solid;
  border-radius: 5px;
  width: 149px;
}

.ratingsDropdownItem {
  border: 1px solid;
  /* border-radius: 5px; */
}

.ratingsDropdownItem:hover {
  background-color: rgb(233, 241, 249);
  cursor: pointer;
}

#notRated {
  color: red;
  font-size: 13px;
  font-weight: lighter !important;
}

#solutionMarkBtn {
  position: absolute !important;
  top: 351px;
  left: 300px;
}

#solutionUnmarkBtn {
  position: absolute !important;
  top: 351px;
  left: 300px;
}
</style>
