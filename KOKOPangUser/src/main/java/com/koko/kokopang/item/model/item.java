package com.koko.kokopang.item.model;

import jakarta.persistence.*;
import lombok.Getter;
import lombok.Setter;

@Entity
@Getter
@Setter
public class item {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private int id;
    private String name;
    private String explanation;

    @ManyToOne
    @JoinColumn(name = "id")
    private itemType itemTypeId;
}
