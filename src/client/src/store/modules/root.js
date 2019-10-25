import utilityHelper from "./helper";
import { AUTHENTICATED, CURRENT_USER } from "../mutations/mutation-types";
import { LOGIN, REGISTER, VALIDATE_USER } from "../actions/action-types";
import {
  isAuthenticated,
  isDev,
  isProd,
  currentUser
} from "../getters/getter-types";
import axios from "axios";
import apis from "../apis/apis";

const state = {
  isAuthenticated: false,
  currentUser: {
    userId: null,
    username: null
  }
};
const getters = {
  [isAuthenticated]: s => s.isAuthenticated,
  [currentUser]: s => s.currentUser,
  [isDev]: () => process.env.VUE_APP_ENV === "dev",
  [isProd]: () => process.env.VUE_APP_ENV === "prod"
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
    if (result) {
      commit(AUTHENTICATED, true);
      commit(CURRENT_USER, p);
      return true;
    }
    return false;
  },
  [VALIDATE_USER]: async (c, p) => {
    let { data } = await axios.post(apis.validate_user, {
      username: p.username,
      pwd: p.pwd
    });
    return data;
  },
  [REGISTER]: async ({ commit }, p) => {
    await axios.post(apis.create_user, {
      username: p.username,
      pwd: p.pwd,
      email: p.email
    });
    commit(AUTHENTICATED, true);
    commit(CURRENT_USER, p);
    return true;
  },
  ...utilityHelper.actions
};

export default {
  state,
  getters,
  actions,
  mutations
};
