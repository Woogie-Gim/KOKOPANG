package com.koko.kokopanguser.dto;

import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Getter;
import lombok.NoArgsConstructor;

@Getter
@NoArgsConstructor
@AllArgsConstructor
@Builder
public class TokenDTO {

    private String grantType; //JWT 인증 타입 : 우리는 Bearer
    private String accessToken;
    private String refreshToken;
}
