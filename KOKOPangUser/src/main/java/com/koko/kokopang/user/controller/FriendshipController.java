package com.koko.kokopang.user.controller;

import com.koko.kokopang.user.model.Friendship;
import com.koko.kokopang.user.service.FriendshipService;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequestMapping("/friend")
public class FriendshipController {

    private final FriendshipService friendshipService;

    public FriendshipController(FriendshipService friendshipService) {
        this.friendshipService = friendshipService;
    }

    @PostMapping("/add")
    public ResponseEntity<?> addToFriends(@RequestBody Friendship friendship) {
        Friendship successAdd = friendshipService.addToFriends(friendship);

        if (successAdd == null) {
            return new ResponseEntity<String>("친구 추가 실패", HttpStatus.BAD_REQUEST);
        }

        return new ResponseEntity<String>("친구 추가 성공", HttpStatus.OK);
    }
}
