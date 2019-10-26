<template>
  <v-dialog :value="show" @click:outside="hideDialog" max-width="350px" width="350px">
    <v-card>
      <v-card-title>
        <span class="headline">Log in</span>
      </v-card-title>
      <v-card-text>
        <v-form>
          <v-text-field v-model="cred.username" label="Username" @keyup.enter.native="doLogin"></v-text-field>
          <v-text-field
            label="Password"
            v-model="cred.pwd"
            @keyup.enter.native="doLogin"
            type="password"
          ></v-text-field>
        </v-form>
        <div v-if="errorMessage" class="red--text text-center">
          <span>{{errorMessage}}</span>
        </div>
      </v-card-text>
      <v-card-actions>
        <v-spacer></v-spacer>
        <v-btn color="primary" @click="doLogin" :disabled="!canLogin">Login</v-btn>
        <v-btn @click="hideDialog">Cancel</v-btn>
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
      errorMessage: null
    };
  },
  methods: {
    ...mapActions([LOGIN]),
    async doLogin() {
      if (!this.canLogin) return;
      let result = await this.LOGIN(this.cred);
      if (result) {
        this.hideDialog();
      }
      this.errorMessage = "Username or passwords is not valid.";
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