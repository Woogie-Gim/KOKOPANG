package com.koko.kokopang.board.repository;

import com.koko.kokopang.board.model.Board;
import com.koko.kokopang.user.model.User;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.stereotype.Repository;

import java.util.List;

@Repository
public interface BoardRepository extends JpaRepository<Board,Integer> {
    Board findBoardById(int boardId);

    @Query("SELECT b FROM Board b ORDER BY b.id DESC")
    List<Board> findAllDesc();

    void deleteBoardById(int boardId);
}
