package com.koko.kokopang.user.service;

import com.koko.kokopang.user.controller.UserProfileController;
import com.koko.kokopang.user.dto.FriendDTO;
import com.koko.kokopang.user.dto.FriendshipDTO;
import com.koko.kokopang.user.dto.UserDTO;
import com.koko.kokopang.user.model.Friendship;
import com.koko.kokopang.user.model.User;
import com.koko.kokopang.user.model.UserProfile;
import com.koko.kokopang.user.repository.FriendshipRepository;
import com.koko.kokopang.user.repository.UserRepository;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.http.ResponseEntity;
import org.springframework.stereotype.Service;

import java.util.ArrayList;
import java.util.List;

@Service
public class FriendshipServiceImpl implements FriendshipService{

    @Value("${file.request.path}")
    private String fileRequestPath;

    private final FriendshipRepository friendshipRepository;
    private final UserService userService;
    private final UserRepository userRepository;
    private final UserProfileService userProfileService;

    public FriendshipServiceImpl(FriendshipRepository friendshipRepository, UserService userService, UserRepository userRepository, UserProfileService userProfileService) {
        this.friendshipRepository = friendshipRepository;
        this.userService = userService;
        this.userRepository = userRepository;
        this.userProfileService = userProfileService;
    }

    @Override
    public Friendship addToFriends(FriendDTO friendDTO) {
        int userId = friendDTO.getUserId();
        int friendId = friendDTO.getFriendId();

        if (userRepository.findById(friendId) == null) {
            return null;
        }

        Friendship friendRequest = new Friendship();
        friendRequest.setUser(userRepository.findById(userId));
        friendRequest.setFriendId(friendId);
        friendRequest.setFriendRating(userRepository.findById(friendId).getRating());

        friendshipRepository.save(friendRequest);

        return friendRequest;
    }

    @Override
    public void acceptFriend(FriendDTO friendDTO) {
        // FriendDTO에 맞는 Friendship 찾아오기
        Friendship friendship = friendshipRepository.findByUserIdAndFriendId(friendDTO.getUserId(), friendDTO.getFriendId());
        // isWaiting을 false로 바꾸며 친구요청 수락
        friendship.setIsWaiting(false);
        friendshipRepository.save(friendship);
    }

    @Override
    public List<FriendshipDTO> getFriends(int userId) {
        List<Friendship> friendslist = friendshipRepository.findFriendshipByUserId(userId);

        List<FriendshipDTO> userFriendsList = new ArrayList<>();

        for (Friendship friendship : friendslist) {
            FriendshipDTO friend = new FriendshipDTO();

            User user = userRepository.findById(userId);
            User user1 = userRepository.findById(friendship.getFriendId());

            friend.setUserId(userId);
            friend.setFriendId(userId == friendship.getFriendId() ? friendship.getUser().getId() : friendship.getFriendId());
            friend.setFriendName(userId == friendship.getFriendId() ? userRepository.findById(friendship.getUser().getId()).getName() : userRepository.findById(friendship.getFriendId()).getName());
            friend.setIsWaiting(friendship.getIsWaiting());
            friend.setIsFrom(userId != friendship.getFriendId());
            friend.setFriendRating(userId == friendship.getFriendId() ? user1.getRating():user.getRating());
            UserProfile friendProfile = userProfileService
                    .getUserProfile(userId == friendship.getFriendId() ? friendship.getUser().getId() : friendship.getFriendId());
            if (friendProfile != null) {
                friend.setFriendProfileImg(fileRequestPath + "/profile/getImg/" + friendProfile.getSaveFolder() + "/" + friendProfile.getOriginalName() + "/" + friendProfile.getSaveName());
            }
            userFriendsList.add(friend);
        }

        return userFriendsList;
    }

    @Override
    public String getFriendProfile(int userId, int friendId) {
        List<FriendshipDTO> userFriendsList = getFriends(userId);

        for (FriendshipDTO friendship : userFriendsList) {
            if (friendship.getFriendId() == friendId) {
                if (!friendship.getIsWaiting()) {
                    return "friend";
                } else {
                    if (friendship.getIsFrom()) {
                        return "waiting";
                    } else {
                        return "accept";
                    }
                }
            }
        }

        return "notFriend";
    }

    @Override
    public List<UserDTO> getWaitingFriends(int userId) {
        List<Friendship> waitingFriends = friendshipRepository.findFriendshipsByFriendIdAndIsWaiting(userId, true);

        List<UserDTO> userList = new ArrayList<UserDTO>();
        UserDTO userDTO = new UserDTO();

        for(Friendship f : waitingFriends) {
            userDTO.setUserId(f.getUser().getId());
            userDTO.setName(f.getUser().getName());
            userList.add(userDTO);
        }

        return userList;
    }
}