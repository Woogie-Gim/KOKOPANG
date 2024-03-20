package com.example.nettyedu2;

import io.netty.handler.codec.LineBasedFrameDecoder;
import io.netty.channel.ChannelHandlerAdapter;
import io.netty.channel.ChannelHandlerContext;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import reactor.netty.DisposableServer;
import reactor.netty.tcp.TcpServer;

import java.util.function.Consumer;

/*
Reactor Netty를 사용하여 TCP 서버를 설정하고 운영하는 Java 클래스

[주요 구성 요소]
- TcpServerConfig 클래스 : TCP 서버의 설정과 실행을 담당하는 클래스
- User 클래스의 인스턴스 : 클라이언트 관리와 메시지 브로드캑스트를 담당
- Logger(log) : 로그 기록을 위한 Logger 인스턴스

[동작 과정]
1) TcpServerConfig 생성자
 : User 클래스의 인스턴스를 파라미터로 받아 이를 userHanler에 할당

2) createTcpServer 메서드
 : TCP 서버 생성 및 설정

3) connectionSetup 메서드
 : 클라이언트 연결 시 필요한 설정을 정의
*/
public class TcpServerConfig {

    private static final Logger log = LoggerFactory.getLogger(TcpServerConfig.class);
    private static final int PORT = 9999;
    private User userHandler;   // 클라이언트 관리와 메시지 브로드캐스팅에 사용

    public TcpServerConfig(User userHandler) { // 파라미터 : User 클래스의 인스턴스
        this.userHandler = userHandler;
    }

    /*
    <<< createTcpServer >>>
    TCP 서버를 생성하고 설정
     */
    public DisposableServer createTcpServer() {
        return TcpServer
                .create()                           // 1) TCP 서버 생성 및 설정
                .port(PORT)                         // 2) 서버가 리슨할 포트 번호 설정
                .doOnConnection(connectionSetup())  // 3) 클라이언트 연ㄷ결 시 실행될 로직 정의
                                                    // 4) connectionSetup 메서드 참조
                .handle(                            // 5) 클라이언트로부터 받은 데이터를 처리하는 로직을 정의
                        (in, out) ->
                                in.receive().asString().flatMap(msg -> {
                                    System.out.println(in.toString() + msg);    // 받은 메시지 출력
                                    log.debug("doOnNext: [{}]", msg);           // 로깅
                                    userHandler.broadcastMessage(msg);          // 브로드캐스팅
                                    return out;
                }))
                .bindNow();                         // 6) 설정된 서버를 즉시 바인딩하고 시작
    }

    /*
    클라이언트 연결 시 필요한 설정 정의
     */
    private Consumer<reactor.netty.Connection> connectionSetup() {
        return conn -> {
            conn.addHandler(new LineBasedFrameDecoder(1024));   // 한 줄 단위로 메시지를 구분하는 디코더 추가
                                                                          // 최대 1024 바이트까지 처리 가능
            conn.addHandler(new ChannelHandlerAdapter() {                 // 채널 핸들러 이벤트 처리

                /*
                클라이언트가 연결되었을 때 실행
                 */
                @Override
                public void handlerAdded(ChannelHandlerContext ctx) throws Exception {
                    System.out.println("client added");
                    userHandler.addClient(conn.address().toString(), conn); // 연결된 클라이언트를 userHandler에 추가
                }

                /*
                클라이언트 연결이 종료되었을 때 실행
                 */
                @Override
                public void handlerRemoved(ChannelHandlerContext ctx) throws Exception {
                    log.info("client removed"); // 로그 기록
                }

                /*
                예외 발생 시 실행
                 */
                @Override
                public void exceptionCaught(ChannelHandlerContext ctx, Throwable cause) throws Exception {
                    log.warn("exception {}", cause.toString()); // 로그 기록
                    ctx.close();                                // 연결 종료
                }
            });
        };
    }
}
