package com.example.nettyedu2.server;

import io.netty.channel.ChannelHandlerAdapter;
import io.netty.channel.ChannelHandlerContext;
import io.netty.handler.codec.LineBasedFrameDecoder;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import reactor.netty.DisposableServer;
import reactor.netty.tcp.TcpServer;

public class NettyServer {
    private static final Logger log = LoggerFactory.getLogger(NettyServer.class);
    private static final int PORT = 9999;

    private final DisposableServer server;

    public NettyServer(UserHandler userHandler) {
        server = TcpServer
                .create()
                .port(PORT)
                .doOnConnection(conn -> {
                    conn.addHandler(new LineBasedFrameDecoder(1024));
                    conn.addHandler(new ChannelHandlerAdapter() {
                        @Override
                        public void handlerAdded(ChannelHandlerContext ctx) throws Exception {
                            System.out.println("client added");
                            userHandler.addClient(conn.address().toString(), conn);
                        }

                        @Override
                        public void handlerRemoved(ChannelHandlerContext ctx) throws Exception {
                            log.info("client removed");
                        }

                        @Override
                        public void exceptionCaught(ChannelHandlerContext ctx, Throwable cause) throws Exception {
                            log.warn("exception {}", cause.toString());
                            ctx.close();
                        }
                    });
                })
                .bind()
                .block();
    }

    public void dispose() {
        server.disposeNow();
    }
}
