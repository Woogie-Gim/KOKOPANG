package com.koko.kokopang.item.service;

import com.koko.kokopang.item.model.Coordinate;
import com.koko.kokopang.item.model.PointDTO;
import org.springframework.stereotype.Service;

import java.util.*;

@Service
public class ItemServiceImpl implements ItemService{

    private static final Random random = new Random();

    // 아이템별 아이템드랍 정보 만드는 함수
    private HashMap<String, Integer> createItemDropInfo(int type) {
        HashMap<String, Integer> itemDropInfo = new HashMap<>(); // 새로운 해쉬맵
        // 타입별 key(드랍아이템 정보 - 인덱스) : value(드랍되는 개수 - int)
        switch (type) {
            case 0:
                itemDropInfo.put("0", random.nextInt(1, 4));
                itemDropInfo.put("1", random.nextInt(1, 4));
                itemDropInfo.put("2", random.nextInt(1, 4));
                break;
            case 1:
                itemDropInfo.put("3", random.nextInt(1, 4));
                itemDropInfo.put("4", random.nextInt(1, 4));
                break;
            case 2:
                itemDropInfo.put("5", random.nextInt(1, 4));
            case 3:
                itemDropInfo.put("6", random.nextInt(1, 4));
                break;
            case 4:
                itemDropInfo.put("7", random.nextInt(1, 4));
                itemDropInfo.put("8", random.nextInt(1, 4));
                itemDropInfo.put("9", random.nextInt(1, 4));
                break;
            case 5:
                itemDropInfo.put("10", random.nextInt(1, 4));
                itemDropInfo.put("11", random.nextInt(1, 4));
                break;
        }
        return itemDropInfo;
    }

    @Override
    public List<Coordinate> createItem(PointDTO pointDTO) {
        // x 좌표
        double firstX = pointDTO.getFirstX();
        double secondX = pointDTO.getSecondX();

        // y 좌표
        double firstZ = pointDTO.getFirstZ();
        double secondZ = pointDTO.getSecondZ();

        // [나무,돌,소,돼지,오크통,랜덤박스,녹슨 자동차]
        int[] typeList = new int[]{20, 20, 15, 15, 15, 5, 10};

        // 어떤 타입의 아이템이 어떤 드랍아이템을 가지고 있는지 (ex. 4번 인덱스 값이 1이면 부싯돌은 돌을 캐야 얻기 가능)
        // [목재, 나뭇잎, 나무 열매, 돌, 부싯돌, 소고기, 돼지고기, 철, 스크랩, 스크류, 구리, 플라스틱, 플레어건]
        // int[] dropItemInfoList = new int[]{0,0,0,1,1,2,3,4,4,4,6,6,5};

        // { "x": 좌표 , "y" : 좌표 , "type": int, "hp": int, }
        List<Coordinate> pointsList = new ArrayList<Coordinate>();

        int row = 40;
        int col = 10;

        // 아이템 개수 row * col 개
        double dividedNumberX = Math.abs(secondX - firstX) / row; // 가로 (더 긴 부분이 가로)
        double dividedNumberZ = Math.abs(secondZ - firstZ) / col; // 세로

        double startX = firstX;
        double startZ = firstZ;

        for (double currPointZ = firstZ + dividedNumberZ; currPointZ <= secondZ; currPointZ += dividedNumberZ) {
            for (double currPointX = firstX + dividedNumberX; currPointX <= secondX; currPointX += dividedNumberX ) {
                Coordinate points = new Coordinate();

                double newZ = random.nextDouble() * (currPointZ - startZ) + startZ;
                points.setZ(newZ);

                double newX = random.nextDouble() * (currPointX - startX) + startX;
                points.setX(newX);

                pointsList.add(points);

                startX = startX + dividedNumberX;
            }

            startX = firstX;
            startZ = startZ + dividedNumberZ;
        }

        // 총 아이템 개수의 25%
        int rate =  (row * col) / 4;

        List<Integer> idxList = new ArrayList<Integer>();

        for (int i = 0; i < (row * col); i++ ) {
            idxList.add(i);
        }

        // 전체 pointsList에서 25% 를 랜덤으로 뽑는다
        Collections.shuffle(idxList);
        List<Integer> randomPoints = idxList.subList(0, rate);

        int startidx = 0;

        // 타입 비율 맞춰서 배분해주기
        for (int i = 0; i < 7; i ++) {
            int itemCount = (randomPoints.size() * typeList[i]) / 100; // 300

            for (int j = startidx ; j < startidx + itemCount; j++) {
                Coordinate item = pointsList.get(randomPoints.get(j));
                item.setType(i);
            }

            startidx = startidx + itemCount;
        }

        // 아이템별 드랍 정보
        for (int i = 0; i < randomPoints.size(); i++) {
            int type = pointsList.get(randomPoints.get(i)).getType(); // 타입 얻기

            Coordinate item = pointsList.get(randomPoints.get(i)); // 수정할 item 얻기

            List<HashMap<String, Integer>> itemInfo = new ArrayList<>(); // 새로운 해쉬맵리스트 생성

            HashMap<String, Integer> itemDropInfo = createItemDropInfo(type); // 타입별 정보 생성

            itemInfo.add(itemDropInfo); // 리스트에 추가

            item.setItemInfo(itemInfo); // 아이템정보 추가
        }

        // 랜덤 박스 정보
        for (int i = 0; i < randomPoints.size(); i++) {
            Coordinate randomBox = pointsList.get(randomPoints.get(i));

            if (randomBox.getType() == 5) {
                double rv = random.nextDouble(); // 터질지 안 터질지
                List<HashMap<String, Integer>> boxInfo = new ArrayList<>();
                HashMap<String, Integer> boxDropInfo = new HashMap<>();

                // 아이템 별 랜덤박스 드랍 확률 정보
                // [목재 - 0, 나뭇잎 - 1, 나무 열매 - 2, 돌 - 3, 부싯돌 - 4, 철 - 7, 스크랩 - 8, 스크류 - 9, 구리 - 10, 플라스틱 - 11, 플레어건 - 12]
                // double[] probabilityList = new double[]{0.15, 0.15, 0.135, 0.1, 0.08, 0.08, 0.15, 0.05, 0.05, 0.05, 0.005};

                if (rv < 0.1) {
                    boxDropInfo.put("-1",-1); // -1 이면 터짐 0 ~ 4,7 ~ 12 는 안 터짐
                    boxInfo.add(boxDropInfo);
                    randomBox.setItemInfo(boxInfo);
                } else {
                    double rv2 = random.nextDouble(); // 어떤 아이템이 나올지
                    if (rv2 < 0.15) {
                        boxDropInfo.put("0", random.nextInt(1, 4));
                    } else if (0.15 <= rv2 && rv2 < 0.3) {
                        boxDropInfo.put("1", random.nextInt(1, 4));
                    } else if (0.3 <= rv2 && rv2 < 0.435) {
                        boxDropInfo.put("2", random.nextInt(1, 4));
                    } else if (0.435 <= rv2 && rv2 < 0.535) {
                        boxDropInfo.put("3", random.nextInt(1, 4));
                    } else if (0.535 <= rv2 && rv2 < 0.615) {
                        boxDropInfo.put("4", random.nextInt(1, 4));
                    } else if (0.615 <= rv2 && rv2 < 0.695) {
                        boxDropInfo.put("7", random.nextInt(1, 4));
                    } else if (0.695 <= rv2 && rv2 < 0.845) {
                        boxDropInfo.put("8", random.nextInt(1, 4));
                    } else if (0.845 <= rv2 && rv2 < 0.895) {
                        boxDropInfo.put("9", random.nextInt(1, 4));
                    } else if (0.895 <= rv2 && rv2 < 0.945) {
                        boxDropInfo.put("10", random.nextInt(1, 4));
                    } else if (0.945 <= rv2 && rv2 < 0.995) {
                        boxDropInfo.put("11", random.nextInt(1, 4));
                    } else {
                        boxDropInfo.put("12", 1);
                    }
                    boxInfo.add(boxDropInfo);
                    randomBox.setItemInfo(boxInfo);
                }
            }
        }

        return pointsList;
    }
}