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
import { useAlert } from "@/composables/useAlert";

const { alertState } = useAlert();

const typeColor = computed(() => {
    const colors = {
        success: "green",
        error: "red",
        info: "blue",
        warning: "orange",
    };
    return colors[alertState.type] ?? "grey";
});
</script>

<style scoped>
.global-alert {
    position: fixed;
    bottom: 20px;
    right: 20px;
    z-index: 1000;
    min-width: 300px;
    max-width: 400px;
}
</style>
