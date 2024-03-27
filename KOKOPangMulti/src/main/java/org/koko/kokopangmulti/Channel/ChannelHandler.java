package org.koko.kokopangmulti.Channel;

import org.koko.kokopangmulti.Object.Channel;
import org.koko.kokopangmulti.Object.ChannelList;
import org.koko.kokopangmulti.Object.Session;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import reactor.netty.Connection;

public class ChannelHandler {
    private static final Logger log = LoggerFactory.getLogger(ChannelHandler.class);

    public static void createChannel(String userName, String channelName) {

        log.info("[userName:{}] CHANNEL CREATED, channelName:{}", userName, channelName);

        // 1) Channel 인스턴스 생성
        Channel channel = new Channel(channelName, userName);

        // 2) Lobby에서 [userName] session 제거
        ChannelList.getLobby().getSessionList().remove(userName);

        // 3) ChannelList에 channel 추가
        int channelIdx = ChannelList.addChannel(channel);

        // 4) BroadCasting

        // 동작 확인
        System.out.println("channel.sessionsInChannel.cnt = " + channel.getSessionsInChannel().getCnt());
        System.out.println("channel.sessionsInChannel.isExisted = " + channel.getSessionsInChannel().getIsExisted());

        channel.getSessionsInChannel().plusCnt();
        System.out.println("channel.sessionsInChannel.cnt = " + channel.getSessionsInChannel().getCnt());

        channel.getSessionsInChannel().minusCnt();
        System.out.println("channel.sessionsInChannel.cnt = " + channel.getSessionsInChannel().getCnt());

        channel.getSessionsInChannel().setTrueIsExisted(0);
        System.out.println("channel.sessionsInChannel.isExisted = " + channel.getSessionsInChannel().getIsExisted());

        channel.getSessionsInChannel().setFalseIsExisted(6);
        System.out.println("channel.sessionsInChannel.isExisted = " + channel.getSessionsInChannel().getIsExisted());

        System.out.println("Lobby: " + ChannelList.getLobby().getSessionList());
        System.out.println(channelName + ": " + ChannelList.getChannelInfo(channelIdx).getSessionList());

    }

    public static void joinChannel(String userName, int channelIndex) {

        // 1) Lobby에서 [userName] 제거
        ChannelList.getLobby().getSessionList().remove(userName);

        // 2) 참가할 channel 인스턴스
        Channel channel = ChannelList.getChannelInfo(channelIndex);

        // 3) channel 참가 인원 확인 : 이미 6명이라면 join 거절
        if (channel.getSessionsInChannel().getCnt() ==6) {
            log.warn("[JOIN REJECTED] ROOM IS FULL");
            return;
        }

        // 4) channel 안 [userName]의 idx 탐색
        for (int i=0; i<6; i++) {
            if (channel.getSessionsInChannel().getIsExisted().get(i) == 0) {
                channel.getSessionsInChannel().setTrueIsExisted(i); // UPDATE isExisted
                channel.getSessionsInChannel().plusCnt();           // UPDATE cnt
                channel.addSession(userName, i);                    // UPDATE nameToIdx, idxToName
                break;
            }
        }

        // 5) LOGGING JOIN
        log.info("[userName:{}] CHANNEL JOINED, channelName:{}", userName, ChannelList.getChannelInfo(channelIndex).getChannelName());

        // 6) Broadcasting

        // 동작 확인
        System.out.println("channel.sessionsInChannel.cnt = " + channel.getSessionsInChannel().getCnt());
        System.out.println("channel.sessionsInChannel.isExisted = " + channel.getSessionsInChannel().getIsExisted());

        for( String key : ChannelList.getChannelInfo(channelIndex).getNameToIdx().keySet() ){
            System.out.println( String.format("[userName:%s], [idx:%s]", key, ChannelList.getChannelInfo(channelIndex).getNameToIdx().get(key)) );
        }

    }

    public static void leaveChannel(String userName, int channelIndex) {
        // channelIndex를 통해 해당 채널에 유저 제거
        ChannelList.getChannelInfo(channelIndex).getSessionList().remove(userName);
        // 세션 목록에서 유저 커넥션 정보 파싱
        Connection connection = Session.getSessionList().get(userName);
        // 로비에 유저 정보 추가
        ChannelList.getLobby().getSessionList().put(userName, connection);

        // 기존 유저가 참가하던 채널에 참가중인 세션목록에 변경사항 브로드캐스팅

        // 로비에 유저 목록, 채널의 변경사항 브로드캐스팅

    }

//    // 채널 내 모든 세션에 메시지 브로드캐스트
////    public static Mono<Void> broadcastMessage(int channelIndex, String userName, String message) {
////        HashMap<String, Connection> channel = ChannelList.getChannelInfo(channelIndex).getSessionList();
////
////        return Flux.fromIterable(ChannelList.getChannelInfo(channelIndex).getSessionList())
////                .flatMap(session -> session.getConnection().outbound().sendString(Mono.just(message)).then())
////                .then();
////    }
}
