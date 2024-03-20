package com.koko.kokopang.item.model;

import lombok.Getter;
import lombok.Setter;
import lombok.ToString;

@Getter
@Setter
@ToString
public class Coordinate {
    private Double x;
    private Double z;
    private int type;
    private ItemInfo itemInfo;
    private int hp;
}
