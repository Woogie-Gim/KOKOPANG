package com.koko.kokopang.game.dto;

import com.koko.kokopang.user.model.User;
import jakarta.persistence.*;
import lombok.Getter;
import lombok.Setter;

@Getter
@Setter
@Entity
public class GameResultDTO {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private int id;

    private int userId;
    private int totalPlay;
    private int winPlay;
    private int losePlay;
}