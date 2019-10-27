<template>
  <v-dialog :value="show" @click:outside="hideModal" width="400px">
    <v-card>
      <v-card-title>
        <span class="headline">Create new category</span>
      </v-card-title>
      <v-card-text>
        <v-form v-model="formValid">
          <v-text-field v-model.trim="cat.catName" maxlength="24" :rules="rules.name" label="Name">
          </v-text-field>
          <v-text-field
            v-model.trim="cat.description"
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
      <v-card-actions>
        <v-spacer></v-spacer>
        <v-btn @click="createCat" color="primary" :disabled="!canSubmit">Create</v-btn>
        <v-btn @click="hideModal">Cancel</v-btn>
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
  data: function name() {
    return {
      cat: {
        catName: null,
        description: null
      },
      rules: {
        name: [
          value => validationRules.requiredValue(value),
          value => validationRules.noSpecialButSpace(value),
          value => validationRules.minCharacter(value, 6),
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
      this.cat = { catName: null, description: null };
      this.isError = false;
      this.dialogMessage = null;
    },
    hideModal() {
      this.clearAll();
      this.$emit("click:outside");
    },
    async createCat() {
      if (!this.canSubmit) return;
      try {
        this.isLoading = true;
        await this.CREATE(this.cat);
        this.hideModal();
        //NYI: go to this cat
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
