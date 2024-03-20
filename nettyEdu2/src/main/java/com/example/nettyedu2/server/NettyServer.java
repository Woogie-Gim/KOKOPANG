package com.example.nettyedu2.server;

import io.netty.channel.ChannelHandlerAdapter;
import io.netty.channel.ChannelHandlerContext;
import io.netty.handler.codec.LineBasedFrameDecoder;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import reactor.netty.DisposableServer;
import reactor.netty.tcp.TcpServer;

/*
<<< TCP Server 구현 >>>
- 특정 port에서 client의 연결을 수신하고 처리하는 기본적인 TCP 서버 구현
- Netty와 Reactor Netty 사용
- Netty란
    * 비동기 이벤트 기반 네트워크 애플리케이션 프레임워크
    * 고성능 네트워크 애플리케이션 개발에 사용된다
- Reactor Netty란
    * 반응형 프로그래밍을 위한 라이브러리

<<< 코드 실행 흐름 >>>
1) NettyServer 클래스의 생성자에서 TcpServer 인스턴스를 생성하고,
   9999 포트에서 들어오는 연결을 처리하기 위한 설정을 한다
2) 클라이언트가 서버에 연결하면, doOnConnection에서 정의된 동작이 실행된다.
   클라이언트마다 LineBasedFrameDecoder와 ChannelHandlerAdapter가 추가된다
3) 클라이언트가 연결되거나, 연결이 끊기거나, 예외가 발생할 때
   handlerAdded, handlerRemoved, exceptionCaught 메소드가 각각 호출된다
4) dispose 메소드를 호출하여 서버를 종료할 수 있다
 */
public class NettyServer {
    private static final Logger log = LoggerFactory.getLogger(NettyServer.class);
    private static final int PORT = 9999;   // 연결한 PORT 번호
    private final DisposableServer server;  // Reactor Netty에서 서버의 생명 주기를 관리하기 위한 인터페이스

    public NettyServer(UserHandler userHandler) {
        server = TcpServer                  // TCP 서버를 생성하고 구성하는 데 사용된다
                .create()                   // 1) TCP 서버 인스턴스 생성
                .port(PORT)                 // 2) 서버가 수신할 PORT 설정
                .doOnConnection(conn -> {   // 3) 클라이언트가 서버에 연ㄷ결될 때 실행할 작업을 정의
                                            //   -> 연결된 클라이언트에 두 개의 핸들러를 추가
                                            //      (1) LineBasedFrameDecoder  (2) ChannelHandlerAdapter
                    conn.addHandler(new LineBasedFrameDecoder(1024)); // 네트워크 스트림에서 줄 단위로 데이터를 분리하는 디코더
                                                                                // 최대 1024 바이트 크기의 줄을 처리할 수 있다
                    conn.addHandler(new ChannelHandlerAdapter() {               // 연결 추가/제거/예외발생 시의 동작을 정의

                        /*
                        <<< hadlerAdded >>>
                        - 클라이언트가 연결되었을 때 실행된다
                        - userHandler.addClient 메서드 호출
                        - 사용자 정의 Handler에 클라이언트를 추가한다
                         */
                        @Override
                        public void handlerAdded(ChannelHandlerContext ctx) throws Exception {
                            System.out.println("client added");
                            userHandler.addClient(conn.address().toString(), conn);
                        }

                        /*
                        <<< handlerRemoved >>>
                        - 클라이언트 연결이 제거될 때 실행된다
                        - 로그를 남긴다
                         */
                        @Override
                        public void handlerRemoved(ChannelHandlerContext ctx) throws Exception {
                            log.info("client removed");
                        }

                        /*
                        <<< exceptionCaught >>>
                        - 연결 중 예외가 발생했을 때 실행된다
                        - 예외 정보를 로깅하고 해당 클라이언트 연결을 종료한다
                         */
                        @Override
                        public void exceptionCaught(ChannelHandlerContext ctx, Throwable cause) throws Exception {
                            log.warn("exception {}", cause.toString());
                            ctx.close();
                        }
                    });
                })
                .bind()                     // 4) 서버가 비동기적으로 바인딩되고 준비되기를 기다린다
                .block();
    }

    /*
     <<< dispose >>>
     - 서버 즉시 종료
     */
    public void dispose() {
        server.disposeNow();
    }
}
