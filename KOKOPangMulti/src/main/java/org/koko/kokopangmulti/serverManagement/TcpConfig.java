package org.koko.kokopangmulti.serverManagement;

import com.fasterxml.jackson.databind.ObjectMapper;
import org.koko.kokopangmulti.Object.Channel;
import org.koko.kokopangmulti.Channel.ChannelHandler;
import org.koko.kokopangmulti.Ingame.IngameMsgHandler;
import org.koko.kokopangmulti.Lobby.LobbyMsgHandler;
import org.koko.kokopangmulti.Channel.ChannelMsgHandler;
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
        return new TcpConnectionHandler();
    }

    @Bean
    public TcpMessageHandler tcpMessageHandler() {
        return new TcpMessageHandler(new LobbyMsgHandler(), channelMsgHandler(), new IngameMsgHandler());
    }

    @Bean
    public ChannelHandler channelHandler() {
        return new ChannelHandler();
    }

    @Bean
    public ChannelMsgHandler channelMsgHandler() {
        return new ChannelMsgHandler(channelHandler());
    }


}
