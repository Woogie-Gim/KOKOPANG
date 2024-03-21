package com.example.nettyedu2;

import com.example.nettyedu2.User.User;
import com.example.nettyedu2.User.UserService;
import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import reactor.netty.DisposableServer;

import java.util.concurrent.CountDownLatch;

/*
Spring Boot와 Reactor Netty를 사용하여 TCP 서버를 설정하고 실행한다
 */
@SpringBootApplication  // Spring Boot Application의 주 진입점
public class Main {

    public static void main(String[] args) {
        // [1] Spring Boot Application 설정
        SpringApplication.run(Main.class, args);    // Spring Boot Application 시작
                                                    // 1) Application Context 생성
                                                    // 2) 설정 로드
                                                    // 3) Application 실행
        // [2] TCP 서버 설정 및 실행
        UserService userHandler = new UserService();  // User 클래스 인스턴스 생성
        TcpServerConfig serverConfig = new TcpServerConfig(userHandler);    // TcpServerConfig 클래스 인스턴스 생성

        CountDownLatch latch = new CountDownLatch(1);   // TCP 서버가 계속 실행되도록 주 스레드 대기
        DisposableServer server = serverConfig.createTcpServer();

        try {
            latch.await();  // CountDownLatch의 카운트가 0이 될 때까지 현재 스레드를 대기상태로 만든다
                            // CountDownLatch의 카운트가 1로 설정되어 있으며
                            // 카운트가 줄어들지 않기 때문에
                            // Application이 종료되지 않고 계속 실행된다
        } catch (InterruptedException e) {
            Thread.currentThread().interrupt();
        }

        server.disposeNow();    // 서버 종료
                                // 위의 코드가 실행되는 경우의 수
                                // 1) latch.await() 메소드로 인해 대기 상태인 스레드가 Interrupt 될 경우
                                // 2) CountDownLatch의 카운트가 0으로 줄어들 경우
    }
}
