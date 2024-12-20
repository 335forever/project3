<script lang="ts" setup>
import UserMenu from "@/components/UserMenu.vue";
import SideBar from "@/components/SideBar.vue";
</script>

<template>
    <v-app>
        <v-main>
            <div style="display: flex">
                <SideBar />
                <div style="width: 100%">
                    <router-view style="padding: 10px; padding-top: 60px" />
                </div>
            </div>
            <UserMenu />
        </v-main>
    </v-app>
</template>
