package org.koko.kokopangmulti.Object;

import reactor.netty.Connection;


public class Session {

    public String ipAddress;
    static Connection connection;

    public Session(String ipAddress, Connection connection) {
        this.ipAddress = ipAddress;
        this.connection = connection;
    }
}
