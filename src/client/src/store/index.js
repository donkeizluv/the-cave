import Vue from "vue";
import Vuex from "vuex";
import rootStore from "./modules/root";
import categoryStore from "./modules/category";
import postStore from "./modules/post";
import moduleNames from "./modules/module-names";

Vue.use(Vuex);

export default new Vuex.Store({
  strict: true,
  modules: {
    [moduleNames.category]: categoryStore,
    [moduleNames.post]: postStore
  },
  state: rootStore.state,
  getters: rootStore.getters,
  mutations: rootStore.mutations,
  actions: rootStore.actions
});
