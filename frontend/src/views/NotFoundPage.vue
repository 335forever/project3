<script lang="ts" setup>
import { useRouter } from "vue-router";
const router = useRouter();
const goToHome = () => {
  router.push("/");
};
</script>

<template>
  <v-empty-state
    headline="Whoops, 404"
    title="Page not found"
    text="The page you were looking for does not exist"
    image="https://vuetifyjs.b-cdn.net/docs/images/logos/v.png"
  >
    <v-btn color="primary" @click="goToHome">Back to home</v-btn></v-empty-state
  >
</template>
