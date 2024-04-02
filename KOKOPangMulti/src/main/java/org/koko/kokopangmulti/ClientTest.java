package org.koko.kokopangmulti;

import java.io.*;
import java.net.Socket;

public class ClientTest {
    public static void main(String[] args) {
        Socket socket;
        try {
            socket = new Socket("211.118.146.161:1370", 1370);

            PrintWriter out = new PrintWriter(socket.getOutputStream(), true);
            BufferedReader in = new BufferedReader(new InputStreamReader(socket.getInputStream()));
            BufferedReader stdIn = new BufferedReader(new InputStreamReader(System.in));

            Thread receiver = new Thread(() -> {

                try {
                    String inputLine;
                    while ((inputLine = in.readLine()) != null) {
                        System.out.println(inputLine);
                    }
                } catch (IOException e) {
                    System.out.println("ERROR!!");
                    e.printStackTrace();
                }
            });

            receiver.start();

            System.out.println("[Client] 서버 접속 성공");

            String json = "{\"channel\":\"lobby\",\"userName\":\"test\",\"data\":{\"type\":\"chat\",\"message\":\"message\"}}";
            out.println(json);

            String userInput;
            while((userInput = stdIn.readLine()) != null) {
                out.println(userInput);
            }

            socket.close();
        } catch (IOException e) {
            e.printStackTrace();
        }
    }
}
