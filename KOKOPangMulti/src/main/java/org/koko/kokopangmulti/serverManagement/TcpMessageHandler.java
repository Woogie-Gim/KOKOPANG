package org.koko.kokopangmulti.serverManagement;

import org.koko.kokopangmulti.Braodcast.BroadcastToLobby;
import org.koko.kokopangmulti.Braodcast.ToJson;
import org.koko.kokopangmulti.Lobby.LobbyHandler;
import org.koko.kokopangmulti.Object.ChannelList;
import org.koko.kokopangmulti.Object.Session;
import org.koko.kokopangmulti.Ingame.IngameMsgHandler;
import org.koko.kokopangmulti.Lobby.LobbyMsgHandler;
import org.koko.kokopangmulti.Channel.ChannelMsgHandler;
import reactor.core.publisher.Mono;
import reactor.netty.NettyInbound;
import reactor.netty.NettyOutbound;
import org.json.JSONObject;

public class TcpMessageHandler {

    /*
     * 의존성 주입
     */
    private final LobbyMsgHandler lobbyMsgHandler;
    private final ChannelMsgHandler channelMsgHandler;
    private final IngameMsgHandler ingameMsgHandler;

    public TcpMessageHandler(LobbyMsgHandler lobbyMsgHandler, ChannelMsgHandler channelMsgHandler, IngameMsgHandler ingameMsgHandler) {
        this.lobbyMsgHandler = lobbyMsgHandler;
        this.channelMsgHandler = channelMsgHandler;
        this.ingameMsgHandler = ingameMsgHandler;
    }

    public Mono<Void> handleMessage(NettyInbound in, NettyOutbound out) {

        return in.receive()
                .asString()
                .flatMap(msg -> {
                    try {
                        // 클라이언트 측에서 송신한 메시지를 JSON객체로 변환
                        JSONObject json = new JSONObject(msg);

                        // JSON객체에서 최상위 값들 파싱(현재 참가 채널 이름 및 하위 데이터)
                        String channelName = json.getString("channel");
                        String userName = json.getString("userName");

                        switch(channelName) {
                            case "lobby" :
                                lobbyMsgHandler.filterData(in, userName, json.getJSONObject("data"));
                                break;
                            case "channel" :
                                // 룸 msg핸들러 호출
                                channelMsgHandler.filterData(userName, json.getJSONObject("data"));
                                break;
                            case "inGame" :
                                ingameMsgHandler.filterData(json.getJSONObject("data"));
                                break;
                        }

                        return Mono.empty();

                    } catch (Exception e) {

                        e.printStackTrace();
                        return Mono.error(e); // 오류 발생 시, Mono.error로 오류를 반환

                    }
                }).then();
    }
}
