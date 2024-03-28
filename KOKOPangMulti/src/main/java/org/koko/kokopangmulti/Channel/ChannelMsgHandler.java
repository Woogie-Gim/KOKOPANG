package org.koko.kokopangmulti.Channel;

import org.json.JSONException;
import org.json.JSONObject;
import org.koko.kokopangmulti.Object.Channel;
import org.koko.kokopangmulti.Object.ChannelList;

public class ChannelMsgHandler {

    public void filterData(String userName, JSONObject data) throws JSONException {

        String type = data.getString("type");

        switch (type) {
            case "create":
                String channelName  = data.getString("channelName");
                ChannelHandler.createChannel(userName, channelName);
                break;
            case "join":
                int channelIndex = data.getInt("channelIndex");
                ChannelHandler.joinChannel(userName, channelIndex);
                break;
            case "leave":
                int channelIdx = data.getInt("channelIndex");
                ChannelHandler.leaveChannel(userName, channelIdx);
                break;
        }
    }

    public void roomJoined() {
        System.out.println("ROOM JOINED");
    }

    public void roomLeaved() {
        System.out.println("ROOM LEAVED");
    }

}
