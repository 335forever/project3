<script lang="ts" setup>
import UserMenu from '@/components/UserMenu.vue';

const menuItems = [
  { text: "Dashboard", to: "/dashboard", icon: "mdi-view-dashboard" },
  { text: "Order Page", to: "/orderpage", icon: "mdi-account-circle" },
  { text: "Settings", to: "/settings", icon: "mdi-cog" },
];
</script>

<template>
  <v-app>
    <v-main>
      <div style="display: flex">
        <div
          style="
            border-right: 1px solid #ccc;
            height: 100vh;
            width: 300px;
            flex-direction: column;
          "
          class="sidebar"
        >
          <v-text
            class="text-center d-inline-block w-100 font-weight-bold"
            style="font-size: 30px"
          >
            QLCV
          </v-text>
          <v-list>
            <v-list-item
              v-for="item in menuItems"
              :key="item.text"
              :to="item.to"
              link
            >
              <v-list-item-content
                style="
                  display: flex;
                  gap: 5px;
                  align-items: center;
                  font-size: 16px;
                "
                ><v-icon>{{ item.icon }}</v-icon
                >{{ item.text }}</v-list-item-content
              >
            </v-list-item>
          </v-list>
        </div>
        <div><router-view style="padding: 10px; padding-top: 60px" /></div>
        <div></div>
      </div>
      <UserMenu />
    </v-main>
  </v-app>
</template>
