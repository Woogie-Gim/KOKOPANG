package org.koko.kokopangmulti.Braodcast;

import org.koko.kokopangmulti.Object.ChannelList;
import org.koko.kokopangmulti.Object.Session;
import reactor.core.publisher.Flux;
import reactor.core.publisher.Mono;
import reactor.netty.Connection;

public class BroadcastToLobby {
    // 브로드캐스트
    public static Mono<Void> broadcastLobby(String json) {
        return Flux.fromIterable(ChannelList.getLobby().getSessionList().keySet())
                .flatMap(userName -> Session.getSessionList().get(userName).outbound().sendString(Mono.just(json)).then())
                .then();
    }

    public static Mono<Void> broadcastPrivate(Connection connection, String json) {
        return Flux.fromIterable(Session.getSessionList().keySet())
                .flatMap(userName -> {
                    if (Session.getSessionList().get(userName).equals(connection)) {
                        return connection.outbound().sendString(Mono.just(json)).then();
                    }
                    return Mono.empty();
                })
                .then();
    }
}
