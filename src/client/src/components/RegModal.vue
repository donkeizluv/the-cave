<template>
  <v-dialog :value="show" @click:outside="hideModal" width="450px">
    <v-card>
      <v-card-title>
        <span class="headline">Register</span>
      </v-card-title>
      <v-card-text>
        <v-form>
          <v-text-field v-model="reg.username" label="Username" :disabled="isLoading"></v-text-field>
          <v-text-field
            v-model="reg.email"
            label="Email"
            type="email"
            :rules="emailRules"
            :disabled="isLoading"
          ></v-text-field>
          <v-text-field label="Password" v-model="reg.pwd" type="password" autocomplete="off"></v-text-field>
          <v-text-field
            label="Retype password"
            v-model="reg.rPwd"
            type="password"
            :disabled="isLoading"
          ></v-text-field>
        </v-form>
        <div v-if="dialogMessage" :class="[isError ? 'red--text' : 'green--text', ' text-center']">
          <span>{{dialogMessage}}</span>
        </div>
      </v-card-text>
      <v-card-actions>
        <v-spacer></v-spacer>
        <v-btn color="primary" @click="createUser" :disabled="!canSubmit || isLoading">Create</v-btn>
        <v-btn @click="hideModal" :disabled="isLoading">Cancel</v-btn>
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
      },
      isError: false,
      dialogMessage: null,
      isLoading: false
    };
  },
  methods: {
    ...mapActions([REGISTER]),
    clearAll() {
      this.cred = {
        username: null,
        email: null,
        pwd: null,
        rPwd: null
      };
      this.isError = false;
      this.dialogMessage = null;
    },
    hideModal() {
      this.clearAll();
      this.$emit("click:outside");
    },
    async createUser() {
      try {
        if (!this.canSubmit) return;
        this.isLoading = true;
        await this.REGISTER(this.reg);
        this.isError = false;
        this.dialogMessage = "Register successfully.";
        this.timedSelfClose(2000);
      } catch (error) {
        this.isLoading = false;
        this.isError = true;
        this.dialogMessage = error.response.data || "Register failed.";
      }
    },
    timedSelfClose(timeout) {
      let vm = this;
      setTimeout(() => {
        vm.isLoading = false;
        vm.hideModal();
      }, timeout);
    }
  }
};
</script>