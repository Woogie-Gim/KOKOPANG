package com.koko.kokopang.user.dto;

import lombok.Getter;
import lombok.Setter;

@Getter
@Setter
public class UserDTO {

    private int userId;
    private String email;
    private String password;
    private String name;
    private String profileImg;
    private int rating;
}
