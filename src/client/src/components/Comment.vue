<template>
  <v-container class="pa-0 ma-0">
    <!-- <div :class="['ma-2', item.children.length > 0 ? 'comment-border' : '' ]"> -->
    <v-col>
      <v-row class="comment-border ml-2" dense>
        <v-col class="pa-0 pl-1" cols="12">
          <div class="ma-2 font-weight-bold">{{ item.data.username }}</div>
          <div class="ma-2 font-weight-light ago-text">{{createdTimeAgo}}</div>
        </v-col>
        <v-col class="pa-0 pl-1" cols="12">
          <div class="ml-2 ma-1">{{ item.data.content }}</div>
        </v-col>
      </v-row>
      <v-row dense>
        <v-col>
          <v-btn @click="showReply = !showReply" icon class="ma-2">
            <v-icon color="red lighten-1 ma-2">mdi-message</v-icon>
          </v-btn>
          <v-btn icon>
            <v-icon color="red lighten-1 ma-2">mdi-heart</v-icon>
          </v-btn>
          <span class="ma-2">0</span>
          <v-btn icon>
            <v-icon color="red lighten-1 ma-2">mdi-heart-broken</v-icon>
          </v-btn>
          <span class="ma-2">0</span>
        </v-col>
      </v-row>
      <v-row dense>
        <v-col>
          <new-comment-textbox
            @submit="submit"
            @close="showReply = false;"
            v-if="showReply"
            :to="item.data.username"
          />
        </v-col>
      </v-row>
    </v-col>
  </v-container>
</template>
<script>
import { timeAgo } from "./shared/utils";
import NewCommentTextbox from "./NewCommentTextbox.vue";

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
  computed: {
    createdTimeAgo() {
      return timeAgo.format(new Date(this.item.data.created));
    }
  },
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