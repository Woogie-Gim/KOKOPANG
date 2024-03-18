package com.koko.kokopang.room.service;

import com.koko.kokopang.room.model.RoomDTO;
import org.springframework.stereotype.Service;

@Service
public class RoomServiceImpl implements RoomService{

    @Override
    public RoomDTO createRoom(RoomDTO roomDTO) {
        System.out.println(roomDTO.toString());

        RoomDTO newRoom = new RoomDTO();
        newRoom.setName(roomDTO.getName());
        newRoom.setPassword(roomDTO.getPassword());
        newRoom.setUserNumber(roomDTO.getUserNumber());
        newRoom.setIsSecret(roomDTO.getIsSecret());

        return newRoom;
    }
}
