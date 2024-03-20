package com.example.nettyedu2;

import reactor.core.publisher.Mono;
import reactor.netty.Connection;

import java.util.HashMap;

/*
TCP 서버에서 클라이언트 관리와 메시지 브로드캐스팅을 담당하는 클래스
[기능]
1) 클라이언트의 연결 추적
2) 모든 클라이언트에게 메시지를 브로드캐스트
 */
public class User {

    // [1] 클라이언트 목록 관리
    HashMap<String, Connection> clientList = new HashMap<String, Connection>(); // 서버에 연결된 모든 클라이언트를 추적하는 데 사용

    // [2] 클라이언트 추가
    public synchronized void addClient(String userName, Connection connection) {
    // synchronized로 선언되어 있어서
    // 동시에 여러 스레드가 이 메서드를 호출할 경우에도 클라이언트 목록의 일관성이 유지된다
        clientList.put(userName, connection);
        System.out.println("[Server]" + userName + "등록됨");
        System.out.println("[Server]" + clientList);
    }

    // [3] 메시지 브로트캐스트
    public synchronized void broadcastMessage(String message) {
    // synchronized로 선언되어 있어서
    // 메시지 전송 과정에서의 동시성 문제를 방지한다
        for (Connection connection : clientList.values()) {
        // clientList.values() : 모든 클라이언트의 Connection 객체에 접근한다
            connection.outbound().sendString(Mono.just("[Server]: " + message + "\r\n")).then().subscribe();
            // connection.outbound().sendString() : 클라이언트에 메시지 전송
            // then().subscribe() : 비동기적으로 메시지 전송 작업을 수행하고 완료를 기다린다
        }
    }
}
