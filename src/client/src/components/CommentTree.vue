<template>
  <v-card class="mx-auto" outlined>
    <v-treeview
      :items="commentTree"
      item-key="data.id"
      expand-icon="mdi-chevron-down"
      dense
      :open-all="openall"
    >
      <template v-slot:prepend="{ item, open }">
        <comment @submit="submit($event)" :item="item" />
      </template>
    </v-treeview>
  </v-card>
</template>
<script>
// import { REGISTER } from "../store/actions/action-types";
//import { posts } from "../store/getters/post/getter-types";
import moduleNames from "../store/modules/module-names";
import { mapGetters } from "vuex";
import { arrayToTree } from "performant-array-to-tree";
import Comment from "./Comment.vue";

export default {
  name: "CommentTree",
  components: {
    Comment
  },
  props: {
    comments: {
      type: Array,
      required: true
    },
    openall: {
      type: Boolean,
      default: true
    }
  },
  computed: {
    commentTree() {
      return arrayToTree(this.comments);
    }
  },
  methods: {
    submit(content) {
      console.log(content);
      this.$emit("submit", content);
    }
  }
};
</script>
<style lang="scss" scoped>
@import "../styles/modules/comment-tree.scss";
</style>