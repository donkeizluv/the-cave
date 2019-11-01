<template>
  <v-card outlined>
    <v-row dense>
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
    </v-row>

    <v-card outlined class="overline mb-8" v-for="post in posts" v-bind:key="post.ID">
      <v-list-item>
        <!-- <--<v-list-item-avatar color="grey"></v-list-item-avatar>-->
        <v-list-item-content>
          <v-list-item-title class="headline">{{ post.title }}</v-list-item-title>
          <v-list-item-subtitle>{{ post.created }}</v-list-item-subtitle>
        </v-list-item-content>
      </v-list-item>

      <v-img src="https://cdn.vuetifyjs.com/images/cards/mountain.jpg" height="200px"></v-img>

      <v-card-text>{{ post.content }}</v-card-text>

      <v-card-actions>
        <v-btn text color="blue" @click="selectPost(post)">Read More</v-btn>
        <v-spacer></v-spacer>
        <v-btn icon>
          <v-icon color="red">mdi-heart</v-icon>
        </v-btn>
        <v-btn icon>
          <v-icon color="red">mdi-heart-broken</v-icon>
        </v-btn>
      </v-card-actions>
    </v-card>
  </v-card>
</template>
<script>
import { posts } from "../store/getters/post/getter-types";
import moduleNames from "../store/modules/module-names";
import { isAuthenticated } from "../store/getters/getter-types";
import { mapGetters, mapActions } from "vuex";
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
    selectPost(post) {
      router.push({ name: "post", params: { postId: post.id } });
    }
  }
};
</script>