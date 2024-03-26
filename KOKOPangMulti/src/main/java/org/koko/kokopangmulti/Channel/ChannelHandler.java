package org.koko.kokopangmulti.Channel;

import org.koko.kokopangmulti.Object.Session;
//import org.koko.kokopangmulti.Object.SessionList;
import reactor.core.publisher.Flux;
import reactor.core.publisher.Mono;

public class ChannelHandler {
    public void addLobby(Session session, Channel channel) {
        channel.getLobby().getSessionList().add(session);
        System.out.println(Channel.getLobby().getSessionList());
    }

    public static void joinChannel(int roomIndex, String userName) {
        Channel channel = ChannelList.getChannelList().get(roomIndex);
        System.out.println(channel);
//        channel.getSessionList().put(userName);
    }

    // 채널 내 모든 세션에 메시지 브로드캐스트
    public Mono<Void> broadcastMessage(String message, Channel channel) {
        return Flux.fromIterable(channel.getSessionList())
                .flatMap(session -> session.getConnection().outbound().sendString(Mono.just(message)).then())
                .then();
    }
}
