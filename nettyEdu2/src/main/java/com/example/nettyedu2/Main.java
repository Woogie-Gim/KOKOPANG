package com.example.nettyedu2;

import com.example.nettyedu2.server.NettyServer;
import com.example.nettyedu2.server.UserHandler;
import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;

/*
<<< Netty >>>
- 자바 기반의 네트워크 애플리케이션 프레임워크
- 1) 고성능  2) 이벤트 기반  3) 비동기 처리
- 다양한 네트워크 프로토콜을 구현할 수 있다
 */

//@SpringBootApplication
public class Main {
    public static void main(String[] args) {
        SpringApplication.run(Main.class, args);                // Spring Boot Application 실행

        UserHandler userHandler = new UserHandler();
        NettyServer nettyServer = new NettyServer(userHandler);

        // 애플리케이션 실행 후 추가적인 로직 작성
        // ...
        System.out.println("SUCCESS");

        nettyServer.dispose();                                  // 서버 종료
    }
}
