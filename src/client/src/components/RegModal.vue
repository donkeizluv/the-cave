<template>
  <v-dialog :value="show" persistent width="450px" max-width="450px">
    <v-card>
      <v-card-title>
        <span class="headline mb-4">Register</span>
        <v-spacer></v-spacer>
        <span class="mb-4">
          <v-btn icon @click="hideModal">
            <v-icon>mdi-close</v-icon>
          </v-btn>
        </span>
      </v-card-title>
      <v-card-text>
        <v-form v-model="formValid">
          <v-text-field
            dense
            maxlength="24"
            class="input-group--focused mb-4 pb-4"
            v-model.trim="reg.username"
            :rules="rules.username"
            label="Username"
            prepend-icon="mdi-account"
            :disabled="isLoading"
          ></v-text-field>
          <v-text-field
            dense
            maxlength="24"
            class="input-group--focused mb-4 pb-4"
            label="Password"
            prepend-icon="mdi-lock"
            :append-icon="showPwd ? 'mdi-eye-off' : 'mdi-eye'"
            v-model.trim="reg.pwd"
            :rules="rules.passwords"
            :type="showPwd ? 'text' : 'password'"
            @click:append="showPwd = !showPwd"
          ></v-text-field>
          <v-text-field
            dense
            maxlength="36"
            class="input-group--focused"
            v-model.trim="reg.email"
            label="Email"
            prepend-icon="mdi-email"
            type="email"
            :rules="rules.email"
            :disabled="isLoading"
          ></v-text-field>
        </v-form>
        <div v-if="dialogMessage" :class="[isError ? 'red--text' : 'green--text', ' text-center']">
          <span>{{dialogMessage}}</span>
        </div>
      </v-card-text>
      <v-card-actions class="pa-0 justify-center">
        <v-btn
          color="primary"
          class="mb-6"
          :loading="isLoading"
          @click="createUser"
          :disabled="!canSubmit || isLoading"
        >Create</v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script>
import { REGISTER } from "../store/actions/action-types";
import { mapActions } from "vuex";
import validationRules from "./shared/validation-rules";

export default {
  name: "RegModal",
  props: {
    show: {
      required: true
    }
  },
  computed: {
    canSubmit() {
      return this.formValid;
    }
  },
  data() {
    return {
      rules: {
        username: [
          value => validationRules.requiredValue(value),
          value => validationRules.noSpecial(value),
          value => validationRules.minCharacter(value, 6),
          value => validationRules.maxCharacter(value, 24)
        ],
        passwords: [
          value => validationRules.requiredValue(value),
          value => validationRules.minCharacter(value, 6),
          value => validationRules.maxCharacter(value, 24),
          v => (v || '').indexOf(' ') < 0 || 'No spaces are allowed'
        ],
        email: [
          value => validationRules.requiredValue(value),
          value => validationRules.maxCharacter(value, 36),
          value => validationRules.email(value)
        ]
      },

      reg: {
        username: null,
        email: null,
        pwd: null,
        rPwd: null
      },
      showPwd: false,
      formValid: null,
      isError: false,
      dialogMessage: null,
      isLoading: false
    };
  },
  methods: {
    ...mapActions([REGISTER]),
    clearAll() {
      this.reg = {
        username: null,
        email: null,
        pwd: null
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
        if(this.reg.pwd == "") return;
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