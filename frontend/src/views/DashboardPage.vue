<template>
  <div>
    <h1>Dashboard</h1>
    <p v-if="authStore.userRole === 'Admin'">Welcome, Admin!</p>
    <p v-else-if="authStore.userRole === 'Saler'">Welcome, Saler!</p>
    <button v-on:click="authStore.logout">Log out</button>
    <!-- Hiển thị theo vai trò người dùng -->
  </div>
</template>

<script lang="ts" setup>
import { useAuthStore } from "../stores/useAuthStore";
const authStore = useAuthStore();
</script>
