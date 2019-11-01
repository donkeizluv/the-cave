// var endpoint =
//   process.env.NODE_ENV === "production"
//     ? process.env.VUE_APP_API_ENDPOINT || ""
//     : "";

var endpoint = process.env.VUE_APP_API_ENDPOINT || "";

export default {
  get_all_categories: endpoint + "/api/category",
  create_categories: endpoint + "/api/category",
  validate_user: endpoint + "/api/user/validate",
  create_user: endpoint + "/api/user",
  get_all_posts: endpoint + "/api/post",
  create_post: endpoint + "/api/post",
  get_landing: endpoint + "/api/landing"
};
