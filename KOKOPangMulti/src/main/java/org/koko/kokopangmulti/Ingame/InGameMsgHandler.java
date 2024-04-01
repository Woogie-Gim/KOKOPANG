package org.koko.kokopangmulti.Ingame;

import com.fasterxml.jackson.core.JsonProcessingException;
import com.fasterxml.jackson.databind.ObjectMapper;
import org.json.JSONObject;
import org.koko.kokopangmulti.Braodcast.BroadcastToChannel;
import org.koko.kokopangmulti.Braodcast.ToJson;

public class InGameMsgHandler {
    static ObjectMapper objectMapper = new ObjectMapper();

    public void filterData(JSONObject data) {
        int channelIndex = data.getInt("channelIndex");

        BroadcastToChannel.broadcastMessage(channelIndex, ToJson.loadingToJson(data)).subscribe();
    }

    public void broadcast(JSONObject json) {
        String data = null;

        try {
            data = objectMapper.writeValueAsString(json) + '\n';
            BroadcastToChannel.broadcastMessage(json.getInt("channelIndex"), data).subscribe();
        } catch (JsonProcessingException e) {
            throw new RuntimeException(e);
        }
    }
}
