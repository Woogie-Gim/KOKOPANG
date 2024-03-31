package org.koko.kokopangmulti.serverManagement;

import io.netty.channel.ChannelHandlerAdapter;
import io.netty.channel.ChannelHandlerContext;
import io.netty.handler.codec.LineBasedFrameDecoder;
import org.koko.kokopangmulti.Braodcast.BroadcastToLobby;
import org.koko.kokopangmulti.Braodcast.ToJson;
import org.koko.kokopangmulti.Object.*;
import org.koko.kokopangmulti.session.Session;
import org.koko.kokopangmulti.session.SessionInfo;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import reactor.netty.Connection;

import java.util.Map;
import java.util.function.Consumer;

public class TcpConnectionHandler implements Consumer<Connection> {

    private static final Logger log = LoggerFactory.getLogger(TcpConnectionHandler.class);

    @Override
    public void accept(Connection conn) {
        conn.addHandler(new LineBasedFrameDecoder(1024));
        conn.addHandler(new ChannelHandlerAdapter() {

            /*
             * 클라이언트 연결 처리 로직
             */
            @Override
            public void handlerAdded(ChannelHandlerContext ctx) {
                // 클라이언트가 연결되었을 때의 처리
                log.info("Client connected: {}", ctx.channel().remoteAddress());
            }

            /*
             * 클라이언트 연결 해제 처리 로직
             */
            @Override
            public void handlerRemoved(ChannelHandlerContext ctx) {
                // 연결이 끊길 때 userName파싱
                String userName = null;
                boolean isLeave = false;

                for (Map.Entry<String, SessionInfo> entry : Session.getSessionList().entrySet()) {
                    if (entry.getValue().equals(conn)) {
                        userName = entry.getKey();
                        break;
                    }
                }

                // Session목록에서 session제거
                Session.getSessionList().remove(userName);

                // Lobby 확인
                if (ChannelList.getLobby().getSessionList().get(userName) != null) {
                    isLeave = true;

                    // 로비 유저 목록에서 세션 제거
                    ChannelList.getLobby().getSessionList().remove(userName);

                    // 로비에 접속 유저 목록 변동 사항 브로드캐스팅
                    BroadcastToLobby.broadcastLobby(ToJson.lobbySessionsToJson()).subscribe();
                    log.info("Client remove from Lobby");
                }

                // 채널목록 확인
                if (!isLeave) {
                    for (Channel channel : ChannelList.getChannelList().values()) {
                        for (String sessionName : channel.getSessionList().keySet()) {
                            if (sessionName.equals(userName)) {
                                isLeave = true;

                                SessionsInChannel sic = channel.getSessionsInChannel();
                                int idx = channel.getIdx(userName);

                                channel.getNameToIdx().remove(userName);
                                channel.getIdxToName().remove(idx);
                                channel.getSessionList().remove(userName);
                                sic.minusCnt();
                                sic.setFalseIsExisted(idx);

                                int channelIdx = channel.getChannelIdx();

                                if (sic.getCnt() == 0) {
                                    ChannelList.getChannelList().remove(channelIdx);
                                }

                                log.info("Client remove from Channel");
                                break;
                            }
                        }

                        if (isLeave) {
                            break;
                        }
                    }
                }
            }

            // 1. 게임중?
            // 2. 세션

            /*
             * 예외 처리 로직
             */
            @Override
            public void exceptionCaught(ChannelHandlerContext ctx, Throwable cause) {

                log.warn("exception {}", cause.toString());
                ctx.close();
            }
        });
    }
}
