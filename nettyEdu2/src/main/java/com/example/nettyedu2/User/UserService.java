package com.example.nettyedu2.User;

import reactor.core.publisher.Mono;
import reactor.netty.Connection;

import java.util.HashMap;
import java.util.concurrent.ConcurrentHashMap;

public class UserService {
    static ConcurrentHashMap<String, Connection> clientList = User.getClientList();

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

    // [4] 클라이언트 목록을 스트링 타입으로 변환
    public static HashMap<String, String> getClientListSimple() {
        HashMap<String, String> simpleClientList = new HashMap<>();

        if (!clientList.isEmpty()) {
            clientList.forEach((key, value) -> simpleClientList.put(key, value.address().toString()));
        }

        return simpleClientList;
    }
}
