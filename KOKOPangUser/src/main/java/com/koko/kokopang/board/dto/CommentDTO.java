package com.koko.kokopang.board.dto;

import com.koko.kokopang.board.model.Board;
import jakarta.persistence.*;
import lombok.Getter;
import lombok.Setter;
import org.hibernate.annotations.OnDelete;
import org.hibernate.annotations.OnDeleteAction;

@Getter
@Setter
public class CommentDTO {

    private String userEmail;
    private String content;
    private int boardId;
    private String created;
    private String modified;
}
