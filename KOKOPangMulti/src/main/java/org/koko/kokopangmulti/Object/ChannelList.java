package org.koko.kokopangmulti.Object;

import org.json.JSONObject;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

public class ChannelList {

    // 채널 목록
    private static final HashMap<Integer, Channel> channelList = new HashMap<>();

    // 채널 생성시 채널에 부여할 채널 인덱스
    private static Integer index = 0;

    // 생성된 채널을 채널 목록에 추가
    public static synchronized int addChannel(Channel channel) {
        index++;                                // 인덱스값 +1
        channelList.put(index, channel);        // 인덱스를 키, 채널을 값으로 리스트에 추가
        channelList.get(index).setChannelIdx(index);
        return index;                           // 인덱스 번호 리턴
    }

    // 채널목록에서 채널 제거
    public synchronized void removeChannel(int index) {
        // 인덱스번호로 채널 목록에 있는 채널 값을 제거
        channelList.remove(index);
    }

    // 채널목록 반환
    public static HashMap<Integer, Channel> getChannelList() {
        return channelList;
    }

    // 해당 index의 채널 반환
    public static Channel getChannelInfo(int index) {
        return channelList.get(index);
    }

    // LOBBY 채널 생성
    public static final Channel lobby = new Channel("lobby");

    // LOBBY 채널 반환
    public static Channel getLobby() {
        return lobby;
    }
}
