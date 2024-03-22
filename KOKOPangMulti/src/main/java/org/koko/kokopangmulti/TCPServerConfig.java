package org.koko.kokopangmulti;

import com.fasterxml.jackson.core.type.TypeReference;
import com.fasterxml.jackson.databind.ObjectMapper;
import io.netty.channel.ChannelHandlerAdapter;
import io.netty.channel.ChannelHandlerContext;
import io.netty.handler.codec.LineBasedFrameDecoder;
import org.koko.kokopangmulti.Channel.Channel;
import org.koko.kokopangmulti.Channel.ChannelHandler;
import org.koko.kokopangmulti.Channel.ChannelList;
import org.koko.kokopangmulti.Object.Session;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.boot.CommandLineRunner;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import reactor.netty.Connection;
import reactor.netty.DisposableServer;
import reactor.netty.NettyInbound;
import reactor.netty.NettyOutbound;
import reactor.netty.tcp.TcpServer;
import reactor.core.publisher.Mono;

import java.util.Map;
import java.util.function.Consumer;

@SpringBootApplication
public class TCPServerConfig implements CommandLineRunner {
    private static final Logger log = LoggerFactory.getLogger(TCPServerConfig.class);
    private static final int PORT = 9999;

    private Channel channel;
    private ChannelHandler channelHandler = new ChannelHandler();

    public TCPServerConfig() {
    }

    @Override
    public void run(String... args) throws Exception {
        System.out.println("run");
        startTcpServer();
    }

    public void startTcpServer() {
        createTcpServer().onDispose().block();
    }

    public DisposableServer createTcpServer() {
        return TcpServer
                .create()
                .port(PORT)
                .doOnConnection(connectionSetup())
                .handle(this::clientDataHandler)
                .bindNow();
    }

    private Consumer<Connection> connectionSetup() {
        return conn -> {
            conn.addHandler(new LineBasedFrameDecoder(1024));
            conn.addHandler(new ChannelHandlerAdapter() {

                @Override
                public void handlerAdded(ChannelHandlerContext ctx) throws Exception {
                    Session session = new Session(conn.address().toString(), conn);
                    channelHandler.addLobby(session, channel.getLobby());
                    System.out.println("client added to lobby");
                }

                @Override
                public void handlerRemoved(ChannelHandlerContext ctx) throws Exception {
                    log.info("client removed");
                }

                @Override
                public void exceptionCaught(ChannelHandlerContext ctx, Throwable cause) throws Exception {
                    log.warn("exception {}", cause.toString());
                    ctx.close();
                }
            });
        };
    }

    private Mono<Void> clientDataHandler(NettyInbound in, NettyOutbound out) {
        ObjectMapper objectMapper = new ObjectMapper();
        return in.receive().asString().flatMap(msg -> {
            try {
                Map<String, String> messageMap = objectMapper.readValue(msg, new TypeReference<Map<String, String>>() {
                });
                String channelName = messageMap.get("channel");
                String messageContent = messageMap.get("message");


                if (channelName.equals("lobby")) {
                    System.out.println(messageContent);
                    return Mono.empty();

                } else {
                    // 채널 리스트에서 해당 채널 이름에 해당하는 채널 객체 찾기
                    Channel channel = ChannelList.getInstance().getChannelList().stream()
                            .filter(ch -> ch.getChannelName().equals(channelName))
                            .findFirst()
                            .orElse(null);

                    if (channel != null) {
                        // broadcastMessage 메서드가 Mono<Void>를 반환하므로, 이를 직접 반환
                        return channelHandler.broadcastMessage(messageContent, channel);
                    } else {
                        System.out.println("채널을 찾을 수 없습니다: " + channelName);
                        return Mono.empty();
                    }
                }
            } catch (Exception e) {
                e.printStackTrace();
                return Mono.error(e); // 오류 발생 시, Mono.error로 오류를 반환
            }
        }).then();
    }
}
