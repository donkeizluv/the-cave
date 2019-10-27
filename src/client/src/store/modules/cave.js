import { CREATE, GET_ALL } from "../actions/cave/action-types";
import {
  SET_CAVE,
  ADD_CAVE
} from "../mutations/cave/mutation-types";
import { caves } from "../getters/cave/getter-types";
import axios from "axios";
import apis from "../apis/apis";

const state = {
  caves: []
};

const getters = {
  [caves]: s => s.caves
};

const mutations = {
  [SET_CAVE]: (s, v) => {
    s.caves = v;
  },
  [ADD_CAVE]: (s, v) => {
    s.caves.push(v);
  }
};

const actions = {
  [CREATE]: async ({ commit }, p) => {
    let cave = {
      id: null,
      caveTitle: p.title,
      caveText: p.text,
      caveComments: p.comments
    };
    let { data } = await axios.post(apis.create_cave, cave);
    cave.id = data;
    commit(ADD_CAVE, cave);
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
