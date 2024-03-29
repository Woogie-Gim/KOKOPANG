package org.koko.kokopangmulti.Braodcast;

import com.fasterxml.jackson.core.JsonProcessingException;
import com.fasterxml.jackson.databind.ObjectMapper;
import org.json.JSONObject;
import org.koko.kokopangmulti.Object.Channel;
import org.koko.kokopangmulti.Object.ChannelList;
import org.koko.kokopangmulti.Object.SessionsInChannel;

import java.util.*;

public class ToJson {
    // 로비 유저목록
    public static String lobbySessionsToJson() {
        ObjectMapper objectMapper = new ObjectMapper();
        Map<String, Object> data = new LinkedHashMap<>();
        List<Object> jsonArray = new ArrayList<>();

        for (Map.Entry<String, Integer> entry : ChannelList.getLobby().getSessionList().entrySet()) {
            Map<String, Object> jsonObject = new LinkedHashMap<>();

            jsonObject.put("userName", entry.getKey());
            jsonObject.put("userId", entry.getValue());

            jsonArray.add(jsonObject);
        }

        data.put("type", "sessionList");
        data.put("data", jsonArray);

        try {
            return objectMapper.writeValueAsString(data) + '\n';
        } catch (JsonProcessingException e) {
            throw new RuntimeException(e);
        }
    }

    // 채널목록
    public static String channelListToJson() {
        ObjectMapper objectMapper = new ObjectMapper();
        Map<String, Object> data = new LinkedHashMap<>();
        List<Object> jsonArray = new ArrayList<>();

        for (Map.Entry<Integer, Channel> entry : ChannelList.getChannelList().entrySet()) {
            Map<String, Object> temp = new LinkedHashMap<>();

            temp.put("channelIndex", entry.getKey());
            temp.put("channelName", entry.getValue().getChannelName());
            temp.put("cnt", entry.getValue().getSessionsInChannel().getCnt());
            temp.put("isOnGame", entry.getValue().getOnGame());

            jsonArray.add(temp);
        }

        data.put("type", "channelList");
        data.put("data", jsonArray);

        try {
            return objectMapper.writeValueAsString(data) + '\n';
        } catch (JsonProcessingException e) {
            throw new RuntimeException(e);
        }
    }

    public static String channelSessionListToJSON(Channel channel) {
        ObjectMapper objectMapper = new ObjectMapper();
        Map<String, Object> data = new LinkedHashMap<>();
        List<Object> jsonArray = new ArrayList<>();

        SessionsInChannel sic = channel.getSessionsInChannel();
        List<Integer> isExisted = sic.getIsExisted();
        int cnt = sic.getCnt();

        for (int i=0; i<6; i++) {

            if (isExisted.get(i)==1) {
                Map<String,Object> temp = new LinkedHashMap<>();
                String userName = channel.getIdxToName().get(i);
                temp.put("userName", userName);
                temp.put("userId", channel.getSessionList().get(userName));
                temp.put("isReady", sic.getIsReady(i));
                Boolean isHost = false;
                if (i==0) { isHost = true; }
                temp.put("isHost", isHost);

                jsonArray.add(temp);
                cnt--;
            }

            if (cnt==0) break;

        }
        data.put("type", "channelSessionList");
        data.put("data", jsonArray);

        try {
            return objectMapper.writeValueAsString(data) + '\n';
        } catch (JsonProcessingException e) {
            throw new RuntimeException(e);
        }
    }
}
