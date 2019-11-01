<template>
  <v-card outlined>
    <div class="overline mb-8">{{isDefault ? 'Trending' : `Trending of ${cate}`}}</div>
    <v-card>
      <v-list-item>
      <!-- <--<v-list-item-avatar color="grey"></v-list-item-avatar>--> 
        <v-list-item-content>
          <v-list-item-title class="headline">Our Changing Planet</v-list-item-title>
          <v-list-item-subtitle>by Kurt Wagner</v-list-item-subtitle>
        </v-list-item-content>
      </v-list-item>

      <v-img
        src="https://cdn.vuetifyjs.com/images/cards/mountain.jpg"
        height="200px"
      ></v-img>

      <v-card-text>
        Visit ten places on our planet that are undergoing the biggest changes today.
      </v-card-text>

      <v-card-actions>
        <v-btn
          text
          color="deep-purple accent-4"
          @click="selectPost"
        >
          Read More
        </v-btn>
        <v-spacer></v-spacer>
        <v-btn icon>
        <v-icon>mdi-heart</v-icon>
        </v-btn>
        <v-btn icon>
          <v-icon>mdi-heart-broken</v-icon>
        </v-btn> 
      </v-card-actions>
    </v-card>
    
    <!-- <v-list-item three-line>
      <v-list-item-content>
        
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
    </v-list-item> -->
  </v-card>
</template>
<script>
// import { REGISTER } from "../store/actions/action-types";
import { posts } from "../store/getters/post/getter-types";
import moduleNames from "../store/modules/module-names";
import { mapGetters } from "vuex";
import router from '../router';
export default {
  name: "TrendingPanel",
  props: {
    cate: {
      type: String,
      default: null
    },
    post: {
      type: Object,
      required: true
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
  },
  methods: {
    selectPost() {
      router.push({ name: "post" });
    }
  }
};
</script>