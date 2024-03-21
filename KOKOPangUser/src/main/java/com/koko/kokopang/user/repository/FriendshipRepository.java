package com.koko.kokopang.user.repository;

import com.koko.kokopang.user.dto.FriendshipDTO;
import com.koko.kokopang.user.model.Friendship;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;
import org.springframework.stereotype.Repository;

import java.util.List;

@Repository
public interface FriendshipRepository extends JpaRepository<Friendship,Integer> {

    @Query("SELECT f " +
            "FROM Friendship f " +
            "WHERE f.user.id = :userId " +
            "OR f.friendId = :userId")
    List<Friendship> findFriendshipByUserId(@Param("userId") int userId);

}
