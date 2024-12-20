import {
    createRouter,
    createWebHistory,
    type RouteRecordRaw,
} from "vue-router";
import { useAuthStore } from "../stores/authStore";
import MainLayout from "@/layouts/MainLayout.vue";

import LoginPage from "../views/LoginPage.vue";
import DashboardPage from "../views/DashboardPage.vue";
import NotFoundPage from "@/views/NotFoundPage.vue";
import SettingsPage from "@/views/SettingsPage.vue";
import UserManagementPage from "@/views/management/UserManagementPage.vue";
import { Role } from "@/type/auth";

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
                path: "usermanagement",
                name: "UserManagement",
                meta: { requiresRole: Role.ADMIN },
                component: UserManagementPage,
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
        // Chỉ gọi fetchUser nếu user chưa được tải
        if (!authStore.user) {
            const userFetched = await authStore.fetchUser();
            if (!userFetched) {
                localStorage.removeItem("token");
                return next("/login");
            }
        }

        if (to.path === "/login") {
            return next("/dashboard");
        }

        if (
            to.meta.requiresRole &&
            authStore.user?.role !== to.meta.requiresRole
        ) {
            return next("/dashboard");
        }

        return next();
    } else if (to.meta.requiresAuth && !authStore.isAuthenticated) {
        return next("/login");
    } else {
        return next();
    }
});

export default router;
