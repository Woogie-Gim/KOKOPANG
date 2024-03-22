package org.koko.kokopangmulti.Object;

import reactor.netty.Connection;

public class Session {
    String userName;
    Connection connection;

    public Session(String userName, Connection connection) {
        this.userName = userName;
        this.connection = connection;
    }

    public Session() {}

    public Connection getConnection() {
        return this.connection;
    }
}
