import { CREATE, GET_ALL, GET_POSTS_BY_CATE, GET_SELECTED_POST, NEW_COMMENT, ADD_COMMENT } from "../actions/post/action-types";
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
  // [SET_SELECTED_POST]: (s, v) => {
  //   s.selectedPost = v;
  // }
};

const actions = {
  [CREATE]: async (_, p) => {
    let post = {
      title: p.title,
      content: p.text,
      cateID: p.cateID
    };
    let { data } = await axios.post(apis.create_post, post);
    return data;
  },

  [GET_ALL]: async () => {
    return true;
  },
  [NEW_COMMENT]: async (c, p) => {
    return true;
  },

  [GET_POSTS_BY_CATE]: async ({ commit }, payload) => {
    let data = await axios.post(apis.get_posts_by_cate, payload);
    console.log(data);
    commit(`${SET_POSTS}`, data.listOfPosts);
  },

  [GET_SELECTED_POST]: async (_, payload) => {
    let { data } = await axios.get(`${apis.get_selected_post}/${payload}`);
    return data;
  },
  [ADD_COMMENT]: async (_, payload) => {
    let { data } = await axios.post(
      apis.add_comment, {
        postId: payload.postId,
      content: payload.content,
      parentId: payload.parentId
    });
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
