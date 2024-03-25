package org.koko.kokopangmulti.Channel;

import java.util.ArrayList;
import java.util.List;

public class ChannelList {

    private static final ChannelList instance = new ChannelList();

    private static final List<Channel> channelList = new ArrayList<>();

    public synchronized void addChannel(Channel channel) {
        channelList.add(channel);
    }

    public synchronized boolean removeChannel(Channel channel) {
        return channelList.remove(channel);
    }

    public static List<Channel> getChannelList() {
        return new ArrayList<>(channelList);
    }

    public static ChannelList getInstance() {
        return instance;
    }
}
