import { CREATE, GET_ALL } from "../actions/category/action-types";
import {
  SET_CATEGORIES,
  ADD_CATEGORY
} from "../mutations/category/mutation-types";
import { categories } from "../getters/category/getter-types";
import axios from "axios";
import apis from "../apis/apis";

const state = {
  categories: []
};

const getters = {
  [categories]: s => s.categories
};

const mutations = {
  [SET_CATEGORIES]: (s, v) => {
    s.categories = v;
  },
  [ADD_CATEGORY]: (s, v) => {
    s.categories.push(v);
  }
};

const actions = {
  [CREATE]: async ({ commit }, p) => {
    let cate = {
      id: null,
      cateName: p.cateName,
      description: p.description
    };
    let { data } = await axios.post(apis.create_categories, cate);
    cate.id = data;
    commit(ADD_CATEGORY, cate);
  },

  [GET_ALL]: async () => {
    return true;
  }
};

export default {
  namespaced: true,
  state,
  getters,
  actions,
  mutations
};
