package org.koko.kokopangmulti.serverManagement;

import com.fasterxml.jackson.databind.ObjectMapper;
import org.koko.kokopangmulti.Channel.Channel;
import org.koko.kokopangmulti.Channel.ChannelHandler;
import org.koko.kokopangmulti.messageHandling.IngameMsgHandler;
import org.koko.kokopangmulti.messageHandling.LobbyMsgHandler;
import org.koko.kokopangmulti.messageHandling.RoomMsgHandler;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;

@Configuration
public class TcpConfig {
    @Bean
    public TcpServerImpl tcpServerConfigTest() {
        return new TcpServerImpl(tcpServerInitializer());
    }

    @Bean
    public TcpServerInitializer tcpServerInitializer() {
        return new TcpServerInitializer(tcpConnectionHandler(), tcpMessageHandler());
    }

    @Bean
    public TcpConnectionHandler tcpConnectionHandler() {
        return new TcpConnectionHandler(channelHandler(), new Channel());
    }

    @Bean
    public TcpMessageHandler tcpMessageHandler() {
        return new TcpMessageHandler(channelHandler(), new ObjectMapper(), new LobbyMsgHandler(), new RoomMsgHandler(), new IngameMsgHandler());
    }

    @Bean
    public ChannelHandler channelHandler() {
        return new ChannelHandler();
    }


}
