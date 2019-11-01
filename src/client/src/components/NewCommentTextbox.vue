<template>
  <v-container>
    <v-row dense>
      <v-col cols="10">
        <v-row dense>
          <v-col>
            <div class="ma-2 comment-text-area comment-textarea-border">
              <v-textarea
                v-model="newComment"
                :label="to ? `Replying to ${to}` : ''"
                clear-icon
                dense
                auto-grow
                flat
                solo
                full-width
                persistent-hint
              ></v-textarea>
            </div>
          </v-col>
        </v-row>
        <v-row dense>
          <v-col align="end">
            <v-btn v-if="!root" small text @click="$emit('close')">Cancel</v-btn>
            <v-btn small text color="primary" :disabled="!canSubmit" @click="submit">OK</v-btn>
          </v-col>
        </v-row>
      </v-col>
    </v-row>
  </v-container>
</template>
<script>
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
    }
  },
  computed: {
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