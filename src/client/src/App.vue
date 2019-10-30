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
    <new-cat-modal :show.sync="catModal" @click:outside="catModal = false;" />
    <profile-modal :show.sync="profileModal" @click:outside="profileModal = false;" />
    <cave-modal :show.sync="caveModal" @click:outside="caveModal = false;" />
    <v-app-bar flat app clipped-left>
      <v-toolbar-title>The Cave</v-toolbar-title>
      <v-spacer></v-spacer>
      <!-- <v-btn icon large @click="userProfileClick">
        <v-icon>mdi-account-circle-outline</v-icon>
      </v-btn>-->
      <v-btn v-if="!isAuthenticated" @click="loginModal = true;" class="ma-2" depressed>Log in</v-btn>
      <v-btn v-if="!isAuthenticated" @click="regModal = true;" class="ma-2" depressed>Register</v-btn>
      <v-btn
        v-if="isAuthenticated"
        @click="LOGOUT(); showSnackbar('success', 'Log out successfully.')"
        class="ma-2"
        depressed
      >Log out</v-btn>
      <v-menu left bottom >
        <template v-slot:activator="{ on }">
          <v-btn icon v-on="on">
            <v-icon>mdi-account-circle-outline</v-icon>
          </v-btn>
        </template>
        <v-list>
          <v-list-item @click="profileModal = true;">
            <v-icon>mdi-account</v-icon>
            <v-list-item-title> Your Profile</v-list-item-title>
          </v-list-item>
          <v-list-item >
            <v-list-item-title> Option 2</v-list-item-title>
          </v-list-item>
        </v-list>
      </v-menu>
    </v-app-bar>

    <v-content>
      <v-container fluid>
        <v-row align="start" justify="start">
          <v-col cols="3"></v-col>
          <!-- <v-col cols="3">
            <v-card class="mx-auto" outlined>
              <v-card
                class="mx-auto"
                color="#26c6da"
                dark
                max-width="400"
              >
                <v-card-title>
                  <v-icon
                    large
                    left
                  >
                    mdi-account-circle
                  </v-icon>
                  <span class="title font-weight-light">Most Active Member</span>
                </v-card-title>

                <v-card-text class="headline font-weight-bold">
                  "Someone is really talkative"
                </v-card-text>

                <v-card-actions>
                  <v-list-item class="grow">
                    <v-list-item-avatar color="grey darken-3">
                      <v-img
                        class="elevation-6"
                        src="https://avataaars.io/?avatarStyle=Transparent&topType=ShortHairShortCurly&accessoriesType=Prescription02&hairColor=Black&facialHairType=Blank&clotheType=Hoodie&clotheColor=White&eyeType=Default&eyebrowType=DefaultNatural&mouthType=Default&skinColor=Light"
                      ></v-img>
                    </v-list-item-avatar>

                    <v-list-item-content>
                      <v-list-item-title>Annonymous</v-list-item-title>
                    </v-list-item-content>

                    <v-row
                      align="center"
                      justify="end"
                    >
                      <v-icon class="mr-1">mdi-heart</v-icon>
                      <span class="subheading mr-2">256</span>
                      <span class="mr-1"></span>
                      <v-icon class="mr-1">mdi-heart-broken</v-icon>
                      <span class="subheading">45</span>
                    </v-row>
                  </v-list-item>
                </v-card-actions>
              </v-card>
            </v-card>
          </v-col> -->
          <v-col cols="7" class="shrink">
            <router-view>            
            </router-view>
          </v-col>
          <v-col cols="2">
            <cat-panel :categories="categories" @click:newcat="catModal = true;" />
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
import NewCatModal from "./components/NewCatModal";
import CatPanel from "./components/CatPanel";
import ProfileModal from "./components/ProfileModal";
import CaveModal from "./components/CaveModal";
import { mapActions } from "vuex";
import { mapGetters } from "vuex";
import { isAuthenticated } from "./store/getters/getter-types";
import { LOGOUT } from "./store/actions/action-types";

export default {
  name: "App",
  components: {
    LoginModal,
    NewCatModal,
    RegisterModal,
    CatPanel,
    ProfileModal,
    CaveModal
  },
  computed: {
    ...mapGetters([isAuthenticated])
  },
  created() {
    // this.$vuetify.theme.dark = true;
  },
  data: () => ({
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
    catModal: false,
    profileModal: false,
    caveModal: false
  }),
  methods: {
    ...mapActions([LOGOUT]),
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
