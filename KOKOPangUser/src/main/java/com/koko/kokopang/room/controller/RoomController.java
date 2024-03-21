package com.koko.kokopang.room.controller;

import com.koko.kokopang.room.model.RoomDTO;
import com.koko.kokopang.room.service.RoomService;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

@RestController
@RequestMapping("/room")
public class RoomController {

    private final RoomService roomService;

    public RoomController(RoomService roomService) {
        this.roomService = roomService;
    }

    @PostMapping("/create")
    public ResponseEntity<?> createRoom(@RequestBody RoomDTO roomDTO) {
        System.out.println(roomDTO.toString());
        RoomDTO newRoom = roomService.createRoom(roomDTO);

        if (newRoom == null) {
            return new ResponseEntity<String>("방 생성 실패", HttpStatus.BAD_REQUEST);
        }

        return new ResponseEntity<RoomDTO>(newRoom, HttpStatus.OK);
    }
}
