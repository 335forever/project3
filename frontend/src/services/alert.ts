import { reactive } from "vue";

interface AlertState {
  message: string;
  type: "success" | "error" | "info" | "warning";
  duration: number;
  visible: boolean;
}

const alertState = reactive<AlertState>({
  message: "",
  type: "info",
  duration: 3000,
  visible: false,
});

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

export { alertState, showAlert };
