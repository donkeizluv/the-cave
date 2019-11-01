<template>
    <v-card v-if="selectedPost != null">
      <v-card-title>
          {{ selectedPost.title }}
      </v-card-title>
      <v-card-subtitle> {{ selectedPost.created }} </v-card-subtitle>
      <v-card-text>
        {{ selectedPost.content }}
      </v-card-text>
      <v-card-actions>
        <v-spacer></v-spacer>
        <v-btn icon>
          <v-icon>mdi-heart</v-icon>
        </v-btn>
        <v-btn icon>
          <v-icon>mdi-heart-broken</v-icon>
        </v-btn>
      </v-card-actions>
      <v-card v-for="comment in comments" width="auto" v-bind:key="comment.id">
        <v-list-item>
          <v-list-item-content>
            <div class="overline mb-4">{{ comment.name }}</div>
            <v-list-item-subtitle>
              {{ comment.content }}
              <v-btn icon style="float:right;">
                <v-icon>mdi-heart-broken</v-icon>
              </v-btn>
              <v-btn icon style="float:right;">
                <v-icon>mdi-heart</v-icon>
              </v-btn>
              <v-card-text>
                <v-text-field
                autocomplete=""
                label="Comment"
                v-model="newComment"
                ></v-text-field>
              </v-card-text>
            </v-list-item-subtitle>
          </v-list-item-content>
        </v-list-item>
      </v-card>
      <v-card>
        <v-card-text>
          <v-text-field
          autocomplete=""
          label="Comment"
          v-model="newComment"
          ></v-text-field>
        </v-card-text>
      </v-card>
    </v-card>
</template>

<script>
// import { LOGIN } from "../store/actions/action-types";
import { mapGetters } from "vuex";
import moduleNames from "../store/modules/module-names";
import { selectedPost } from '../store/getters/post/getter-types';
export default {
  name: "Post",
  props: {
  },
  computed: {
    ...mapGetters(moduleNames.post, [selectedPost])
  },
  data() {
    return {
      newComment: ""
    };
  },
  methods: {

  }
};
</script>
