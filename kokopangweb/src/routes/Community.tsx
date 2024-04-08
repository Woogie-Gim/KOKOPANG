import axios from 'axios'
import React, { useEffect, useState } from 'react'
import useAuthStore from '../stores/AuthStore';
import { useShallow } from 'zustand/react/shallow';

import NaviBar from './NaviBar';
import { BoardBox, Board, BoardCreateBox } from '../styles/Community/Community';
import { PageNation } from '../styles/Rank/Rank';
import { useNavigate } from 'react-router-dom';
import { TextBox } from '../styles/Rank/Rank';

const Comunity = () => {
  const [boardList, setBoardList] = useState([]);
  const navigate = useNavigate();

  const defaultProfile =
    "https://cdn.pixabay.com/photo/2015/10/05/22/37/blank-profile-picture-973460_1280.png";

  const { isLogin, PATH, token, } =
    useAuthStore(
      useShallow((state) => ({
        isLogin: state.isLogIn,
        PATH: state.PATH,
        token: state.token,
      }))
    );

  const ITEMS_PER_PAGE = 8;
  const PAGINATION_NUMBERS = 5;

  const [currentPage, setCurrentPage] = useState(1);

  const totalPageCount = Math.ceil(boardList.length / ITEMS_PER_PAGE);

  const handlePageChange = (page: number) => {
    setCurrentPage(page);
  };

  const renderPaginationNumbers = () => {
    const paginationNumbers = [];
    const start =
      Math.floor((currentPage - 1) / PAGINATION_NUMBERS) * PAGINATION_NUMBERS +
      1;

    for (
      let i = start;
      i < start + PAGINATION_NUMBERS && i <= totalPageCount;
      i++
    ) {
      paginationNumbers.push(
        <button
          key={i}
          onClick={() => handlePageChange(i)}
          className={currentPage === i ? "current" : ""}
        >
          {i}
        </button>
      );
    }

    return paginationNumbers;
  };

  const startIdx = (currentPage - 1) * ITEMS_PER_PAGE;
  const endIdx = startIdx + ITEMS_PER_PAGE;
  const currentItems = boardList.slice(startIdx, endIdx);

  useEffect(() => {
    axios.get(`${PATH}/board/all`, {
      headers: {
        Authorization: token,
      },
    })
      .then((res: any) => {
        console.log(res.data)
        setBoardList(res.data)
      })
      .catch((error: any) => {
        console.log(error)
      })
  }, [])

  return (
    <div>
      <NaviBar />
      <TextBox>전체 게시판</TextBox>
      <BoardBox>
        <div className='container2'>
          <div className='item2'>#</div>
          <div className='item2'>제목</div>
          <div className='item2'>작성자</div>
        </div>
        {currentItems.map((board, idx) => (
          <Board key={idx}>
            <div className='item'>{boardList.length - idx - (currentPage - 1)*10}</div>
            <div className='item' onClick={() => navigate(`/community/${board["boardId"]}`)}>{board["title"]}</div>
            <div className='item'>
              <img src={board["profileImg"] === null ? defaultProfile : board["profileImg"]} alt='프로필이미지'
                style={{ width: "45px", height: "45px", borderRadius: "100px", border: "1px solid lightgray" }}
              />
            </div>
            <div className='item'>{board["username"]}</div>
          </Board>
        ))}
        <BoardCreateBox>
          <div className='btn' onClick={() => navigate("/community/create")}>글쓰기</div>
        </BoardCreateBox>
        {totalPageCount > 0 && (
          <PageNation>
            <div className="nav_buttons">
              <button onClick={() => handlePageChange(1)}>&lt;&lt;</button>
              <button
                onClick={() => handlePageChange(currentPage - 1)}
                disabled={currentPage === 1}
              >
                &lt;
              </button>
              {renderPaginationNumbers()}
              <button
                onClick={() => handlePageChange(currentPage + 1)}
                disabled={currentPage === totalPageCount}
              >
                &gt;
              </button>
              <button onClick={() => handlePageChange(totalPageCount)}>
                &gt;&gt;
              </button>
            </div>
          </PageNation>
        )}
      </BoardBox>
    </div>
  )
}

export default Comunity