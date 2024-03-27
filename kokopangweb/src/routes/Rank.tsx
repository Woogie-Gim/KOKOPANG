import React, { useEffect, useState } from 'react'
import NaviBar from './NaviBar'

import { UserRankBox, RankTable, SearchBox, SearchInput, SearchBtn, TextBox, PageNation } from '../styles/Rank/Rank'
import axios from 'axios';
import useAuthStore from '../stores/AuthStore';
import { useShallow } from 'zustand/react/shallow';

const Rank = () => {
  const [search,setSearch] = useState("");
  const [userList,setUserList] = useState([]);
  const [isSearch,setIsSearch] = useState(true);
  const [saveUserList,setSaveUserList] = useState([]);

  const defaultProfile =
    "https://cdn.pixabay.com/photo/2015/10/05/22/37/blank-profile-picture-973460_1280.png";

  const { PATH, token} =
    useAuthStore(
      useShallow((state) => ({
        PATH: state.PATH,
        token: state.token,
      }))
    );

  const changeSearch = (event : React.ChangeEvent<HTMLInputElement>) => {
    setSearch(event.target.value);
  }

  const searchUser = (event : React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();

    if (search === "") {
      setIsSearch(!isSearch)
    }

    const searchUser = saveUserList.filter((item) => item["name"] === search)
    setUserList(searchUser)
    setCurrentPage(1);
  }

  const ITEMS_PER_PAGE = 10;
  const PAGINATION_NUMBERS = 5;

  const [currentPage, setCurrentPage] = useState(1);

  const totalPageCount = Math.ceil(userList.length / ITEMS_PER_PAGE);

  const handlePageChange = (page : number) => {
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
  const currentItems = userList.slice(startIdx, endIdx);

  useEffect(() => {
    axios.get(`${PATH}/user/all`,{
      headers: {
        Authorization: token,
      }
    })
    .then((res) => {
      console.log(res.data)
      const sortedFriendsList = res.data.sort((a: any, b: any) => b.rating - a.rating);
      setUserList(sortedFriendsList)
      setSaveUserList(sortedFriendsList);
    })
    .catch((error) => console.log(error))
  },[isSearch])

  return (
    <div>
      <NaviBar />
      <SearchBox onSubmit={searchUser}>
        <SearchInput placeholder='닉네임을 검색해보세요!' value={search} onChange={changeSearch}></SearchInput>
        <SearchBtn>검색</SearchBtn>
      </SearchBox>
      <TextBox>KOKOPang 전체 랭킹</TextBox>
      <UserRankBox>
        <RankTable>
          {currentItems.map((user,idx) => (
            <div key={idx} className='container'>
              <div className='item'>{idx + 1 + (currentPage - 1)*10}</div>
              <div className='item'>
                <img src={user["profileImg"] === null ? defaultProfile : user["profileImg"]} alt='프로필이미지'
                  style={{ width: "45px", height: "45px", borderRadius: "100px" }}
                />
              </div>
              <div className='item' >{user["name"]}</div>
              <div className='item'>{user["rating"]}</div>
            </div>
          ))}
          {userList.length === 0 ? <div className='no_result'>존재하지 않는 유저입니다.</div> : null}
        </RankTable>
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
      </UserRankBox>
    </div>
  )
}

export default Rank