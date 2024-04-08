import React, { useState } from 'react'
import { useLocation, useNavigate} from 'react-router-dom'
import NaviBar from './NaviBar';
import { BoardBox,Title, Content, BtnBox } from '../styles/Community/CommunityDeatil';
import { TextBox } from '../styles/Rank/Rank';

const NoticeDetail = () => {
  const location = useLocation();
  const { item } = location.state;
  const navigate = useNavigate();
  const defaultProfile =
    "https://cdn.pixabay.com/photo/2015/10/05/22/37/blank-profile-picture-973460_1280.png";
  
  return (
    <div>
      <NaviBar />
      <TextBox>📣 공지사항</TextBox>
      <BoardBox>
        <Title>
          <span className='font'>{item.title}</span>
        </Title>
        <div className='box'>
          <div className='item'>작성자</div>
          <div className='item' style={{ display: "flex" , flexDirection: "row", alignItems: "center"}}>
            <img src={defaultProfile} alt="프로필 이미지" style={{ width: "30px", height: "30px", borderRadius: "100px", marginRight: "10px"}}/>
            <div>운영자</div>
          </div>
        </div>
        <Content>
          <pre className='font'>{item.content}</pre>
        </Content>
        
        <div style={{ display: "flex" , flexDirection: "row", alignItems: "center", justifyContent: "space-between", borderBottom: "1px solid lightgray" }}>
          <div></div>
          <BtnBox>
            <div className='btn' onClick={() => navigate("/notice")}>목록</div>
          </BtnBox>
        </div>
      </BoardBox>
    </div>
  )
}

export default NoticeDetail