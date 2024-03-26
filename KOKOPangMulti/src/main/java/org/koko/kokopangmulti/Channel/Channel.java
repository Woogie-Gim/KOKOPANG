package org.koko.kokopangmulti.Channel;

import reactor.netty.Connection;

import java.util.HashMap;

public class Channel {
    private String channelName;
    private HashMap<String, Connection> sessionList;


    public Channel(String channelName) {
        this.channelName = channelName;
        this.sessionList = new HashMap<>();
    }

    public Channel() {

    }

    /*
     * CHANNEL 정보 반환 : 채널 이름, SESSIONLIST
     */
    public String getChannelName() {
        return channelName;
    }

    public HashMap<String, Connection> getSessionList() {
        return this.sessionList;
    }

}
