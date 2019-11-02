<template>
  <v-card class="mx-auto" width="auto">
    <v-card-title>Create Your Post</v-card-title>
    <v-card-text class="text--primary">
      <v-text-field 
      v-model="post.title" 
      label="Title" 
      class="input-group--focused mb-4"
      counter
      maxlength="50"
      single-line
      outlined></v-text-field>
      <vue-editor v-model="post.content" :editorToolbar="customToolbar" class="mb-4"></vue-editor> 
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
      <v-btn text color="deep-purple accent-4" @click="clearAll">Cancel</v-btn>
    </v-card-actions>
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
      default: "123123123123123123123125"
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
      ]
    };
  },
  methods: {
    clearAll() {
      this.post.text = null;
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
      console.log(input);
      if (files && files[0]) {
        const reader = new FileReader();
        reader.onload = e => {
          this.imageData = e.target.result;
          console.log(this.imageData);
        };
        reader.readAsDataURL(files[0]);
        this.$emit("input", files[0]);
      }
    }
  }
};
</script>