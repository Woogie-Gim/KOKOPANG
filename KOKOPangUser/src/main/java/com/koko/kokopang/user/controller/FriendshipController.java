package com.koko.kokopang.user.controller;

import com.koko.kokopang.user.dto.FriendDTO;
import com.koko.kokopang.user.dto.FriendshipDTO;
import com.koko.kokopang.user.dto.UserDTO;
import com.koko.kokopang.user.model.Friendship;
import com.koko.kokopang.user.model.User;
import com.koko.kokopang.user.service.FriendshipService;
import org.apache.coyote.Response;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@RequestMapping("/friend")
@CrossOrigin(origins = "*", methods = {RequestMethod.GET, RequestMethod.POST, RequestMethod.PUT, RequestMethod.DELETE}, maxAge = 6000)
public class FriendshipController {

    private final FriendshipService friendshipService;

    public FriendshipController(FriendshipService friendshipService) {
        this.friendshipService = friendshipService;
    }

    @PostMapping("/add")
    public ResponseEntity<?> addToFriends(@RequestBody FriendDTO friendDTO) {
        Friendship successRequest = friendshipService.addToFriends(friendDTO);

        if (successRequest == null) {
            return new ResponseEntity<String>("친구 추가 실패", HttpStatus.BAD_REQUEST);
        }
        System.out.println(successRequest.toString());
        return new ResponseEntity<String>("친구 추가 성공", HttpStatus.OK);
    }

    /**
     * 친구 요청 수락
     * @param friendDTO 내의 userId, friendId는 friendship DB에 맞춘다
     * @return
     */
    @PostMapping("/accept")
    public ResponseEntity<?> acceptFriendRequest(@RequestBody FriendDTO friendDTO) {
        friendshipService.acceptFriend(friendDTO);

        return new ResponseEntity<Void>(HttpStatus.OK);
    }

    /**
     * 로그인 후 로비 화면에서 나의 친구목록 띄우기 (서로 친구일때만)
     * @param userId
     * @return List<FriendshipDTO>
     */
    @GetMapping("/list")
    public ResponseEntity<?> getFriendList(@RequestParam int userId) {
        List<FriendshipDTO> list = friendshipService.getFriends(userId);

        return new ResponseEntity<List<FriendshipDTO>>(list, HttpStatus.OK);
    }

    /**
     * 상대방 프로필 페이지에서 친구인지 아닌지에 관한 정보
     */
    @GetMapping("/profile")
    public ResponseEntity<?> getFriendInfo(@RequestParam int userId, @RequestParam int friendId) {
        String result = friendshipService.getFriendProfile(userId,friendId);
        return new ResponseEntity<String>(result,HttpStatus.OK);
    }

    /**
     * 내가 받은 친구 요청 목록(친구 대기 상태)
     */
    @GetMapping("/waiting")
    public ResponseEntity<?> getWaitingFriends(@RequestParam int userId) {
        List<UserDTO> list = friendshipService.getWaitingFriends(userId);

        return new ResponseEntity<List<UserDTO>>(list, HttpStatus.OK);
    }
}
