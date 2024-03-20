package com.example.nettyedu2;

import io.netty.handler.codec.LineBasedFrameDecoder;
import io.netty.channel.ChannelHandlerAdapter;
import io.netty.channel.ChannelHandlerContext;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import reactor.netty.DisposableServer;
import reactor.netty.tcp.TcpServer;

import java.util.function.Consumer;

public class TcpServerConfig {

    private static final Logger log = LoggerFactory.getLogger(TcpServerConfig.class);
    private User userHandler;

    public TcpServerConfig(User userHandler) {
        this.userHandler = userHandler;
    }

    public DisposableServer createTcpServer() {
        return TcpServer.create()
                .port(9999)
                .doOnConnection(connectionSetup())
                .handle((in, out) -> in.receive().asString().flatMap(msg -> {
                    System.out.println(in.toString() + msg);
                    log.debug("doOnNext: [{}]", msg);
                    userHandler.broadcastMessage(msg);
                    return out;
                }))
                .bindNow();
    }

    private Consumer<reactor.netty.Connection> connectionSetup() {
        return conn -> {
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
        };
    }
}
