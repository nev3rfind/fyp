<template>
        <layout v-if="!isLoginPage">
            <router-view></router-view>
        </layout>
        <router-view v-else></router-view>
</template>

<script>
import Layout from './Layout.vue';

export default {
  components: {
    Layout
  },
  computed: {
    isLoginPage() {
      return this.$route.path === '/login';
    }
  }
};
</script>
