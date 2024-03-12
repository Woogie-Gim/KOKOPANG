package com.koko.kokopanguser.model;

import jakarta.persistence.*;
import lombok.*;

@Entity
@Getter
@Builder
@AllArgsConstructor
//@NoArgsConstructor(access = AccessLevel.PROTECTED)
public class Friendship {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;

    @ManyToOne(fetch = FetchType.LAZY)
    @JoinColumn(name = "userId")
    private User user;

    private String userEmail;
    private String friendEmail;

    // 수락한지 안 한지
    private boolean isWaiting;

    // 보낸요청인자 아닌지
    private boolean isFrom;

    private Long counterpartId;

    public Friendship() {
        this.isWaiting = true;
    }

    public void acceptFriendshipRequest() {
        isWaiting = false;
    }

}
