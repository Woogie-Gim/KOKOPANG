package com.koko.kokopang.user.service;

import com.koko.kokopang.user.model.UserProfile;
import com.koko.kokopang.user.repository.UserProfileRepository;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import java.util.Optional;

@Service
public class UserProfileServiceImpl implements UserProfileService{

    private final UserProfileRepository userProfileRepository;

    public UserProfileServiceImpl(UserProfileRepository userProfileRepository) {
        this.userProfileRepository = userProfileRepository;
    }

    @Override
    public void uploadProfile(UserProfile userProfile) {
        userProfileRepository.save(userProfile);
    }

    @Override
    public UserProfile getUserProfile(int userId) {
        return userProfileRepository.findByUserId(userId);
    }

    @Override
    @Transactional
    public void deleteUserProfile(int profileImgId) {
        userProfileRepository.deleteById(profileImgId);
    }

    @Override
    public Optional<UserProfile> getProfileImg(int profileImgId) {
        return userProfileRepository.findById(profileImgId);
    }

    @Override
    public Long existImgCount(Integer userId) {
        return userProfileRepository.countByUserId(userId);
    }
}
