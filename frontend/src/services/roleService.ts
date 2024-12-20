import type { GrantTarget } from "@/type/userManagement";
import apiClient from "./apiClient";
import type { Role, User } from "@/type/auth";

export const RoleService = {
    async grant({ id, roleName }: GrantTarget) {
        const response = await apiClient.post(`/admin/grant/${id}/${roleName}`);
        return response.data;
    },
    async getRoleMembership(roleName: Role | null): Promise<User[]> {
        const response = await apiClient.get(
            `/admin/roleMembership/${roleName ?? ""}`
        );
        return response.data;
    },
};
