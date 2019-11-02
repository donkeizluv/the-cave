<template>
  <v-dialog :value="show" max-width="350px" width="350px" persistent>
    <v-card>
      <v-card-title>
        <span class="headline mb-4">Log in</span>
        <v-spacer></v-spacer>
        <span class="mb-4">
          <v-btn icon @click="hideModal">
            <v-icon>mdi-close</v-icon>
          </v-btn>
        </span>
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
      <v-card-actions class="pa-0 justify-center">
        <v-btn
          color="primary"
          :loading="isLoading"
          class="mb-6"
          @click="doLogin"
          :disabled="!canLogin || isLoading"
        >Login</v-btn>
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
    },
    clear() {
      if(!this.show) {
        this.cred.username = null;
        this.cred.pwd = null;
        this.errorMessage = null;
      }
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
        if (!result.valid) {
          this.errorMessage = result.message;
          return;
        }
        this.hideModal();
      } catch (ex) {
        this.errorMessage = "Login failed.";
      } finally {
        this.isLoading = false;
      }
    },
    clearAll() {
      this.cred.username = null;
      this.cred.pwd = null;
      this.errorMessage = null;
    },
    hideModal() {
      this.clearAll();
      this.$emit("click:outside");
    }
  }
};
</script>
