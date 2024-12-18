import type { User } from "./auth";

export type AlertType = "success" | "info" | "warning" | "error";

export interface AlertState {
    message: string;
    type: AlertType;
    duration: number;
    visible: boolean;
}

export type TResponse = {
    success: boolean;
    message: string;
};

export type TResponseUsers = TResponse & {
    users: User[];
};

export type TError = {
    code: string;
    description: string;
};
