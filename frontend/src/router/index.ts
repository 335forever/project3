import {
  createRouter,
  createWebHistory,
  type RouteRecordRaw,
} from "vue-router";
import { useAuthStore } from "../stores/useAuthStore";
import MainLayout from "@/layouts/MainLayout.vue";

import LoginPage from "../views/LoginPage.vue";
import DashboardPage from "../views/DashboardPage.vue";
import OrdersPage from "../views/OrderPage.vue";
import NotFoundPage from "@/views/NotFoundPage.vue";
import SettingsPage from "@/views/SettingsPage.vue";

const routes: Array<RouteRecordRaw> = [
  { path: "/login", name: "Login", component: LoginPage },
  {
    path: "/",
    name: "MainLayout",
    component: MainLayout,
    meta: { requiresAuth: true },
    redirect: "/dashboard",
    children: [
      {
        path: "dashboard",
        name: "Dashboard",
        component: DashboardPage,
      },
      {
        path: "orderpage",
        name: "OrderPage",
        component: OrdersPage,
      },
      {
        path: "rolemanager",
        name: "RoleManager",
        component: OrdersPage,
      },
      {
        path: "settings",
        name: "Settings",
        component: SettingsPage,
      },
    ],
  },
  {
    path: "/:pathMatch(.*)*",
    name: "NotFound",
    component: NotFoundPage,
  },
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

router.beforeEach(async (to, _from, next) => {
  const authStore = useAuthStore();
  const token = localStorage.getItem("token");

  if (token) {
    const userFetched = await authStore.fetchUser();
    if (userFetched) {
      if (to.path === "/login") {
        return next("/dashboard");
      }
      next();
    } else {
      localStorage.removeItem("token");
      next("/login");
    }
  } else if (to.meta.requiresAuth && !authStore.isAuthenticated) {
    next("/login");
  } else {
    next();
  }
});

export default router;
