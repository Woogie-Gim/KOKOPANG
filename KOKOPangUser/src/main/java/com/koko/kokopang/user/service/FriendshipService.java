package com.koko.kokopang.user.service;

import com.koko.kokopang.user.dto.FriendDTO;
import com.koko.kokopang.user.dto.FriendshipDTO;
import com.koko.kokopang.user.dto.UserDTO;
import com.koko.kokopang.user.model.Friendship;
import com.koko.kokopang.user.model.User;

import java.util.List;

public interface FriendshipService {
    Friendship addToFriends(FriendDTO friendDTO);

    void acceptFriend(FriendDTO friendDTO);

    List<FriendshipDTO> getFriends(int userId);

    String getFriendProfile(int userId ,int friendId);

    List<UserDTO> getWaitingFriends(int userId);
}
