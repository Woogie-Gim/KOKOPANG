package org.koko.kokopangmulti.Object;

import reactor.netty.Connection;

import java.util.HashMap;
import java.util.concurrent.ConcurrentHashMap;


public class Session {

    private static final ConcurrentHashMap<String, Connection> sessionList = new ConcurrentHashMap<>();

    public static ConcurrentHashMap<String, Connection> getSessionList() {
        return sessionList;
    }

}
