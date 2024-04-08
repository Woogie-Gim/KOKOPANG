package com.koko.kokopang.user.dto;

import lombok.Getter;
import lombok.Setter;
import lombok.ToString;

@Getter
@Setter
@ToString
public class FriendDTO {
    private int userId;
    private int friendId;
}
