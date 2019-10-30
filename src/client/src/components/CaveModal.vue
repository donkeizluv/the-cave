<template>
  <v-dialog :value="show" @click:outside="hideDialog" max-width="700px" width="700px" height="1090px">
    <v-card>
      <v-card-title>
        <span class="headline mb-4">{{ cave.title }}</span>
      </v-card-title>
      <v-card-text>
        {{ cave.text }}
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
      <v-card v-for="comment in cave.comments" width="auto" v-bind:key="comment.id">
        <v-list-item>
          <v-list-item-content>
            <div class="overline mb-4">{{ comment.name }}</div>
            <v-list-item-subtitle>{{ comment.content }}
              <v-btn icon style="float:right;">
                <v-icon>mdi-heart-broken</v-icon>
              </v-btn>
              <v-btn icon style="float:right;">
                <v-icon>mdi-heart</v-icon>
              </v-btn>
            </v-list-item-subtitle>
          </v-list-item-content>
        </v-list-item>
      </v-card>
    </v-card>
  </v-dialog>
</template>

<script>
// import { LOGIN } from "../store/actions/action-types";
// import { mapActions } from "vuex";
export default {
  name: "CaveModal",
  props: {
    show: {
      required: true
    }
  },
  computed: {
  },
  data() {
    return {
      cave: {
        title: "A Very Funny Folk",
        text: "Once upon a time, and the end",
        comments:
        [{
          id: 1,
          name: "Annonymous",
          content: "Positive Comments"
        },
        {
          id: 2,
          name: "Annonymousse",
          content: "Negative Comments"
        }]
      }
    };
  },
  methods: {
    clearAll() {
      this.title = null;
      this.text = null;
      this.comments = null;
    },
    hideDialog() {
      this.clearAll();
      this.$emit("click:outside");
    }
  }
};
</script>
