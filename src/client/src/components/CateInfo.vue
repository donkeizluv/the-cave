<template>
  <v-card class="mx-auto" outlined>
    <v-container>
      <v-row dense>
        <v-col class="text-center blue--text text--darken-1">
          <div>ApocalypseCigarettes</div>
        </v-col>
      </v-row>
      <v-row dense>
        <v-col class="ma-4">
          <v-row class="grey--text text--light-2">Total post:</v-row>
          <v-row>2444</v-row>
        </v-col>
        <v-col class="ma-4">
          <v-row class="grey--text text--light-2">Created:</v-row>
          <v-row>20/20/2020</v-row>
        </v-col>
      </v-row>
      <v-row dense>
        <v-col>
          <div>cate desc cate desc cate desc cate desc</div>
        </v-col>
      </v-row>
      <v-row dense>
        <v-col align="center">
          <v-btn
            class="mt-2"
            min-width="100%"
            color="primary"
            @click="$router.push({ name: 'create_post'})"
          >Create Post</v-btn>
        </v-col>
      </v-row>
    </v-container>
  </v-card>
</template>

<script>
export default {
  name: "CateInfo",
  props: {},
  computed: {},
  data: function name() {
    return {};
  },
  methods: {}
};
