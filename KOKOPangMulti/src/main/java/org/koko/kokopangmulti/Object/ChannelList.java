package org.koko.kokopangmulti.Object;

import java.util.HashMap;

public class ChannelList {

    private static final ChannelList instance = new ChannelList();
    private static Integer index = 0;

    private static final HashMap<Integer, Channel> channelList = new HashMap<>();

    public static synchronized int addChannel(Channel channel) {
        index++;
        channelList.put(index, channel);
        return index;
    }

    public synchronized void removeChannel(int index) {
        channelList.remove(index);
    }

    // 채널 목록 반환
    public static HashMap<Integer, Channel> getChannelList() {
        return channelList;
    }

    // 채널 정보 반환
    public static Channel getChannelInfo(int index) {
        return channelList.get(index);
    }

    public static ChannelList getInstance() {
        return instance;
    }


    // LOBBY 채널 생성
    public static final Channel lobby = new Channel("lobby");

    // LOBBY 채널 반환
    public static Channel getLobby() {
        return lobby;
    }
}
