export type User = {
    id: string;
    userName: string;
    isActived: boolean;
    created_at: Date;
    updated_at: Date;
    information: string | null;
    profilePictureLink: string | null;
    socialLinks: string | null;
    email: string;
    emailConfirmed: boolean;
    phoneNumber: string | null;
    phoneNumberConfirmed: boolean;
    role: Role | null;
};

export enum Role {
    ADMIN = "Admin",
    PRODUCER = "Producer",
    DESIGNER = "Designer",
    SALER = "Saler",
    UNASSIGNED = "Unassigned",
}

export interface DecodedToken {
    nameid: string;
    unique_name: string;
    role: string;
    nbf: number;
    exp: number;
    iat: number;
    iss: string;
    aud: string;
}
