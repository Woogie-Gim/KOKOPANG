package org.koko.kokopangmulti.Channel;

import org.koko.kokopangmulti.Object.Channel;
import org.koko.kokopangmulti.Object.ChannelList;
import org.koko.kokopangmulti.Object.SessionsInChannel;
import org.koko.kokopangmulti.session.Session;
import org.koko.kokopangmulti.session.SessionState;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import java.util.ArrayList;
import java.util.Iterator;
import java.util.Map;

import static org.koko.kokopangmulti.Braodcast.BroadcastToChannel.broadcastMessage;
import static org.koko.kokopangmulti.Braodcast.BroadcastToLobby.broadcastLobby;
import static org.koko.kokopangmulti.Braodcast.BroadcastToLobby.broadcastPrivate;
import static org.koko.kokopangmulti.session.Session.getSessionList;
import static org.koko.kokopangmulti.Braodcast.ToJson.*;

public class ChannelHandler {
    private static final Logger log = LoggerFactory.getLogger(ChannelHandler.class);
    public static void createChannel(String userName, String channelName) {

        // 1) Channel 인스턴스 생성
        Channel channel = new Channel(channelName, userName);

        // 2) Lobby에서 [userName] session 제거
        int userId = ChannelList.getLobby().getSessionList().get(userName);
        ChannelList.getLobby().getSessionList().remove(userName);

        // 2-1) SessionInfo 수정
        getSessionList().get(userName).setSessionState(channel.getChannelIdx());

        // 3) ChannelList에 channel 추가
        int channelIndex = ChannelList.addChannel(channel);

        // sessionList해쉬맵에 이름, id값 추가
        channel.getSessionList().put(userName, userId);

        // 4) BroadCasting
        // 4-1) channel 내 sessions
        broadcastMessage(channelIndex, channelSessionListToJSON(channel)).subscribe();
        // 4-2) lobby 내 sessions : channelList UPDATE
        broadcastLobby(channelListToJson()).subscribe();
        // 4-3) lobby 내 sessions : sessionList UPDATE
        broadcastLobby(lobbySessionsToJson()).subscribe();

        // 5) LOGGING CREATE
        log.info("[userName:{}] CHANNEL CREATED, channelName:{}", userName, channelName);

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

        // 3-1) sessionInfo 수정
        Session.getSessionList().get(userName).setSessionState(channelIndex);

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
        // 6-1) channel 내 sessions
        broadcastMessage(channelIndex, channelSessionListToJSON(channel)).subscribe();
        // 6-2) lobby 내 sessions : channelList UPDATE
        broadcastLobby(channelListToJson()).subscribe();
        // 6-3) lobby 내 sessions : sessionList UPDATE
        broadcastLobby(lobbySessionsToJson()).subscribe();

        // 동작 확인
        System.out.println("channel.sessionsInChannel.cnt = " + channel.getSessionsInChannel().getCnt());
        System.out.println("channel.sessionsInChannel.isExisted = " + channel.getSessionsInChannel().getIsExisted());

        for (String key : ChannelList.getChannelInfo(channelIndex).getNameToIdx().keySet()) {
            System.out.println(String.format("[userName:%s], [idx:%s]", key, ChannelList.getChannelInfo(channelIndex).getNameToIdx().get(key)));
        }

    }

    public static void isReady(String userName, int channelIndex) {

        // 1) channel data
        Channel channel = ChannelList.getChannelInfo(channelIndex);
        int idx = channel.getNameToIdx().get(userName);
        ArrayList<Boolean> isReady = channel.getSessionsInChannel().getIsReadyList();

        // 2) ready 상태 변경
        if (isReady.get(idx)) {
            // 준비 -> 준비 안 됨
            isReady.set(idx, false);
            log.info("[userName:{}] NOT READY, channelName:{}", userName, ChannelList.getChannelInfo(channelIndex).getChannelName());

        } else {
            // 준비 안 됨 -> 준비
            isReady.set(idx, true);
            log.info("[userName:{}] READY, channelName:{}", userName, ChannelList.getChannelInfo(channelIndex).getChannelName());

        }

        // 3) Broadcasting : channel 내 sessions
        broadcastMessage(channelIndex, channelSessionListToJSON(channel)).subscribe();

        // 동작 확인
        Iterator ready = ChannelList.getChannelInfo(channelIndex).getSessionsInChannel().getIsReadyList().iterator();
        while(ready.hasNext()) {
            System.out.println(ready.next());
        }

    }

    public static void leaveChannel(String userName, int channelIndex) {

        // 1) channel 정보
        boolean flag = true;                                         // channel 유무
        Channel channel = ChannelList.getChannelInfo(channelIndex);
        SessionsInChannel sic = channel.getSessionsInChannel();

        // 2) 나가는 [userName] 정보
        int idx = channel.getIdx(userName);                     // 나가는 [userName]의 idx 정보
        int userId = channel.getSessionList().get(userName);    // 나가는 [userName]의 userId

        // 3) 나가는 [userName] 삭제
        channel.getNameToIdx().remove(userName);    // nameToIdx
        channel.getIdxToName().remove(idx);         // idxToName
        channel.getSessionList().remove(userName);  // sessionList
        sic.minusCnt();                             // cnt
        sic.setFalseIsExisted(idx);                 // isExisted

        // 4) 나가는 [userName] lobby에 추가
        ChannelList.getLobby().getSessionList().put(userName, userId);

        // 4-1) sessionInfo수정
        Session.getSessionList().get(userName).setSessionState(0);

        // 5) LOGGING LEAVE
        log.info("[userName:{}] CHANNEL LEAVED, channelName:{}", userName, ChannelList.getChannelInfo(channelIndex).getChannelName());


        switch (sic.getCnt()) {

            // 6) 방이 없어지는 경우
            case 0:
                // 6-1) channel 객체 제거
                channel = null;
                // 6-2) channelList에서 channel 제거
                ChannelList.getChannelList().remove(channelIndex);
                System.out.println(ChannelList.getChannelList());
                // 6-3) 방 제거 flag 설정
                flag = false;
                break;


            // 7) 방이 유지되는 경우
            default :

                // 방장이 나간 경우 : 새로운 방장 설정
                if (idx == 0) {
                    ArrayList<Integer> isExisted = sic.getIsExisted();
                    for (int i = 1; i < isExisted.size(); i++) {
                        if (isExisted.get(i) == 1) {
                            // UPDATE : channel 내 idx (i -> 0)
                            sic.setFalseIsExisted(i);
                            sic.setTrueIsExisted(0);

                            // UPDATE : Ready 상태
                            Boolean isReady = sic.getIsReady(i);    // 새로운 방장의 Ready 상태
                            sic.setIsReady(0, isReady);         // 방장 idx에 ready 상태 동기화
                            sic.setIsReady(i, false);       // 기존 idx의 ready 상태 초기화

                            // UPDATE : idxToName, nameToIdx
                            String leftUserName = channel.getIdxToName().get(i);    // 새로운 방장의 userName
                            channel.getIdxToName().remove(i);
                            channel.getIdxToName().put(0, leftUserName);
                            channel.getNameToIdx().put(leftUserName, 0);

                            break;
                        }
                    }
                }

                // 팀원이 나간 경우
                else {
                    // ready 상태 초기화
                    sic.setIsReady(idx, false);
                }

        }


        // 8) broadcasting :
        // 8-1) channel 내 sessions : 방이 사라지지 않은 경우
        if (flag) {
            broadcastMessage(channelIndex, channelSessionListToJSON(channel)).subscribe();
        }
        // 8-2) lobby 내 sessions : channelList UPDATE
        broadcastLobby(channelListToJson()).subscribe();
        // 8-3) lobby 내 sessions : sessionList UPDATE
        broadcastLobby(lobbySessionsToJson()).subscribe();

        // 동작 확인
        if(flag) {

            System.out.println("channel.sessionsInChannel.cnt = " + channel.getSessionsInChannel().getCnt());
            System.out.println("channel.sessionsInChannel.isExisted = " + channel.getSessionsInChannel().getIsExisted());

            for (String key : ChannelList.getChannelInfo(channelIndex).getNameToIdx().keySet()) {
                System.out.println(String.format("[userName:%s], [idx:%s]", key, ChannelList.getChannelInfo(channelIndex).getNameToIdx().get(key)));
            }

        }
    }
}


