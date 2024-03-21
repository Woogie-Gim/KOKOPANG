package com.example.nettyedu2.User;

import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RestController;

import java.util.HashMap;

@RestController
public class UserController {

    @GetMapping("/api/list")
    public HashMap<String, String> getClients() {
        return UserService.getClientListSimple();
    }
}
