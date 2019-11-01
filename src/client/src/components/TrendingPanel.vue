<template>
  <v-card class="mx-auto" outlined>
    <v-list-item three-line>
      <v-list-item-content>
        <div class="overline mb-8">{{isDefault ? 'Trending' : `Trending of ${cate}`}}</div>
        <v-card class="d-inline-block mx-auto">
          <v-container>
            <v-row justify="space-between">
              <v-col cols="auto">
                <v-img
                  height="200"
                  width="200"
                  src="https://cdn.vuetifyjs.com/images/cards/store.jpg"
                ></v-img>
              </v-col>
              <v-col cols="auto">
                <v-card-text>This is the trending cave</v-card-text>
              </v-col>
            </v-row>
          </v-container>
        </v-card>
      </v-list-item-content>
    </v-list-item>
  </v-card>
</template>
<script>
// import { REGISTER } from "../store/actions/action-types";
import { posts } from "../store/getters/post/getter-types";
import moduleNames from "../store/modules/module-names";
import { mapGetters } from "vuex";
export default {
  props: {
    cate: {
      type: String,
      default: null
    }
  },
  computed: {
    ...mapGetters(moduleNames.post, [posts]),
    isDefault() {
      return this.$router.currentRoute.name === "default";
    }
  },
  data() {
    return {};
  }
};
</script>