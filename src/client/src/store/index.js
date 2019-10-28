import Vue from "vue";
import Vuex from "vuex";
import rootStore from "./modules/root";
import categoryStore from "./modules/category";
import caveStore from "./modules/cave";

Vue.use(Vuex);

export default new Vuex.Store({
  strict: true,
  modules: {
    category: categoryStore,
    cave: caveStore
  },
  state: rootStore.state,
  getters: rootStore.getters,
  mutations: rootStore.mutations,
  actions: rootStore.actions
});
