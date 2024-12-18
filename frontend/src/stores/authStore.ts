import { defineStore } from "pinia";
import { jwtDecode } from "jwt-decode";
import router from "@/router";
import type { DecodedToken, User } from "@/type/auth";
import { AuthService } from "@/services/authService";

interface AuthState {
    user: User | null;
    loading: boolean;
    error: string | null;
}

export const useAuthStore = defineStore("auth", {
    state: (): AuthState => ({
        user: null,
        loading: false,
        error: null,
    }),
    actions: {
        async login(credentials: { username: string; password: string }) {
            this.loading = true;
            this.error = null;

            try {
                const data = await AuthService.login(credentials);
                const decodedToken: DecodedToken = jwtDecode(data.token);
                if (decodedToken) {
                    localStorage.setItem("token", data.token);
                    await this.fetchUser();
                    return { success: true, message: "Login successful" };
                }
                throw new Error("Failed to decode token");
            } catch (error: any) {
                this.error = error.message ?? "Login failed";
                return { success: false, message: this.error };
            } finally {
                this.loading = false;
            }
        },
        async fetchUser() {
            try {
                const user = await AuthService.fetchUser();
                this.user = user;
                return true;
            } catch (error) {
                console.error("Failed to fetch user", error);
                this.logout();
                return false;
            }
        },
        logout() {
            this.user = null;
            localStorage.removeItem("token");
            router.push("/login");
        },
        async signUp(credentials: {
            username: string;
            email: string;
            password: string;
        }): Promise<{
            success: boolean;
            message: string;
        }> {
            try {
                await AuthService.signUp(credentials);
                return {
                    success: true,
                    message: "Signup successful! Please log in.",
                };
            } catch (error: any) {
                console.error("Signup failed", error);
                return {
                    success: false,
                    message:
                        error.response?.data
                            ?.map(
                                (err: { code: string; description: string }) =>
                                    err.description
                            )
                            .reduce((result: string, err: string) => {
                                return result + "\n" + err;
                            }, "") ?? "Signup failed",
                };
            }
        },
    },
    getters: {
        isAuthenticated: (state) => state.user !== null,
        userInfo: (state) => state.user,
    },
});
