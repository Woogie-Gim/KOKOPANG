package org.koko.kokopangmulti.serverManagement;

import org.springframework.boot.CommandLineRunner;

//@Service
public class TcpServerImpl implements CommandLineRunner {

    private final TcpServerInitializer tcpServerInitializer;

    public TcpServerImpl(TcpServerInitializer tcpServerInitializer) {
        this.tcpServerInitializer = tcpServerInitializer;
    }

    @Override
    public void run(String... args) throws Exception {
        System.out.println("run");
        tcpServerInitializer.initializeTcpServer().onDispose().block();
    }
}
