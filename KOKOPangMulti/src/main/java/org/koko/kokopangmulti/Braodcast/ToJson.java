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
        try {
            ObjectMapper objectMapper = new ObjectMapper();
            Map<String, Object> data = new LinkedHashMap<>();
            data.put("type", "channelList");

            List<Map<String, Object>> jsonArray = new ArrayList<>();

            for(Map.Entry<String, Integer> entry : ChannelList.getLobby().getSessionList().entrySet()) {
                Map<String, Object> session = new HashMap<>();
                session.put(entry.getKey(), entry.getValue());
                jsonArray.add(session);
            }

            data.put("data", jsonArray);

            String jsonString = objectMapper.writeValueAsString(data) + '\n';

            return jsonString;
        } catch (JsonProcessingException e) {
            throw new RuntimeException(e);
        }
    }

    // 채널목록
    public static String channelListToJson() {
        JSONObject jsonObject = new JSONObject();

        List<Object> jsonArray = new ArrayList<>();

        for (Map.Entry<Integer, Channel> entry : ChannelList.getChannelList().entrySet()) {
            JSONObject temp = new JSONObject();
            temp.put("channelIndex", entry.getKey());
            temp.put("channelName", entry.getValue().getChannelName());
            temp.put("cnt", entry.getValue().getSessionsInChannel().getCnt());
            temp.put("isOnGame", entry.getValue().getOnGame());

            jsonArray.add(temp);
        }

        jsonObject.put("type", "channelList");
        jsonObject.put("data", jsonArray);

        return jsonObject.toString() + '\n';
    }
}
