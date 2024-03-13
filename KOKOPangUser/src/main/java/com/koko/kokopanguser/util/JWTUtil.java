package com.koko.kokopanguser.util;

import com.koko.kokopanguser.dto.TokenDTO;
import io.jsonwebtoken.*;
import io.jsonwebtoken.io.Decoders;
import io.jsonwebtoken.security.Keys;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.stereotype.Component;

import java.security.Key;
import java.time.Duration;
import java.util.Date;

@Component
public class JWTUtil {

    private Key key;

    private static final String BEARER_TYPE = "Bearer";
    private long accessTokenValidTime = Duration.ofSeconds(20).toMillis(); // 만료 시간 20초

    private long refreshTokenValidTime = Duration.ofDays(7).toMillis(); // 만료 시간 7일

    public JWTUtil(@Value("${spring.jwt.secret}") String secret) {

        byte[] byteSecretKey = Decoders.BASE64.decode(secret);
        key = Keys.hmacShaKeyFor(byteSecretKey);
    }

    public String getUsername(String token) {

        return Jwts.parserBuilder().setSigningKey(key).build().parseClaimsJws(token).getBody().get("email", String.class);
    }

    public String getRole(String token) {

        return Jwts.parserBuilder().setSigningKey(key).build().parseClaimsJws(token).getBody().get("role", String.class);
    }

    public Boolean isExpired(String token) {
        try {
            Jwts.parserBuilder().setSigningKey(key).build().parseClaimsJws(token).getBody().getExpiration().before(new Date());
            return false; // 토큰이 만료되지 않았다면 false 반환
        } catch (ExpiredJwtException e) {
            return true; // 토큰이 만료되었다면 true 반환
        } catch (JwtException e) {
            // 다른 JWT 관련 예외 처리, 필요에 따라 false 또는 true 반환, 또는 추가적인 예외 처리
            throw e;
        }
//        return Jwts.parserBuilder().setSigningKey(key).build().parseClaimsJws(token).getBody().getExpiration().before(new Date());
    }

    public TokenDTO createJwt(String email, String role) {

        Claims accessClaims = Jwts.claims();
        accessClaims.put("email", email);
        accessClaims.put("role", role);

        String accessToken = Jwts.builder()
                .setClaims(accessClaims)
                .setIssuedAt(new Date(System.currentTimeMillis()))
                .setExpiration(new Date(System.currentTimeMillis() + accessTokenValidTime))
                .signWith(key, SignatureAlgorithm.HS256)
                .compact();

        Claims refreshClaims = Jwts.claims();
        refreshClaims.put("email", email);

        String refreshToken = Jwts.builder()
                .setClaims(refreshClaims)
                .setIssuedAt(new Date(System.currentTimeMillis()))
                .setExpiration(new Date(System.currentTimeMillis() + refreshTokenValidTime))
                .signWith(key, SignatureAlgorithm.HS256)
                .compact();

        return TokenDTO.builder()
                .grantType(BEARER_TYPE)
                .accessToken(accessToken)
                .refreshToken(refreshToken)
                .build();
    }
}
