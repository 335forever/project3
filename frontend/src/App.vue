<script setup lang="ts">
import { RouterLink, RouterView } from "vue-router";
import Alert from "@/components/Alert.vue";
</script>

<template>
    <RouterView min-width="100vw" />
    <Alert />
</template>
