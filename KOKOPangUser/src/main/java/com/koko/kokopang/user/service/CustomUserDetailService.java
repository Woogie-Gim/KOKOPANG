package com.koko.kokopang.user.service;

import com.koko.kokopang.user.dto.CustomUserDetails;
import com.koko.kokopang.user.model.User;
import com.koko.kokopang.user.repository.UserRepository;
import org.springframework.security.core.userdetails.UserDetails;
import org.springframework.security.core.userdetails.UserDetailsService;
import org.springframework.security.core.userdetails.UsernameNotFoundException;
import org.springframework.stereotype.Service;

@Service
public class CustomUserDetailService implements UserDetailsService {

    private final UserRepository userRepository;

    public CustomUserDetailService(UserRepository userRepository) {
        this.userRepository = userRepository;
    }

    @Override
    public UserDetails loadUserByUsername(String username) throws UsernameNotFoundException {
        System.out.println(username);
        User userData = userRepository.findByEmail(username);
        System.out.println(userData);
        if (userData != null) {
            return new CustomUserDetails(userData);
        }

        return null;
    }
}
