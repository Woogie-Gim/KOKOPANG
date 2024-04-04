package com.koko.kokopang.board.controller;

import com.koko.kokopang.board.dto.BoardDTO;
import com.koko.kokopang.board.dto.BoardListDTO;
import com.koko.kokopang.board.model.Board;
import com.koko.kokopang.board.service.BoardService;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@RequestMapping("/board")
@CrossOrigin(origins = "*", methods = {RequestMethod.GET, RequestMethod.POST, RequestMethod.PUT, RequestMethod.DELETE}, maxAge = 6000)
public class BoardController {

    public final BoardService boardService;

    public BoardController(BoardService boardService) {
        this.boardService = boardService;
    }

    /**
     * 게시글 생성
     * @param board
     * @return
     */
    @PostMapping("/create")
    public ResponseEntity<?> createBoard(@RequestBody Board board) {
        boardService.createBoard(board);
        return new ResponseEntity<Board>(board, HttpStatus.OK);
    }

    /**
     * 단일 게시글 조회
     * @param boardId
     * @return
     */
    @GetMapping("/read")
    public ResponseEntity<?> readBoard(@RequestParam int boardId) {
        BoardDTO board = boardService.readBoard(boardId);
        if (board == null) {
            return new ResponseEntity<String>("게시글 조회 실패", HttpStatus.BAD_REQUEST);
        }

        return new ResponseEntity<BoardDTO>(board,HttpStatus.OK);
    }

    @GetMapping("/all")
    public ResponseEntity<?> getBoardAll() {
        List<BoardListDTO> boardList = boardService.getBoardAll();
        return new ResponseEntity<List<BoardListDTO>>(boardList, HttpStatus.OK);
    }

    @PutMapping("/update")
    public ResponseEntity<?> updtateBoard(@RequestBody Board board) {
        boardService.updateBoard(board);
        return new ResponseEntity<Board>(board, HttpStatus.OK);
    }

    @DeleteMapping("/delete")
    public ResponseEntity<?> deleteBoard(@RequestParam int boardId) {
        boardService.deleteBoard(boardId);
        return new ResponseEntity<String>("게시글 삭제 성공", HttpStatus.OK);
    }
}
