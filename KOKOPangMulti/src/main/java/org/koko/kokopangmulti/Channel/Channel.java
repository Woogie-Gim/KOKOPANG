package org.koko.kokopangmulti.Channel;

import org.koko.kokopangmulti.Object.Session;
import reactor.netty.Connection;

import java.util.ArrayDeque;
import java.util.Deque;
import java.util.HashMap;

public class Channel {

    private String channelName;
    private HashMap<String, Connection> sessionList;
    private Deque q;

    // LOBBY CHANNEL
    public Channel(String channelName) {
        this.channelName = channelName;
        this.sessionList = new HashMap<>();
    }

    // GAME CHANNEL
    public Channel(String channelName, String userName) {
        this.channelName = channelName;
        this.sessionList = new HashMap<>();
        this.sessionList.put(userName, Session.getSessionList().get(userName));
        this.q = new ArrayDeque<String>();
        q.addLast(userName); // 들어온 순서대로 기록
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

    public Deque<String> getUsers() {
        return this.q;
    }

}
