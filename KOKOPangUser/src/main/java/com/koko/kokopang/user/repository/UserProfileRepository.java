package com.koko.kokopang.user.repository;

import com.koko.kokopang.user.model.UserProfile;
import org.springframework.data.jpa.repository.JpaRepository;

import java.util.Optional;

public interface UserProfileRepository extends JpaRepository<UserProfile,Integer> {

    UserProfile findByUserId(int userId);

    void deleteById(int id);

    Optional<UserProfile> findById(int id);

    Long countByUserId(int userId);
}
