import React, { useState } from 'react'
import NaviBar from './NaviBar'

import { MyRankBox, UserRankBox, RankTable, SearchBox, SearchInput, SearchBtn } from '../styles/Rank/Rank'

const Rank = () => {
  const [search,setSearch] = useState("");
  const changeSearch = (event : React.ChangeEvent<HTMLInputElement>) => {
    setSearch(event.target.value);
  }

  const searchUser = (event : React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();
  }
  return (
    <div>
      <NaviBar />
      <SearchBox onSubmit={searchUser}>
        <SearchInput placeholder='닉네임을 검색해보세요!' value={search} onChange={changeSearch}></SearchInput>
        <SearchBtn>검색</SearchBtn>
      </SearchBox>
      <MyRankBox></MyRankBox>
      <UserRankBox>
        <RankTable>
          
        </RankTable>
      </UserRankBox>
    </div>
  )
}

export default Rank