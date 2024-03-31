package org.koko.kokopangmulti.session;

import java.util.concurrent.ConcurrentHashMap;


public class Session {

    private static final ConcurrentHashMap<String, SessionInfo> sessionList = new ConcurrentHashMap<>();

    public static ConcurrentHashMap<String, SessionInfo> getSessionList() {
        return sessionList;
    }

}
