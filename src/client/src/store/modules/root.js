import utilityHelper from "./helper";
import { AUTHENTICATED, CURRENT_USER } from "../mutations/mutation-types";
import moduleNames from "./module-names";
import { SET_POSTS } from "../mutations/post/mutation-types";
import { SET_CATEGORIES } from "../mutations/category/mutation-types";
import {
  LOGIN,
  REGISTER,
  LOGOUT,
  VALIDATE_USER,
  REFRESH_LANDING,
  RELOAD_USER
} from "../actions/action-types";
import {
  isAuthenticated,
  isDev,
  isProd,
  currentUser,
  token
} from "../getters/getter-types";
import axios from "axios";
import apis from "../apis/apis";

const state = {
  isAuthenticated: false,
  currentUser: {
    userId: null,
    username: null
  },
  token: localStorage.getItem("token") || null
};
const getters = {
  [isAuthenticated]: s => s.isAuthenticated,
  [currentUser]: s => s.currentUser,
  [isDev]: () => process.env.VUE_APP_ENV === "dev",
  [isProd]: () => process.env.VUE_APP_ENV === "prod",
  [token]: s => s.token
};
const mutations = {
  [AUTHENTICATED]: (s, v) => {
    s.isAuthenticated = v;
  },
  [CURRENT_USER]: (s, v) => {
    s.currentUser = v;
  }
};
const actions = {
  [LOGIN]: async ({ dispatch, commit }, p) => {
    let result = await dispatch(VALIDATE_USER, p);
    if (result.valid) {
      // set axios bearer
      axios.defaults.headers.common["Authorization"] = `Bearer ${result.token}`;
      localStorage.setItem("token", `${result.token}`);
      commit(CURRENT_USER, p);
      commit(AUTHENTICATED, true);
      return { valid: true, message: result.message };
    }
    return { valid: false, message: result.message };
  },
  [VALIDATE_USER]: async (c, p) => {
    let { data } = await axios.post(apis.validate_user, {
      username: p.username,
      pwd: p.pwd
    });
    return data;
  },
  [REGISTER]: async ({ commit }, p) => {
    let { data } = await axios.post(apis.create_user, {
      username: p.username,
      pwd: p.pwd,
      email: p.email
    });
    axios.defaults.headers.common["Authorization"] = `Bearer ${data.token}`;
    localStorage.setItem("token", `${data.token}`);
    commit(AUTHENTICATED, true);
    commit(CURRENT_USER, p);
    return true;
  },
  [LOGOUT]: async ({ commit }) => {
    commit(AUTHENTICATED, false);
    commit(CURRENT_USER, null);
    localStorage.removeItem("token");
    axios.defaults.headers.common["Authorization"] = null;
  },
  [REFRESH_LANDING]: async ({ commit }) => {
    let { data } = await axios.get(apis.get_landing);
    commit(`${moduleNames.post}/${SET_POSTS}`, data.trendingPosts);
    commit(`${moduleNames.category}/${SET_CATEGORIES}`, data.categories);
  },
  [RELOAD_USER]: async ({ dispatch, commit }, payload) => {
    try {
      axios.defaults.headers.common["Authorization"] = `Bearer ${payload}`;
      let { data } = await axios.get(apis.reload_user);
      commit(CURRENT_USER, {
        userId: data.result.userId,
        username: data.result.username
      });
      commit(AUTHENTICATED, true);
    }
    catch (ex) {
      await dispatch(LOGOUT);
    }

  },
  ...utilityHelper.actions
};

export default {
  state,
  getters,
  actions,
  mutations
};
