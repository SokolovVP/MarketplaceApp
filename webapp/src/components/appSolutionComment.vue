<template>
  <div class="commentContainer">
    <div class="commentText">{{ _comment.text }}</div>
    <div class="commentUserName">- {{ user.userName }}</div>
  </div>
</template>

<script>
import userCRUD from "../vuex/userCRUD";
export default {
  name: "appSolutionComment",
  props: ["_comment"],
  data() {
    return {
      user: Object,
    };
  },
  methods: {
    retrieveUser(userId) {
      userCRUD
        .get(userId)
        .then((response) => {
          this.user = response.data;
        })
        .catch((error) => {
          console.log(error);
        });
    },
    refreshUser() {
      this.retrieveUser(this._comment.userId);
    },
  },
  mounted() {
    this.retrieveUser(this._comment.userId);
  },
};
</script>

<style>
.commentContainer {
  list-style-type: none;
  border: 1px solid;
  border-radius: 5px;
  height: 69px;
  width: 340px;
  /* align-items: center;
  justify-content: center; */
  /* margin-right: 30px !important; */
  border-color: rgb(175, 194, 238);
  /* display: none; */
}

.commentText {
  margin-top: 15px;
  font-size: 15px;
}

.commentUserName {
  margin-top: 7px;
  margin-left: 200px;
  font-size: 13px;
}
</style>
