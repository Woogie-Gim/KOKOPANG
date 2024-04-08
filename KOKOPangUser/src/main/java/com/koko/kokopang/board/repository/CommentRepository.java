package com.koko.kokopang.board.repository;

import com.koko.kokopang.board.dto.CommentListDTO;
import com.koko.kokopang.board.model.Board;
import com.koko.kokopang.board.model.Comment;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

import java.util.List;

@Repository
public interface CommentRepository extends JpaRepository<Comment,Long> {

    List<Comment> findByBoard(Board board);

    Comment findCommentById(Long commentId);

    void deleteCommentById(Long commentId);
}
