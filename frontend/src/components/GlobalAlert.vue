<template>
  <v-alert
    v-if="alertState.isVisible"
    :type="alertState.type"
    class="global-alert"
    dismissible
  >
    {{ alertState.message }}
  </v-alert>
</template>

<script lang="ts" setup>
import { globalAlertState } from "../hook/useAlert";

const alertState = globalAlertState;
</script>

<style scoped>
.global-alert {
  position: fixed;
  bottom: 20px;
  right: 20px;
  width: 300px;
  z-index: 1000;
}
</style>
