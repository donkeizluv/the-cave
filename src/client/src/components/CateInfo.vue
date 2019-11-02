<template>
  <v-card class="mx-auto" outlined>
    <v-container v-if="cate">
      <v-row dense>
        <v-col class="text-center headline blue--text text--darken-1">
          <div>{{ cate.cateName }}</div>
        </v-col>
      </v-row>
      <v-row dense>
        <v-col class="ma-4">
          <v-row justify="center" class="grey--text text--light-2">Total post:</v-row>
          <v-row justify="center">{{ cate.postCount }}</v-row>
        </v-col>
        <v-col class="ma-4">
          <v-row justify="center" class="grey--text text--light-2">Created:</v-row>
          <v-row justify="center">{{ createdTimeAgo(cate.created) }}</v-row>
        </v-col>
      </v-row>
      <v-row dense>
        <v-col>
          <div class="text-center ma-2">{{ cate.description }}</div>
        </v-col>
      </v-row>
      <v-row dense>
        <v-col align="center">
          <v-btn
            class="mt-2"
            min-width="100%"
            color="primary"
            :disable="isAuthenticated"
            @click="$router.push({ name: 'create_post', params: { cateID: cate.id } })"
          >Create Post</v-btn>
        </v-col>
      </v-row>
    </v-container>
    <v-container v-else></v-container>
  </v-card>
</template>

<script>
import { timeAgo } from "./shared/utils";
import { mapGetters } from "vuex";
import { isAuthenticated } from "../store/getters/getter-types";
export default {
  name: "CateInfo",
  props: {
    cate: {
      type: Object,
      required: true
    }
  },
  computed: {
    ...mapGetters([isAuthenticated])
  },
  data: function name() {
    return {};
  },
  methods: {
    createdTimeAgo(t) {
      return timeAgo.format(new Date(t));
    }
  }
};
</script>