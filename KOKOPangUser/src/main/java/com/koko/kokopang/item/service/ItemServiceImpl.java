package com.koko.kokopang.item.service;

import com.koko.kokopang.item.model.Coordinate;
import com.koko.kokopang.item.model.PointDTO;
import org.springframework.stereotype.Service;

import java.util.ArrayList;
import java.util.Collections;
import java.util.List;
import java.util.Random;

@Service
public class ItemServiceImpl implements ItemService{

    private static final Random random = new Random();



    @Override
    public List<Coordinate> createItem(PointDTO pointDTO) {
        // x 좌표
        Double firstX = pointDTO.getFirstX();
        Double secondX = pointDTO.getSecondX();

        // y 좌표
        Double firstZ = pointDTO.getFirstZ();
        Double secondZ = pointDTO.getSecondZ();

        // [나무,돌,소,돼지,랜덤박스,오크통]
        int[] typeList = new int[]{30, 30, 15, 15, 5, 5};

        List<Coordinate> pointsList = new ArrayList<Coordinate>();  // { "x": 좌표 , "y" : 좌표 , "type": int, "info":...}

        int row = 50;
        int col = 40;

        // 아이템 개수 48*20 = 960 개
        Double dividedNumberX = Math.abs(secondX - firstX) / row; // 가로 (더 긴 부분이 가로)
        Double dividedNumberZ = Math.abs(secondZ - firstZ) / col; // 세로

        Double startX = firstX;
        Double startZ = firstZ;

        for (Double currPointZ = firstZ + dividedNumberZ; currPointZ <= secondZ; currPointZ += dividedNumberZ) {
            Coordinate points = new Coordinate();

            for (Double currPointX = firstX + dividedNumberX; currPointX <= secondX; currPointX += dividedNumberX ) {
                Double newZ = random.nextDouble() * (currPointZ - startZ) + startZ;
                points.setZ(newZ);

                Double newX = random.nextDouble() * (currPointX - startX) + startX;
                points.setX(newX);

                pointsList.add(points);

                startX = startX + dividedNumberX;
            }

            startX = firstX;
            startZ = startZ + dividedNumberZ;
        }

        int rate =  (row * col) / 4;

        List<Integer> idxList = new ArrayList<Integer>();

        for (int i = 0; i < (row * col); i++ ) {
            idxList.add(i);
        }

        Collections.shuffle(idxList);
        List<Integer> randomPoints = idxList.subList(0, rate);

        int startidx = 0;

        for (int i = 0; i < 6; i ++) {
            int itemCount = randomPoints.size() * (typeList[i] / 100); // 300
            for (int j = startidx; j < itemCount; j++) {

                System.out.println(pointsList.get(randomPoints.get(j)));
            }
        }

        return pointsList;
    }
}
