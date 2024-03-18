package com.koko.kokopanguser.service;

import com.koko.kokopanguser.dto.UserDTO;
import com.koko.kokopanguser.model.User;

public interface UserService {
    User signup(UserDTO userDTO);

    User getProfile(String email);

    User updateProfile(User user);

    boolean updatePassword(int userId, String password);
}
