<template>
  <v-card outlined>
    <v-card-title class="headline post-title">{{ post.title }}</v-card-title>
    <v-card-subtitle>{{ createdTimeAgo }}</v-card-subtitle>
    <v-card-text class="text--primary post-content">{{ post.content }}</v-card-text>
    <v-container>
      <v-row dense>
        <v-col>
          <new-comment-textbox @submit="submitCommentRoot" />
        </v-col>
      </v-row>
      <v-row dense>
        <v-col>
          <comment-tree
            :openall="true"
            @submit="submitCommentChild"
            :comments="post.comments"
            :root="true"
          />
        </v-col>
      </v-row>
    </v-container>
    <!-- <v-btn icon>
        <v-icon>mdi-heart</v-icon>
      </v-btn>
      <v-btn icon>
        <v-icon>mdi-heart-broken</v-icon>
    </v-btn>-->
  </v-card>
</template>

<style scoped>
.post-content {
  font-size: 1em;
}
</style>

<script>
import { timeAgo } from "./shared/utils";
import NewCommentTextbox from "./NewCommentTextbox.vue";
import {
  GET_SELECTED_POST,
  ADD_COMMENT
} from "../store/actions/post/action-types";
import { mapActions } from "vuex";
import moduleNames from "../store/modules/module-names";
import CommentTree from "./CommentTree.vue";
export default {
  name: "Post",
  components: {
    CommentTree,
    NewCommentTextbox
  },
  async mounted() {
    this.post = await this.GET_SELECTED_POST(this.postId);
  },

  props: {
    postId: {
      type: String,
      required: true
    }
  },
  computed: {
    createdTimeAgo() {
      return timeAgo.format(new Date(this.post.created));
    }
  },

  data() {
    return {
      post: null
    };
  },
  methods: {
    ...mapActions(moduleNames.post, [GET_SELECTED_POST, ADD_COMMENT]),
    async submitCommentRoot(content) {
      let comment = {
        postId: this.postId,
        content: content,
        parentId: null
      };
      comment.id = await this.ADD_COMMENT(comment);
      this.post.comments.push(comment);
    },
    async submitCommentChild(childComment) {
      let comment = {
        postId: this.postId,
        content: childComment.content,
        parentId: childComment.parentId
      };
      console.log(comment);
      comment.id = await this.ADD_COMMENT(comment);
      // let childCommentIndex = this.post.comments.findIndex(c => c.id === childComment.parentId);
      this.post.comments.push(comment);
    }
  }
};
</script>
