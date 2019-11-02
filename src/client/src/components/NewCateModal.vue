<template>
  <v-dialog :value="show" persistent width="400px">
    <v-card>
      <v-card-title>
        <span class="headline">Create new category</span>
        <v-spacer></v-spacer>
        <span class="mb-4">
          <v-btn icon @click="hideModal">
            <v-icon>mdi-close</v-icon>
          </v-btn>
        </span>
      </v-card-title>
      <v-card-text>
        <v-form v-model="formValid">
          <v-text-field
            v-model.trim="cate.cateName"
            maxlength="24"
            :rules="rules.name"
            label="Name"
          ></v-text-field>
          <v-text-field
            v-model.trim="cate.description"
            maxlength="64"
            :rules="rules.desc"
            label="Description"
            counter
          ></v-text-field>
        </v-form>
      </v-card-text>
      <div
        v-if="dialogMessage"
        :class="[isError ? 'red--text' : 'green--text', ' text-center mb-2']"
      >
        <span>{{dialogMessage}}</span>
      </div>
      <v-card-actions class="pa-0 justify-center">
        <v-btn
          @click="createCate"
          :loading="isLoading"
          class="mb-6"
          color="primary"
          :disabled="!canSubmit"
        >Create</v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script>
import moduleNames from "../store/modules/module-names";
import validationRules from "./shared/validation-rules";
// import { isAuthenticated } from "../store/getters/getter-types";
import { mapActions } from "vuex";
import { CREATE } from "../store/actions/category/action-types";
export default {
  name: "NewCatModal",
  props: {
    show: {
      required: true
    }
  },
  computed: {
    // ...mapGetters([isAuthenticated]),
    canSubmit() {
      return this.formValid;
    }
  },
  data() {
    return {
      cate: {
        cateName: null,
        description: null
      },
      rules: {
        name: [
          value => validationRules.requiredValue(value),
          value => validationRules.noSpecial(value),
          value => validationRules.minCharacter(value, 4),
          value => validationRules.maxCharacter(value, 24)
        ],
        desc: [value => validationRules.maxCharacter(value, 64)]
      },
      isError: false,
      dialogMessage: null,
      isLoading: false,
      formValid: null
    };
  },
  methods: {
    ...mapActions(moduleNames.category, [CREATE]),
    clearAll() {
      this.cate = { cateName: null, description: null };
      this.isError = false;
      this.dialogMessage = null;
    },
    hideModal() {
      this.clearAll();
      this.$emit("click:outside");
    },
    async createCate() {
      if (!this.canSubmit) return;
      try {
        this.isLoading = true;
        let newCateId = await this.CREATE(this.cate);
        this.$router.push({
          name: "cave",
          params: { cate: this.cate.cateName }
        });
        this.hideModal();
      } catch (error) {
        this.isError = true;
        this.dialogMessage =
          error.response.data || "Failed to create new category";
      } finally {
        this.isLoading = false;
      }
    }
  }
};
</script>
