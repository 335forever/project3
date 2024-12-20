<script setup lang="ts">
import type { User } from "@/type/auth";
defineProps<{
    title: string;
    users: User[];
}>();
</script>
<template>
    <div class="table-container">
        <h2>{{ title }}</h2>
        <v-table
            height="300px"
            fixed-header
            density="comfortable"
            class="table"
        >
            <thead>
                <tr>
                    <th class="text-left">Name</th>
                    <th class="text-left">Email</th>
                </tr>
            </thead>
            <tbody>
                <tr v-if="!users.length">
                    <td colspan="2" style="text-align: center">No data!</td>
                </tr>
                <tr v-for="item in users" :key="item.id">
                    <td>{{ item.userName }}</td>
                    <td>{{ item.email }}</td>
                </tr>
            </tbody>
        </v-table>
    </div>
</template>

<style scoped>
.table-container {
    margin: 10px;
}
.table {
    max-height: 60vh;
}
.table tr:hover {
    background-color: #dcdcdc;
}
</style>
