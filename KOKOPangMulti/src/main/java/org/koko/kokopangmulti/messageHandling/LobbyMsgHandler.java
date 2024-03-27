package org.koko.kokopangmulti.messageHandling;

import com.fasterxml.jackson.databind.ObjectMapper;
import org.json.JSONException;
import org.json.JSONObject;
import org.koko.kokopangmulti.Channel.ChannelHandler;
import org.koko.kokopangmulti.Channel.ChannelList;
import reactor.core.publisher.Flux;
import reactor.core.publisher.Mono;

import java.util.HashMap;

public class LobbyMsgHandler {

    private static final ObjectMapper objectMapper = new ObjectMapper();

    public void filterData(String userName, JSONObject data) throws JSONException {
        String type = data.getString("type");

        if (type.equals("chat")) {
            String message = data.getString("message");
            HashMap<String, String> chatMap = new HashMap<>();

            chatMap.put("type", "chat");
            chatMap.put("userName", userName);
            chatMap.put("message", message);

            String chatJson;
            try {
                chatJson = objectMapper.writeValueAsString(chatMap) + "\n";
                broadcastLobby(chatJson).subscribe(
                        null,
                        error -> System.err.println("Error broadcasting to lobby: " + error),
                        () -> System.out.println("Broadcast to lobby completed")
                );
            } catch (Exception e) {
                e.printStackTrace();
            }
        }
    }

    // 브로드캐스트
    public static Mono<Void> broadcastLobby(String json) {
        return Flux.fromIterable(ChannelList.getLobby().getSessionList().values())
                .flatMap(connection -> connection.outbound().sendString(Mono.just(json)).then())
                .then();
    }
}
