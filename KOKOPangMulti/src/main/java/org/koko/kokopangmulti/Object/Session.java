package org.koko.kokopangmulti.Object;

import reactor.netty.Connection;

import java.util.HashMap;


public class Session {

    private static HashMap<String, Connection> sessionList = new HashMap<>();

    public static HashMap<String, Connection> getSessionList() {
        return sessionList;
    }


}
