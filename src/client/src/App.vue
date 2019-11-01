<template>
  <v-app>
    <v-snackbar
      top
      v-model="snackbar.show"
      :color="snackbar.color"
      :timeout="2000"
    >{{ snackbar.text }}</v-snackbar>
    <login-modal :show.sync="loginModal" @click:outside="loginModal = false;" />
    <register-modal :show.sync="regModal" @click:outside="regModal = false;" />
    <new-cate-modal :show.sync="cateModal" @click:outside="cateModal = false;" />
    <profile-modal :show.sync="profileModal" @click:outside="profileModal = false;" />
    <v-app-bar flat app clipped-left>
      <v-toolbar-title>
        <v-btn link text large rounded :ripple="false" @click="$router.push({ name: 'default'})">{{appName}}</v-btn>
      </v-toolbar-title>
      <v-spacer></v-spacer>
      <v-btn v-if="!isAuthenticated" @click="loginModal = true;" class="ma-2" depressed>Log in</v-btn>
      <v-btn v-if="!isAuthenticated" @click="regModal = true;" class="ma-2" depressed>Register</v-btn>
      <v-btn
        v-if="isAuthenticated"
        @click="LOGOUT(); showSnackbar('success', 'Log out successfully.')"
        class="ma-2"
        depressed
      >Log out</v-btn>
      <v-menu left bottom>
        <template v-slot:activator="{ on }">
          <v-btn icon v-on="on">
            <v-icon>mdi-account-circle-outline</v-icon>
          </v-btn>
        </template>
        <v-list>
          <v-list-item @click="profileModal = true;">
            <v-icon>mdi-account</v-icon>
            <v-list-item-title>Your Profile</v-list-item-title>
          </v-list-item>
          <v-list-item>
            <v-list-item-title>Option 2</v-list-item-title>
          </v-list-item>
        </v-list>
      </v-menu>
    </v-app-bar>

    <v-content>
      <v-container fluid>
        <v-row align="start" justify="start">
          <v-col cols="3"></v-col>
          <v-col cols="7" class="shrink">
            <router-view :key="$route.path"></router-view>
          </v-col>
          <v-col cols="2">
            <cate-panel :categories="categories" @click:newcat="cateModal = true;" />
          </v-col>
        </v-row>
      </v-container>
    </v-content>

    <v-footer>
      <v-col class="text-center" cols="12">
        <span>KMS &copy; 2019</span>
      </v-col>
    </v-footer>
  </v-app>
</template>

<style scoped>
</style>

<script>
import LoginModal from "./components/LoginModal";
import RegisterModal from "./components/RegModal";
import NewCateModal from "./components/NewCateModal";
import CatePanel from "./components/CatePanel";
import ProfileModal from "./components/ProfileModal";
// import Post from "./components/Post";
import { mapActions } from "vuex";
import { mapGetters } from "vuex";
import { isAuthenticated } from "./store/getters/getter-types";
import { LOGOUT, REFRESH_LANDING } from "./store/actions/action-types";

export default {
  name: "App",
  components: {
    LoginModal,
    NewCateModal,
    RegisterModal,
    CatePanel,
    ProfileModal
    // Post
  },
  computed: {
    ...mapGetters([isAuthenticated])
  },
  async mounted() {
    await this.REFRESH_LANDING();
  },
  data: () => ({
    appName: "The Cave",
    snackbar: {
      show: false,
      color: "success",
      text: null
    },
    categories: [
      { id: 1, name: "catA" },
      { id: 2, name: "catB" },
      { id: 3, name: "catC" },
      { id: 4, name: "catD" }
    ],
    loginModal: false,
    regModal: false,
    cateModal: false,
    profileModal: false
  }),
  methods: {
    ...mapActions([LOGOUT, REFRESH_LANDING]),
    showLoginModal() {
      this.loginModal = true;
    },
    showCatModal() {
      this.catModal = true;
    },
    showSnackbar(color, text) {
      this.snackbar.show = true;
      this.snackbar.color = color;
      this.snackbar.text = text;
    },
    showProfileModal() {
      this.profileModal = true;
    },
    showCaveModal() {
      this.caveModal = true;
    }
  }
};
</script>
