package com.koko.kokopang.user.service;

import com.koko.kokopang.user.dto.UserDTO;
import com.koko.kokopang.user.model.User;

public interface UserService {
    User signup(UserDTO userDTO);

    User getProfile(String email);

    User updateProfile(User user);

    boolean updatePassword(int userId, String password);
}
