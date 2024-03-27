package com.koko.kokopang.user.service;

import com.koko.kokopang.user.dto.UserDTO;
import com.koko.kokopang.user.model.User;

import java.util.List;

public interface UserService {
    User signup(UserDTO userDTO);

    UserDTO getProfile(String email);

    User updateProfile(User user);

    boolean updatePassword(int userId, String password);

    List<UserDTO> getAllUser();
}
