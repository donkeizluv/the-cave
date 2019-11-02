<template>
  <v-card v-if="post">
    <v-card-subtitle class="overline pb-2">Posted By {{ post.createdBy }} {{ createdTimeAgo }}</v-card-subtitle>
    <v-card-title class="display-1 pt-0 font-weight-bold">{{ post.title }}</v-card-title>
    <v-card-text class="black--text diaplay">
      <span v-html="post.content" />
      <img v-if="post.image" v-bind:src="'data:image/jpeg;base64,' + post.image" width="80%"/>
    </v-card-text>
    <v-container>
      <v-row justify="end" dense>
        <v-btn @click="addVote(post.id, 1)" icon v-ripple="{ class: 'red--text' }">
          <v-icon color="red lighten-2">mdi-heart</v-icon>
        </v-btn>
        <span class="overline mt-2">{{post.upVotes}}</span>
        <v-btn @click="addVote(post.id, 2)" icon v-ripple="{ class: 'red--text' }">
          <v-icon color="red lighten-2">mdi-heart-broken</v-icon>
        </v-btn>
        <span class="overline mt-2">{{post.downVotes}}</span>
      </v-row>
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
  ADD_COMMENT,
  ADD_VOTE
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
    ...mapActions(moduleNames.post, [GET_SELECTED_POST, ADD_COMMENT, ADD_VOTE]),
    async submitCommentRoot(content) {
      let comment = {
        postId: this.postId,
        content: content,
        parentId: null
      };
      comment.id = await this.ADD_COMMENT(comment);
      this.post.comments.push(comment);
    },
    async addVote(postId, type) {
      let votes = await this.ADD_VOTE({ postId: postId, voteType: type });
      this.post.upVotes = votes.upVotes;
      this.post.downVotes = votes.downVotes;
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
