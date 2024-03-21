package org.koko.kokopangmulti.Object;

public class Channel {
    String channelName;
    Session[] sessionList;

    public Channel(String channelName, Session[] sessionList) {
        this.channelName = channelName;
        this.sessionList = sessionList;
    }

    public Channel() {}
}
