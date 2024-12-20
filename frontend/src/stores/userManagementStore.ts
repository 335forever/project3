import { defineStore } from "pinia";
import { Role, type User } from "@/type/auth";
import { RoleService } from "@/services/roleService";
import type { GrantTarget } from "@/type/userManagement";
import type { TResponse } from "@/type/common";

interface UserManagementState {
    usersByRole: Record<string, User[]>;
    loadingStates: Record<string, boolean>;
    errorStates: Record<string, string | null>;
}

export const useUserManagementStore = defineStore("userManagement", {
    state: (): UserManagementState => ({
        usersByRole: {
            [Role.SALER]: [],
            [Role.DESIGNER]: [],
            [Role.PRODUCER]: [],
            [Role.UNASSIGNED]: [],
        },
        loadingStates: {
            [Role.SALER]: false,
            [Role.DESIGNER]: false,
            [Role.PRODUCER]: false,
            [Role.UNASSIGNED]: false,
        },
        errorStates: {
            [Role.SALER]: null,
            [Role.DESIGNER]: null,
            [Role.PRODUCER]: null,
            [Role.UNASSIGNED]: null,
        },
    }),
    actions: {
        setLoading(role: Role, isLoading: boolean) {
            this.loadingStates[role] = isLoading;
        },
        setError(role: Role, error: string | null) {
            this.errorStates[role] = error;
        },

        async grant({ id, roleName }: GrantTarget): Promise<TResponse> {
            this.setLoading(roleName, true);
            this.setError(roleName, null);
            try {
                await RoleService.grant({ id, roleName });
                await Promise.all([
                    this.refreshRoleMembership(roleName),
                    this.refreshRoleMembership(Role.UNASSIGNED),
                ]);
                return { success: true, message: "Grant successfully" };
            } catch (error: any) {
                const errorMsg = error.response?.data.message || "Grant failed";
                this.setError(roleName, errorMsg);
                return { success: false, message: errorMsg };
            } finally {
                this.setLoading(roleName, false);
            }
        },

        async getRoleMembership(roleName: Role): Promise<void> {
            this.setLoading(roleName, true);
            this.setError(roleName, null);

            try {
                const users = await RoleService.getRoleMembership(roleName);
                this.usersByRole[roleName] = users;
            } catch (error: any) {
                const errorMsg =
                    error.response?.data.message ||
                    "Failed to fetch role membership";
                this.setError(roleName, errorMsg);
            } finally {
                this.setLoading(roleName, false);
            }
        },

        async refreshRoleMembership(roleName: Role): Promise<void> {
            await this.getRoleMembership(roleName);
        },

        async fetchAllRoles(): Promise<void> {
            const roles = [
                Role.SALER,
                Role.DESIGNER,
                Role.PRODUCER,
                Role.UNASSIGNED,
            ];
            await Promise.all(
                roles.map((role) => this.getRoleMembership(role))
            );
        },
    },
});
