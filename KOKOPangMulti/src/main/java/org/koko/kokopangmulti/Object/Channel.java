package org.koko.kokopangmulti.Object;

import com.fasterxml.jackson.databind.ObjectMapper;
import org.json.JSONObject;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

public class Channel {

    private String channelName;
    private HashMap<String, Integer> nameToIdx;
    private HashMap<Integer, String> idxToName;
    private SessionsInChannel sessionsInChannel;
    private Boolean isOnGame;
    private HashMap<String, Integer> sessionList;

    private static final ObjectMapper objectMapper = new ObjectMapper();

    // LOBBY CHANNEL
    public Channel(String channelName) {
        this.channelName = channelName;
        this.sessionList = new HashMap<>();
    }

    // GAME CHANNEL 생성자
    public Channel(String channelName, String userName) {
        this.channelName = channelName;
        this.nameToIdx = new HashMap<>();
        this.nameToIdx.put(userName, 0);
        this.idxToName = new HashMap<>();
        this.idxToName.put(0, userName);
        this.sessionsInChannel = new SessionsInChannel();   // cnt=1, isExisted=[1,0,0,0,0,0]
        this.isOnGame = false;
        this.sessionList = new HashMap<>();
    }
    public Channel() {

    }

    /*
     * CHANNEL 정보 반환 : 채널 이름, SESSIONLIST
     */
    public String getChannelName() {
        return channelName;
    }

    public HashMap<String, Integer> getNameToIdx() {
        return this.nameToIdx;
    }

    public void addSession(String userName, Integer idx) {
        this.nameToIdx.put(userName, idx);
        this.idxToName.put(idx, userName);
    }

    public SessionsInChannel getSessionsInChannel() {
        return this.sessionsInChannel;
    }

    public HashMap<String, Integer> getSessionList() {
        return this.sessionList;
    }

    public int getIdx(String userName) { return this.nameToIdx.get(userName); }

    public HashMap<Integer, String> getIdxToName() {
        return idxToName;
    }

    public String toJson() {
        JSONObject jsonObject = new JSONObject();
        jsonObject.put("type", "sessionList");

        List<Map<String, Object>> jsonArray = new ArrayList<>();

        for(Map.Entry<String, Integer> entry : sessionList.entrySet()) {
            Map<String, Object> session = new HashMap<>();
            session.put(entry.getKey(), entry.getValue());
            jsonArray.add(session);
        }

        jsonObject.put("data", jsonArray);

        return jsonObject.toString();
    }
}
