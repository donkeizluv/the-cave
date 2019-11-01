<template>
  <v-container>
    <!-- <div :class="['ma-2', item.children.length > 0 ? 'comment-border' : '' ]"> -->
    <div class="comment-border ml-2">
      <v-row dense>
        <v-col class="pa-0 pl-1" cols="12">
          <div class="ma-2 font-weight-bold">{{ item.data.username }}</div>
        </v-col>
        <v-col class="pa-0 pl-1" cols="12">
          <div class="ml-2 ma-1">{{ item.data.content }}</div>
        </v-col>
      </v-row>
      <v-row dense></v-row>
      <v-row dense>
        <v-col>
          <v-btn
            @click="showReply = !showReply; newComment= null;"
            small
            icon
            class="comment-actions ma-2"
          >
            <v-icon>mdi-message</v-icon>
          </v-btn>
          <v-btn small icon class="comment-actions ma-2">
            <v-icon>mdi-thumb-up</v-icon>
          </v-btn>
          <span>0</span>
          <v-btn small icon class="comment-actions ma-2">
            <v-icon>mdi-thumb-down</v-icon>
          </v-btn>
          <span>0</span>
        </v-col>
      </v-row>
      <template v-if="showReply">
        <v-row dense>
          <v-col cols="10">
            <v-row dense>
              <v-col>
                <div class="ma-2 comment-text-area comment-textarea-border">
                  <v-textarea
                    v-model="newComment"
                    :label="`Replying to ${item.data.username}`"
                    clear-icon
                    dense
                    auto-grow
                    flat
                    solo
                    full-width
                    persistent-hint
                  ></v-textarea>
                </div>
              </v-col>
            </v-row>
            <v-row dense>
              <v-col align="end">
                <v-btn small text @click="showReply = false; newComment= null;">Cancel</v-btn>
                <v-btn
                  small
                  text
                  color="primary"
                  :disabled="!canSubmit"
                  @click="submitNewComment"
                >OK</v-btn>
              </v-col>
            </v-row>
          </v-col>
        </v-row>
      </template>
    </div>
  </v-container>
</template>
<script>
import { NEW_COMMENT } from "../store/actions/post/action-types";
import moduleNames from "../store/modules/module-names";
import { mapActions } from "vuex";

export default {
  name: "Comment",
  props: {
    item: {
      type: Object,
      required: true
    }
  },
  computed: {
    canSubmit() {
      return !!this.newComment;
    }
  },
  data: () => ({
    newComment: null,
    showReply: false
  }),
  methods: {
    ...mapActions(moduleNames.post, [NEW_COMMENT]),
    async submitNewComment() {
      await this.NEW_COMMENT({
        parentId: this.item.data.parentId,
        content: this.newComment
      });
    }
  }
};
</script>
<style lang="scss" scoped>
@import "../styles/modules/comment-tree.scss";
</style>