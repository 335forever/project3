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
  roles: Role[];
};

export enum Role {
  ADMIN = "ADMIN",
  PRODUCER = "PRODUCER",
  DESIGNER = "DESIGNER",
  SALER = "SALER",
}
