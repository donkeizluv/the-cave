<template>
  <v-dialog :value="show" @click:outside="hideDialog" max-width="350px" width="350px">
    <v-card>
      <v-card-title>
        <span class="headline mb-4">Log in</span>
      </v-card-title>
      <v-card-text>
        <v-form>
          <v-text-field
            v-model.trim="cred.username"
            label="Username"
            prepend-icon="mdi-account"
            @keyup.enter.native="doLogin"
            :disabled="isLoading"
            class="input-group--focused mb-4"
          ></v-text-field>
          <v-text-field
            label="Password"
            prepend-icon="mdi-lock"
            v-model.trim="cred.pwd"
            @keyup.enter.native="doLogin"
            type="password"
            :disabled="isLoading"
          ></v-text-field>
        </v-form>
        <div v-if="errorMessage" class="red--text text-center">
          <span>{{errorMessage}}</span>
        </div>
      </v-card-text>
      <v-card-actions>
        <v-spacer></v-spacer>
        <v-btn color="primary" @click="doLogin" :disabled="!canLogin || isLoading">Login</v-btn>
        <v-btn @click="hideDialog" :disabled="isLoading">Cancel</v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script>
import { LOGIN } from "../store/actions/action-types";
import { mapActions } from "vuex";
export default {
  name: "LoginModal",
  props: {
    show: {
      required: true
    }
  },
  computed: {
    canLogin() {
      return this.cred.username && this.cred.pwd;
    }
  },
  data: function name() {
    return {
      cred: {
        username: "",
        pwd: ""
      },
      errorMessage: null,
      isLoading: false
    };
  },
  methods: {
    ...mapActions([LOGIN]),
    async doLogin() {
      if (!this.canLogin) return;
      try {
        this.isLoading = true;
        let result = await this.LOGIN(this.cred);
        if (result) {
          this.hideDialog();
          return;
        }
        this.errorMessage = "Username or passwords is not valid.";
      } finally {
        this.isLoading = false;
      }
    },
    clearAll() {
      this.username = null;
      this.pwd = null;
      this.errorMessage = null;
    },
    hideDialog() {
      this.clearAll();
      this.$emit("click:outside");
    }
  }
};
</script>