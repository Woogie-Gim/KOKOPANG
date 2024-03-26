package org.koko.kokopangmulti.serverManagement;

import com.fasterxml.jackson.core.type.TypeReference;
import com.fasterxml.jackson.databind.ObjectMapper;
import org.koko.kokopangmulti.Channel.ChannelHandler;
import org.koko.kokopangmulti.Object.Session;
import org.koko.kokopangmulti.messageHandling.IngameMsgHandler;
import org.koko.kokopangmulti.messageHandling.LobbyMsgHandler;
import org.koko.kokopangmulti.messageHandling.RoomMsgHandler;
import reactor.core.publisher.Mono;
import reactor.netty.NettyInbound;
import reactor.netty.NettyOutbound;
import org.json.JSONObject;

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
                        // 클라이언트 측에서 송신한 메시지를 JSON객체로 변환
                        JSONObject json = new JSONObject(msg);

                        // JSON객체에서 최상위 값들 파싱(현재 참가 채널 이름 및 하위 데이터)
                        String channelName = json.getString("channel");
                        String userName = json.getString("userName");
                        String data = json.getString("data");

                        // 파싱한 데이터 테스트 출력
                        System.out.println("테스트");
                        System.out.println(userName);
                        System.out.println(channelName);
                        System.out.println(data);

                        if (channelName.equals("lobby")) {

                            // 최초 접속 시 로비에 userName과 connection 정보 등록
                            if (Session.getSessionList().get(userName) == null) {
                                in.withConnection(connection -> {
                                    Session.getSessionList().put(userName, connection);
                                    System.out.println(Session.getSessionList());
                                });
                            }
                        }

//                        System.out.println(data);

//                        if (channelName.equals("initial")) {
//                            System.out.println(in);
//                        }
//
//                        // channelName이 lobby일 경우
//                        if (channelName.equals("lobby")) {
//
//                            // 로비 msg핸들러 호출
//                            lobbyMsgHandler.printData(data);
//                            return Mono.empty();
//
//                        }
//
//                        // channelName이 room일 경우
//                        else if (channelName.equals("room")) {
//
//                            // 룸 msg핸들러 호출
//                            roomMsgHandler.filterData(data);
//                            return Mono.empty();
//
//                        }
//
//                        // Broadcast INGAME DATA
//                        else if (channelName.equals("ingame")) {
//
//                            ingameMsgHandler.printData(data);
//                            return Mono.empty();
//
//                        }

                        return Mono.empty();

                    } catch (Exception e) {

                        e.printStackTrace();
                        return Mono.error(e); // 오류 발생 시, Mono.error로 오류를 반환

                    }
                }).then();
    }
}
