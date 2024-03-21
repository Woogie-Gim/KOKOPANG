package com.koko.kokopang.item.model;

import lombok.Getter;
import lombok.Setter;
import lombok.ToString;

import java.util.HashMap;
import java.util.List;

@Getter
@Setter
@ToString
public class Coordinate {
    private double x;
    private double z;
    private int type = -1;
    private int hp = 3;
    private List<HashMap<String,Integer>> itemInfo;
}
