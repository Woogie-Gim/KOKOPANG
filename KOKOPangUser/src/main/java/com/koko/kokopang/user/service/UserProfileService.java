package com.koko.kokopang.user.service;

import com.koko.kokopang.user.model.UserProfile;

import java.util.Optional;

public interface UserProfileService {
    void uploadProfile(UserProfile userProfile);

    UserProfile getUserProfile(int userId);

    void deleteUserProfile(int profileImgId);

    Optional<UserProfile> getProfileImg(int profileImgId);

    Long existImgCount(Integer userId);
}
