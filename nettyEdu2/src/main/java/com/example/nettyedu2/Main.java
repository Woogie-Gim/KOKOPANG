package com.example.nettyedu2;

import com.example.nettyedu2.server.NettyServer;
import com.example.nettyedu2.server.UserHandler;
import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;

//@SpringBootApplication
public class Main {
    public static void main(String[] args) {
        SpringApplication.run(Main.class, args);

        UserHandler userHandler = new UserHandler();
        NettyServer nettyServer = new NettyServer(userHandler);

        // 애플리케이션 실행 후 추가적인 로직 작성
        // ...
        System.out.println("SUCCESS");

        nettyServer.dispose();
    }
}
