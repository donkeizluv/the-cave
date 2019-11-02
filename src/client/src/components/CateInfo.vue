<template>
  <v-card class="mx-auto" outlined>
    <v-container v-if="cate">
      <v-row dense>
        <v-col class="text-center headline cyan--text text--light-2">
          <div>{{ currentCate.cateName }}</div>
        </v-col>
      </v-row>
      <v-row dense>
        <v-col class="ma-4">
          <v-row justify="center" class="grey--text text--light-2">Total post:</v-row>
          <v-row justify="center">{{ currentCate.postCount }}</v-row>
        </v-col>
        <v-col class="ma-4">
          <v-row justify="center" class="grey--text text--light-2">Created:</v-row>
          <v-row justify="center">{{ createdTimeAgo(currentCate.created) }}</v-row>
        </v-col>
      </v-row>
      <v-row dense>
        <v-col>
          <div class="text-center ma-2">{{ currentCate.description }}</div>
        </v-col>
      </v-row>
      <v-row dense>
        <v-col align="center">
          <v-btn
            class="mt-2"
            min-width="100%"
            color="light-green accent-3"
            v-if="isAuthenticated"
            @click="$router.push({ name: 'create_post', params: { cateID: currentCate.id } })"
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
    ...mapGetters([isAuthenticated]),
    currentCate() {
      return this.cate;
    }
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