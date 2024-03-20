package com.koko.kokopang.user.service;

import com.koko.kokopang.user.model.Friendship;
import com.koko.kokopang.user.repository.FriendshipRepository;
import org.springframework.stereotype.Service;

@Service
public class FriendshipServiceImpl implements FriendshipService{

    private final FriendshipRepository friendshipRepository;

    public FriendshipServiceImpl(FriendshipRepository friendshipRepository) {
        this.friendshipRepository = friendshipRepository;
    }

    @Override
    public Friendship addToFriends(Friendship friendship) {
        return null;
    }
}