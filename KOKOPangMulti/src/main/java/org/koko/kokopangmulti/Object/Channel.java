package org.koko.kokopangmulti.Object;

import reactor.netty.Connection;

import java.util.HashMap;

public class Channel {

    private String channelName;
    private HashMap<String, Integer> nameToIdx;
    private HashMap<Integer, String> idxToName;
    private SessionsInChannel sessionsInChannel;
    private Boolean isOnGame;
    private HashMap<String, Connection> sessionList;

    // LOBBY CHANNEL
    public Channel(String channelName) {
        this.channelName = channelName;
        this.sessionList = new HashMap<>();
    }

    // GAME CHANNEL 생성자
    public Channel(String channelName, String userName) {
        this.channelName = channelName;
        this.nameToIdx = new HashMap<>();
        this.nameToIdx.put(userName, 0);
        this.idxToName = new HashMap<>();
        this.idxToName.put(0, userName);
        this.sessionsInChannel = new SessionsInChannel();   // cnt=1, isExisted=[1,0,0,0,0,0]
        this.isOnGame = false;
    }
    public Channel() {

    }

    /*
     * CHANNEL 정보 반환 : 채널 이름, SESSIONLIST
     */
    public String getChannelName() {
        return channelName;
    }

    public HashMap<String, Integer> getNameToIdx() {
        return this.nameToIdx;
    }

    public void addSession(String userName, Integer idx) {
        this.nameToIdx.put(userName, idx);
        this.idxToName.put(idx, userName);
    }

    public SessionsInChannel getSessionsInChannel() {
        return this.sessionsInChannel;
    }

    public HashMap<String, Connection> getSessionList() {
        return this.sessionList;
    }

}
