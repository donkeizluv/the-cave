<template>
  <v-container class="ma-0 pa-0" fluid>
    <!-- <v-col>
      <v-row justify="end" dense>
        <v-col class="ml-auto">
          <div class="overline ma-4">{{isDefault ? 'Trending' : `Trending of ${cate}`}}</div>
        </v-col>
        <v-col cols="auto">
          <v-btn
            text
            color="blue"
            :disabled="!isAuthenticated"
            @click="$router.push({ name: 'create_post'})"
          >New Post</v-btn>
        </v-col>
        <div>
          <v-text-field small append-icon="mdi-magnify"></v-text-field>
        </div>
      </v-row>
    </v-col>-->
    <v-col class="pa-0 ma-0">
      <v-row dense v-for="post in posts" v-bind:key="post.ID">
        <v-card :width="'100%'" flat outlined class="overline mb-4">
          <v-list-item>
            <!-- <--<v-list-item-avatar color="grey"></v-list-item-avatar>-->
            <v-list-item-content>
              <v-list-item-title class="headline">{{ post.title }}</v-list-item-title>
              <v-list-item-subtitle>{{post.username}} - {{ createdTimeAgo(post.created) }}</v-list-item-subtitle>
            </v-list-item-content>
          </v-list-item>

          <v-img v-if="post.imgData" v-bind:src="post.imgData" height="200px"></v-img>

          <v-card-text>{{ post.content }}</v-card-text>

          <v-card-actions>
            <v-btn text color="blue" @click="selectPost(post)">Read More</v-btn>
            <v-spacer></v-spacer>
            <v-btn icon>
              <v-icon color="red lighten-2">mdi-heart</v-icon>
            </v-btn>
            <v-btn icon>
              <v-icon color="red lighten-2">mdi-heart-broken</v-icon>
            </v-btn>
          </v-card-actions>
        </v-card>
      </v-row>
    </v-col>
  </v-container>
</template>
<script>
import { timeAgo } from "./shared/utils";
import { posts } from "../store/getters/post/getter-types";
import moduleNames from "../store/modules/module-names";
import { isAuthenticated } from "../store/getters/getter-types";
import { mapGetters } from "vuex";
import router from "../router";
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
    createdTimeAgo(t) {
      return timeAgo.format(new Date(t));
    },
    selectPost(post) {
      router.push({ name: "post", params: { postId: post.id } });
    }
  }
};
</script>