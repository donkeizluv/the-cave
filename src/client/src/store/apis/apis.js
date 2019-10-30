var endpoint = process.env.VUE_APP_API_ENDPOINT || "";

export default {
  get_all_categories: endpoint + "/api/category/all",
  create_categories: endpoint + "/api/category/create",
  validate_user: endpoint + "/api/user/validate",
  create_user: endpoint + "/api/user/create",
  get_all_caves: endpoint + "/api/cave/all",
  create_cave: endpoint + "/api/cave/create",
  get_landing: endpoint + "/api/landing"
};
