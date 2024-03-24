package org.koko.kokopangmulti.serverManagement;

import com.fasterxml.jackson.core.type.TypeReference;
import com.fasterxml.jackson.databind.ObjectMapper;
import org.koko.kokopangmulti.Channel.Channel;
import org.koko.kokopangmulti.Channel.ChannelHandler;
import org.koko.kokopangmulti.Channel.ChannelList;
import org.koko.kokopangmulti.messageHandling.IngameMsgHandler;
import org.koko.kokopangmulti.messageHandling.LobbyMsgHandler;
import org.koko.kokopangmulti.messageHandling.RoomMsgHandler;
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
    private final LobbyMsgHandler lobbyMsgHandler;
    private final RoomMsgHandler roomMsgHandler;
    private final IngameMsgHandler ingameMsgHandler;
    public TcpMessageHandler(ChannelHandler channelHandler, ObjectMapper objectMapper, LobbyMsgHandler lobbyMsgHandler, RoomMsgHandler roomMsgHandler, IngameMsgHandler ingameMsgHandler) {
        this.channelHandler = channelHandler;
        this.objectMapper = objectMapper;
        this.lobbyMsgHandler = lobbyMsgHandler;
        this.roomMsgHandler = roomMsgHandler;
        this.ingameMsgHandler = ingameMsgHandler;
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

                        // Broadcast LOBBY DATA
                        if (channelName.equals("lobby")) {

                            lobbyMsgHandler.printData(data);
                            return Mono.empty();

                        }

                        // Broadcast GAMEROOM DATA
                        else if (channelName.equals("room")) {

                            roomMsgHandler.printData(data);
                            return Mono.empty();

                        }

                        // Broadcast INGAME DATA
                        else if (channelName.equals("ingame")) {

                            ingameMsgHandler.printData(data);
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
