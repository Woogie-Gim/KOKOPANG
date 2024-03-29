package com.koko.kokopang.board.dto;

import lombok.Getter;
import lombok.Setter;
import lombok.ToString;

@Getter
@Setter
@ToString
public class BoardListDTO {
    private int boardId;
    private String username;
    private String title;
    private String profileImg;
}
