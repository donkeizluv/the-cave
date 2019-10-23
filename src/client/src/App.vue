<template>
  <v-app>
    <login-modal :show.sync="showLoginModal" @click:outside="showLoginModal = false;" />
    <new-cat-modal :show.sync="showNewCat" @click:outside="showNewCat = false;"></new-cat-modal>
    <v-app-bar flat app clipped-left>
      <v-toolbar-title>The Cave</v-toolbar-title>
      <v-spacer></v-spacer>
      <v-btn icon large @click="userProfileClick">
        <v-icon>mdi-account-circle-outline</v-icon>
      </v-btn>
      <v-menu left bottom>
        <template v-slot:activator="{ on }">
          <v-btn icon v-on="on">
            <v-icon>mdi-dots-vertical</v-icon>
          </v-btn>
        </template>
        <v-list>
          <v-list-item>
            <v-list-item-title>Option A</v-list-item-title>
          </v-list-item>
          <v-list-item>
            <v-list-item-title>Option B</v-list-item-title>
          </v-list-item>
        </v-list>
      </v-menu>
    </v-app-bar>

    <v-content>
      <v-container fluid>
        <v-row align="start" justify="start">
          <v-col cols="3"></v-col>
          <v-col cols="7" class="shrink">
            <v-card class="mx-auto" outlined>
              <v-list-item three-line>
                <v-list-item-content>
                  <div class="overline mb-8">Trending</div>
                </v-list-item-content>
              </v-list-item>
            </v-card>
          </v-col>
          <v-col cols="2">
            <v-card class="mx-auto" outlined>
              <v-list-item three-line>
                <v-list-item-content>
                  <div class="overline mb-8">
                    Categories
                    <v-btn class="mb-1" small icon @click="showNewCat = true;">
                      <v-icon>mdi-plus</v-icon>
                    </v-btn>
                  </div>
                </v-list-item-content>
              </v-list-item>
              <v-divider class="mx-4"></v-divider>

              <v-list-item class="text-center">
                <v-list-item-content>
                  <span>Category X</span>
                </v-list-item-content>
              </v-list-item>

              <v-divider class="mx-4"></v-divider>

              <v-list-item class="text-center">
                <v-list-item-content>
                  <span>Category Y</span>
                </v-list-item-content>
              </v-list-item>
            </v-card>
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


<script>
import LoginModal from "./components/LoginModal";
import NewCatModal from "./components/NewCatModal";

export default {
  name: "App",
  components: {
    LoginModal,
    NewCatModal
  },
  created() {
    // this.$vuetify.theme.dark = true;
  },
  data: () => ({
    showLoginModal: false,
    showNewCat: false
  }),
  methods: {
    userProfileClick() {
      this.showLoginModal = true;
    }
  }
};
</script>
