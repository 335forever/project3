import "./assets/main.css";
import { createApp } from "vue";
import { createPinia } from "pinia";
import App from "./App.vue";
import router from "./router";
import { createVuetify } from "vuetify";
import "vuetify/styles";
import "@mdi/font/css/materialdesignicons.css";
import piniaPersist from "pinia-plugin-persistedstate";
import "@mdi/font/css/materialdesignicons.css";

const vuetify = createVuetify({
  icons: {
    defaultSet: "mdi",
  },
  theme: {
    defaultTheme: "light",
  },
});

const pinia = createPinia();
pinia.use(piniaPersist);

const app = createApp(App);
app.use(vuetify);
app.use(pinia);
app.use(router);

app.mount("#app");
