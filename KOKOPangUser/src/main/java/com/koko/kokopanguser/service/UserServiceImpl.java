package com.koko.kokopanguser.service;

import com.koko.kokopanguser.dto.UserDTO;
import com.koko.kokopanguser.model.User;
import com.koko.kokopanguser.repository.UserRepository;
import org.springframework.security.crypto.bcrypt.BCryptPasswordEncoder;
import org.springframework.stereotype.Service;

@Service
public class UserServiceImpl implements UserService {

    private final UserRepository userRepository;
    private final BCryptPasswordEncoder bCryptPasswordEncoder;

    public UserServiceImpl(UserRepository userRepository, BCryptPasswordEncoder bCryptPasswordEncoder) {
        this.userRepository = userRepository;
        this.bCryptPasswordEncoder = bCryptPasswordEncoder;
    }

    @Override
    public User signup(UserDTO userDTO) {
        String email = userDTO.getEmail();
        String password = userDTO.getPassword();

        if (!email.contains("@") || email.contains("'")) {
            return null;
        }

        if (userRepository.existsByEmail(email)) {
            return null;
        }

        User newUser = new User();
        newUser.setEmail(email);
        newUser.setPassword(bCryptPasswordEncoder.encode(password));
        newUser.setName(userDTO.getName());
        newUser.setNickname("");

        userRepository.save(newUser);

        return newUser;
    }
}
