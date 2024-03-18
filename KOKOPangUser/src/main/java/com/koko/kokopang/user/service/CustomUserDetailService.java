package com.koko.kokopanguser.service;

import com.koko.kokopanguser.dto.CustomUserDetails;
import com.koko.kokopanguser.model.User;
import com.koko.kokopanguser.repository.UserRepository;
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
