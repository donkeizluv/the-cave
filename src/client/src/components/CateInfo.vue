<template>
  <v-card class="mx-auto" outlined v-if="selectedCate">
    <v-container>
      <v-row dense>
        <v-col class="text-center blue--text text--darken-1">
          <div>{{ selectedCate.name }}</div>
        </v-col>
      </v-row>
      <v-row dense>
        <v-col class="ma-4">
          <v-row class="grey--text text--light-2">Total post:</v-row>
          <v-row>{{ selectedCate.noOfPost }}</v-row>
        </v-col>
        <v-col class="ma-4">
          <v-row class="grey--text text--light-2">Created:</v-row>
          <v-row>{{ selectedCate.createdDate }}</v-row>
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
            :disabled="isAuthenticated"
            @click="$router.push({ name: 'create_post', params: { cateID: selectedCate.id } })"
          >Create Post</v-btn>
        </v-col>
      </v-row>
    </v-container>
  </v-card>
</template>

<script>
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
  methods: {}
};
</script>