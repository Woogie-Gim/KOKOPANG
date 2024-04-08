package com.koko.kokopang.board.service;

import com.koko.kokopang.board.dto.BoardDTO;
import com.koko.kokopang.board.dto.CommentDTO;
import com.koko.kokopang.board.dto.CommentListDTO;
import com.koko.kokopang.board.model.Board;
import com.koko.kokopang.board.model.Comment;
import com.koko.kokopang.board.repository.BoardRepository;
import com.koko.kokopang.board.repository.CommentRepository;
import com.koko.kokopang.user.dto.UserDTO;
import com.koko.kokopang.user.model.User;
import com.koko.kokopang.user.service.UserService;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import java.time.LocalDate;
import java.time.format.DateTimeFormatter;
import java.util.ArrayList;
import java.util.List;

@Service
public class CommentServiceImpl implements CommentService{

    public final CommentRepository commentRepository;
    public final BoardService boardService;
    public final BoardRepository boardRepository;
    public final UserService userService;

    public CommentServiceImpl(CommentRepository commentRepository, BoardService boardService, BoardRepository boardRepository, UserService userService) {
        this.commentRepository = commentRepository;
        this.boardService = boardService;
        this.boardRepository = boardRepository;
        this.userService = userService;
    }

    @Override
    public void createComment(CommentDTO commentDTO) {
        Comment newComment = new Comment();

        LocalDate currentDate = LocalDate.now();
        DateTimeFormatter formatter = DateTimeFormatter.ofPattern("yy-MM-dd");
        String formattedDate = currentDate.format(formatter);

        Board board = boardRepository.findBoardById(commentDTO.getBoardId());
        newComment.setBoard(board);
        newComment.setContent(commentDTO.getContent());
        newComment.setUserEmail(commentDTO.getUserEmail());
        newComment.setCreated(formattedDate);
        newComment.setModified(formattedDate);
        commentRepository.save(newComment);
    }

    @Override
    public List<CommentListDTO> getCommentAll(int boardId) {
        Board board = boardRepository.findBoardById(boardId);
        List<Comment> comments = commentRepository.findByBoard(board);

        List<CommentListDTO> commentList = new ArrayList<>();

        for (Comment comment:comments) {
            CommentListDTO c = new CommentListDTO();

            c.setCommentId(comment.getId());
            c.setContent(comment.getContent());
            c.setCreated(comment.getCreated());
            c.setModified(comment.getModified());
            UserDTO user = userService.getProfile(comment.getUserEmail());
            c.setUsername(user.getName());
            c.setProfileImg(user.getProfileImg());

            commentList.add(c);
        }

        return commentList;
    }

    @Override
    @Transactional
    public void updateComment(Comment comment) {
        Comment getComment = commentRepository.findCommentById(comment.getId());

        getComment.setContent(comment.getContent());
    }

    @Override
    @Transactional
    public void deleteComment(Long commentId) {
        commentRepository.deleteCommentById(commentId);
    }
}
