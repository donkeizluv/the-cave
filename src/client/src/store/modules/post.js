import { CREATE, GET_ALL, REFRESH_POSTS_BY_CATE, GET_SELECTED_POST, ADD_COMMENT, ADD_VOTE, REFRESH_POSTS_BY_SEARCH } from "../actions/post/action-types";
import { REFRESH_LANDING } from "../actions/action-types";
import {
  SET_POSTS,
  ADD_POST,
  SET_POST_VOTES
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
  },
  [SET_POST_VOTES]: (s, v) => {
    let idx = s.posts.findIndex(p => p.id === v.postId);
    console.log(v);
    if (idx < 0) return;
    s.posts[idx].upVotes = v.upVotes;
    s.posts[idx].downVotes = v.downVotes;
  }
};

const actions = {
  [CREATE]: async (_, p) => {
    let post = {
      title: p.title,
      content: p.content,
      cateId: p.cateID,
      image: !p.imgData ? null : p.imgData.split(',')[1]
    };
    console.log(post);
    let { data } = await axios.post(apis.create_post, post);
    return data;
  },

  [GET_ALL]: async () => {
    return true;
  },

  [REFRESH_POSTS_BY_CATE]: async ({ dispatch, commit }, payload) => {
    if (!payload) {
      await dispatch(REFRESH_LANDING, { root: true });
      return;
    }
    let { data } = await axios.get(`${apis.refresh_posts_by_cate}/${payload}`);
    commit(`${SET_POSTS}`, data);
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
  },
  [ADD_VOTE]: async ({ commit }, payload) => {
    let { data } = await axios.post(
      apis.add_vote, {
      postId: payload.postId,
      voteType: payload.voteType
    });
    commit(SET_POST_VOTES, {
      postId: data.id,
      upVotes: data.upVotes,
      downVotes: data.downVotes
    });
    return data;
  },
  [REFRESH_POSTS_BY_SEARCH]: async ({ commit }, payload) => {
    let data = await axios.get(`${apis.get_posts_by_search}/${payload.cateID}?query=${payload.query}`);
    commit(`${SET_POSTS}`, data);
  }
};

export default {
  namespaced: true,
  state,
  getters,
  actions,
  mutations
};
