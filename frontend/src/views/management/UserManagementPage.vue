<script setup lang="ts">
import { useAlert } from "@/composables/useAlert";
import { useUserManagementStore } from "@/stores/userManagementStore";
import { Role } from "@/type/auth";
import { ref } from "vue";

const id = ref<string>("");
const roleName = ref<Role>();

const userManagementStore = useUserManagementStore();
const { showAlert } = useAlert();

const handleGrant = async () => {
    if (!id.value || !roleName.value) return;
    const result = await userManagementStore.grant({
        id: id.value,
        roleName: roleName.value,
    });
    showAlert(result.message, result.success ? "success" : "error", 2000);
};
</script>

<template>
    <div>
        <h1>Test Grant API</h1>
        <div>
            <label for="id">User ID:</label>
            <input
                id="id"
                v-model="id"
                type="text"
                placeholder="Enter user ID"
            />
        </div>
        <div>
            <label for="roleName">Role Name:</label>
            <select id="roleName" v-model="roleName">
                <option :value="Role.DESIGNER">{{ Role.DESIGNER }}</option>
                <option :value="Role.PRODUCER">{{ Role.PRODUCER }}</option>
                <option :value="Role.SALER">{{ Role.SALER }}</option>
            </select>
        </div>
        <button @click="handleGrant">Grant Role</button>
    </div>
</template>
