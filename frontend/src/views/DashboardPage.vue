<script lang="ts" setup>
import { useAuthStore } from "../stores/authStore";
const authStore = useAuthStore();
</script>

<template>
    <div>
        <h1>Welcome, {{ authStore.userInfo?.userName }}</h1>
    </div>
</template>
