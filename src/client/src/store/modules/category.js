import { CREATE, GET_ALL, REFRESH_SELECTED_CATEGORY } from "../actions/category/action-types";
import {
  SET_CATEGORIES,
  ADD_CATEGORY,
  SET_SELECTED_CATE
} from "../mutations/category/mutation-types";
import { categories, selectedCate } from "../getters/category/getter-types";
import axios from "axios";
import apis from "../apis/apis";

const state = {
  categories: [],
  selectedCate: null
};

const getters = {
  [categories]: s => s.categories,
  [selectedCate]: s => s.selectedCate
};

const mutations = {
  [SET_CATEGORIES]: (s, v) => {
    s.categories = v;
  },
  [ADD_CATEGORY]: (s, v) => {
    s.categories.push(v);
  },
  [SET_SELECTED_CATE]: (s, v) => {
    s.selectedCate = v;
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
  },

  [REFRESH_SELECTED_CATEGORY]: async ({ commit }, payload) => {
    console.log(apis.refresh_selected_category);
    let { data } = await axios.get(`${apis.refresh_selected_category}/${payload}`);
    commit(SET_SELECTED_CATE, data);
  }
};

export default {
  namespaced: true,
  state,
  getters,
  actions,
  mutations
};
