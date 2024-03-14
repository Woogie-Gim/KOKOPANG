package com.koko.kokopanguser.controller;

import com.koko.kokopanguser.service.RedisService;
import com.koko.kokopanguser.util.JWTUtil;
import io.jsonwebtoken.ExpiredJwtException;
import jakarta.servlet.http.Cookie;
import jakarta.servlet.http.HttpServletRequest;
import jakarta.servlet.http.HttpServletResponse;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequestMapping("/token")
public class ReissueController {

    private final JWTUtil jwtUtil;
    private final RedisService redisService;

    public ReissueController(JWTUtil jwtUtil, RedisService redisService) {
        this.jwtUtil = jwtUtil;
        this.redisService = redisService;
    }

    /**
     * refreshToken 재발급
     * @param request
     * @param response
     * @return
     */
    @PostMapping("/reissue")
    public ResponseEntity<?> reissue(HttpServletRequest request, HttpServletResponse response) {

        //get refresh token
        String refreshToken = request.getHeader("refreshToken");

        if (refreshToken == null || !refreshToken.startsWith("Bearer ")) {
            //response status code
            return new ResponseEntity<String>("refresh token null", HttpStatus.BAD_REQUEST);
        }

        //expired check
        try {
            jwtUtil.isExpired(refreshToken);
        } catch (ExpiredJwtException e) {

            //response status code
            return new ResponseEntity<String>("refresh token expired", HttpStatus.BAD_REQUEST);
        }

        // 토큰이 refresh인지 확인 (발급시 페이로드에 명시)
        String category = jwtUtil.getCategory(refreshToken);

        if (!category.equals("refresh")) {

            //response status code
            return new ResponseEntity<String>("invalid refresh token", HttpStatus.BAD_REQUEST);
        }

        String username = jwtUtil.getUsername(refreshToken);
        String role = jwtUtil.getRole(refreshToken);

        //make new JWT
        String newAccessToken = jwtUtil.createJwt("access", username, role);
        String newRefreshToken = jwtUtil.createJwt("refresh", username, role);

        if (!redisService.equalToken(username,newRefreshToken)) {
            return new ResponseEntity<String>("유효하지 않은 토큰입니다.", HttpStatus.BAD_REQUEST);
        }

        //refreshToken Redis 저장
        redisService.saveRefreshToken(username, newRefreshToken);

        //response
        response.setHeader("Authorization", "Bearer " + newAccessToken);
        response.setHeader("refreshToken", "Bearer " +  newRefreshToken);
        
        return new ResponseEntity<Void>(HttpStatus.OK);
    }
}
