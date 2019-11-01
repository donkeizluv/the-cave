import { CREATE, GET_ALL } from "../actions/post/action-types";
import {
  SET_POSTS,
  ADD_POST
} from "../mutations/post/mutation-types";
import { posts } from "../getters/post/getter-types";
import axios from "axios";
import apis from "../apis/apis";

const state = {
  posts: []
};

const getters = {
  [posts]: s => s.posts
};

const mutations = {
  [SET_POSTS]: (s, v) => {
    s.posts = v;
  },
  [ADD_POST]: (s, v) => {
    s.posts.push(v);
  }
};

const actions = {
  [CREATE]: async (p) => {
    let post = {
      postTitle: p.title,
      content: p.text
    };
    await axios.post(apis.create_post, post);
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
