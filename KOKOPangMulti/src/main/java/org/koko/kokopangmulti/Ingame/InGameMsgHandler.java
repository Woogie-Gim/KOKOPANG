package org.koko.kokopangmulti.Ingame;

import com.fasterxml.jackson.databind.ObjectMapper;
import org.json.JSONObject;
import org.koko.kokopangmulti.Braodcast.BroadcastToChannel;

public class InGameMsgHandler {

//    public void filterData(JSONObject data) {
//        int channelIndex = data.getInt("channelIndex");
//
//
//
//        BroadcastToChannel.broadcastMessage(channelIndex, ToJson.loadingToJson(data)).subscribe();
//    }

    public void filterData(int channelIndex, JSONObject json) {
        String type = json.getString("type");

        switch (type) {
            case "changePos":
                BroadcastToChannel.broadcastMessage(channelIndex, ToJson.positionToJson(json)).subscribe();
                break;
            case "changeArm":
                BroadcastToChannel.broadcastMessage(channelIndex, ToJson.equipmentToJson(json)).subscribe();
                break;
            case "attack":
                BroadcastToChannel.broadcastMessage(channelIndex, ToJson.attackToJson(json)).subscribe();
        }
    }
}
