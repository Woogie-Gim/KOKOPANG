package com.koko.kokopang.board.controller;

import com.koko.kokopang.board.dto.CommentDTO;
import com.koko.kokopang.board.dto.CommentListDTO;
import com.koko.kokopang.board.model.Comment;
import com.koko.kokopang.board.service.CommentService;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.security.core.userdetails.UserDetails;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@RequestMapping("/comment")
public class CommentController {

    private final CommentService commentService;

    public CommentController(CommentService commentService) {
        this.commentService = commentService;
    }

    /**
     * 댓글 작성
     */
    @PostMapping("/create")
    public ResponseEntity<?> createComment(@RequestBody CommentDTO commentDTO) {
        commentService.createComment(commentDTO);
        return new ResponseEntity<CommentDTO>(commentDTO, HttpStatus.OK);
    }

    @GetMapping("/read")
    public ResponseEntity<?> readCommentByBoard(@RequestParam int boardId) {
        List<CommentListDTO> commentListDTO = commentService.getCommentAll(boardId);

        return new ResponseEntity<List<CommentListDTO>>(commentListDTO, HttpStatus.OK);
    }

    /**
     * 댓글 수정
     */
    @PutMapping("/update")
    public ResponseEntity<?> updateComment(@RequestBody Comment comment) {
        commentService.updateComment(comment);
        return new ResponseEntity<Comment>(comment, HttpStatus.OK);
    }

    /**
     * 댓글 삭제
     */
    @GetMapping("/delete")
    public ResponseEntity<?> deleteComment(@RequestParam Long commentId) {
        commentService.deleteComment(commentId);
        return new ResponseEntity<String>("댓글 삭제 성공", HttpStatus.OK);
    }

}
