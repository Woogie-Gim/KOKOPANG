package com.koko.kokopang.user.repository;

import com.koko.kokopang.user.model.Friendship;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

@Repository
public interface FriendshipRepository extends JpaRepository<Friendship,Integer> {
}
