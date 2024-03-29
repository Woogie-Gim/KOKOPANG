import React, {useEffect, useState} from 'react'
import { useNavigate } from 'react-router-dom'
import { MenuBox, Menu, LogoBox,Hme } from '../styles/Main/MainStyle'
import logo from '../assets/koko_logo.png'

const NaviBar = () => {
  
  const navigate = useNavigate();
  return (
    <Hme>
      <MenuBox>
        <LogoBox>
          <img src={logo} alt="로고" onClick={() => navigate("/")}></img>
        </LogoBox>
        <Menu onClick={() => navigate("/notice")}>공지사항</Menu>
        <Menu onClick={() => navigate("/rank")}>랭킹</Menu>
        <Menu onClick={() => navigate("/comunity")}>커뮤니티</Menu>
        <Menu onClick={() => navigate("/iteminfo")}>게임 가이드</Menu>
      </MenuBox>
    </Hme>
  )
}

export default NaviBar