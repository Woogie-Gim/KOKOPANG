package org.koko.kokopangmulti.serverManagement;

import com.fasterxml.jackson.databind.ObjectMapper;
import org.koko.kokopangmulti.Channel.Channel;
import org.koko.kokopangmulti.Channel.ChannelHandler;
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
        return new TcpConnectionHandler(channelHandler(), channel());
    }

    @Bean
    public TcpMessageHandler tcpMessageHandler() {
        return new TcpMessageHandler(channelHandler(), new ObjectMapper());
    }

    @Bean
    public ChannelHandler channelHandler() {
        return new ChannelHandler();
    }

    @Bean
    public Channel channel() {
        return new Channel();
    }
}
