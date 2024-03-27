package org.koko.kokopangmulti.Channel;

import org.json.JSONObject;
import org.koko.kokopangmulti.Object.Channel;
import org.koko.kokopangmulti.Object.ChannelList;
import org.koko.kokopangmulti.Object.Session;
import reactor.netty.Connection;

public class ChannelHandler {
    public static void createChannel(String userName, String channelName) {

        System.out.println("ROOM CREATED");

        // userName을 통해 connection 정보 추출 및 로비의 세션 목록에서 해당 유저 제거
        Connection connection = ChannelList.getLobby().getSessionList().get(userName);
        ChannelList.getLobby().getSessionList().remove(userName);

        // 방 생성 및 방 목록에 방 추가 하면서 생성된 방의 index 할당
        Channel channel = new Channel(channelName, userName, connection);
        int index = ChannelList.addChannel(channel);

//        // 생성된 방에 유저 추가
//        ChannelList.getChannelInfo(index).getSessionList().put(userName, connection);

        // 동작 확인
        System.out.println("Lobby: " + ChannelList.getLobby().getSessionList());
        System.out.println(channelName + ": " + ChannelList.getChannelInfo(index).getSessionList());

        // 생성된 채널에 대한 정보 채널 내 세션에게 브로드캐스팅

        // 로비 클라이언트에게 방 목록, 로비 참가 유저 변경사항 브로드캐스팅
    }

    public static void joinChannel(String userName, int roomIndex) {
        // userName을 통해 connection 정보 추출 및 로비의 세션 목록에서 해당 유저 제거
        Connection connection = Session.getSessionList().get(userName);
        ChannelList.getLobby().getSessionList().remove(userName);

        // roomIndex를 통해 ChannelList에서 채널 정보 파싱
        Channel channel = ChannelList.getChannelList().get(roomIndex);

        // 해당 채널에 유저 추가
        channel.getSessionList().put(userName, connection);

        // 정상 작동 확인 테스트
        System.out.println(channel.getChannelName() + ": " + channel.getSessionList());
        System.out.println("Lobby: " + ChannelList.getLobby().getSessionList());

        // 로비 클라이언트에게 로비 참가중인 유저 목록 브로드캐스팅, 방에 참가중인 세션들에게 세션 목록 브로드캐스트

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
