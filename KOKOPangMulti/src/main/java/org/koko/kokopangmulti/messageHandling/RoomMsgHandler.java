package org.koko.kokopangmulti.messageHandling;

import com.fasterxml.jackson.databind.ObjectMapper;
import org.json.JSONException;
import org.json.JSONObject;
import org.koko.kokopangmulti.Channel.ChannelHandler;


public class RoomMsgHandler {

    public void filterData(String userName, JSONObject data) throws JSONException {

        String type = data.getString("type");

        if (type.equals("create")) {
            String channelName = data.getString("channelName");
            ChannelHandler.createChannel(userName, channelName);
        } else if (type.equals("join")) {
            int channelIndex = data.getInt("channelIndex");
            ChannelHandler.joinChannel(userName, channelIndex);
        } else if (type.equals("leave")) {
            int channelIndex = data.getInt("channelIndex");
            ChannelHandler.leaveChannel(userName, channelIndex);
        }
    }

}
