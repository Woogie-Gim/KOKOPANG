package com.koko.kokopang.Rank.controller;

import com.koko.kokopang.Rank.dto.RankDTO;
import com.koko.kokopang.user.dto.UserDTO;
import com.koko.kokopang.user.service.UserService;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.ArrayList;
import java.util.Collections;
import java.util.Comparator;
import java.util.List;

@RestController
@RequestMapping("/rank")
@CrossOrigin(origins = "*", methods = {RequestMethod.GET, RequestMethod.POST, RequestMethod.PUT, RequestMethod.DELETE}, maxAge = 6000)
public class RankController {

    public final UserService userService;

    public RankController(UserService userService) {
        this.userService = userService;
    }

    @GetMapping("/all")
    public ResponseEntity<?> getRank() {
        List<UserDTO> userList = userService.getAllUser();
        userList.sort(Comparator.comparing(UserDTO::getRating).reversed());

        List<RankDTO> rankList = new ArrayList<>();

        int index = 1;
        for (UserDTO userDTO : userList) {
            RankDTO rankDTO = new RankDTO();
            rankDTO.setRanking(index);
            rankDTO.setName(userDTO.getName());
            rankDTO.setProfileImg(userDTO.getProfileImg());
            rankDTO.setRating(userDTO.getRating());

            rankList.add(rankDTO);

            index++;
        }

        return new ResponseEntity<List<RankDTO>>(rankList, HttpStatus.OK);
    }

    @GetMapping("/time")
    public ResponseEntity<?> getTimeRank() {
        List<UserDTO> userList = userService.getAllUser();
        userList.sort(Comparator.comparing(UserDTO::getPlayTime));

        List<RankDTO> rankList = new ArrayList<>();

        int index = 1;
        for (UserDTO userDTO : userList) {
            RankDTO rankDTO = new RankDTO();
            rankDTO.setRanking(index);
            rankDTO.setName(userDTO.getName());
            rankDTO.setProfileImg(userDTO.getProfileImg());
            int time = userDTO.getPlayTime();
            rankDTO.setPlayTime((time/60) + "분 " + (time%60) + "초");
            rankList.add(rankDTO);

            index++;
        }

        return new ResponseEntity<List<RankDTO>>(rankList, HttpStatus.OK);
    }
}
