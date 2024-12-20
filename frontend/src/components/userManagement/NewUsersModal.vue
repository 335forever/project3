<script setup lang="ts">
import { Role, type User } from "@/type/auth";
import UserTable from "./UserTable.vue";
import { onMounted, ref } from "vue";
import { useUserManagementStore } from "@/stores/userManagementStore";
import { useAlert } from "@/composables/useAlert";
const props = defineProps<{
    btn: any;
    users: User[];
}>();
const selectedRoles = ref<(Role | null)[]>([]);
const userManagementStore = useUserManagementStore();
const { showAlert } = useAlert();

const handleGrant = async (id: string, roleName: Role | null) => {
    if (!id || !roleName) return;
    const result = await userManagementStore.grant({
        id,
        roleName,
    });
    showAlert(result.message, result.success ? "success" : "error", 2000);
};

onMounted(() => {
    selectedRoles.value = Array(props.users.length).fill(null);
});
</script>
<template>
    <v-dialog :activator="btn" style="width: 600px; max-width: 90%">
        <template v-slot:default="{ isActive }">
            <v-card title="New users">
                <v-table
                    height="300px"
                    fixed-header
                    density="comfortable"
                    class="table"
                >
                    <thead>
                        <tr>
                            <th class="text-left">Username</th>
                            <th class="text-left">Email</th>
                            <th class="text-left">Grant role</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr v-if="!users.length">
                            <td colspan="3" style="text-align: center">
                                No data!
                            </td>
                        </tr>
                        <tr v-for="(item, index) in users" :key="item.id">
                            <td>{{ item.userName }}</td>
                            <td>{{ item.email }}</td>
                            <td>
                                <div style="display: flex; align-items: center">
                                    <v-select
                                        label="Select role"
                                        style="margin-top: 20px; width: 150px"
                                        :items="[
                                            Role.DESIGNER,
                                            Role.PRODUCER,
                                            Role.SALER,
                                        ]"
                                        v-model="selectedRoles[index]"
                                    />
                                    <v-btn
                                        v-if="selectedRoles[index]"
                                        icon
                                        variant="text"
                                        @click="
                                            () => {
                                                handleGrant(
                                                    item.id,
                                                    selectedRoles[index]
                                                );
                                            }
                                        "
                                    >
                                        <v-icon size="large"
                                            >mdi-account-check</v-icon
                                        >
                                    </v-btn>
                                </div>
                            </td>
                        </tr>
                    </tbody>
                </v-table>
                <template v-slot:actions>
                    <v-btn
                        class="ml-auto"
                        text="Close"
                        @click="isActive.value = false"
                    ></v-btn>
                </template>
            </v-card>
        </template>
    </v-dialog>
</template>
