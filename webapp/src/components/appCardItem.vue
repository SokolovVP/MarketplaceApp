<template>
  <div class="card" style="width: 18rem" v-show="this.isVisible">
    <img src="../components/Code.jpg" class="card-img-top" alt="" />
    <div class="card-body">
      <h5 class="card-title">{{ card_data.title }}</h5>
      <!-- <p class="card-text">Description: {{ card_data.description ?? "\t" }}</p> -->
    </div>
    <ul class="list-group list-group-flush">
      <li
        class="list-group-item"
        v-bind:class="{ solvedClass: card_data.isSolved }"
        v-show="
          card_data.isAvailable && new Date(card_data.endDate) > new Date()
        "
      >
        Активно до:
        {{ new Date(card_data.endDate).toLocaleDateString() }}
      </li>
      <li
        class="list-group-item"
        v-bind:class="{ solvedClass: card_data.isSolved }"
        v-show="
          !card_data.isAvailable || new Date(card_data.endDate) < new Date()
        "
      >
        Неактивно
      </li>
    </ul>
    <div class="card-body">
      <!-- <router-link :to="{ name: 'quest', params: {} }"> -->
      <button class="btn" id="cardDetailsBtn" @click="cardDetailsClick">
        Подробнее
      </button>
    </div>
    <div class="card-body">
      <button
        class="btn"
        id="cardDeleteBtn"
        @click="deleteQuestClick"
        v-show="canDelete"
      >
        Удалить
      </button>
      <!-- </router-link> -->
    </div>
  </div>
</template>

<script>
import questCRUD from "../vuex/questCRUD";
import { ref } from "vue";
export default {
  name: "appCardItem",
  components: {},
  props: {
    card_data: {
      type: Object,
      default() {
        return {};
      },
    },
  },
  data() {
    return {
      title: "card item!",
      isVisible: ref(true),
      canDelete:
        JSON.parse(localStorage.getItem("user")).user.category === "Admin",
    };
  },
  computed: {},
  mounted() {},
  methods: {
    cardDetailsClick() {
      this.$emit("cardDetailsClick", this.card_data.questId);
    },
    deleteQuestClick() {
      if (JSON.parse(localStorage.getItem("user")).user.category === "Admin") {
        questCRUD
          .delete(this.card_data.questId)
          .then((response) => {
            console.log(response);
            this.isVisible = false;
            return response;
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
.card {
  margin-top: 0px rem;
  margin-right: 13px;
  margin-left: 0px rem;
  margin-bottom: 13px;
  justify-content: start;
}

#details_link {
  text-decoration: none;
  color: rgba(var(--bs-dark-bg-subtle));
}

#cardDetailsBtn {
  transition: 1s;
}

.card-title {
  height: 50px !important;
  /*text-overflow: ellipsis !important; */
  overflow: hidden !important;
  /* white-space: nowrap !important; */
  text-overflow: ellipsis !important;
  /* width: 100px !important; */
}

.solvedClass {
  color: rgb(13, 195, 13) !important;
}
</style>
