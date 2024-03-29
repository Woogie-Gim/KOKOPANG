package org.koko.kokopangmulti.Braodcast;

import org.koko.kokopangmulti.Object.ChannelList;
import org.koko.kokopangmulti.Object.Session;
import reactor.core.publisher.Flux;
import reactor.core.publisher.Mono;
import reactor.netty.Connection;

public class BroadcastToLobby {
    // 로비 전체 브로드캐스트
    public static Mono<Void> broadcastLobby(String json) {
        return Flux.fromIterable(ChannelList.getLobby().getSessionList().keySet())
                .flatMap(userName -> {
                    // 커넥션 정보가 없는 경우 일단 스킵하는 코드 추가(임시)
                    if (Session.getSessionList().get(userName) == null) {
                        return Mono.empty();
                    } else {
                        return Session.getSessionList().get(userName).outbound().sendString(Mono.just(json)).then();
                    }
                })
                .then();
    }

    // 특정 유저에게만 브로드캐스트
    public static Mono<Void> broadcastPrivate(Connection connection, String json) {
        return connection.outbound().sendString(Mono.just(json)).then();
    }
}
