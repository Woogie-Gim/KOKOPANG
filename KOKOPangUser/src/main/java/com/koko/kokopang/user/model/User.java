package com.koko.kokopang.user.model;

import jakarta.persistence.*;
import lombok.Getter;
import lombok.Setter;
import lombok.ToString;

import java.util.ArrayList;
import java.util.List;

@Entity
@Getter
@Setter
@ToString
public class User {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private int id;
    private String email;
    private String password;
    private String name;
    private String nickname;
    private String role;
    private String gender;

    @OneToMany(mappedBy = "user")
    private List<Friendship> friendsList = new ArrayList<>();
}