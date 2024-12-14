<template>
  <v-alert
    v-if="alertState.visible"
    :type="alertState.type"
    :color="typeColor"
    dismissible
    class="global-alert"
  >
    {{ alertState.message }}
  </v-alert>
</template>

<script setup lang="ts">
import { computed } from "vue";
import { alertState } from "@/services/alert";

const typeColor = computed(() => {
  switch (alertState.type) {
    case "success":
      return "green";
    case "error":
      return "red";
    case "info":
      return "blue";
    case "warning":
      return "orange";
    default:
      return "grey";
  }
});
</script>

<style scoped>
.global-alert {
  position: fixed;
  bottom: 20px;
  right: 20px;
  z-index: 1000;
}
</style>
