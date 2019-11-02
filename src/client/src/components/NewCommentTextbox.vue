<template>
  <v-container class="ma-0 pa-0">
    <v-row :justify="fullwidth ? 'center' : 'end'" dense>
      <v-col cols="11">
        <v-row dense>
          <v-col>
            <div class>
              <v-textarea
                v-model="newComment"
                :label="to ? `Replying to ${to}` : ''"
                clear-icon
                dense
                auto-grow
                maxlength="1024"
                counter
                outlined
                :clearable="true"
                full-width
                persistent-hint
                :disabled="!isAuthenticated"
              ></v-textarea>
            </div>
          </v-col>
        </v-row>
        <v-row dense>
          <v-col align="end">
            <v-btn
              class="mr-4"
              v-if="!root"
              small
              text
              @click="$emit('close'); newComment = null;"
            >Cancel</v-btn>
            <v-btn small color="primary" :disabled="!canSubmit || !isAuthenticated"  @click="submit">OK</v-btn>
          </v-col>
        </v-row>
      </v-col>
    </v-row>
  </v-container>
</template>
<script>
import { isAuthenticated } from "../store/getters/getter-types";
import { mapGetters } from "vuex";
export default {
  name: "Comment",
  props: {
    to: {
      type: String,
      default: null
    },
    root: {
      type: Boolean,
      default: false
    },
    clearOnSubmit: {
      type: Boolean,
      default: true
    },
    fullwidth: {
      type: Boolean,
      default: true
    }
  },
  computed: {
    ...mapGetters([isAuthenticated]),
    canSubmit() {
      return !!this.newComment;
    }
  },
  data: () => ({
    newComment: null
  }),
  methods: {
    submit() {
      this.$emit("submit", this.newComment);
      if (this.clearOnSubmit) this.newComment = null;
    }
  }
};
</script>
<style lang="scss" scoped>
@import "../styles/modules/comment-tree.scss";
</style>