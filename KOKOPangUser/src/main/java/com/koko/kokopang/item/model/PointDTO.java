package com.koko.kokopang.item.model;

import jakarta.persistence.ManyToOne;
import lombok.Getter;
import lombok.Setter;
import lombok.ToString;

@Getter
@Setter
@ToString
public class PointDTO {

    private float firstX;
    private float firstZ;
    private float secondX;
    private float secondZ;
}
