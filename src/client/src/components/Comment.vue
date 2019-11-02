<template>
  <v-container class="pa-0 ma-0">
    <!-- <div :class="['ma-2', item.children.length > 0 ? 'comment-border' : '' ]"> -->
    <v-col class="comment-border ma-0 ml-2 pa-0">
      <v-row class="ml-2" dense>
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
          <v-btn
            :disabled="!isAuthenticated"
            @click="showReplyBox"
            text
            color="primary"
            class="ma-2"
          >Reply</v-btn>
          <!-- <v-btn icon>
            <v-icon color="red lighten-1 ma-2">mdi-heart</v-icon>
          </v-btn>
          <span class="ma-2">0</span>
          <v-btn icon>
            <v-icon color="red lighten-1 ma-2">mdi-heart-broken</v-icon>
          </v-btn>
          <span class="ma-2">0</span>-->
        </v-col>
      </v-row>
      <v-row dense>
        <v-col class="ma-0 pa-0">
          <new-comment-textbox
            @submit="submit"
            @close="showReply = false;"
            v-if="shouldShowReply"
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
import { isAuthenticated } from "../store/getters/getter-types";
import { mapGetters } from "vuex";
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
    },
    openreplyid: {
      type: String,
      default: null
    }
  },
  computed: {
    ...mapGetters([isAuthenticated]),
    createdTimeAgo() {
      return timeAgo.format(new Date(this.item.data.created));
    },
    shouldShowReply(){
      if(this.openreplyid !== null) {
        return this.item.data.id === this.openreplyid && this.showReply;
      }
      return this.showReply;
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
    },
    showReplyBox() {
      this.showReply = true;
      this.$emit("showreply", this.item.data.id);
    }
  }
};
</script>
<style lang="scss" scoped>
@import "../styles/modules/comment-tree.scss";
</style>