<template>
    <div
        style="
            border-right: 1px solid #ccc;
            height: 100vh;
            width: 300px;
            display: flex;
            flex-direction: column;
        "
        class="sidebar"
    >
        <!-- Title -->
        <p
            class="text-center d-inline-block w-100 font-weight-bold"
            style="font-size: 30px; padding: 10px 0"
        >
            QLCV
        </p>

        <!-- List -->
        <v-list dense>
            <!-- Static List Item -->
            <v-list-item class="tab" to="/dashboard" link>
                <v-icon start>mdi-view-dashboard</v-icon>
                Dashboard
            </v-list-item>

            <!-- Accordion List Item -->
            <v-expansion-panels expand>
                <v-expansion-panel>
                    <!-- Title for Accordion -->
                    <v-expansion-panel-title class="tab">
                        <v-icon start>mdi-folder</v-icon>
                        Management
                    </v-expansion-panel-title>

                    <!-- Content for Accordion -->
                    <v-expansion-panel-text>
                        <v-list>
                            <v-list-item
                                v-if="userRole === Role.ADMIN"
                                to="/usermanagement"
                                link
                            >
                                <v-icon start>mdi-account</v-icon>
                                Users
                            </v-list-item>
                            <v-list-item to="/settings" link>
                                <v-icon start>mdi-cog</v-icon>
                                Settings
                            </v-list-item>
                        </v-list>
                    </v-expansion-panel-text>
                </v-expansion-panel>
            </v-expansion-panels>
        </v-list>
        <p
            style="
                font-style: italic;
                text-align: center;
                margin-top: auto;
                margin-bottom: 10px;
            "
        >
            You are logged in as:
            <span style="font-weight: bold">{{ userRole }}</span>
        </p>
    </div>
</template>

<script lang="ts" setup>
import { useAuthStore } from "@/stores/authStore";
import { Role } from "@/type/auth";
import { computed } from "vue";

const authStore = useAuthStore();
const userRole = computed(() => authStore.userInfo?.role);
</script>

<style scoped>
.sidebar {
    background-color: #f8f9fa;
}

.sidebar .v-list-item:hover {
    background-color: #e4e4e4;
    cursor: pointer;
}

.tab {
    height: 30px;
}
</style>
