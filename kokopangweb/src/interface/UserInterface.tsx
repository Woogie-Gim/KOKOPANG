interface UserInterface {
  userId: number | null;
  email: string | null;
  name: string | null;
  gender?: string | null;
  role?: string | null;
  rating: number | null;
}

export type { UserInterface };
