package org.koko.kokopangmulti.serverManagement;

import io.netty.channel.ChannelHandlerAdapter;
import io.netty.channel.ChannelHandlerContext;
import io.netty.channel.SimpleChannelInboundHandler;
import io.netty.handler.codec.LineBasedFrameDecoder;
import io.netty.handler.codec.string.StringDecoder;
import io.netty.util.CharsetUtil;
import org.json.JSONObject;
import org.koko.kokopangmulti.Channel.Channel;
import org.koko.kokopangmulti.Channel.ChannelHandler;
import org.koko.kokopangmulti.Object.Session;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import reactor.netty.Connection;

import java.util.function.Consumer;

public class TcpConnectionHandler implements Consumer<Connection> {

    /*
     * 의존성 주입
     */
    private final ChannelHandler channelHandler;
    private final Channel channel;
    private static final Logger log = LoggerFactory.getLogger(TcpConnectionHandler.class);

    public TcpConnectionHandler(ChannelHandler channelHandler, Channel channel) {
        this.channelHandler = channelHandler;
        this.channel = channel;
    }



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
                Session.getSessionList().entrySet().removeIf(entry -> entry.getValue().equals(conn));
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
