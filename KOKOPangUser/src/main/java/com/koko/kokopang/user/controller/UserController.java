package com.koko.kokopanguser.controller;

import com.koko.kokopanguser.dto.UserDTO;
import com.koko.kokopanguser.model.User;
import com.koko.kokopanguser.service.UserService;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

@RestController
@RequestMapping("/api/user")
public class UserController {

    private final UserService userService;

    public UserController(UserService userService) {
        this.userService = userService;
    }

    /**
     * 회원가입
     * @param userDTO id,pw,name
     * @return 성공 또는 실패 메세지
     */
    @PostMapping("/signup")
    public ResponseEntity<?> signup(@RequestBody UserDTO userDTO) {
        System.out.println(userDTO);
        User myUser = userService.signup(userDTO);

        if (myUser == null) {
            return new ResponseEntity<String>("회원가입 실패", HttpStatus.BAD_REQUEST);
        }


        return new ResponseEntity<String>("회원가입성공", HttpStatus.OK);
    }

    /**
     * 친구목록 조회
     * @return
     * @throws Exception
     */

    @GetMapping("/friends")
    public ResponseEntity<?> getWaitingFriendInfo() throws Exception {
        return new ResponseEntity<String>("asdf", HttpStatus.OK);
    }

    /**
     * 유저 정보 조회
     * @param email 유저 아이디
     * @return 유저 정보
                                                                                                                         */
    @GetMapping("/profile")
    public ResponseEntity<?> getUserProfile(@RequestParam String email) {
        User user = userService.getProfile(email);
        return new ResponseEntity<User>(user, HttpStatus.OK);
    }

    /**
     * 유저 정보 업데이트
     * @param user 수정한 유저 정보 (email은 필수!)
     * @return 수정된 유저 정보
     */
    @PutMapping("/profile/update")
    public ResponseEntity<?> updateUserProfile(@RequestBody User user) {
        User newProfile = userService.updateProfile(user);

        if (newProfile == null) {

            return new ResponseEntity<Void>(HttpStatus.BAD_REQUEST);
        }

        return new ResponseEntity<User>(newProfile, HttpStatus.OK);
    }

    /**
     * 유저 비밀번호 변경
     * @param userId 유저pk값
     * @param password 변경할 비밀번호
     * @return
     */
    @PutMapping("/password/update")
    public ResponseEntity<?> updateUserPassword(@RequestParam int userId, @RequestParam String password) {
        boolean updateSuccess = userService.updatePassword(userId,password);

        if (updateSuccess) {

            return new ResponseEntity<String>("비밀번호 변경 성공",HttpStatus.OK);
        }

        return new ResponseEntity<String>("비밀번호 변경 실패", HttpStatus.BAD_REQUEST);
    }
}
