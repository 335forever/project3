<script setup lang="ts">
import { useAlert } from "@/composables/useAlert";
import { useUserManagementStore } from "@/stores/userManagementStore";
import { Role, type User } from "@/type/auth";
import { onMounted, ref, type Ref } from "vue";
import UserTable from "@/components/userManagement/UserTable.vue";
import NewUsersModal from "@/components/userManagement/NewUsersModal.vue";

const userManagementStore = useUserManagementStore();
const { showAlert } = useAlert();

const salers = ref<User[]>([]);
const designers = ref<User[]>([]);
const producers = ref<User[]>([]);
const newUsers = ref<User[]>([]);

const loading = ref(false);
const errorMessage = ref<string | null>(null);

const fetchRoleMembership = async (role: Role | null, target: Ref<User[]>) => {
    loading.value = true;

    const response = await userManagementStore.getRoleMembership(role);

    if (response.success) {
        target.value = response.users;
    } else {
        errorMessage.value = response.message;
    }

    loading.value = false;
};

const fetchAllRoles = async () => {
    await Promise.all([
        fetchRoleMembership(Role.SALER, salers),
        fetchRoleMembership(Role.DESIGNER, designers),
        fetchRoleMembership(Role.PRODUCER, producers),
        fetchRoleMembership(null, newUsers),
    ]);
};



onMounted(() => {
    fetchAllRoles();
});

const modalBtn = ref(null);
</script>

<template>
    <div style="height: 100vh">
        <h1>
            Role Membership

            <v-btn ref="modalBtn" icon variant="text">
                <v-badge :content="newUsers.length" class="badge">
                    <v-icon size="large">mdi-bell</v-icon></v-badge
                >
            </v-btn>

            <NewUsersModal v-if="modalBtn" :btn="modalBtn" :users="newUsers" />
        </h1>

        <div style="display: flex">
            <div v-if="loading">Loading...</div>
            <div v-if="errorMessage" class="error-message">
                {{ errorMessage }}
            </div>

            <UserTable title="Saler User" :users="salers" />
            <UserTable title="Designer User" :users="designers" />
            <UserTable title="Producer User" :users="producers" />
        </div>
    </div>
</template>

<style scoped>
.badge {
    color: #9567ea;
}
</style>
