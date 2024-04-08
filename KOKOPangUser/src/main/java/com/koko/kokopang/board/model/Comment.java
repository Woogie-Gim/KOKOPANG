package com.koko.kokopang.board.model;

import jakarta.persistence.*;
import lombok.Getter;
import lombok.Setter;
import org.hibernate.annotations.OnDelete;
import org.hibernate.annotations.OnDeleteAction;

@Entity
@Getter
@Setter
public class Comment{

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;
    private String userEmail;
    private String content;

    @ManyToOne
    @JoinColumn(name = "boardId",referencedColumnName = "id")
    @OnDelete(action = OnDeleteAction.CASCADE)
    private Board board;
    private String created;
    private String modified;
}
