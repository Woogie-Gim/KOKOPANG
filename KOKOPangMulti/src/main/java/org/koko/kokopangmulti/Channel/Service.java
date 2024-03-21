package org.koko.kokopangmulti.Channel;

import io.netty.channel.Channel;
import reactor.netty.Connection;

import java.util.concurrent.ConcurrentHashMap;

public class Service {

    // 로비
    public static synchronized void addLobby(String userName, Connection connection) {
        DTO.getLobby().;
        System.out.println("[Lobby]" + userName);
    }

    // 방 생성
    public static synchronized void createChannel(String channelName, String userName, Channel channel) {
        DTO.getLobby().remove(userName);
        DTO.getChannelList().get(channelName); // 커넥션 정보(채널에 등록 필요)

    }

    // 방 참가


    // 방 나가기


    // 게임 시작
}
