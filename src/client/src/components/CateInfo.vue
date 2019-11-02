<template>
  <v-card class="mx-auto" outlined >
    <v-container v-if="selectedCate">
      <v-row dense>
        <v-col class="text-center blue--text text--darken-1">
          <div>{{ selectedCate.cateName }}</div>
        </v-col>
      </v-row>
      <v-row dense>
        <v-col class="ma-4">
          <v-row class="grey--text text--light-2">Total post:</v-row>
          <v-row>{{ selectedCate.postCount }}</v-row>
        </v-col>
        <v-col class="ma-4">
          <v-row class="grey--text text--light-2">Created:</v-row>
          <v-row>{{ createdTimeAgo(selectedCate.created) }}</v-row>
        </v-col>
      </v-row>
      <v-row dense>
        <v-col>
          <div>{{ selectedCate.description }}</div>
        </v-col>
      </v-row>
      <v-row dense>
        <v-col align="center">
          <v-btn
            class="mt-2"
            min-width="100%"
            color="primary"
            :disable="isAuthenticated"
            @click="$router.push({ name: 'create_post', params: { cateID: selectedCate.id } })"
          >Create Post</v-btn>
        </v-col>
      </v-row>
    </v-container>
    <v-container v-else>

    </v-container>
  </v-card>
</template>

<script>
import { timeAgo } from "./shared/utils";
import { selectedCate } from '../store/getters/category/getter-types';
import { mapGetters } from "vuex";
import { isAuthenticated } from "../store/getters/getter-types";
import moduleNames from "../store/modules/module-names";
export default {
  name: "CateInfo",
  props: {
  },
  computed: {
    ...mapGetters([isAuthenticated]),
    ...mapGetters(moduleNames.category, [selectedCate])
  },
  data: function name() {
    return {};
  },
  methods: {
    createdTimeAgo(t) {
      return timeAgo.format(new Date(t));
    },
  }
};
</script>