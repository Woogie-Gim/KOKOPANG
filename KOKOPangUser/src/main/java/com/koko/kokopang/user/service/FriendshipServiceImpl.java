package com.koko.kokopang.user.service;

import com.koko.kokopang.user.controller.UserProfileController;
import com.koko.kokopang.user.dto.FriendDTO;
import com.koko.kokopang.user.dto.FriendshipDTO;
import com.koko.kokopang.user.model.Friendship;
import com.koko.kokopang.user.model.UserProfile;
import com.koko.kokopang.user.repository.FriendshipRepository;
import com.koko.kokopang.user.repository.UserRepository;
import org.springframework.http.ResponseEntity;
import org.springframework.stereotype.Service;

import java.util.ArrayList;
import java.util.List;

@Service
public class FriendshipServiceImpl implements FriendshipService{

    private final FriendshipRepository friendshipRepository;
    private final UserRepository userRepository;
    private final UserProfileService userProfileService;

    public FriendshipServiceImpl(FriendshipRepository friendshipRepository, UserRepository userRepository, UserProfileService userProfileService) {
        this.friendshipRepository = friendshipRepository;
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
    public List<FriendshipDTO> getFriends(int userId) {
        List<Friendship> friendslist = friendshipRepository.findFriendshipByUserId(userId);

        List<FriendshipDTO> userFriendsList = new ArrayList<>();

        for (Friendship friendship : friendslist) {
            FriendshipDTO friend = new FriendshipDTO();

            friend.setUserId(userId);
            friend.setFriendId(userId == friendship.getFriendId() ? friendship.getUser().getId() : friendship.getFriendId());
            friend.setFriendName(userId == friendship.getFriendId() ? userRepository.findById(friendship.getUser().getId()).getName() : userRepository.findById(friendship.getFriendId()).getName());
            friend.setIsWaiting(friendship.getIsWaiting());
            friend.setIsFrom(userId != friendship.getFriendId());
            friend.setFriendRating(friendship.getFriendRating());
            UserProfile friendProfile = userProfileService
                    .getUserProfile(userId == friendship.getFriendId() ? friendship.getUser().getId() : friendship.getFriendId());
            if (friendProfile != null) {
                friend.setFriendProfileImg("http://localhost:8080/profile/getImg/" + friendProfile.getSaveFolder() + "/" + friendProfile.getOriginalName() + "/" + friendProfile.getSaveName());
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
}