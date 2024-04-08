package org.koko.kokopangmulti.Lobby;

import org.json.JSONException;
import org.json.JSONObject;
import org.koko.kokopangmulti.Braodcast.BroadcastToLobby;
import org.koko.kokopangmulti.Braodcast.ToJson;
import org.koko.kokopangmulti.session.Session;
import reactor.netty.NettyInbound;

public class LobbyMsgHandler {
    public void filterData(NettyInbound in, String userName, JSONObject data) throws JSONException {
        String type = data.getString("type");

        switch (type) {
            case "initial":
                int userId = data.getInt("userId");
                LobbyHandler.initialLogIn(in, userName, userId);
                break;
            case "refresh":
                BroadcastToLobby.broadcastPrivate(Session.getSessionList().get(userName).getConnection(), ToJson.channelListToJson()).subscribe();
                break;
            case "chat":
                String message = data.getString("message");
                LobbyHandler.chat(userName, message);
                break;
        }
    }
}
