import type { User } from "@/type/auth";
import apiClient from "./apiClient";

export const AuthService = {
    async login(credentials: { username: string; password: string }) {
        const response = await apiClient.post("/auth/login", credentials);
        return response.data;
    },
    async fetchUser() : Promise<User> {
        const response = await apiClient.get("/auth/me");
        return response.data;
    },
    async signUp(credentials: {
        username: string;
        email: string;
        password: string;
    }) {
        return await apiClient.post("/auth/register", credentials);
    },
};
