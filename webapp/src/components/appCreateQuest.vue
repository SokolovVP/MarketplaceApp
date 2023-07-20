<template>
  <div class="createQuestContainer">
    <p>Create a quest</p>
    <div class="mb-3">
      <input
        class="form-control"
        id="exampleFormControlInput1"
        placeholder="Quest's title"
        v-model="questData.title"
        v-show="isAdmin"
      />
    </div>
    <div class="mb-3">
      <textarea
        class="form-control"
        id="exampleFormControlTextarea1"
        rows="3"
        placeholder="Write a description for your task"
        v-model="questData.description"
        v-show="isAdmin"
      ></textarea>
    </div>
    <div class="mb-3" v-show="isAdmin">
      <p>Enter end date:</p>
      <input
        class="form-control"
        id="exampleFormControlInput1"
        placeholder="yyyy-mm-dd"
        v-model="questData.endDate"
      />
    </div>

    <button
      class="btn btn-primary"
      @click="createQuestSaveBtnClick"
      id="createQuestSaveBtn"
      v-show="isAdmin"
    >
      Save
    </button>
    <div class="unauthorizedMsg" v-show="!isAdmin">
      Sorry, but this page is available for administrators only :(
    </div>
    <button class="btn" @click="backBtnClicked" id="createQuestBackBtn">
      Back
    </button>
  </div>
</template>

<script>
import router from "@/router/router";
import questCRUD from "@/vuex/questCRUD";
import { ref } from "vue";

export default {
  name: "appCreateQuest",
  data() {
    return {
      isAdmin:
        JSON.parse(localStorage.getItem("user")) === null
          ? false
          : JSON.parse(localStorage.getItem("user")).user.category === "Admin",
      questData: ref({
        title: ref(""),
        description: ref(null),
        userId: ref(null),
        endDate: ref(null),
      }),
    };
  },
  methods: {
    createQuestSaveBtnClick() {
      var data = {
        userId: JSON.parse(localStorage.getItem("user")).user.userId,
        publishingDate: new Date().toJSON(),
        isAvailable: true,
        title: this.questData.title,
        description:
          (this.questData.description === null) |
          (this.questData.description === undefined)
            ? null
            : this.questData.description,
        isSolved: false,
        // endDate: new Date(this.questData.endDate).toJSON(),
        endDate:
          (this.questData.endDate === null) |
          (this.questData.endDate === undefined)
            ? null
            : new Date(this.questData.endDate).toJSON(),
        solutions: [],
        user: null,
      };
      console.log(data);

      questCRUD
        .create(data)
        .then((response) => {
          alert("Quest successfully created!");
          return response;
        })
        .catch((error) => {
          console.log("error: !!!" + error);
          alert("error");
          return error;
        });
    },
    backBtnClicked() {
      router.go(-1);
    },
  },
};
</script>

<style>
.createQuestContainer {
  display: flex;
  flex-direction: column;
  justify-content: space-between;
  align-items: center;
}

#exampleFormControlInput1 {
  width: 280px !important;
  /* margin-bottom: 14px !important; */
}

#exampleFormControlTextarea1 {
  width: 450px;
  height: 180px !important;
  /* height: fit-content;
  align-items: start !important; */
}

#createQuestBackBtn {
  margin-top: 15px;
}

#createQuestSaveBtn {
  margin-top: 15px;
}

#createQuestBackBtn:hover {
  border: 1px solid !important;
}

.unauthorizedMsg {
  font-size: 25px;
}
</style>
