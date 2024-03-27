import { create } from "zustand";
import { persist } from "zustand/middleware";

interface AuthStore {
  PATH: String;
  isLogIn: boolean;
  token: string | null;
  refToken: string | null;
  tokenExpireTime: number | null;
  login: () => void;
  logout: () => void;
  setToken: (token: string | null, refToken: string | null) => void;
  setTokenExpireTime: (time: number | null) => void;
}

const useAuthStore = create(
  persist<AuthStore>(
    (set, get) => ({
      PATH: "http://localhost:8080",
      token: null,
      refToken: null,
      isLogIn: false,
      tokenExpireTime: null,

      login: () => {
        set({ isLogIn: true });
      },
      logout: () => {
        set({ isLogIn: false });
        localStorage.clear();
      },
      setToken: (token, refToken) => {
        set({ token: token });
        set({ refToken: refToken });
      },
      setTokenExpireTime: (time) => {
        set({ tokenExpireTime: time == null ? null : time + 1000 * 60 * 60 });
      },
    }),
    {
      name: "userLoginStatus",
    }
  )
);

export default useAuthStore;
