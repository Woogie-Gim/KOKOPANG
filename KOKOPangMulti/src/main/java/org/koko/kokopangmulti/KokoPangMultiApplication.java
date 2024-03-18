package org.koko.kokopangmulti;

import org.koko.kokopangmulti.TcpServer.TcpServer;
import org.springframework.boot.CommandLineRunner;
import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;

@SpringBootApplication
public class KokoPangMultiApplication implements CommandLineRunner {

	public static void main(String[] args) {
		SpringApplication.run(KokoPangMultiApplication.class, args);
	}

	@Override
	public void run(String... args) throws Exception {
		TcpServer tcpServer = new TcpServer(4444);
		new Thread(() -> {
			try {
				tcpServer.start();
			} catch (Exception e) {
				e.printStackTrace();
			}
		}).start();
	}
}
