package org.koko.kokopangmulti.Braodcast;

import com.fasterxml.jackson.core.JsonProcessingException;
import com.fasterxml.jackson.databind.ObjectMapper;
import org.json.JSONObject;
import org.koko.kokopangmulti.Object.Channel;
import org.koko.kokopangmulti.Object.ChannelList;

import java.util.*;

public class ToJson {
    // 로비 유저목록
    public static String lobbySessionsToJson() {
        ObjectMapper objectMapper = new ObjectMapper();
        Map<String, Object> data = new LinkedHashMap<>();
        List<Map<String, Object>> jsonArray = new ArrayList<>();

        for (Map.Entry<String, Integer> entry : ChannelList.getLobby().getSessionList().entrySet()) {
            Map<String, Object> session = new HashMap<>();
            session.put(entry.getKey(), entry.getValue());
            jsonArray.add(session);
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
            JSONObject temp = new JSONObject();
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
}
