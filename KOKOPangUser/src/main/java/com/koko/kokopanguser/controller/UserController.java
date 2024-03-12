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


    @PostMapping("/signup")
    public ResponseEntity<?> signup(@RequestBody UserDTO userDTO) {

        User myUser = userService.createUser(userDTO);

        if (myUser == null) {
            return new ResponseEntity<String>("회원가입 실패", HttpStatus.BAD_REQUEST);
        }


        return new ResponseEntity<String>("회원가입성공", HttpStatus.OK);
    }

    @PostMapping("/login")
    public ResponseEntity<?> login(@RequestBody User user) {
        return null;
    }



    @GetMapping("/friends")
    public ResponseEntity<?> getWaitingFriendInfo() throws Exception {
        return new ResponseEntity<String>("asdf", HttpStatus.OK);
    }


}
