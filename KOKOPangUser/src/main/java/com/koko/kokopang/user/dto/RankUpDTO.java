package com.koko.kokopang.user.dto;

import lombok.Getter;
import lombok.Setter;

@Getter
@Setter
public class RankUpDTO {

    private int userId;
    private int score;
    private boolean isEscape; // true 탈출 성공, false 탈출 실패
}
