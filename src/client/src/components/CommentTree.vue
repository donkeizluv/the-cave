<template>
  <v-card class="mx-auto" flat>
    <v-treeview
      :items="commentTree"
      item-key="data.id"
      expand-icon="mdi-chevron-down"
      dense
      :open="open"
      open-all
    >
      <template v-slot:prepend="{ item, open }">
        <comment @submit="submit($event)" :item="item" />
      </template>
    </v-treeview>
  </v-card>
</template>
<script>
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
    }
  },
  computed: {
    commentTree() {
      return arrayToTree(this.comments);
    },
    open() {
      return this.comments.map(c => c.id);
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