import React, {useEffect, useState} from 'react'
import { useNavigate } from 'react-router-dom'
import { MenuBox, Menu, LogoBox } from '../styles/Main/MainStyle'
import logo from '../assets/jam.gif'

const NaviBar = () => {
  
  const navigate = useNavigate();
  return (
    <MenuBox>
      <LogoBox>
        <img src={logo} alt="로고" onClick={() => navigate("/")}></img>
      </LogoBox>
      <Menu onClick={() => navigate("/rank")}>랭킹</Menu>
      <Menu onClick={() => navigate("/story")}>스토리 소개</Menu>
      <Menu onClick={() => navigate("/iteminfo")}>아이템 조합법</Menu>
    </MenuBox>
  )
}

export default NaviBar