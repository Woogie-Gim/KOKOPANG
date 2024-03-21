package com.koko.kokopang.user.controller;

import com.koko.kokopang.user.dto.UserDTO;
import com.koko.kokopang.user.model.User;
import com.koko.kokopang.user.service.UserService;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

@RestController
@RequestMapping("/user")
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
            return new ResponseEntity<String>("회원가입 실패", HttpStatus.CONFLICT); // 409 에러
        }

        return new ResponseEntity<String>("회원가입성공", HttpStatus.OK);
    }

    /**
     * 유저 정보 조회
     * @param userId 유저 아이디
     * @return 유저 정보
     */
    @GetMapping("/profile")
    public ResponseEntity<?> getUserProfile(@RequestParam int userId) {
        User user = userService.getProfile(userId);
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
