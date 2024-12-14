<template>
  <v-container>
    <v-btn color="primary" @click="handleButtonClick">Show Alert</v-btn>
  </v-container>
</template>

<script lang="ts" setup>
import { showAlert } from "@/services/alert";
const handleButtonClick = () => showAlert("heelo", "success", 2000);
</script>

<style scoped></style>
