package org.koko.kokopangmulti.Ingame;

import com.fasterxml.jackson.core.JsonProcessingException;
import com.fasterxml.jackson.databind.ObjectMapper;
import org.json.JSONObject;

import java.util.HashMap;
import java.util.LinkedHashMap;


public class ToJson {

    private static final ObjectMapper objectMapper = new ObjectMapper();

    public static String positionToJson(JSONObject json) {
        HashMap<String, Object> temp = new LinkedHashMap<>();

        temp.put("type", "changePos");
        temp.put("userId", json.getInt("userId"));
        temp.put("x", json.getFloat("x"));
        temp.put("y", json.getFloat("y"));
        temp.put("z", json.getFloat("z"));
        temp.put("rw", json.getFloat("rw"));
        temp.put("rx", json.getFloat("rx"));
        temp.put("ry", json.getFloat("ry"));
        temp.put("rz", json.getFloat("rz"));

        try {
            return objectMapper.writeValueAsString(temp) + '\n';
        } catch (JsonProcessingException e) {
            throw new RuntimeException(e);
        }
    }

    public static String equipmentToJson(JSONObject json) {
        HashMap<String, Object> temp = new LinkedHashMap<>();

        temp.put("type", "changeArm");
        temp.put("userId", json.getInt("userId"));
        temp.put("armType", json.getString("armType"));
        temp.put("armName", json.getString("armName"));

        try {
            return objectMapper.writeValueAsString(temp) + '\n';
        } catch (JsonProcessingException e) {
            throw new RuntimeException(e);
        }
    }

    public static String attackToJson(JSONObject json) {
        HashMap<String, Object> temp = new LinkedHashMap<>();

        temp.put("type", "attack");
        temp.put("userId", json.getInt("userId"));

        try {
            return objectMapper.writeValueAsString(temp) + '\n';
        } catch (JsonProcessingException e) {
            throw new RuntimeException(e);
        }
    }
}
