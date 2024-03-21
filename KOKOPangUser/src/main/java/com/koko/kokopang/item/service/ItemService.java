package com.koko.kokopang.item.service;

import com.koko.kokopang.item.model.Coordinate;
import com.koko.kokopang.item.dto.PointDTO;

import java.util.List;

public interface ItemService {
    List<Coordinate> createItem(PointDTO pointDTO);
}
