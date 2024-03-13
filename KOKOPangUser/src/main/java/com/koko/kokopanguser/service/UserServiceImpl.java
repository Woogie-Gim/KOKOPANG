package com.koko.kokopanguser.service;

import com.koko.kokopanguser.dto.UserDTO;
import com.koko.kokopanguser.model.User;
import com.koko.kokopanguser.repository.UserRepository;
import lombok.ToString;
import org.springframework.security.crypto.bcrypt.BCryptPasswordEncoder;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

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

    @Override
    public User getProfile(String email) {
        User user = userRepository.findByEmail(email);

        if (user == null) {
            System.out.println("User not exist");
            return null;
        }

        return user;
    }

    @Override
    @Transactional
    public User updateProfile(User user) {

        User userProfile = userRepository.findByEmail(user.getEmail());

        if (userProfile == null) {
            return null;
        }

        userProfile.setName(user.getName());
        userProfile.setNickname(user.getNickname());

        return userProfile;
    }

    @Override
    @Transactional
    public boolean updatePassword(int userId, String password) {

        if (!password.contains("!") ||
                !password.contains("@") ||
                !password.contains("~") ||
                !password.contains("#") ||
                password.length() < 9 ||
                password.length() > 15
        ) {
            return false;
        }

        User user = userRepository.findByUserId(userId);

        if (user == null) {
            return false;
        }

        user.setPassword(bCryptPasswordEncoder.encode(password));
        return true;
    }
}
