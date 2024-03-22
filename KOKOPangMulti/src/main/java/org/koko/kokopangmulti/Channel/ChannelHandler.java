package org.koko.kokopangmulti.Channel;

import org.koko.kokopangmulti.Object.Session;
import reactor.core.publisher.Flux;
import reactor.core.publisher.Mono;

public class ChannelHandler {
    public boolean addLobby(Session session, Channel channel) {
        return channel.getLobby().getSessionList().add(session);
    }

    // 채널 내 모든 세션에 메시지 브로드캐스트
    public Mono<Void> broadcastMessage(String message, Channel channel) {
        return Flux.fromIterable(channel.getSessionList())
                .flatMap(session -> session.getConnection().outbound().sendString(Mono.just(message)).then())
                .then();
    }
}
