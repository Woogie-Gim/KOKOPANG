package com.example.nettyedu2;

import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import reactor.netty.DisposableServer;

import java.util.concurrent.CountDownLatch;

@SpringBootApplication
public class Main {

    public static void main(String[] args) {
        SpringApplication.run(Main.class, args);
        User userHandler = new User();
        TcpServerConfig serverConfig = new TcpServerConfig(userHandler);

        CountDownLatch latch = new CountDownLatch(1);
        DisposableServer server = serverConfig.createTcpServer();

        try {
            latch.await();
        } catch (InterruptedException e) {
            Thread.currentThread().interrupt();
        }

        server.disposeNow();
    }
}
