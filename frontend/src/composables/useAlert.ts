import type { AlertState } from "@/type/common";
import { reactive } from "vue";

// Reactive state cho thông báo
const alertState = reactive<AlertState>({
    message: "",
    type: "info",
    duration: 3000,
    visible: false,
});

// Hàm hiển thị thông báo
function showAlert(
    message: string,
    type: AlertState["type"] = "info",
    duration: number = 3000
) {
    alertState.message = message;
    alertState.type = type;
    alertState.duration = duration;
    alertState.visible = true;

    setTimeout(() => {
        alertState.visible = false;
    }, duration);
}

export function useAlert() {
    return { alertState, showAlert };
}
