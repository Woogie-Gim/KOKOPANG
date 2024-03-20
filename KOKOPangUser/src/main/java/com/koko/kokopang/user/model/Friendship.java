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
    @JoinColumn(name="user_id",referencedColumnName = "id")
    private User user;

    private int friendId;
    private boolean isWaiting; // 수락대기중인지 아닌지
    private boolean isFrom; // 내가 보낸건지(false) 상대방이 보낸건지(true)

}
