import Vue from "vue";
import Vuex from "vuex";
import rootStore from "./modules/root";
import categoryStore from "./modules/category";

Vue.use(Vuex);

export default new Vuex.Store({
  strict: true,
  modules: {
    category: categoryStore
  },
  state: rootStore.state,
  getters: rootStore.getters,
  mutations: rootStore.mutations,
  actions: rootStore.actions
});
