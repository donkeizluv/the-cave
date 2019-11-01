<<<<<<< Updated upstream
import { CREATE, GET_ALL, NEW_COMMENT } from "../actions/post/action-types";
=======
import { CREATE, GET_ALL, GET_POSTS_BY_CATE, GET_SELECTED_POST } from "../actions/post/action-types";
>>>>>>> Stashed changes
import {
  SET_POSTS,
  ADD_POST,
  SET_SELECTED_POST
} from "../mutations/post/mutation-types";
import { posts } from "../getters/post/getter-types";
import axios from "axios";
import apis from "../apis/apis";

const state = {
  posts: [],
  selectedPost: null
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
  },
  [SET_SELECTED_POST]: (s, v) => {
    s.selectedPost = v;
  }
};

const actions = {
  [CREATE]: async ({ commit }, p) => {
    let post = {
      title: p.title,
      content: p.text,
      cateID: p.cateID
    };
    console.log(post);
    console.log(apis.create_post);
    let data = await axios.post(apis.create_post, post);
    alert(data);
  },

  [GET_ALL]: async () => {
<<<<<<< Updated upstream
    return true;
  },
  [NEW_COMMENT]: async (c, p) => {
    return true;
=======
  },
  
  [GET_POSTS_BY_CATE]: async ({ commit }, payload) => {
    let data = await axios.post(apis.get_posts_by_cate, payload);
    console.log(data);
    commit(`${SET_POSTS}`, data.listOfPosts);
  },
  
  [GET_SELECTED_POST]: async ({ commit }, payload) => {
    let data = await axios.post(apis.get_selected_post, payload);
    commit(`${SET_SELECTED_POST}`, data.selectedPost);
>>>>>>> Stashed changes
  }
};

export default {
  namespaced: true,
  state,
  getters,
  actions,
  mutations
};
