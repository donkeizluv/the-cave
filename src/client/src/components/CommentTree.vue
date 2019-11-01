<template>
  <v-card class="mx-auto" outlined>
    <v-treeview
      :items="commentTree"
      item-key="data.id"
      expand-icon="mdi-chevron-down"
      dense
      open-all
    >
      <template v-slot:prepend="{ item, open }">
        <comment :item="item" />
      </template>
    </v-treeview>
  </v-card>
</template>
<script>
// import { REGISTER } from "../store/actions/action-types";
import { posts } from "../store/getters/post/getter-types";
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
    // comments: {
    //   type: Object,
    //   required: true
    // }
  },
  computed: {
    ...mapGetters(moduleNames.post, [posts]),
    commentTree() {
      return arrayToTree(this.comments);
    }
  },
  data: () => ({
    comments: [
      {
        id: "1",
        creatorId: "1",
        username: "user1",
        content: "xxxxxxxxxx",
        created: new Date() + 1,
        parentId: null
      },
      {
        id: "2",
        creatorId: "2",
        username: "user2",
        content: "yttttt",
        created: new Date() + 2,
        parentId: "1"
      },
      {
        id: "5",
        creatorId: "2",
        username: "user2",
        content: "nested bitch!",
        created: new Date() + 2,
        parentId: "2"
      },
      {
        id: "7",
        creatorId: "2",
        username: "user2",
        content: "nested bitch!",
        created: new Date() + 2,
        parentId: "2"
      },
      {
        id: "6",
        creatorId: "111",
        username: "user2",
        content:
          "a really long commentssssssssssssssssssssssssssssssssssssss a really long commentssssssssssssssssssssssssssssssssssssss a really long commentssssssssssssssssssssssssssssssssssssss a really long commentssssssssssssssssssssssssssssssssssssss a really long commentssssssssssssssssssssssssssssssssssssss",
        created: new Date() + 2,
        parentId: "5"
      },
      {
        id: "3",
        creatorId: "2",
        username: "user3",
        content: "haha!",
        created: new Date() + 3,
        parentId: null
      },
      {
        id: "4",
        creatorId: "2",
        username: "user2",
        content: "yttttt",
        created: new Date() + 4,
        parentId: "3"
      }
    ]
  })
};
</script>
<style lang="scss" scoped>
@import "../styles/modules/comment-tree.scss";
</style>