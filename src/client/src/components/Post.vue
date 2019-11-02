<template>
  <v-card v-if="post">
    <v-card-subtitle class="overline pb-2">Posted By {{ post.createdBy }} {{ post.createdDate }} </v-card-subtitle>
    <v-card-title class="display-2 pt-0 font-weight-bold">{{ post.title }}</v-card-title>
    <v-card-text class="black--text title">{{ post.content }}</v-card-text>
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
  <v-card v-else>
    <v-card-title>Unable to display post :(</v-card-title>
  </v-card>
</template>

<script>
import NewCommentTextbox from "./NewCommentTextbox.vue";
import {
  GET_SELECTED_POST,
  ADD_COMMENT
} from "../store/actions/post/action-types";
import { mapGetters, mapActions } from "vuex";
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
  data() {
    return {
      post: {
        title: "asdasdasd",
        content: "2222222",
        createdDate: "23/10/2019",
        createdBy: "Hein"
      }
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
  },
  computed: {}
};
</script>
