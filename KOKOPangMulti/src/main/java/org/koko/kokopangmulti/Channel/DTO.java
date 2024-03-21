package org.koko.kokopangmulti.Channel;

import io.netty.channel.Channel;
import lombok.Data;
import lombok.Getter;

import java.util.concurrent.ConcurrentHashMap;

@Data
public class DTO {
    @Getter
    private static ConcurrentHashMap<String, Channel> lobby = new ConcurrentHashMap<>();

    @Getter
    private static ConcurrentHashMap<String, ConcurrentHashMap<String, Channel>> channelList = new ConcurrentHashMap<>();
}
