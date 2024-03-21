package com.koko.kokopang.item.model;

import com.koko.kokopang.item.dto.PointDTO;
import lombok.Getter;
import lombok.Setter;
import lombok.ToString;

import java.util.List;

@Getter
@Setter
@ToString
public class MapItemSpawn {
    private String name;
    private String explanation;
    private String type;
    private List<PointDTO> itemsPoint;
}

