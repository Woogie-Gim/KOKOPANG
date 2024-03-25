package org.koko.kokopangmulti.messageHandling;

import org.json.JSONException;
import org.json.JSONObject;

public class LobbyMsgHandler {

    public void printData(String data) throws JSONException {
        JSONObject json = new JSONObject(data);
        String type = json.getString("type");
        String userName = json.getString("userName");

        System.out.println("DATA FROM LOBBY");
        System.out.println(type);
        System.out.println(userName);

    }

}
