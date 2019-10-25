<template>
  <v-card class="mx-auto" outlined>
    <v-list-item three-line>
      <v-list-item-content>
        <div class="overline mb-8">
          Categories
          <v-btn class="mb-1" small icon @click="$emit('click:newcat')">
            <v-icon>mdi-plus</v-icon>
          </v-btn>
        </div>
      </v-list-item-content>
    </v-list-item>
    <v-divider class="mx-4"></v-divider>
    <template v-for="cat in categories">
      <v-list-item :key="cat.id" class="text-center">
        <v-list-item-content>
          <span>{{cat.name}}</span>
        </v-list-item-content>
      </v-list-item>
      <v-divider class="mx-4" :key="cat.id"></v-divider>
    </template>
  </v-card>
</template>

<script>
export default {
  name: "CatPanel",
  props: {
    categories: {
      type: Array,
      required: true
    }
  },
  computed: {},
  data: function name() {
    return {};
  },
  methods: {}
};
</script>