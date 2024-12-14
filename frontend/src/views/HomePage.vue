<script lang="ts" setup>
import { useRouter } from "vue-router";
const router = useRouter();
</script>

<template>
    <p>Home page</p>
    <button @click="() => router.push('/login')">Login</button>
</template>

<style></style>
