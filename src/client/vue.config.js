module.exports = {
  css: {
    loaderOptions: {
      scss: {
        prependData: `@import "~@/styles/global-imports.scss";`
      }
    }
  },
  "transpileDependencies": [
    "vuetify"
  ]
};  