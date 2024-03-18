package com.koko.kokopang.item.service;

import com.koko.kokopang.item.model.MapItemSpawn;
import com.koko.kokopang.item.model.PointDTO;

public interface ItemService {
    MapItemSpawn createItem(PointDTO pointDTO);
}
