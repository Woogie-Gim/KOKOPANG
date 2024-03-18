package com.example.nettyedu2;

import io.netty.channel.ChannelHandlerAdapter;
import io.netty.channel.ChannelHandlerContext;
import io.netty.handler.codec.LineBasedFrameDecoder;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import reactor.netty.DisposableServer;
import reactor.netty.NettyInbound;
import reactor.netty.NettyOutbound;
import reactor.netty.tcp.TcpServer;

import java.util.concurrent.CountDownLatch;

@SpringBootApplication
public class NettyEdu2Application {

    private static Logger log = LoggerFactory.getLogger(NettyEdu2Application.class);

    public NettyEdu2Application() {
        CountDownLatch latch = new CountDownLatch(1);
        User userHandler = new User();

        DisposableServer server = TcpServer.create()
                .port(9999)    // 서버가 사용할 포트
                .doOnConnection(conn -> {    // 클라 연결 시 호출
                    // conn: reactor.netty.Connection
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
//                    conn.onReadIdle(10_000, () -> {
//                        log.warn("client read timeout");
//                        conn.dispose();
//                    });
                })
                .handle((NettyInbound in, NettyOutbound out) -> // 연결된 커넥션에 대한 IN/OUT 처리
                    // reactor.netty (NettyInbound, NettyOutbound)
                    in.receive() // 데이터 읽기 선언, ByteBufFlux 리턴
                        .asString()  // 문자열로 변환 선언, Flux<String> 리턴
                        .flatMap(msg -> {
                            log.debug("doOnNext: [{}]", msg);
                            userHandler.broadcastMessage(msg);
                            return out;
                        })
                )
                .bind() // Mono<DisposableServer> 리턴
                .block();

        try {
            latch.await();
        } catch (InterruptedException e) {
        }
        log.info("dispose server");
        server.disposeNow(); // 서버 종료
    }

    public static void main(String[] args) {
        SpringApplication.run(NettyEdu2Application.class, args);
    }
}
