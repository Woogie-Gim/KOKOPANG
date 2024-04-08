package org.koko.kokopangmulti.Object;

import java.util.concurrent.ConcurrentHashMap;

public class Channel {

    private String channelName;
    private ConcurrentHashMap<String, Integer> nameToIdx;
    private ConcurrentHashMap<Integer, String> idxToName;
    private SessionsInChannel sessionsInChannel;
    private Boolean isOnGame;
    private ConcurrentHashMap<String, Integer> sessionList;
    private int channelIdx;

    public int getChannelIdx() {
        return channelIdx;
    }

    public void setChannelIdx(int channelIdx) {
        this.channelIdx = channelIdx;
    }


    // LOBBY CHANNEL
    public Channel(String channelName) {
        this.channelName = channelName;
        this.sessionList = new ConcurrentHashMap<>();
    }

    // GAME CHANNEL 생성자
    public Channel(String channelName, String userName) {
        this.channelName = channelName;
        this.nameToIdx = new ConcurrentHashMap<>();
        this.nameToIdx.put(userName, 0);
        this.idxToName = new ConcurrentHashMap<>();
        this.idxToName.put(0, userName);
        this.sessionsInChannel = new SessionsInChannel();   // cnt=1, isExisted=[1,0,0,0,0,0]
        this.isOnGame = false;
        this.sessionList = new ConcurrentHashMap<>();
        this.channelIdx = 0;
    }

    /*
     * CHANNEL 정보 반환 : 채널 이름, SESSIONLIST
     */
    public String getChannelName() {
        return channelName;
    }

    public ConcurrentHashMap<String, Integer> getNameToIdx() {
        return this.nameToIdx;
    }

    public void addSession(String userName, Integer idx) {
        this.nameToIdx.put(userName, idx);
        this.idxToName.put(idx, userName);
    }

    public SessionsInChannel getSessionsInChannel() {
        return this.sessionsInChannel;
    }

    public ConcurrentHashMap<String, Integer> getSessionList() {
        return this.sessionList;
    }

    public int getIdx(String userName) { return this.nameToIdx.get(userName); }

    public ConcurrentHashMap<Integer, String> getIdxToName() {
        return idxToName;
    }

    public void setOnGame() {
        isOnGame = true;
    }

    public Boolean getOnGame() {
        return isOnGame;
    }
}
