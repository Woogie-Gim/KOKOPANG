interface UserInterface {
  id: number | null;
  email: string | null;
  name: string | null;
  gender?: string | null;
  nickname?: string | null;
  password?: string | null;
  role?: string | null;
  rating: number | null;
}

export type { UserInterface };
