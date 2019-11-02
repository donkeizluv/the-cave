<template>
  <v-container class="ma-0 pa-0" fluid>
    <v-col class="pa-0 ma-0">
      <v-row dense v-for="post in posts" v-bind:key="post.id">
        <v-card :width="'100%'" flat outlined class="overline mb-4">
          <v-list-item>
            <!-- <--<v-list-item-avatar color="grey"></v-list-item-avatar>-->
            <v-list-item-content>
              <v-list-item-title class="headline">{{ post.title }}</v-list-item-title>
              <v-list-item-subtitle>{{post.creatorName}} - {{ createdTimeAgo(post.created) }}</v-list-item-subtitle>
            </v-list-item-content>
          </v-list-item>

          <img v-if="post.image" v-bind:src="'data:image/jpeg;base64,' + post.image" height="100px"/>

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
  </v-container>
</template>
<script>
import { timeAgo } from "./shared/utils";
import { posts } from "../store/getters/post/getter-types";
import { ADD_VOTE } from "../store/actions/post/action-types";
import moduleNames from "../store/modules/module-names";
import { isAuthenticated } from "../store/getters/getter-types";
import { mapGetters, mapActions } from "vuex";

export default {
  name: "TrendingPanel",
  props: {
    cate: {
      type: String,
      default: null
    }
  },
  computed: {
    ...mapGetters([isAuthenticated]),
    ...mapGetters(moduleNames.post, [posts]),

    isDefault() {
      return this.$router.currentRoute.name === "default";
    }
  },
  data() {
    return {};
  },
  methods: {
    ...mapActions(moduleNames.post, [ADD_VOTE]),
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