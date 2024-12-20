import { defineStore } from "pinia";
import { Role, type User } from "@/type/auth";
import { RoleService } from "@/services/roleService";
import type { GrantTarget } from "@/type/userManagement";
import type { TError, TResponse, TResponseUsers } from "@/type/common";

interface UserManagementState {
    usersByRole: {
        [key: string]: User[];
    };
}

export const useUserManagementStore = defineStore("userManagement", {
    state: (): UserManagementState => ({
        usersByRole: {
            [Role.SALER]: [],
            [Role.DESIGNER]: [],
            [Role.PRODUCER]: [],
            unassigned: [],
        },
    }),
    actions: {
        async grant({ id, roleName }: GrantTarget): Promise<TResponse> {
            try {
                await RoleService.grant({ id, roleName });
                await this.refreshRoleMembership(roleName);
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
        async getRoleMembership(
            roleName: Role | null
        ): Promise<TResponseUsers> {
            try {
                const users = await RoleService.getRoleMembership(roleName);
                if (roleName === Role.ADMIN)
                    throw new Error("Role admin can't retrived");
                const roleKey = roleName ?? "unassigned";
                this.usersByRole[roleKey] = users;

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
        async refreshRoleMembership(roleName: Role | null) {
            await this.getRoleMembership(roleName);
        },
    },
});
