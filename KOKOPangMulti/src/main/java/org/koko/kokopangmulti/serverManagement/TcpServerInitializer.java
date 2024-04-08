package org.koko.kokopangmulti.serverManagement;

import reactor.core.publisher.Mono;
import reactor.netty.DisposableServer;
import reactor.netty.tcp.TcpServer;

import java.time.Duration;

public class TcpServerInitializer {
    private static final int PORT = 1370;

    /*
     * 의존성 주입
     */
    private final TcpConnectionHandler tcpConnectionHandler;
    private final TcpMessageHandler tcpMessageHandler;

    public TcpServerInitializer(TcpConnectionHandler tcpConnectionHandler, TcpMessageHandler tcpMessageHandler) {
        this.tcpConnectionHandler = tcpConnectionHandler;
        this.tcpMessageHandler = tcpMessageHandler;
    }

    /*
     * TCP 서버 초기화
     */
    public Mono<? extends DisposableServer> initializeTcpServer() {
        return TcpServer
                .create()                                   // TcpServer 객체 생성
                .port(PORT)                                 // Port 설정
                .doOnConnection(tcpConnectionHandler)       // 새로운 Client 연결 생성 시 실행할 작업 정의
                .handle(tcpMessageHandler::handleMessage)   // Message Handling 방법 정의
                .bind();                                    // 비동기적으로 서버 시작, Mono<DisposableServer> 반환
    }
}
