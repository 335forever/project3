import { defineStore } from "pinia";
import type { Role, User } from "@/type/auth";
import { RoleService } from "@/services/roleService";
import type { GrantTarget } from "@/type/userManagement";
import type { TError, TResponse, TResponseUsers } from "@/type/common";

interface UserManagementState {
    user: User | null;
    loading: boolean;
    error: string | null;
}

export const useUserManagementStore = defineStore("userManagement", {
    state: (): UserManagementState => ({
        user: null,
        loading: false,
        error: null,
    }),
    actions: {
        async grant({ id, roleName }: GrantTarget): Promise<TResponse> {
            try {
                await RoleService.grant({ id, roleName });
                return {
                    success: true,
                    message: "Grant successfully",
                };
            } catch (error: any) {
                return {
                    success: false,
                    message: error.response?.data.message ?? "Grant failed",
                };
            }
        },
        async getRoleMembership(roleName: Role): Promise<TResponseUsers> {
            try {
                const users = await RoleService.getRoleMembership(roleName);
                return {
                    success: true,
                    message: "Get role membership successfully",
                    users,
                };
            } catch (error: any) {
                return {
                    success: false,
                    message:
                        error.response?.data.message ??
                        "Get role membership failed",
                    users: [],
                };
            }
        },
    },
    getters: {},
});
