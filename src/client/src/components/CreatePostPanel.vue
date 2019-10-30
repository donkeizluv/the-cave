<template>
    <v-card
    class="mx-auto"
    width="auto"
    >
        <v-card-title>
            Create Your Post
        </v-card-title>
        <v-card-text class="text--primary">
            <v-textarea
            autocomplete=""
            label="Post"
            v-model="post.text"
            ></v-textarea>

        </v-card-text>
        <v-card-actions>
            <v-btn
                text
                color="deep-purple accent-4"
                @click="CREATE_POST"
            >
                Create
            </v-btn>
            <v-btn
                text
                color="deep-purple accent-4"
                @click="clearAll"
            >
                Cancel
            </v-btn>
        </v-card-actions>
    </v-card>
</template>
<script>
import { mapActions } from "vuex";
//import moduleNames from "../store/modules/module-names";
//import { CREATE_POST } from "../store/actions/createPost/action-types";

export default {
  name: "CreatePostPanel",
  props: {
  },
  computed: {
  },
  data: function name() {
    return {
      post: {
        text: ""
      }
    };
  },
  methods: {
    clearAll() {
      this.post.text = null;
    },
    //...mapActions(moduleNames.createPost, [CREATE_POST])
  }
};
</script>