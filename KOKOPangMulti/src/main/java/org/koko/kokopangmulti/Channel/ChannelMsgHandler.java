package org.koko.kokopangmulti.Channel;

import org.json.JSONException;
import org.json.JSONObject;
import org.koko.kokopangmulti.Object.Channel;
import org.koko.kokopangmulti.Object.ChannelList;

public class ChannelMsgHandler {

    private final ChannelHandler channelHandler;

    public ChannelMsgHandler(ChannelHandler channelHandler) {
        this.channelHandler = channelHandler;
    }

    public void filterData(String userName, JSONObject data) throws JSONException {

        String type = data.getString("type");

        switch (type) {
            case "create":
                String channelName  = data.getString("channelName");
                channelHandler.createChannel(userName, channelName);
                break;
            case "join":
                int channelIndex = data.getInt("channelIndex");
                channelHandler.joinChannel(userName, channelIndex);
                break;
            case "leave":
                channelIndex = data.getInt("channelIndex");
                ChannelHandler.leaveChannel(userName, channelIndex);
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
