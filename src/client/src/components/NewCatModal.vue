<template>
  <v-dialog :value="show" @click:outside="$emit('click:outside');" width="400px">
    <v-card>
      <v-card-title>
        <span class="headline">Create new category</span>
      </v-card-title>
      <v-card-text>
        <v-form>
          <v-text-field v-model="catName" label="Name"></v-text-field>
          <v-text-field v-model="desc" label="Description"></v-text-field>
        </v-form>
      </v-card-text>
      <v-card-actions>
        <v-spacer></v-spacer>
        <v-btn color="primary">Create</v-btn>
        <v-btn @click="$emit('click:outside');">Cancel</v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script>
import { LOGIN } from "../store/actions/action-types";
import { mapActions } from "vuex";
export default {
  name: "NewCatModal",
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
      catName: "",
      desc: ""
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