import { reactive } from "vue";

interface AlertOptions {
  message: string;
  type: "success" | "error" | "info" | "warning";
  duration: number;
}

export const globalAlertState = reactive({
  isVisible: false,
  message: "",
  type: "success" as AlertOptions["type"],
  duration: 3000,
});

export function useAlert() {
  function show(message: string, type: AlertOptions["type"] = "success", duration: number = 3000) {
    globalAlertState.isVisible = true;
    globalAlertState.message = message;
    globalAlertState.type = type;
    globalAlertState.duration = duration;

    // Tự động ẩn sau `duration` giây
    setTimeout(() => {
      globalAlertState.isVisible = false;
    }, duration);
  }

  function hide() {
    globalAlertState.isVisible = false;
  }

  return { show, hide, globalAlertState };
}
