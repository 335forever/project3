<script lang="ts" setup>
import { useAuthStore } from "../stores/useAuthStore";
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
        <v-list-item class="list-item" @click="">
          <v-list-item-content
            style="display: flex; gap: 6px; align-items: center"
            ><v-icon>{{}}</v-icon>{{
          }}</v-list-item-content>
        </v-list-item>
        <v-divider></v-divider>
        <v-list-item class="list-item" @click="authStore.logout">
          <v-list-item-content
            style="color: red; display: flex; gap: 6px; align-items: center"
            ><v-icon>mdi-logout</v-icon>Log out</v-list-item-content
          >
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
