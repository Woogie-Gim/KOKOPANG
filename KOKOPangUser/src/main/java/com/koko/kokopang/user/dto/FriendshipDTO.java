package com.koko.kokopang.user.dto;

import lombok.Getter;
import lombok.Setter;
import lombok.ToString;

@Getter
@Setter
@ToString
public class FriendshipDTO {

    private int userId;
    private int friendId;
    private String friendName;

    // isWaiting  isFrom
    //  true       true   :  내가 보냈고 아직 수락 안 받음
    //  true       false  :  내가 받았고 아직 수락 안 받음
    //  false      true   :  내가 보냈고 서로 친구임
    //  false      false  :  내가 받았고 서로 친구임
    private Boolean isWaiting;
    private Boolean isFrom;
}
