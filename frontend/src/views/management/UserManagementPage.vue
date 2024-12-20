<script setup lang="ts">
import { useAlert } from "@/composables/useAlert";
import { useUserManagementStore } from "@/stores/userManagementStore";
import { Role, type User } from "@/type/auth";
import { onMounted, ref, type Ref } from "vue";
import UserTable from "@/components/userManagement/UserTable.vue";
import NewUsersModal from "@/components/userManagement/NewUsersModal.vue";

const userManagementStore = useUserManagementStore();
userManagementStore.fetchAllRoles();
const { showAlert } = useAlert();

const modalBtn = ref(null);
</script>

<template>
    <div style="height: 100vh">
        <h1>
            Role Membership
            <v-btn ref="modalBtn" icon variant="text">
                <v-badge
                    :content="
                        userManagementStore.usersByRole[Role.UNASSIGNED]
                            .length || 0
                    "
                    class="badge"
                >
                    <v-icon size="large">mdi-bell</v-icon></v-badge
                >
            </v-btn>
            <NewUsersModal v-if="modalBtn" :btn="modalBtn" />
        </h1>

        <div>
            <v-container style="display: flex">
                <v-row>
                    <v-col sm="12" md="6" lg="4"
                        ><UserTable
                            title="Saler User"
                            :users="
                                userManagementStore.usersByRole[Role.SALER]
                            "
                    /></v-col>
                    <v-col sm="12" md="6" lg="4"
                        ><UserTable
                            title="Designer User"
                            :users="
                                userManagementStore.usersByRole[Role.DESIGNER]
                            "
                    /></v-col>
                    <v-col sm="12" md="6" lg="4"
                        ><UserTable
                            title="Producer User"
                            :users="
                                userManagementStore.usersByRole[Role.PRODUCER]
                            "
                    /></v-col>
                </v-row>
            </v-container>
        </div>
    </div>
</template>

<style scoped>
.badge {
    color: #9567ea;
}
</style>
