import { create } from "zustand";
import { persist } from "zustand/middleware";
import { UserInterface } from "../interface/UserInterface";

interface UserStore extends UserInterface {
  setUser: (userInfo: UserInterface) => void;
  
  profileImage: string;
  setProfileImage: (image: string) => void;

  friendsList: Array<any>;
  setFriendsList: (friends: Array<any>) => void;
}

const useUserStore = create(
  persist<UserStore>(
    (set) => ({
      id: null,
      email: null,
      name: null,
      gender: null,
      nickname: null,
      password: null,
      role: null,
      rating: 1000,

      profileImage:
      "https://cdn.pixabay.com/photo/2015/10/05/22/37/blank-profile-picture-973460_1280.png",
      
      friendsList: [],

      setUser: (userInfo) => {
        set({
          id: userInfo.id,
          email: userInfo.email,
          name: userInfo.name,
          gender: userInfo.gender,
          nickname: userInfo.nickname,
          password: userInfo.password,
          role: userInfo.role,
          rating: userInfo.rating,
        });
      },

      setProfileImage: (image) => {
        set({ profileImage: image });
      },

      setFriendsList: (friends) => {
        set({ friendsList: friends })
      }
    }),
    
    {
      name: "userStatus",
    }
  )
);

export default useUserStore;
