package com.koko.kokopang.user.model;

import jakarta.persistence.*;
import lombok.Getter;
import lombok.Setter;
import lombok.ToString;

@Getter
@Setter
@Entity
@ToString
public class Friendship {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private int id;

    @ManyToOne
    @JoinColumn(name="userId",referencedColumnName = "id")
    private User user;

    private int friendId;
    private int friendRating;
    private Boolean isWaiting = true; // 수락대기중인지 아닌지
    private Boolean isFrom = true; // 내가 보낸건지(true) 상대방이 보낸건지(false)
}