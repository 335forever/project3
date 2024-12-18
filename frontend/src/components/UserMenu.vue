<script lang="ts" setup>
import { useAuthStore } from "../stores/authStore";
const authStore = useAuthStore();
</script>

<template>
    <v-btn
        icon
        size="large"
        style="position: fixed; top: 20px; right: 20px; border-radius: 50%"
    >
        <v-icon size="large">{ mdi-account-circle }</v-icon>
        <v-menu activator="parent">
            <v-list>
                <v-list-item class="list-item" @click=""> </v-list-item>
                <v-divider></v-divider>
                <v-list-item
                    class="list-item"
                    style="color: red"
                    @click="authStore.logout"
                >
                    <v-icon start>mdi-logout</v-icon>Log out
                </v-list-item>
            </v-list>
        </v-menu>
    </v-btn>
</template>

<style scoped>
.list-item:hover {
    background-color: #e4e4e4;
    cursor: pointer;
}
</style>
