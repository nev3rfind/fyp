<template>
        <layout v-if="!isLoginPage && !isNotFoundPage && !isNotMfa">
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
      },
      isNotFoundPage() {
          return this.$route.name === "NotFound";
      },
      isNotMfa() {
          return this.$route.name === "Mfa";
      },
  }
};
</script>
