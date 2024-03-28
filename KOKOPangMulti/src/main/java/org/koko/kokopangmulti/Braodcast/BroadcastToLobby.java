package org.koko.kokopangmulti.Braodcast;

import org.koko.kokopangmulti.Object.ChannelList;
import org.koko.kokopangmulti.Object.Session;
import reactor.core.publisher.Flux;
import reactor.core.publisher.Mono;

public class BroadcastToLobby {
    // 브로드캐스트
    public static Mono<Void> broadcastLobby(String json) {
        System.out.println("hi");
        return Flux.fromIterable(ChannelList.getLobby().getSessionList().keySet())
                .flatMap(userName -> Session.getSessionList().get(userName).outbound().sendString(Mono.just(json)).then())
                .then();
    }
}
