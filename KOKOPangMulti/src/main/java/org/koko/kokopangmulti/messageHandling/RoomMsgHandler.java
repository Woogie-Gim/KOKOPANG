package org.koko.kokopangmulti.messageHandling;

import com.fasterxml.jackson.core.JsonProcessingException;
import com.fasterxml.jackson.core.type.TypeReference;
import com.fasterxml.jackson.databind.ObjectMapper;
import org.json.JSONException;
import org.json.JSONObject;
import org.koko.kokopangmulti.Channel.ChannelHandler;

import java.util.Map;

public class RoomMsgHandler {
    ObjectMapper objectMapper;

    public void filterData(String data) throws JSONException {
        JSONObject json = new JSONObject(data);

        String order = json.getString("order");
        String userName = json.getString("userName");
        int roomIndex = Integer.parseInt(json.getString("index"));

        if (order.equals("join")) {
            ChannelHandler.joinChannel(roomIndex, userName);
        } else if (order.equals("exit")) {
//            exitRoom(userName, roomIndex);
        }
    }

}
