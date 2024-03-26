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
//        conn.addHandler(new LineBasedFrameDecoder(1024));
//        conn.addHandler(new ChannelHandlerAdapter()
        conn.addHandler(new LineBasedFrameDecoder(1024));
        conn.addHandler(new StringDecoder(CharsetUtil.UTF_8)); // String으로 디코딩
        conn.addHandler(new SimpleChannelInboundHandler<String>() {

            /*
             * 클라이언트 연결 처리 로직
             */
            @Override
            protected void channelRead0(ChannelHandlerContext ctx, String msg) throws Exception {
                // 클라이언트로부터 메시지(여기서는 JSON 문자열)를 받음
                JSONObject json = new JSONObject(msg); // JSON 파싱
                String userName = json.getString("userName"); // userName 추출

                // 테스트 메시지 출력
                System.out.println(userName);

                // 세션 생성 및 로비에 추가하는 로직
                Session.getSessionList().put(userName, conn);

                // 세션 리스트 확인
                System.out.println(Session.getSessionList());
            }

            @Override
            public void handlerAdded(ChannelHandlerContext ctx) {
                log.info("client added to lobby");
            }

            /*
             * 클라이언트 연결 해제 처리 로직
             */
            @Override
            public void handlerRemoved(ChannelHandlerContext ctx) {

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
