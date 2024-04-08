package org.koko.kokopangmulti.serverManagement;

import org.koko.kokopangmulti.InGame.InGameMsgHandler;
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
        return new TcpMessageHandler(new LobbyMsgHandler(), channelMsgHandler(), inGameMsgHandler());
    }

    @Bean
    public ChannelMsgHandler channelMsgHandler() {
        return new ChannelMsgHandler();
    }

    @Bean
    public InGameMsgHandler inGameMsgHandler() {
        return new InGameMsgHandler();
    }

}
