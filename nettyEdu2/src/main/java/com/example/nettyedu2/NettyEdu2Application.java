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
        CountDownLatch latch = new CountDownLatch(1);   // CountDownLatch : 어떤 쓰레드가 다른 쓰레드에서 작업이 완료될 때까지 기다릴 수 있도록 해주는 클래스
        User userHandler = new User();

        DisposableServer server = TcpServer
                .create()
                .port(9999)                             // 서버가 사용할 포트
                .doOnConnection(conn -> {               // 클라 연결 시 호출
                    // conn: reactor.netty.Connection
                    conn.addHandler(new LineBasedFrameDecoder(1024));   // LineBasedFrameDecoder : 라인 단위로 가져와서 처리
                    conn.addHandler(new ChannelHandlerAdapter() {
                        /*
                        << ChannelHandlerAdapter >>
                        클라이언트 연결 이벤트에 따라 로그를 출력하기 위해 등록한 객체
                         */
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
                    /*
                    << onReadIdle >>
                    첫 번째 인자로 지정한 시간(밀리초) 동안 데이터 읽기가 없으면
                    두 번째 인자로 전달받은 코드를 실행 (연결 종료)
                     */
//                    conn.onReadIdle(10_000, () -> {
//                        log.warn("client read timeout");
//                        conn.dispose();   // 연결 종료
//                    });
                })
                /*
                <<< .handle() >>>
                - Netty의 채널 핸들러를 등록하는 메서드
                - 입
                 */
                .handle((NettyInbound in, NettyOutbound out) -> // 데이터 입출력 처리 코드 : 연결된 커넥션에 대한 IN/OUT 처리
                    // reactor.netty (NettyInbound, NettyOutbound)
                    in.receive()            // 데이터 읽기 선언, 데이터 수신을 위한 ByteBufFlux 리턴

                    /*
                    <<< ByteBufFlux >>>
                    - Reactor의 Flux 인터페이스를 구현한 클래스
                    - 비동기적으로 데이터 스트림을 생성하고 처리할 수 있다
                    - 네티의 ByteBuf 객체를 사용하여 데이터를 표현하고
                    - Flux의 리액티브 스트림 모델을 통해 데이터를 처리하느 기능을 제공한다
                     */
                        .asString()         // 문자열로 변환 선언, 데이터를 문자열로 수신 처리, Flux<String> 리턴
                        .flatMap(msg -> {   // 수신한 메시지 처리
                            /*
                            <<< flatMap >>>
                            중복 구조로 되어있는 리스트를 하나의 스트림처럼 다룰 수 있다
                             */
                            System.out.println(in.toString() + msg);
                            log.debug("doOnNext: [{}]", msg);
                            userHandler.broadcastMessage(msg);

//                            // 수신된 메시지와 IP 주소를 로그에 출력합니다.
//                            log.debug("Received message [{}] from IP address [{}]", msg, ipAddress);

                            return out;
                        })
                )
                .bind()     // 서버 실핼에 사용됨 : Mono<DisposableServer> 리턴
                .block();   // 서버 실행 및 DisposableServer 리턴

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
