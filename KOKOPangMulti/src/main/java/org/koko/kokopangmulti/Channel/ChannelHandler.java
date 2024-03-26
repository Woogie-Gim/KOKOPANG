package org.koko.kokopangmulti.Channel;

import reactor.netty.Connection;

public class ChannelHandler {
    public static void createChannel(String userName, String channelName) {
        // userName을 통해 connection 정보 추출 및 로비의 세션 목록에서 해당 유저 제거
        Connection connection = ChannelList.getLobby().getSessionList().get(userName);
        ChannelList.getLobby().getSessionList().remove(userName);

        // 방 생성 및 방 목록에 방 추가 하면서 생성된 방의 index 할당
        Channel channel = new Channel(channelName);
        int index = ChannelList.addChannel(channel);

        // 생성된 방에 유저 추가
        ChannelList.getChannelInfo(index).getSessionList().put(userName, connection);

        // 동작 확인
        System.out.println("Lobby: " + ChannelList.getLobby().getSessionList());
        System.out.println(channelName + ": " + ChannelList.getChannelInfo(index).getSessionList());
    }

    public static void joinChannel(int roomIndex, String userName) {
        Channel channel = ChannelList.getChannelList().get(roomIndex);
        System.out.println(channel);
//        channel.getSessionList().put(userName);
    }

    // 채널 내 모든 세션에 메시지 브로드캐스트
//    public Mono<Void> broadcastMessage(String message, Channel channel) {
//        return Flux.fromIterable(channel.getSessionList())
//                .flatMap(session -> session.getConnection().outbound().sendString(Mono.just(message)).then())
//                .then();
//    }
}
