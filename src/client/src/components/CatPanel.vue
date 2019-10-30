<template>
  <v-card class="mx-auto" outlined>
    <v-list-item three-line>
      <v-list-item-content>
        <div class="overline mb-8">
          Categories
          <v-btn
            class="mb-1"
            small
            icon
            :disabled="!isAuthenticated"
            @click="$emit('click:newcat')"
          >
            <v-icon>mdi-plus</v-icon>
          </v-btn>
        </div>
      </v-list-item-content>
    </v-list-item>
    <!-- <v-divider class="mx-4"></v-divider> -->
    <template v-for="cat in categories">
      <v-list-item :key="cat.id" class="text-center">
        <v-list-item-content>
          <v-btn
            class="no-texttransform cate-name"
            link
            text
            color="primary"
            small
            @click="$router.push({ name: 'category', params: { cate: cat.catName } })"
          >{{cat.catName}}</v-btn>
        </v-list-item-content>
      </v-list-item>
      <v-divider class="mx-4" :key="`${cat.id}1`"></v-divider>
    </template>
  </v-card>
</template>

<script>
import moduleNames from "../store/modules/module-names";
import { isAuthenticated } from "../store/getters/getter-types";
import { mapGetters } from "vuex";
import { categories } from "../store/getters/category/getter-types";
export default {
  name: "CatPanel",
  props: {},
  computed: {
    ...mapGetters([isAuthenticated]),
    ...mapGetters(moduleNames.category, [categories])
  },
  data: function name() {
    return {};
  },
  methods: {
    // ...mapActions(moduleNames.category, [CREATE])
  }
};
</script>
<style lang="scss" scoped>
@import "../styles/modules/cat-panel.scss";
</style>
