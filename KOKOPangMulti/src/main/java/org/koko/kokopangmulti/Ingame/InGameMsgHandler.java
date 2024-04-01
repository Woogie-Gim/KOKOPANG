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

    public void broadcast(int channelIndex, JSONObject json) {
        BroadcastToChannel.broadcastMessage(channelIndex, ToJson.locationToJson(json)).subscribe();
    }
}
