package org.koko.kokopangmulti.Braodcast;

import org.koko.kokopangmulti.Object.ChannelList;
import org.koko.kokopangmulti.Object.Session;
import reactor.core.publisher.Flux;
import reactor.core.publisher.Mono;

public class BroadcastToChannel {
    // 채널 내 모든 세션에 메시지 브로드캐스트
    public static Mono<Void> broadcastMessage(int channelIndex, String json) {
        return Flux.fromIterable(ChannelList.getChannelInfo(channelIndex).getSessionList().keySet())
                .flatMap(userName -> Session.getSessionList().get(userName).outbound().sendString(Mono.just(json)).then())
                .then();
    }
}
