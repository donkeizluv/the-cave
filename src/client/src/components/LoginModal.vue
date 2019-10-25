<template>
  <v-dialog :value="show" @click:outside="$emit('click:outside');" max-width="350px" width="350px">
    <v-card>
      <v-card-title>
        <span class="headline">Log in</span>
      </v-card-title>
      <v-card-text>
        <v-form>
          <v-text-field v-model="username" label="Username" @keyup.enter.native="login"></v-text-field>
          <v-text-field label="Password" v-model="pwd" @keyup.enter.native="login" type="password"></v-text-field>
        </v-form>
      </v-card-text>
      <v-card-actions>
        <v-spacer></v-spacer>
        <v-btn color="primary" @click="login" :disabled="!canLogin">Login</v-btn>
        <v-btn @click="$emit('click:outside');">Cancel</v-btn>
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
      return this.username && this.pwd;
    }
  },
  data: function name() {
    return {
      username: "",
      pwd: ""
    };
  },
  methods: {
    ...mapActions([LOGIN]),
    async login() {
      if (!this.canLogin) return;
      await this.LOGIN({
        username: this.username,
        pwd: this.pwd
      });
      this.username = "";
      this.pwd = "";
    }
  }
};
</script>