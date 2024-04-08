package com.koko.kokopang.item.controller;

import com.koko.kokopang.item.model.Coordinate;
import com.koko.kokopang.item.dto.PointDTO;
import com.koko.kokopang.item.service.ItemService;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@RequestMapping("/item")
@CrossOrigin(origins = "*", methods = {RequestMethod.GET, RequestMethod.POST, RequestMethod.PUT, RequestMethod.DELETE}, maxAge = 6000)
public class ItemController {

    private final ItemService itemService;

    public ItemController(ItemService itemService) {
        this.itemService = itemService;
    }

    @PostMapping("/create")
    public ResponseEntity<?> createItem(@RequestBody PointDTO pointDTO) {
        System.out.println(pointDTO.toString());
        List<Coordinate> pointsList = itemService.createItem(pointDTO);
        return new ResponseEntity<List<Coordinate>>(pointsList, HttpStatus.OK);
    }
}

