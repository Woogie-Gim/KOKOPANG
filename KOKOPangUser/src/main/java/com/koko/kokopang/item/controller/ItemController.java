package com.koko.kokopang.item.controller;

import com.koko.kokopang.item.model.PointDTO;
import com.koko.kokopang.item.service.ItemService;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequestMapping("/item")
public class ItemController {

    private final ItemService itemService;

    public ItemController(ItemService itemService) {
        this.itemService = itemService;
    }

    @PostMapping("/create")
    public ResponseEntity<?> createItem(@RequestBody PointDTO pointDTO) {
        itemService.createItem(pointDTO);
        return null;
    }
}

