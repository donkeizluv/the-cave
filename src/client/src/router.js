import Vue from "vue";
import Router from "vue-router";
import Trending from "./components/TrendingPanel.vue";
import CreatePost from "./components/CreatePostPanel.vue";
import Post from "./components/Post.vue";

Vue.use(Router);

export default new Router({
  mode: "history",
  base: process.env.BASE_URL,
  routes: [
    {
      path: "/",
      name: "default",
      component: Trending
    },
    {
      path: "/post/create/:cateID",
      name: "create_post",
      component: CreatePost,
      props: r => r.params
    },
    {
      path: "/cave/:cate",
      name: "cave",
      component: Trending,
      props: r => r.params
    }, 
    {
      path: "/post/:postId",
      name: "post",
      component: Post,
      props: r => r.params
    }
    // {
    //   path: '/about',
    //   name: 'about',
    //   // route level code-splitting
    //   // this generates a separate chunk (about.[hash].js) for this route
    //   // which is lazy-loaded when the route is visited.
    //   component: () => import(/* webpackChunkName: "about" */ './views/About.vue')
    // }
  ]
});
