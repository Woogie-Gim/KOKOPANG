package com.example.nettyedu2.connect.service;

import com.example.nettyedu2.TcpServerConfig;
import com.example.nettyedu2.User.UserService;
import org.springframework.boot.CommandLineRunner;
import org.springframework.stereotype.Service;
import reactor.netty.DisposableServer;

import java.util.concurrent.CountDownLatch;

@Service
public class ConnectionService implements CommandLineRunner {

    @Override
    public void run(String... args) throws Exception {
        // UserService와 TcpServerConfig 인스턴스 생성
        UserService userHandler = new UserService();
        TcpServerConfig serverConfig = new TcpServerConfig(userHandler);

        // TCP 서버 설정 및 실행
        DisposableServer server = serverConfig.createTcpServer();

        // CountDownLatch를 사용하여 메인 스레드를 대기 상태로 유지
        CountDownLatch latch = new CountDownLatch(1);
        try {
            latch.await();  // 메인 스레드를 대기 상태로 유지
        } catch (InterruptedException e) {
            Thread.currentThread().interrupt();
        } finally {
            server.disposeNow();  // 서버 종료 처리
        }
    }
}
