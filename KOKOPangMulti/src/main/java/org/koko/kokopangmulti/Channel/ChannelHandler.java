package org.koko.kokopangmulti.Channel;

import org.koko.kokopangmulti.Object.Channel;
import org.koko.kokopangmulti.Object.ChannelList;
import org.koko.kokopangmulti.Object.Session;
import org.koko.kokopangmulti.Object.SessionsInChannel;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import reactor.core.publisher.Flux;
import reactor.core.publisher.Mono;

import java.util.ArrayList;
import java.util.Iterator;

public class ChannelHandler {
    private static final Logger log = LoggerFactory.getLogger(ChannelHandler.class);

    public static void createChannel(String userName, String channelName) {

        log.info("[userName:{}] CHANNEL CREATED, channelName:{}", userName, channelName);

        // 1) Channel 인스턴스 생성
        Channel channel = new Channel(channelName, userName);

        // 2) Lobby에서 [userName] session 제거
        int userId = ChannelList.getLobby().getSessionList().get(userName);
        ChannelList.getLobby().getSessionList().remove(userName);

        // 3) ChannelList에 channel 추가
        int channelIndex = ChannelList.addChannel(channel);

        // sessionList해쉬맵에 이름, id값 추가
        channel.getSessionList().put(userName, userId);

        // 4) BroadCasting

        // 동작 확인
        System.out.println("channel.sessionsInChannel.cnt = " + channel.getSessionsInChannel().getCnt());
        System.out.println("channel.sessionsInChannel.isExisted = " + channel.getSessionsInChannel().getIsExisted());

        for (String key : ChannelList.getChannelInfo(channelIndex).getNameToIdx().keySet()) {
            System.out.println(String.format("[userName:%s], [idx:%s]", key, ChannelList.getChannelInfo(channelIndex).getNameToIdx().get(key)));
        }
    }

    public static void joinChannel(String userName, int channelIndex) {
        // 1) 참가할 channel 인스턴스
        Channel channel = ChannelList.getChannelInfo(channelIndex);

        // 2) channel 참가 인원 확인 : 이미 6명이라면 join 거절
        if (channel.getSessionsInChannel().getCnt() == 6) {
            log.warn("[JOIN REJECTED] ROOM IS FULL");
            return;
        }

        // 3) Lobby에서 [userName] 제거
        int userId = ChannelList.getLobby().getSessionList().get(userName);
        ChannelList.getLobby().getSessionList().remove(userName);

        // 4) channel 안 [userName]의 idx 탐색
        for (int i = 0; i < 6; i++) {
            if (channel.getSessionsInChannel().getIsExisted().get(i) == 0) {
                channel.getSessionsInChannel().setTrueIsExisted(i); // UPDATE isExisted
                channel.getSessionsInChannel().plusCnt();           // UPDATE cnt
                channel.addSession(userName, i);                    // UPDATE nameToIdx, idxToName
                channel.getSessionList().put(userName, userId);
                break;
            }
        }

        // 5) LOGGING JOIN
        log.info("[userName:{}] CHANNEL JOINED, channelName:{}", userName, ChannelList.getChannelInfo(channelIndex).getChannelName());

        // 6) Broadcasting

        // 동작 확인
        System.out.println("channel.sessionsInChannel.cnt = " + channel.getSessionsInChannel().getCnt());
        System.out.println("channel.sessionsInChannel.isExisted = " + channel.getSessionsInChannel().getIsExisted());

        for (String key : ChannelList.getChannelInfo(channelIndex).getNameToIdx().keySet()) {
            System.out.println(String.format("[userName:%s], [idx:%s]", key, ChannelList.getChannelInfo(channelIndex).getNameToIdx().get(key)));
        }

    }

    public static void isReady(String userName, int channelIndex) {

        Channel channel = ChannelList.getChannelInfo(channelIndex);
        int idx = channel.getNameToIdx().get(userName);
        ArrayList<Boolean> isReady = channel.getSessionsInChannel().getIsReady();

        if (isReady.get(idx)) {
            isReady.set(idx, false);
            log.info("[userName:{}] NOT READY, channelName:{}", userName, ChannelList.getChannelInfo(channelIndex).getChannelName());

        } else {
            isReady.set(idx, true);
            log.info("[userName:{}] READY, channelName:{}", userName, ChannelList.getChannelInfo(channelIndex).getChannelName());

        }

        // 동작 확인
        Iterator ready = ChannelList.getChannelInfo(channelIndex).getSessionsInChannel().getIsReady().iterator();
        while(ready.hasNext()) {
            System.out.println(ready.next());
        }

    }

    public static void leaveChannel(String userName, int channelIndex) {
        Boolean flag = false;
        Channel channel = ChannelList.getChannelInfo(channelIndex);
        SessionsInChannel sic = channel.getSessionsInChannel();

        int idx = channel.getIdx(userName);         // 나가는 [userName]의 idx 정보

        // 1. 나가는 [userName] 삭제
        channel.getNameToIdx().remove(userName);    // nameToIdx
        channel.getIdxToName().remove(idx);         // idxToName
        channel.getSessionList().remove(userName);  // sessionList
        sic.minusCnt();                             // cnt
        sic.setFalseIsExisted(idx);                 // isExist

        switch (sic.getCnt()) {
            // 2. 방이 없어지는 경우
            case 0:
                ChannelList.getChannelList().remove(channelIndex);
                channel = null;
                flag = true;
                break;


            // 3. 방이 유지되는 경욷
            default :
                // 방장이 나간 경우 : 새로운 방장 설정
                if (idx == 0) {
                    ArrayList<Integer> isExisted = sic.getIsExisted();
                    for (int i = 1; i < isExisted.size(); i++) {
                        if (isExisted.get(i) == 1) {
                            sic.setFalseIsExisted(i);
                            sic.setTrueIsExisted(0);

                            String leftUserName = channel.getIdxToName().get(i);

                            channel.getIdxToName().remove(i);
                            channel.getIdxToName().put(0, leftUserName);
                            channel.getNameToIdx().put(leftUserName, 0);
                            break;
                        }
                    }
                }

        }

        // 4) LOGGING LEAVE
        log.info("[userName:{}] CHANNEL LEAVED, channelName:{}", userName, ChannelList.getChannelInfo(channelIndex).getChannelName());


        // 동작 확인
        System.out.println("channel.sessionsInChannel.cnt = " + channel.getSessionsInChannel().getCnt());
        System.out.println("channel.sessionsInChannel.isExisted = " + channel.getSessionsInChannel().getIsExisted());

        for (String key : ChannelList.getChannelInfo(channelIndex).getNameToIdx().keySet()) {
            System.out.println(String.format("[userName:%s], [idx:%s]", key, ChannelList.getChannelInfo(channelIndex).getNameToIdx().get(key)));
        }

    }
    
    // 채널 내 모든 세션에 메시지 브로드캐스트
    public static Mono<Void> broadcastMessage(int channelIndex, String json) {
        return Flux.fromIterable(ChannelList.getChannelInfo(channelIndex).getSessionList().keySet())
                .flatMap(userName -> Session.getSessionList().get(userName).outbound().sendString(Mono.just(json)).then())
                .then();
    }
}


