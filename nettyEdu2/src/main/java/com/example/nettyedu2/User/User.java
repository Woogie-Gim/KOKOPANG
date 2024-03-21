package com.example.nettyedu2.User;

import lombok.Getter;
import reactor.core.publisher.Mono;
import reactor.netty.Connection;

import java.util.HashMap;
import java.util.concurrent.ConcurrentHashMap;

/*
TCP 서버에서 클라이언트 관리와 메시지 브로드캐스팅을 담당하는 클래스
[기능]
1) 클라이언트의 연결 추적
2) 모든 클라이언트에게 메시지를 브로드캐스트
 */
public class User {
    // [1] 클라이언트 목록 관리
    @Getter
    private static ConcurrentHashMap<String, Connection> clientList = new ConcurrentHashMap<String, Connection>(); // 서버에 연결된 모든 클라이언트를 추적하는 데 사용
    /*
    <<< HashMap 대신 사용하는 ConcurrentHashMap >>
    - ConcurrentHashMap은 동시성에 최적화된 자료구조
    - 여러 스레드가 동시에 Map에 접근하더라도, 데이터의 일관성을 유지할 수 있다.
    - 내부적으로 여러 세그먼트로 나누어 동시에 접근하는 스레드 간 경쟁을 줄여 성능을 향상시킨다
    - 동시에 여러 스레드가 읽기 작업 수행 가능
    - 쓰기 작업 시에도 전체 Map을 Lock하지 않는다. 특정 세그먼트만 Lock하여 다른 부분들은 동시에 읽거나 쓸 수 있게 한다
    - HashMap에 Synchronized 키워드를 사용하여 동기화를 강제하는 것보다 성능상 이점을 가진다
    - HashMap보다 사용하는 리소스가 더 많을 수 있기 때문에, 필요한 상황에만 사용하는 것을 권장
    - User 클래스의 인스턴스로 동시에 접근하여 클라이언트를 추가하거나 메시지를 브로드캐스트하는 경우가 많은 경우 사용 권장
     */
}
