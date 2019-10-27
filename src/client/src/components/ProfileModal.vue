<template>
  <v-dialog :value="show" @click:outside="hideDialog" max-width="350px" width="350px">
    <v-card>
      <v-card-title>
        <span class="headline mb-4">Profile</span>
      </v-card-title>
      <v-card-text>
        <v-form>
          <v-text-field
            v-model.trim="profile.username"
            label="Username"
            prepend-icon="mdi-account"
            class="input-group--focused mb-4"
          ></v-text-field>
          <v-text-field
            label="Password"
            prepend-icon="mdi-lock"
            v-model.trim="profile.pwd"
            type="password"
          ></v-text-field>
          <v-text-field
            v-model.trim="profile.email"
            label="Email"
            prepend-icon="mdi-email"
            class="input-group--focused mb-4"
          ></v-text-field>
        </v-form>
        <div v-if="errorMessage" class="red--text text-center">
          <span>{{errorMessage}}</span>
        </div>
      </v-card-text>
      <v-card-actions>
        <v-spacer></v-spacer>
        <v-btn color="primary">Update</v-btn>
        <v-btn @click="hideDialog">Cancel</v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script>
import { LOGIN } from "../store/actions/action-types";
import { mapActions } from "vuex";
export default {
  name: "ProfileModal",
  props: {
    show: {
      required: true
    }
  },
  computed: {
  },
  data: function name() {
    return {
      profile: {
        username: "abc",
        pwd: "abc",
        email: "email@gmail.com"
      },
      errorMessage: null,
      isLoading: false
    };
  },
  methods: {
    clearAll() {
      this.username = null;
      this.pwd = null;
      this.email = null;
    },
    hideDialog() {
      this.clearAll();
      this.$emit("click:outside");
    }
  }
};
</script>
