package com.koko.kokopang.board.dto;

import lombok.Getter;
import lombok.Setter;

@Getter
@Setter
public class CommentListDTO {

    private Long commentId;
    private String username;
    private String content;
    private String profileImg;
    private String created;
    private String modified;
}
