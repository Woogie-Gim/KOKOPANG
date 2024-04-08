package org.koko.kokopangmulti.session;

import reactor.netty.Connection;

import java.util.concurrent.ConcurrentHashMap;


public class Session {

    private static final ConcurrentHashMap<String, SessionInfo> sessionList = new ConcurrentHashMap<>();

    private static final ConcurrentHashMap<Connection, String> connectionList = new ConcurrentHashMap<>();

    public static ConcurrentHashMap<String, SessionInfo> getSessionList() {
        return sessionList;
    }
    public static ConcurrentHashMap<Connection, String> getConnectionList() { return connectionList; }

}
