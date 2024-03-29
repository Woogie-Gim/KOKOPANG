package com.koko.kokopang.board.service;

import com.koko.kokopang.board.dto.BoardDTO;
import com.koko.kokopang.board.dto.BoardListDTO;
import com.koko.kokopang.board.model.Board;
import org.springframework.transaction.annotation.Transactional;

import java.util.List;

public interface BoardService {
    void createBoard(Board board);

    BoardDTO readBoard(int boardId);

    List<BoardListDTO> getBoardAll();

    void updateBoard(Board board);

    void deleteBoard(int boardId);
}
