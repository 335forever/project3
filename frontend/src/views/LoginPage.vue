<script lang="ts" setup>
import { ref } from "vue";
import { useAuthStore } from "../stores/useAuthStore";
import { useRouter } from "vue-router";

const authStore = useAuthStore();
const router = useRouter();

// Credentials object
const credentials = ref({
    username: "",
    password: "",
});

// Form validation
const isValid = ref(false);
const isLoading = ref(false);
const loginError = ref<string | null>(null);
const rules = {
    required: (value: string) => !!value || "This field is required",
    email: (value: string) => {
        const pattern = /^[^@]+@[^@]+\.[a-zA-Z]{2,}$/;
        return pattern.test(value) || "Invalid email";
    },
    min: (length: number) => (value: string) =>
        value.length >= length || `Minimum ${length} characters`,
};

// Handle login
const handleLogin = async () => {
    if (!isValid.value) return;
    try {
        isLoading.value = true;
        loginError.value = null;
        const result = await authStore.login(credentials.value);
        if (result.success) {
            router.push("/dashboard");
        } else {
            loginError.value = result.message;
        }
        isLoading.value = false;
    } catch (error) {
        console.error("Login failed", error);
    } finally {
        isLoading.value = false;
    }
};
</script>

<template>
    <v-container
        class="d-flex justify-center align-center"
        width="full"
        height="100vh"
        style="background: linear-gradient(to bottom left, #ffd89b, #19547b)"
    >
        <v-card
            class="pa-4"
            width="400"
            variant="elevated"
            :loading="isLoading"
            style="background-color: #f5f5f5 ;"
        >
            <v-card-title class="justify-center">Login</v-card-title>
            <v-card-text>
                <v-form ref="loginForm" v-model="isValid" lazy-validation>
                    <!-- Email Field -->
                    <v-text-field
                        label="Email"
                        v-model="credentials.username"
                        :rules="[rules.required]"
                        outlined
                        @input="() => (loginError = null)"
                    />
                    <!-- Password Field -->
                    <v-text-field
                        label="Password"
                        v-model="credentials.password"
                        :rules="[rules.required, rules.min(6)]"
                        outlined
                        type="password"
                        @input="() => (loginError = null)"
                    />

                    <v-alert v-if="loginError" type="error" dismissible pa-1>
                        {{ loginError }}
                    </v-alert>
                </v-form>
            </v-card-text>
            <v-card-actions class="d-flex justify-space-between">
                <v-btn color="primary" width="50%" @click="" variant="tonal"
                    >Sign in</v-btn
                >
                <v-btn
                    color="primary"
                    width="50%"
                    @click="handleLogin"
                    variant="flat"
                    >Login</v-btn
                >
            </v-card-actions>
        </v-card>
    </v-container>
</template>
