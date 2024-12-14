import {
  createRouter,
  createWebHistory,
  type RouteRecordRaw,
} from "vue-router";
import { useAuthStore } from "../stores/useAuthStore";
import LoginPage from "../views/LoginPage.vue";
import DashboardPage from "../views/DashboardPage.vue";
import OrdersPage from "../views/OrderPage.vue";
import HomePage from "../views/HomePage.vue";

const routes: Array<RouteRecordRaw> = [
  {
    path: "/",
    name: "Home",
    component: HomePage,
  },
  { path: "/login", name: "Login", component: LoginPage },
  {
    path: "/dashboard",
    name: "Dashboard",
    component: DashboardPage,
    meta: { requiresAuth: true },
  },
  {
    path: "/orders",
    name: "Orders",
    component: OrdersPage,
  },
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

router.beforeEach((to, from, next) => {
  const authStore = useAuthStore();

  if (to.meta.requiresAuth && !authStore.isAuthenticated) {
    next("/login");
  } else if (to.meta.role && to.meta.role !== authStore.userRole) {
    next("/unauthorized");
  } else {
    next();
  }
});

export default router;
