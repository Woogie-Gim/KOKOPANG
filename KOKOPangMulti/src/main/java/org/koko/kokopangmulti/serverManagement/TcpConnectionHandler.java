package org.koko.kokopangmulti.serverManagement;

import io.netty.channel.ChannelHandlerAdapter;
import io.netty.channel.ChannelHandlerContext;
import io.netty.handler.codec.LineBasedFrameDecoder;
import org.koko.kokopangmulti.Object.Channel;
import org.koko.kokopangmulti.Channel.ChannelHandler;
import org.koko.kokopangmulti.Object.Session;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import reactor.netty.Connection;

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
                // 소켓 연결이 끊길 때 전체 세션 목록에서 정보 삭제
                Session.getSessionList().entrySet().removeIf(entry -> entry.getValue().equals(conn));

                // 현재 참가 중인 채널에서 정보 삭제

                log.info("client removed");
            }

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
