package org.koko.kokopangmulti.Braodcast;

import org.json.JSONObject;
import org.koko.kokopangmulti.Object.Channel;
import org.koko.kokopangmulti.Object.ChannelList;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

public class ToJson {
    // 로비 유저목록
    public static String lobbySessionsToJson() {
        JSONObject jsonObject = new JSONObject();
        jsonObject.put("type", "sessionList");

        List<Map<String, Object>> jsonArray = new ArrayList<>();

        for(Map.Entry<String, Integer> entry : ChannelList.getLobby().getSessionList().entrySet()) {
            Map<String, Object> session = new HashMap<>();
            session.put(entry.getKey(), entry.getValue());
            jsonArray.add(session);
        }

        jsonObject.put("data", jsonArray);

        return jsonObject.toString() + '\n';
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
