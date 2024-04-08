package org.koko.kokopangmulti.InGame;

import org.json.JSONException;
import org.json.JSONObject;
import org.koko.kokopangmulti.Braodcast.BroadcastToChannel;
import org.koko.kokopangmulti.Braodcast.ToJson;

public class InGameMsgHandler {
    public void filterData(int channelIndex, JSONObject data) throws JSONException {
        String type = data.getString("type");

        switch (type) {
            case "changePos":
                BroadcastToChannel.broadcastMessage(channelIndex, ToJson.positionToJson(data)).subscribe();
                break;
            case "score":
                BroadcastToChannel.broadcastMessage(channelIndex, ToJson.scoreToJson(data)).subscribe();
                break;
            case "clear":
                BroadcastToChannel.broadcastMessage(channelIndex, ToJson.clearToJson(data)).subscribe();
                break;
        }
    }
}
