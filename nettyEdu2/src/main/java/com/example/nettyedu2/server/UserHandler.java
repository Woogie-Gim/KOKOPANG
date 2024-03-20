package com.example.nettyedu2.server;

import reactor.core.publisher.Mono;
import reactor.netty.Connection;

import java.util.Map;
import java.util.concurrent.ConcurrentHashMap;

/*
Netty 서버에서 클라이언트와의 연결을 관리하고 메시지를 Broadcast하는 역할을 수행
 */
public class UserHandler {
    private final Map<String, Connection> clients;      // Client 주소와 연결 객체를 매핑하여 저장

    /*
    <<< 생성자 초기화 >>>
    - client 필드 초기화
    - ConcurrentHashMap -> 동시성 지원
     */
    public UserHandler() {
        clients = new ConcurrentHashMap<>();
    }

    /*
    <<< addClient >>>
    - 클라이언트 주소와 연결 객체를 받아서 clients 맵에 추가
    - 클라이언트와의 연결을 관리할 수 있다
     */
    public void addClient(String address, Connection connection) {
        clients.put(address, connection);
    }

    /*
    <<< broadcastMessage >>>
    - 클라이언트들에게 메시지를 broadcast
     */
    public synchronized void broadcastMessage(String message) {
        for (Connection connection : clients.values()) {
            connection.outbound().sendString(Mono.just("[Server]: " + message + "\r\n")).then().subscribe();
        }
    }
}
