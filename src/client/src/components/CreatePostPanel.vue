<template>
  <v-card class="mx-auto" width="auto">
    <v-card-title>Create Your Post</v-card-title>
    <v-card-text class="text--primary">
      <v-text-field 
      v-model="post.title" 
      label="Title" 
      class="input-group--focused"
      maxlength="300"
      single-line
      outlined></v-text-field>
      <vue-editor v-model="post.content" :editorToolbar="customToolbar" class="mb-6"></vue-editor> 
      <div
        class="image-input"
        :style="{ 'background-image': `url(${imageData})` }"
        @click="chooseImage"
      >
        <span v-if="!imageData" class="placeholder">Choose an Image</span>
        <input
          class="file-input"
          ref="fileInput"
          type="file"
          accept="image/x-png, image/gif, image/jpeg"
          @input="onSelectFile"
        />
      </div>
      
    </v-card-text>
    <v-card-actions>
      <v-btn text color="deep-purple accent-4" @click="create">Create</v-btn>
      <v-btn text color="deep-purple accent-4" @click.stop="confirm = true">Cancel</v-btn>
    </v-card-actions>
    <v-dialog
      v-model="confirm"
      max-width="290"
    >
      <v-card>
        <v-card-title class="headline">You are creating post. Do you wish to exit?</v-card-title>

        <v-card-actions>
          <v-spacer></v-spacer>

          <v-btn
            color="green darken-1"
            text
            @click="clearAll"
          >
            OK
          </v-btn>

          <v-btn
            color="green darken-1"
            text
            @click="confirm = false"
          >
            Cancel
          </v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </v-card>
</template>
<style scoped>
.image-input {
  display: block;
  width: auto;
  height: 200px;
  cursor: pointer;
  background-size: cover;
  background-position: center center;
}

.placeholder {
  background: #f0f0f0;
  width: 100%;
  height: 100%;
  display: flex;
  justify-content: center;
  align-items: center;
  color: #333;
  font-size: 18px;
  font-family: Helvetica;
}

.placeholder:hover {
  background: #e0e0e0;
}
.file-input {
  display: none;
}

* {
  border-radius: 4px important!;
}

</style>
<script>
import { mapActions, mapGetters } from "vuex";
import { isAuthenticated } from "../store/getters/getter-types";
import moduleNames from "../store/modules/module-names";
import { CREATE } from "../store/actions/post/action-types";
import { VueEditor } from "vue2-editor";

export default {
  components: {
    VueEditor
  },
  name: "CreatePostPanel",
  props: {
    cateID: {
      default: ""
    }
  },
  computed: {
    ...mapGetters([isAuthenticated])
  },
  data: function name() {
    return {
      post: {
        title: "",
        content: "",
        imgData: "",
        cateID: ""
      },
      imageData: null,
      customToolbar: [
        ["bold", "italic", "underline"], 
        [{ list: "ordered" }, { list: "bullet" }]
      ],
      confirm: {
        type: Boolean,
        default: false
      }
    };
  },
  methods: {
    clearAll() {
      this.post.title = null;
      this.post.content = null;
      this.imageData = null;
      this.confirm = false;
    },
    ...mapActions(moduleNames.post, [CREATE]),
    async create() {
      this.post.cateID = this.cateID;
      this.post.imgData = this.imageData;
      let postId = await this.CREATE(this.post);
      this.$router.push({ name: "post", params: { postId: postId } });
    },
    chooseImage() {
      this.$refs.fileInput.click();
    },

    onSelectFile() {
      const input = this.$refs.fileInput;
      const files = input.files;
      if (files && files[0] && files[0].size <= 120000) {
        const reader = new FileReader();
        reader.onload = e => {
          this.imageData = e.target.result;
          console.log(this.imageData);
        };
        reader.readAsDataURL(files[0]);
        this.$emit("input", files[0]);
      } else if (files[0].size > 120000) {
        alert("File too big");
      }
    }
  },
  mounted() {
    this.confirm = false;
  }
};
</script>