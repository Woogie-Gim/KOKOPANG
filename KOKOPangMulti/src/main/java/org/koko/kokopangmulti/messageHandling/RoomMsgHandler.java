package org.koko.kokopangmulti.messageHandling;

import org.json.JSONException;
import org.json.JSONObject;
import org.koko.kokopangmulti.Channel.Channel;
import org.koko.kokopangmulti.Channel.ChannelList;

public class RoomMsgHandler {

    public void filterData(String userName, JSONObject data) throws JSONException {

        String type = data.getString("type");

        switch (type) {
            case "create":
                roomCreated(data);
                break;
            case "join":
                roomJoined();
                break;
            case "leave":
                roomLeaved();
                break;
        }

    }

    public void roomCreated(JSONObject data) {
        System.out.println("ROOM CREATED");
        String channelName = data.getString("channelName");
        String userName = data.getString("userName");
        Channel channel = new Channel(channelName, userName);   // channel 생성
        ChannelList.addChannel(channel);                        // channelList에 추가
        System.out.println(channel.getChannelName()+"'s q = " + channel.getUsers());
    }

    public void roomJoined() {
        System.out.println("ROOM JOINED");
    }

    public void roomLeaved() {
        System.out.println("ROOM LEAVED");
    }

}
