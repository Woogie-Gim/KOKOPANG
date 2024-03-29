package com.koko.kokopang.board.model;

import com.koko.kokopang.board.dto.BaseTimeEntity;
import jakarta.persistence.*;
import lombok.Getter;
import lombok.Setter;
import org.hibernate.annotations.OnDelete;
import org.hibernate.annotations.OnDeleteAction;

@Entity
@Getter
@Setter
public class Comment extends BaseTimeEntity {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;
    private String userEmail;
    private String content;

    @ManyToOne
    @JoinColumn(name = "boardId",referencedColumnName = "id")
    @OnDelete(action = OnDeleteAction.CASCADE)
    private Board board;
}
