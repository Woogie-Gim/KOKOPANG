package org.koko.kokopangmulti.session;

import reactor.netty.Connection;

public class SessionInfo {

    private Connection conn;
    private int state;

    public SessionInfo(Connection conn) {
        this.conn = conn;
        this.state = 0;
    }

    public Connection getConnection() {
        return this.conn;
    }
    public void setSessionState(int idx) {
        this.state = idx;
    }

    public int getSessionState() {
        return this.state;
    }

}
