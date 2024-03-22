package org.koko.kokopangmulti.Channel;

import org.koko.kokopangmulti.Object.Session;

import java.util.ArrayList;
import java.util.List;

public class Channel {
    private String channelName;
    private List<Session> sessionList;

    public Channel(String channelName, List<Session> sessionList) {
        this.channelName = channelName;
        this.sessionList = sessionList;
    }

    public List<Session> getSessionList() {
        return sessionList;
    }

    public Channel() {}

    // Channel클래스 로드시 lobby인스턴스 바로 생성(eager initialization: 이른 초기화)
    public static final Channel lobby = new Channel("lobby", new ArrayList<>());

    public static Channel getLobby() {
        return lobby;
    }

    public String getChannelName() {
        return channelName;
    }
}
