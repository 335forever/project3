<template>
    <v-container>
        <v-btn color="primary" @click="handleButtonClick">Show Alert</v-btn>
    </v-container>
</template>

<script lang="ts" setup>
import { useAlert } from "@/composables/useAlert";

const { showAlert } = useAlert();

const handleButtonClick = () => showAlert("heelo", "error", 2000);
</script>

<style scoped></style>
