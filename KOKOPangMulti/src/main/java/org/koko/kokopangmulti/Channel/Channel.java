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

    public HashMap<String, Connection> getSessionList() {
        return this.sessionList;
    }

    public Channel() {}

    // Channel클래스 로드시 lobby인스턴스 바로 생성(eager initialization: 이른 초기화)
    public static final Channel lobby = new Channel("lobby");

    public static Channel getLobby() {
        return lobby;
    }

    public String getChannelName() {
        return channelName;
    }
}
