package com.koko.kokopanguser.repository;

import com.koko.kokopanguser.model.User;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

@Repository
public interface UserRepository extends JpaRepository<User,Integer> {
    Boolean existsByEmail(String email);

    User findByEmail(String email);

    User findByUserId(int userId);
}
