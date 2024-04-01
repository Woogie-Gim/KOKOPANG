package org.koko.kokopangmulti.Channel;

import org.json.JSONException;
import org.json.JSONObject;
import org.koko.kokopangmulti.session.SessionState;

public class ChannelMsgHandler {
    public void filterData(String userName, JSONObject data) throws JSONException {

        String type = data.getString("type");
        int channelIndex = 0;

        switch (type) {
            case "create":
                String channelName  = data.getString("channelName");
                ChannelHandler.createChannel(userName, channelName);
                break;
            case "join":
                channelIndex = data.getInt("channelIndex");
                ChannelHandler.joinChannel(userName, channelIndex);
                break;
            case "ready":
                channelIndex = data.getInt("channelIndex");
                ChannelHandler.isReady(userName, channelIndex);
                break;
            case "leave":
                channelIndex = data.getInt("channelIndex");
                ChannelHandler.leaveChannel(userName, channelIndex, SessionState.NORMAL);
                break;
            case "chat":
                channelIndex = data.getInt("channelIndex");
                String message = data.getString("message");
                ChannelHandler.chat(channelIndex, userName, message);
            case "start":
                channelIndex = data.getInt("channelIndex");
                ChannelHandler.startGame(channelIndex);
        }
    }

}
