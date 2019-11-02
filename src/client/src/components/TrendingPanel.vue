<template>
  <v-container class="ma-0 pa-0" fluid>
    <v-col v-if="hasPost" class="pa-0 ma-0">
      <v-row dense v-for="post in posts" v-bind:key="post.id">
        <v-card :width="'100%'" flat outlined class="overline mb-4">
          <v-row>
            <v-col>
              <span class="font-weight-bold ma-4">cave/{{post.cateName}} &nbsp;&nbsp;&middot;</span>
              <span>posted By {{post.creatorName}}</span>
            </v-col>
          </v-row>
          <v-row dense>
            <v-col>
              <span class="ma-4">{{ createdTimeAgo(post.created) }}</span>
            </v-col>
          </v-row>
          <v-row>
            <v-col>
              <span class="headline ma-4">{{ post.title }}</span>
            </v-col>
          </v-row>

          <img
            v-if="post.image"
            v-bind:src="'data:image/jpeg;base64,' + post.image"
            width="40%"
            align="center"
          />

          <v-card-actions>
            <v-btn text color="primary" @click="selectPost(post)">Read More</v-btn>
            <v-spacer></v-spacer>
            <v-btn
              @click="addVote(post.id, 1)"
              icon
              v-ripple="{ class: 'red--text' } "
              :disabled="!isAuthenticated"
            >
              <v-icon color="red lighten-2">mdi-heart</v-icon>
            </v-btn>
            <span class="overline">{{post.upVotes}}</span>
            <v-btn
              @click="addVote(post.id, 2)"
              icon
              v-ripple="{ class: 'red--text' }"
              :disabled="!isAuthenticated"
            >
              <v-icon color="red lighten-2">mdi-heart-broken</v-icon>
            </v-btn>
            <span class="overline">{{post.downVotes}}</span>
          </v-card-actions>
        </v-card>
      </v-row>
    </v-col>
    <v-col v-else>
      <v-row justify="center">
        <div class="headline grey--text text--darken-1 text-center">Be the first to post something!</div>
      </v-row>
    </v-col>
  </v-container>
</template>
<style scoped>
img {
  display: block;
  margin: 0 auto;
}
</style>
<script>
import { timeAgo } from "./shared/utils";
import { posts } from "../store/getters/post/getter-types";
import {
  ADD_VOTE,
  REFRESH_POSTS_BY_CATE
} from "../store/actions/post/action-types";
import moduleNames from "../store/modules/module-names";
import { isAuthenticated } from "../store/getters/getter-types";
import { mapGetters, mapActions } from "vuex";
export default {
  name: "TrendingPanel",
  // props: {
  //   cate: {
  //     type: String,
  //     default: null
  //   }
  // },
  computed: {
    ...mapGetters([isAuthenticated]),
    ...mapGetters(moduleNames.post, [posts]),
    hasPost() {
      return this.posts.length > 0;
    },
    isDefault() {
      return this.$router.currentRoute.name === "default";
    }
  },
  data() {
    return {};
  },
  methods: {
    ...mapActions(moduleNames.post, [ADD_VOTE, REFRESH_POSTS_BY_CATE]),
    createdTimeAgo(t) {
      return timeAgo.format(new Date(t));
    },
    async addVote(postId, type) {
      await this.ADD_VOTE({ postId: postId, voteType: type });
    },
    selectPost(post) {
      this.$router.push({ name: "post", params: { postId: post.id } });
    }
  }
};
</script>