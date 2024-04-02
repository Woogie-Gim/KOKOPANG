package org.koko.kokopangmulti.serverManagement;

import org.json.JSONException;
import org.koko.kokopangmulti.Braodcast.BroadcastToChannel;
import org.koko.kokopangmulti.Braodcast.ToJson;
import org.koko.kokopangmulti.Lobby.LobbyMsgHandler;
import org.koko.kokopangmulti.Channel.ChannelMsgHandler;
import org.koko.kokopangmulti.session.Session;
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


    public TcpMessageHandler(LobbyMsgHandler lobbyMsgHandler, ChannelMsgHandler channelMsgHandler) {
        this.lobbyMsgHandler = lobbyMsgHandler;
        this.channelMsgHandler = channelMsgHandler;
    }

    public Mono<Void> handleMessage(NettyInbound in, NettyOutbound out) {
        return in.receive()
                .asString()
                .flatMap(msg -> {
                    try {
                        // 클라이언트 측에서 송신한 메시지를 JSON객체로 변환
                        JSONObject json = new JSONObject(msg);
                        System.out.println(msg);

                        // JSON객체에서 최상위 값들 파싱(현재 참가 채널 이름 및 하위 데이터)
                        String channelName = json.getString("channel");
                        String userName;

                        switch(channelName) {
                            case "lobby" :
                                userName = json.getString("userName");
                                lobbyMsgHandler.filterData(in, userName, json.getJSONObject("data"));
                                break;
                            case "channel" :
                                userName = json.getString("userName");
                                channelMsgHandler.filterData(userName, json.getJSONObject("data"));
                                break;
                            case "loading" :
//                                inGameMsgHandler.filterData(json.getJSONObject("data"));
                                break;
                            case "inGame" :
                                int channelIndex = json.getInt("channelIndex");
                                BroadcastToChannel.broadcastMessage(channelIndex, ToJson.positionToJson(json.getJSONObject("data"))).subscribe();
                                break;
                        }

                        return Mono.empty();

                    } catch (JSONException e) {
                        System.out.println("error: JSON 파싱에러");
                        return Mono.empty();
                    }
                }).then();
    }
}
