package com.koko.kokopang.board.service;

import com.koko.kokopang.board.dto.BoardListDTO;
import com.koko.kokopang.board.dto.CommentDTO;
import com.koko.kokopang.board.dto.CommentListDTO;
import com.koko.kokopang.board.model.Board;
import com.koko.kokopang.board.model.Comment;

import java.util.List;

public interface CommentService {
    void createComment(CommentDTO commentDTO);

    List<CommentListDTO> getCommentAll(int boardId);

    void updateComment(Comment comment);

    void deleteComment(Long commentId);
}
