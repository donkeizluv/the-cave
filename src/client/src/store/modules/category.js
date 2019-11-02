import {
  CREATE, GET_ALL,
  GET_SELECTED_CATEGORY,
  SET_SELECTED_CATE
} from "../actions/category/action-types";
import {
  SET_CATEGORIES,
  ADD_CATEGORY,
  SELECTED_CATE
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
  [selectedCate]: s => s.selectedCate || {}
};

const mutations = {
  [SET_CATEGORIES]: (s, v) => {
    s.categories = v;
  },
  [ADD_CATEGORY]: (s, v) => {
    s.categories.push(v);
  },
  [SELECTED_CATE]: (s, v) => {
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
    return data;
  },

  [GET_ALL]: async () => {
    return true;
  },
  [SET_SELECTED_CATE]: async ({ commit }, payload) => {
    commit(SELECTED_CATE, payload);
  },
  [GET_SELECTED_CATEGORY]: async (_, payload) => {
    let { data } = await axios.get(`${apis.refresh_selected_category}/${payload}`);
    return data;
  }
};

export default {
  namespaced: true,
  state,
  getters,
  actions,
  mutations
};
