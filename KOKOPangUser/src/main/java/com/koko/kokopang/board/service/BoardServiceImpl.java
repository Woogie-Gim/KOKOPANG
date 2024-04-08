package com.koko.kokopang.board.service;

import com.koko.kokopang.board.dto.BoardDTO;
import com.koko.kokopang.board.dto.BoardListDTO;
import com.koko.kokopang.board.model.Board;
import com.koko.kokopang.board.repository.BoardRepository;
import com.koko.kokopang.user.dto.UserDTO;
import com.koko.kokopang.user.model.User;
import com.koko.kokopang.user.service.UserService;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import java.util.ArrayList;
import java.util.Collection;
import java.util.Collections;
import java.util.List;

@Service
public class BoardServiceImpl implements BoardService{

    public final BoardRepository boardRepository;
    public final UserService userService;

    public BoardServiceImpl(BoardRepository boardRepository, UserService userService) {
        this.boardRepository = boardRepository;
        this.userService = userService;
    }

    @Override
    public void createBoard(Board board) {
        Board newBoard = new Board();

        newBoard.setUserEmail(board.getUserEmail());
        newBoard.setTitle(board.getTitle());
        newBoard.setContent(board.getContent());

        boardRepository.save(newBoard);
    }

    @Override
    public BoardDTO readBoard(int boardId) {
        Board board = boardRepository.findBoardById(boardId);

        BoardDTO boardDTO = new BoardDTO();
        boardDTO.setTitle(board.getTitle());
        boardDTO.setContent(board.getContent());
        boardDTO.setBoardId(board.getId());

        UserDTO user = userService.getProfile(board.getUserEmail());
        boardDTO.setProfileImg(user.getProfileImg());
        boardDTO.setName(user.getName());

        return boardDTO;
    }

    @Override
    public List<BoardListDTO> getBoardAll() {
        List<Board> BoardList = boardRepository.findAllDesc();

        List<BoardListDTO> getBoardList = new ArrayList<>();

        for (Board board:BoardList) {
            BoardListDTO b = new BoardListDTO();

            b.setBoardId(board.getId());
            b.setTitle(board.getTitle());
            UserDTO user = userService.getProfile(board.getUserEmail());

            b.setUsername(user.getName());
            b.setProfileImg(user.getProfileImg());

            getBoardList.add(b);
        }

        return getBoardList;
    }

    @Override
    @Transactional
    public void updateBoard(Board board) {
        Board getBoard = boardRepository.findBoardById(board.getId());

        getBoard.setTitle(board.getTitle());
        getBoard.setContent(board.getContent());
    }

    @Override
    @Transactional
    public void deleteBoard(int boardId) {
        boardRepository.deleteBoardById(boardId);
    }
}
