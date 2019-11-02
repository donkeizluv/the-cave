<template>
  <v-card class="mx-auto" outlined>
    <div class="overline ma-4">
      Categories
      <v-btn class="mb-1" small icon :disabled="!isAuthenticated" @click="$emit('click:newcat')">
        <v-icon>mdi-plus</v-icon>
      </v-btn>
    </div>
    <!-- <v-divider class="mx-4"></v-divider> -->
    <template v-for="cate in categories">
      <v-list-item :key="cate.id" class="text-center">
        <v-list-item-content>
          <v-btn
            class="no-texttransform cate-name"
            link
            text
            color="primary"
            small
            @click="onSelectCate(cate)"
          >{{cate.cateName}}</v-btn>
        </v-list-item-content>
      </v-list-item>
      <v-divider class="mx-4" :key="`${cate.id}1`"></v-divider>
    </template>
  </v-card>
</template>

<script>
import moduleNames from "../store/modules/module-names";
import { isAuthenticated } from "../store/getters/getter-types";
import { mapGetters, mapActions } from "vuex";
import { categories } from "../store/getters/category/getter-types";
import {
  REFRESH_POSTS_BY_CATE
} from "../store/actions/post/action-types";
export default {
  name: "CatePanel",
  props: {},
  computed: {
    ...mapGetters([isAuthenticated]),
    ...mapGetters(moduleNames.category, [categories])
  },
  data: function name() {
    return {};
  },
  methods: {
    ...mapActions(moduleNames.post, [REFRESH_POSTS_BY_CATE]),
    onSelectCate(cate) {
      console.log(cate);
      this.REFRESH_POSTS_BY_CATE(cate.id);
      this.$router.push({ name: 'category', params: { cate: cate.cateName }});
    }
  }
};
</script>
<style lang="scss" scoped>
@import "../styles/modules/cat-panel.scss";
</style>
