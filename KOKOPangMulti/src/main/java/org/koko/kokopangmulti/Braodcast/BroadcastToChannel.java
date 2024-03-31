package org.koko.kokopangmulti.Braodcast;

import org.koko.kokopangmulti.Object.ChannelList;
import org.koko.kokopangmulti.session.Session;
import reactor.core.publisher.Flux;
import reactor.core.publisher.Mono;

public class BroadcastToChannel {
    // 채널 내 모든 세션에 메시지 브로드캐스트
    public static Mono<Void> broadcastMessage(int channelIndex, String json) {
        return Flux.fromIterable(ChannelList.getChannelInfo(channelIndex).getSessionList().keySet())
                .flatMap(userName -> {
                    // 커넥션 정보가 없는 경우 일단 스킵하는 코드 추가(임시)
                    if (Session.getSessionList().get(userName) == null) {
                        return Mono.empty();
                    } else {
                        return Session.getSessionList().get(userName).getConnection().outbound().sendString(Mono.just(json)).then();
                    }
                })
                .then();
    }
}
