import { defineStore } from "pinia";
import { jwtDecode } from "jwt-decode";
import apiClient from "../services/api";
import router from "@/router";

interface User {
    id: string;
    username: string;
    role: string;
}

interface AuthState {
    user: User | null;
    role: string | null;
    token: string | null;
}

interface DecodedToken {
    nameid: string;
    unique_name: string;
    role: string;
    nbf: number;
    exp: number;
    iat: number;
    iss: string;
    aud: string;
}

export const useAuthStore = defineStore("auth", {
    state: (): AuthState => ({
        user: null,
        role: null,
        token: null
    }),
    actions: {
        async login(credentials: {
            username: string;
            password: string;
        }): Promise<{
            success: boolean;
            message: string;
        }> {
            try {
                const response = await apiClient.post(
                    "/auth/login",
                    credentials
                );
                const decodedToken: DecodedToken = jwtDecode(
                    response.data.token ?? ""
                );
                if (decodedToken) {
                    this.user = {
                        id: decodedToken.nameid,
                        username: decodedToken.unique_name,
                        role: decodedToken.role,
                    };
                    this.role = decodedToken.role;
                    this.token = response.data.token;
                    return { success: true, message: "Login successfully" };
                }
                return { success: false, message: "Failed to decode token" };
            } catch (error: any) {
                console.log(error);
                return {
                    success: false,
                    message: error.response?.data?.message ?? "Login failed",
                };
            }
        },
        logout() {
            this.user = null;
            this.role = null;
            this.token = null;
            router.push("/login");
        },
    },
    getters: {
        isAuthenticated: (state) => state.user !== null,
        userRole: (state) => state.role,
    },
    persist: true,
});
