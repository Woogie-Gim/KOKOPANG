package org.koko.kokopangmulti.serverManagement;

import com.fasterxml.jackson.core.type.TypeReference;
import com.fasterxml.jackson.databind.ObjectMapper;
import org.koko.kokopangmulti.Channel.Channel;
import org.koko.kokopangmulti.Channel.ChannelHandler;
import org.koko.kokopangmulti.Channel.ChannelList;
import org.springframework.context.annotation.Bean;
import reactor.core.publisher.Mono;
import reactor.netty.NettyInbound;
import reactor.netty.NettyOutbound;

import java.util.Map;

public class TcpMessageHandler {

    /*
    * 의존성 주입
     */
    private final ChannelHandler channelHandler;
    private final ObjectMapper objectMapper;
    public TcpMessageHandler(ChannelHandler channelHandler, ObjectMapper objectMapper) {
        this.channelHandler = channelHandler;
        this.objectMapper = objectMapper;
    }

    public Mono<Void> handleMessage(NettyInbound in, NettyOutbound out) {

        return in.receive()
                .asString()
                .flatMap(msg -> {
                    try {
                        Map<String, String> messageMap = objectMapper.readValue(msg, new TypeReference<Map<String, String>>() {
                        });

                        String channelName = messageMap.get("channel");
                        String data = messageMap.get("data");

                        // LOBBY DATA
                        if (channelName.equals("lobby")) {
                            System.out.println("FROM LOBBY");
                            System.out.println("data = " + data);
                            return Mono.empty();
                        }

                        // GAMEROOM DATA
                        else if (channelName.equals("channel")) {
                            System.out.println("FROM WAITING ROOM");
                            System.out.println("data = " + data);
                            return Mono.empty();
                        }

                        // INGAME DATA
                        else if (channelName.equals("ingame")) {
                            System.out.println("DURING GAME");
                            System.out.println("data = " + data);
                            return Mono.empty();
                        }

                        return Mono.empty();

                    } catch (Exception e) {
                        e.printStackTrace();
                        return Mono.error(e); // 오류 발생 시, Mono.error로 오류를 반환
                    }
                }).then();
    }
}
