package org.koko.kokopangmulti.Lobby;

import com.fasterxml.jackson.databind.ObjectMapper;
import org.koko.kokopangmulti.Braodcast.BroadcastToLobby;
import org.koko.kokopangmulti.Braodcast.ToJson;
import org.koko.kokopangmulti.Object.ChannelList;
import org.koko.kokopangmulti.session.Session;
import org.koko.kokopangmulti.session.SessionInfo;
import reactor.netty.NettyInbound;

import java.util.HashMap;
import java.util.LinkedHashMap;

public class LobbyHandler {
    public static void initialLogIn(NettyInbound in, String userName, int userId) {
        in.withConnection(connection -> {

            SessionInfo sessionInfo = new SessionInfo(connection);

            Session.getSessionList().put(userName, sessionInfo);
            ChannelList.getLobby().getSessionList().put(userName, userId);

            // 유저에게 현재 채널 정보 브로드캐스팅
            BroadcastToLobby.broadcastPrivate(connection, ToJson.channelListToJson()).subscribe();

            // 로비에 유저목록 브로드캐스팅
            BroadcastToLobby.broadcastLobby(ToJson.lobbySessionsToJson()).subscribe();
        });
    }

    public static void chat(String userName, String message) {
        ObjectMapper objectMapper = new ObjectMapper();
        HashMap<String, String> chatMap = new LinkedHashMap<>();

        chatMap.put("type", "chat");
        chatMap.put("userName", userName);
        chatMap.put("message", message);

        String chatJson;

        try {
            chatJson = objectMapper.writeValueAsString(chatMap) + '\n';
            BroadcastToLobby.broadcastLobby(chatJson).subscribe();
        } catch (Exception e) {
            e.printStackTrace();
        }
    }
}
