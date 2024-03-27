package org.koko.kokopangmulti.messageHandling;

import com.fasterxml.jackson.databind.ObjectMapper;
import org.json.JSONException;
import org.json.JSONObject;

public class RoomMsgHandler {

    public void filterData(String userName, JSONObject data) throws JSONException {

        String type = data.getString("type");

        switch (type) {
            case "create":
                roomCreated();
                break;
            case "join":
                roomJoined();
                break;
            case "leave":
                roomLeaved();
                break;
        }

    }

    public void roomCreated() {
        System.out.println("ROOM CREATED");
    }

    public void roomJoined() {
        System.out.println("ROOM JOINED");
    }

    public void roomLeaved() {
        System.out.println("ROOM LEAVED");
    }

}
