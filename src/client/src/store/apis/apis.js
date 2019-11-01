var endpoint = process.env.VUE_APP_API_ENDPOINT || "";

export default {
  get_all_categories: endpoint + "/api/category",
  create_categories: endpoint + "/api/category",
  validate_user: endpoint + "/api/user/validate",
  create_user: endpoint + "/api/user",
  get_all_posts: endpoint + "/api/post",
  create_post: endpoint + "/api/post/create",
  get_landing: endpoint + "/api/landing",
  get_posts_by_cate: endpoint + "/api/post/getPostsByCate",
  get_selected_post: endpoint + "/api/post",
  add_comment: endpoint + "/api/post/addcomment"
};
