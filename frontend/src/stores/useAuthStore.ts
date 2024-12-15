import { defineStore } from "pinia";
import { jwtDecode } from "jwt-decode";
import apiClient from "../services/api";
import router from "@/router";
import type { User } from "@/type/auth";

interface AuthState {
  user: User | null;
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
  }),
  actions: {
    async login(credentials: { username: string; password: string }): Promise<{
      success: boolean;
      message: string;
    }> {
      try {
        const response = await apiClient.post("/auth/login", credentials);
        const decodedToken: DecodedToken = jwtDecode(response.data.token ?? "");
        if (decodedToken) {
          localStorage.setItem("token", response.data.token);
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
    async fetchUser() {
      try {
        const response = await apiClient.get("/auth/me", {
          headers: {
            Authorization: `Bearer ${localStorage.getItem("token")}`,
          },
        });
        if (response.data) {
          this.user = response.data;
          return true;
        }
        {
          return false;
        }
      } catch (error) {
        console.error("Failed to fetch user", error);
        return false;
      }
    },
    logout() {
      this.user = null;

      localStorage.removeItem("token");

      router.push("/login");
    },
  },
  getters: {
    isAuthenticated: (state) => state.user !== null,
    userInfo: (state) => state.user,
  },
  persist: false,
});
