<template>
    <v-card>
      <v-card-title>
          {{ title }}
      </v-card-title>
      <v-card-subtitle> {{ created }} </v-card-subtitle>
      <v-card-text>
        {{ c }}
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
// import { mapActions } from "vuex";
export default {
  name: "Post",
  props: {
    title: {
      type: String, 
      default: "Dummy"
    },
    content: {
      type: String,
      default: "Hello World"
    },
    comments : {
      type: Array
    },
    upVotes: {
      default: 0
    },
    downVotes: {
      default: 0
    },
    created: {
      type: Date,
      default: "00/00/0000"
    }
  },
  computed: {
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
