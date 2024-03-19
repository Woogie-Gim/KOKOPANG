package com.example.nettyedu2.server;

import reactor.netty.Connection;

import java.util.Map;
import java.util.concurrent.ConcurrentHashMap;

public class UserHandler {
    private final Map<String, Connection> clients;

    public UserHandler() {
        clients = new ConcurrentHashMap<>();
    }

    public void addClient(String address, Connection connection) {
        clients.put(address, connection);
    }

    public void broadcastMessage(String message) {
        // 메시지를 클라이언트들에게 브로드캐스트하는 로직 작성
    }
}
