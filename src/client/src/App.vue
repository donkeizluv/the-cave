<template>
  <v-app dark>
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
    <v-app-bar flat app clipped-left color="cyan darken-2">
      <v-avatar tile>
        <img src="/img/cave.png" alt="cave" />
      </v-avatar>
      <v-toolbar-title>
        <v-btn
          link
          text
          large
          rounded
          :ripple="false"
          color="white"
          class="app-name"
          @click="homeClick"
        >{{appName}}</v-btn>
      </v-toolbar-title>
      <v-spacer></v-spacer>
      <div class="ma-4 pt-4">
        <v-text-field color="white" small append-icon="mdi-magnify" @submit="onSubmit"></v-text-field>
      </div>
      <v-btn
        v-if="isAuthenticated"
        class="ma-2"
        :ripple="false"
        text
        color="white"
        depressed
      >{{currentUser ? currentUser.username.toUpperCase() : "" }}</v-btn>
      <v-btn v-if="!isAuthenticated" @click="loginModal = true;" class="ma-2" depressed>Log in</v-btn>
      <v-btn v-if="!isAuthenticated" @click="regModal = true;" class="ma-2" depressed>Register</v-btn>
      <v-btn
        v-if="isAuthenticated"
        @click="LOGOUT(); showSnackbar('success', 'Log out successfully.')"
        class="ma-2"
        depressed
      >Log out</v-btn>
    </v-app-bar>

    <v-content>
      <v-container fluid>
        <v-row dense>
          <v-col cols="2"></v-col>
          <v-col cols="8" class="ma-0 pa-0"></v-col>
          <v-col cols="2"></v-col>
        </v-row>
        <v-row dense align="start" justify="start">
          <v-col cols="2"></v-col>
          <v-col cols="8" class="shrink">
            <v-container fluid>
              <v-row>
                <v-col class="pl-2 ma-0" cols="9">
                  <router-view :key="$route.path"></router-view>
                </v-col>
                <v-col class="pr-2 ma-0" cols="3">
                  <div v-show="selectedCate.id" class="mb-4">
                    <cate-info :cate="selectedCate" />
                  </div>
                  <div>
                    <cate-panel :categories="categories" @click:newcat="cateModal = true;" />
                  </div>
                </v-col>
              </v-row>
            </v-container>
          </v-col>
          <v-col cols="2"></v-col>
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
.app-name {
  font-size: 1.2rem;
  font-weight: 600;
}
</style>

<script>
import LoginModal from "./components/LoginModal";
import RegisterModal from "./components/RegModal";
import NewCateModal from "./components/NewCateModal";
import CatePanel from "./components/CatePanel";
import CateInfo from "./components/CateInfo";
import ProfileModal from "./components/ProfileModal";
// import Post from "./components/Post";
import { mapActions } from "vuex";
import { mapGetters } from "vuex";
import {
  isAuthenticated,
  currentUser,
  token
} from "./store/getters/getter-types";
import {
  LOGOUT,
  REFRESH_LANDING,
  RELOAD_USER
} from "./store/actions/action-types";
import { SET_SELECTED_CATE } from "./store/actions/category/action-types";
import { selectedCate } from "./store/getters/category/getter-types";
import moduleNames from "./store/modules/module-names";

export default {
  name: "App",
  components: {
    LoginModal,
    NewCateModal,
    RegisterModal,
    CatePanel,
    ProfileModal,
    CateInfo
  },
  computed: {
    ...mapGetters([isAuthenticated, currentUser, token]),
    ...mapGetters(moduleNames.category, [selectedCate])
  },
  async created() {
    if (this.token) {
      await this.RELOAD_USER(this.token);
    }
    await this.REFRESH_LANDING();
  },
  data: () => ({
    appName: "KMS Caves",
    snackbar: {
      show: false,
      color: "success",
      text: null
    },
    categories: [],
    loginModal: false,
    regModal: false,
    cateModal: false,
    profileModal: false
  }),
  methods: {
    ...mapActions([LOGOUT, REFRESH_LANDING, RELOAD_USER]),
    ...mapActions(moduleNames.category, [SET_SELECTED_CATE]),
    showLoginModal() {
      this.loginModal = true;
    },
    async homeClick() {
      this.$router.push({ name: "default" });
      await this.SET_SELECTED_CATE(null);
      await this.REFRESH_LANDING();
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
    },
    onSubmit() {}
  }
};
</script>
