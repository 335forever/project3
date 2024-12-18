<script lang="ts" setup>
import { ref, watch } from "vue";
import { useAuthStore } from "../stores/authStore";
import { useRouter } from "vue-router";
import { useAlert } from "@/composables/useAlert";

const authStore = useAuthStore();
const router = useRouter();
const { showAlert } = useAlert();

// Credentials object
const credentials = ref({
    username: "",
    password: "",
});

const newUser = ref({
    username: "",
    email: "",
    password: "",
});

// Login form validation
const isLoginValid = ref(false);
const isLoadingLogin = ref(false);
const loginError = ref<string | null>(null);

// Sign up form validation
const isSignUpValid = ref(false);
const isLoadingSignUp = ref(false);
const signUpError = ref<string | null>(null);
const isSignUpDialogActive = ref(false);

const rules = {
    required: (value: string) => !!value || "This field is required",
    min: (length: number) => (value: string) =>
        value.length >= length || `Minimum ${length} characters`,
    email: (value: string) =>
        /^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(value) || "Invalid email address",
};

// Handle login
const handleLogin = async () => {
    if (!isLoginValid.value) return;
    try {
        isLoadingLogin.value = true;
        loginError.value = null;
        const result = await authStore.login(credentials.value);
        if (result.success) {
            router.push("/dashboard");
        } else {
            loginError.value = result.message;
        }
        isLoadingLogin.value = false;
    } catch (error) {
        console.error("Login failed", error);
    } finally {
        isLoadingLogin.value = false;
    }
};
const handleKeydown = (event: KeyboardEvent) => {
    if (event.key === "Enter") {
        handleLogin();
    }
};

const handleSignUp = async () => {
    if (!isSignUpValid.value) return;

    isLoadingSignUp.value = true;
    signUpError.value = null;

    try {
        const response = await authStore.signUp(newUser.value);

        if (response.success) {
            isSignUpDialogActive.value = false;
            showAlert("Sign up successfully", "success", 2000);
        } else {
            signUpError.value = response.message;
        }
    } catch (error) {
        console.error("Sign up error", error);
        signUpError.value = "Unexpected error occurred.";
    } finally {
        isLoadingSignUp.value = false;
    }
};

const clearSignUpForm = () => {
    newUser.value = {
        username: "",
        email: "",
        password: "",
    };
};

watch(isSignUpDialogActive, (newStatus) => {
    if (!newStatus) {
        clearSignUpForm();
    }
});

const btn = ref<Element>();
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
            :loading="isLoadingLogin"
            style="background-color: #f5f5f5"
        >
            <v-card-title class="justify-center">Login</v-card-title>
            <v-card-text>
                <v-form ref="loginForm" v-model="isLoginValid" lazy-validation>
                    <!-- Email Field -->
                    <v-text-field
                        label="Username"
                        v-model="credentials.username"
                        :rules="[rules.required]"
                        outlined
                        @input="() => (loginError = null)"
                        @keydown="handleKeydown"
                    />
                    <!-- Password Field -->
                    <v-text-field
                        label="Password"
                        v-model="credentials.password"
                        :rules="[rules.required, rules.min(6)]"
                        outlined
                        type="password"
                        @input="() => (loginError = null)"
                        @keydown="handleKeydown"
                    />

                    <v-alert v-if="loginError" type="error" dismissible pa-1>
                        {{ loginError }}
                    </v-alert>
                </v-form>
            </v-card-text>
            <v-card-actions class="d-flex justify-space-between">
                <v-btn
                    color="primary"
                    width="50%"
                    @click=""
                    variant="tonal"
                    ref="btn"
                    >Sign up</v-btn
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
        <v-dialog
            :activator="btn"
            max-width="90%"
            width="600"
            v-model="isSignUpDialogActive"
        >
            <v-card prepend-icon="mdi-account" title="Sign up">
                <v-card-text
                    ><v-form
                        ref="signUpForm"
                        v-model="isSignUpValid"
                        lazy-validation
                    >
                        <v-text-field
                            label="Username"
                            v-model="newUser.username"
                            :rules="[rules.required]"
                            outlined
                            @input="() => (signUpError = null)"
                        />
                        <v-text-field
                            label="Email"
                            v-model="newUser.email"
                            :rules="[rules.required, rules.email]"
                            outlined
                            @input="() => (signUpError = null)"
                        />
                        <v-text-field
                            label="Password"
                            v-model="newUser.password"
                            :rules="[rules.required, rules.min(6)]"
                            outlined
                            type="password"
                            @input="() => (signUpError = null)"
                        />

                        <v-alert
                            v-if="signUpError"
                            type="error"
                            dismissible
                            pa-1
                        >
                            {{ signUpError }}
                        </v-alert>
                    </v-form>
                </v-card-text>

                <v-divider></v-divider>

                <v-card-actions>
                    <v-spacer></v-spacer>
                    <v-btn
                        text="Cancel"
                        variant="plain"
                        @click="isSignUpDialogActive = false"
                        :disabled="false"
                    ></v-btn>

                    <v-btn
                        color="primary"
                        text="Sign up"
                        variant="tonal"
                        :disabled="!isSignUpValid"
                        @click="handleSignUp"
                        :loading="false"
                    ></v-btn>
                </v-card-actions>
            </v-card>
        </v-dialog>
    </v-container>
</template>
