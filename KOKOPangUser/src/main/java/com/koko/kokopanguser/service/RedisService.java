package com.koko.kokopanguser.service;

import org.springframework.data.redis.core.RedisTemplate;
import org.springframework.data.redis.core.ValueOperations;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.stereotype.Service;

@Service
public class RedisService {

    private final RedisTemplate<String,String> redisTemplate;

    public RedisService(RedisTemplate<String, String> redisTemplate) {
        this.redisTemplate = redisTemplate;
    }

    public String saveRefreshToken(String key, String refreshToken) {
        ValueOperations<String,String> valueOperations = redisTemplate.opsForValue();
        valueOperations.set(key, refreshToken);

        return "토큰 저장 성공";
    }

    public boolean equalToken(String key, String refreshToken) {
        String savedToken = redisTemplate.opsForValue().get(key);

        return savedToken != null && savedToken.equals(refreshToken);
    }
}
