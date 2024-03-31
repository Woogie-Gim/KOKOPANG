package org.koko.kokopangmulti.session;

import reactor.netty.Connection;

public class SessionInfo {

    private Connection conn;
    private SessionState state;

    public SessionInfo(Connection conn) {
        this.conn = conn;
        this.state = SessionState.LOBBY;
    }

    public Connection getConnection() {
        return this.conn;
    }
    public void setSessionState(SessionState state) {
        this.state = state;
    }

    public SessionState getSessionState() {
        return this.state;
    }

}
