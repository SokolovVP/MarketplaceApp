<template>
  <div class="loginPage">
    <div class="loginFormContainer">
      <div class="userNameInputContainer formContainer">
        <input
          class="userNameInput form-control"
          placeholder="Username"
          v-model="username"
          v-show="createAccount"
        />
      </div>
      <div
        class="userNameErrorField errorField"
        v-show="usernameError != null && createAccount"
      >
        {{ usernameError }}
      </div>
      <div class="nameInputContainer formContainer">
        <input
          class="nameInput form-control"
          placeholder="Name"
          v-model="name"
          v-show="createAccount"
        />
      </div>
      <div
        class="nameErrorField errorField"
        v-show="nameError != null && createAccount"
      >
        {{ nameError }}
      </div>
      <div class="loginInputContainer formContainer">
        <input
          class="loginInput form-control"
          placeholder="Login"
          v-model="login"
          v-show="createAccount"
        />
      </div>
      <div
        class="loginErrorField errorField"
        v-show="loginError != null && createAccount"
      >
        {{ loginError }}
      </div>
      <div class="surnameInputContainer formContainer">
        <input
          class="surnameInput form-control"
          placeholder="Surname"
          v-model="surname"
          v-show="createAccount"
        />
      </div>
      <div
        class="surnameErrorField errorField"
        v-show="surnameError != null && createAccount"
      >
        {{ surnameError }}
      </div>
      <div class="emailInputContainer formContainer">
        <input
          class="emailInput form-control"
          placeholder="Email@email.com"
          v-model="email"
        />
      </div>
      <div
        class="emailErrorField errorField"
        v-show="emailError != null && createAccount"
      >
        {{ emailError }}
      </div>
      <div class="passwordInputContainer formContainer">
        <input
          class="passwordInput form-control"
          type="password"
          placeholder="Password"
          v-model="password"
        />
      </div>
      <div
        class="passwordErrorField errorField"
        v-show="passwordError != null && createAccount"
      >
        {{ passwordError }}
      </div>
      <div class="roleInputContainer" v-show="createAccount">
        <input
          type="checkbox"
          name="roleCheckbox"
          value="true"
          v-model="role"
        />
        Admin
      </div>
      <button
        class="btn btn-primary authorizationBtn"
        v-show="!createAccount"
        @click="loginClick"
      >
        Log in
      </button>
      <button
        class="btn btn-primary authorizationBtn"
        v-show="createAccount"
        @click="signUpclick"
      >
        Sign up
      </button>
      <div
        class="successfullyRegisteredText"
        v-show="successfullyRegistered && createAccount"
      >
        Successfully registered!
      </div>
      <div class="createAccountOffer">
        <div class="registerText">{{ questionMsg }}</div>
        <button class="btn" @click="createAccountOfferClick">
          {{ authorizationLoginMsg }}
        </button>
      </div>
    </div>
  </div>
</template>

<script>
import userCRUD from "@/vuex/userCRUD";
import { ref } from "vue";
export default {
  name: "appAuthorization",
  data() {
    return {
      email: ref(null),
      password: ref(null),
      username: ref(null),
      createAccount: ref(false),
      authorizationLoginMsg: "Create an account!",
      questionMsg: "Not registered yet?",
      name: ref(null),
      surname: ref(null),
      role: ref(false),
      login: ref(null),
      successfullyRegistered: false,
      usernameError: ref(null),
      emailError: ref(null),
      nameError: ref(null),
      loginError: ref(null),
      surnameError: ref(null),
      passwordError: ref(null),
    };
  },
  computed: {
    loggedIn() {
      return this.$store.state.auth.status.loggedIn;
    },
  },
  created() {
    if (this.loggedIn) {
      this.$router.push({ name: "catalog" });
    }
  },
  methods: {
    // loginClick(user) {
    signUpclick() {
      var _user = {
        userName: this.username,
        name: this.name,
        surname: this.surname,
        email: this.email,
        registrationDate: new Date().toJSON(),
        category: this.role ? "Admin" : "User",
        login: this.login,
        password: this.password,
      };
      console.log(JSON.stringify(_user));
      userCRUD
        .create(_user)
        .then((response) => {
          console.log(response);
          this.successfullyRegistered = true;
        })
        .catch((error) => {
          if (error.response) {
            if (error.response.status === 400) {
              if (error.response.data) {
                if (error.response.data.errors) {
                  console.log(
                    `ERROR: ${JSON.stringify(error.response.data.errors)}`
                  );
                  if (error.response.data.errors.userName) {
                    this.usernameError = error.response.data.errors.userName[0];
                    setTimeout(() => {
                      this.usernameError = null;
                    }, 3000);
                  }
                  if (error.response.data.errors.email) {
                    this.emailError = error.response.data.errors.email[0];
                    setTimeout(() => {
                      this.emailError = null;
                    }, 3000);
                  }
                  if (error.response.data.errors.name) {
                    this.nameError = error.response.data.errors.name[0];
                    setTimeout(() => {
                      this.nameError = null;
                    }, 3000);
                  }
                  if (error.response.data.errors.surname) {
                    this.surnameError = error.response.data.errors.surname[0];
                    setTimeout(() => {
                      this.surnameError = null;
                    }, 3000);
                  }
                  if (error.response.data.errors.login) {
                    this.loginError = error.response.data.errors.login[0];
                    setTimeout(() => {
                      this.loginError = null;
                    }, 3000);
                  }
                  if (error.response.data.errors.password) {
                    this.passwordError = error.response.data.errors.password[0];
                    setTimeout(() => {
                      this.passwordError = null;
                    }, 3000);
                  }
                }
              }
            }
          }
        });
    },
    createAccountOfferClick() {
      this.authorizationLoginMsg =
        this.authorizationLoginMsg === "Create an account!"
          ? "Sign in"
          : "Create an account!";
      this.questionMsg =
        this.questionMsg === "Not registered yet?"
          ? "Already have an account?"
          : "Not registered yet?";
      this.createAccount = !this.createAccount;
    },
    loginClick() {
      var user = {
        email: this.email,
        password: this.password,
      };
      this.$store.dispatch("auth/login", user).then(
        () => {
          this.$router.push({ name: "catalog" });
        },
        (error) => {
          console.log(error);
        }
      );
    },
  },
};
</script>

<style>
.loginPage {
  display: flex;
  flex-direction: column;
  align-items: center;
}

.loginFormContainer {
  width: 400px !important;
  border: 2px solid;
  border-radius: 5px;
  border-color: rgb(175, 194, 238);
  align-items: center;
  display: flex;
  flex-direction: column;
}

.formContainer {
  margin-top: 10px;
  margin-bottom: 10px;
  width: 300px;
}

.authorizationBtn {
  margin-top: 10px;
  margin-bottom: 10px;
}

.roleInputContainer {
  margin-left: 230px;
}

.successfullyRegisteredText {
  color: rgb(48, 184, 48);
}

.errorField {
  font-size: 14px;
  color: red;
}
</style>
