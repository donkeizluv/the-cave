<template>
  <v-container>
    <!-- <div :class="['ma-2', item.children.length > 0 ? 'comment-border' : '' ]"> -->
    <div class="comment-border ml-2">
      <v-row dense>
        <v-col class="pa-0 pl-1" cols="12">
          <div class="ma-2 font-weight-bold">{{ item.data.username }}</div>
        </v-col>
        <v-col class="pa-0 pl-1" cols="12">
          <div class="ml-2 ma-1">{{ item.data.content }}</div>
        </v-col>
      </v-row>
      <v-row dense></v-row>
      <v-row dense>
        <v-col>
          <v-btn @click="showReply = !showReply" small icon class="comment-actions ma-2">
            <v-icon>mdi-message</v-icon>
          </v-btn>
          <v-btn small icon class="comment-actions ma-2">
            <v-icon>mdi-thumb-up</v-icon>
          </v-btn>
          <span>0</span>
          <v-btn small icon class="comment-actions ma-2">
            <v-icon>mdi-thumb-down</v-icon>
          </v-btn>
          <span>0</span>
        </v-col>
      </v-row>
      <new-comment-textbox @submit="submit" v-if="showReply" :to="item.data.username" />
    </div>
  </v-container>
</template>
<script>
import NewCommentTextbox from "./NewCommentTextbox.vue";
import moduleNames from "../store/modules/module-names";
import { mapActions } from "vuex";

export default {
  name: "Comment",
  components: {
    NewCommentTextbox
  },
  props: {
    item: {
      type: Object,
      required: true
    },
    closeNewCommentOnSubmit: {
      type: Boolean,
      default: true
    }
  },
  computed: {},
  data: () => ({
    showReply: false
  }),
  methods: {
    async submit(content) {
      this.$emit("submit", {
        parentId: this.item.data.id,
        content: content
      });
      if (this.closeNewCommentOnSubmit) this.showReply = false;
    }
  }
};
</script>
<style lang="scss" scoped>
@import "../styles/modules/comment-tree.scss";
</style>