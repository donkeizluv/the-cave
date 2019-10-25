<template>
  <v-dialog :value="show" @click:outside="$emit('click:outside');" width="450px">
    <v-card>
      <v-card-title>
        <span class="headline">Register</span>
      </v-card-title>
      <v-card-text>
        <v-form>
          <v-text-field v-model="reg.username" label="Username" autocomplete="off"></v-text-field>
          <v-text-field
            v-model="reg.email"
            label="Email"
            type="email"
            :rules="emailRules"
            autocomplete="off"
          ></v-text-field>
          <v-text-field label="Password" v-model="reg.pwd" type="password" autocomplete="off"></v-text-field>
          <v-text-field
            label="Retype password"
            v-model="reg.rPwd"
            type="password"
            autocomplete="off"
          ></v-text-field>
        </v-form>
      </v-card-text>
      <v-card-actions>
        <v-spacer></v-spacer>
        <v-btn color="primary" @click="createUser" :disabled="!canSubmit">Create</v-btn>
        <v-btn @click="$emit('click:outside');">Cancel</v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script>
import { REGISTER } from "../store/actions/action-types";
import { mapActions } from "vuex";
export default {
  name: "RegModal",
  props: {
    show: {
      required: true
    }
  },
  computed: {
    canSubmit() {
      return true;
    }
  },
  data() {
    return {
      emailRules: [
        value => !!value || "Required.",
        value => (value || "").length <= 20 || "Max 20 characters",
        value => {
          const pattern = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
          return pattern.test(value) || "Invalid e-mail.";
        }
      ],
      reg: {
        username: null,
        email: null,
        pwd: null,
        rPwd: null
      }
    };
  },
  methods: {
    ...mapActions([REGISTER]),
    async createUser() {
      try {
        if (!this.canSubmit) return;
        await this.REGISTER(this.reg);
        this.username = "";
        this.pwd = "";
      } catch (error) {
        // reg fail
      }
    }
  }
};
</script>