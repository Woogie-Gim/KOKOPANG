import React, { useEffect, useState } from 'react'
import NaviBar from '../../routes/NaviBar'
import { BoardBox,Title,Content } from '../../styles/Community/CommunityCreate'
import { TextBox } from '../../styles/Rank/Rank'
import useUserStore from '../../stores/UserStore'
import { useShallow } from 'zustand/react/shallow'
import axios from 'axios'
import useAuthStore from '../../stores/AuthStore'
import { useNavigate,useLocation } from 'react-router-dom'

const CommunityUpdate = () => {
  const navigate = useNavigate();
  const location = useLocation();
  const { boardInfo } = location.state;

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

  const { name, profileImage,email } =
    useUserStore(
      useShallow((state) => ({
        name: state.name,
        profileImage: state.profileImage,
        email: state.email
      }))
    )

  const [title, setTitle] = useState(boardInfo.title);
  const [content, setContent] = useState(boardInfo.content);

  const changeTitle = (event: React.ChangeEvent<HTMLInputElement>) => {
    setTitle(event.target.value);
  }

  const maxLineCount = 10; // 최대 줄바꿈 횟수

  const changeContent= (event: React.ChangeEvent<HTMLTextAreaElement>) => {
    let lineCount = (event.target.value.match(/\n/g) || []).length + 1; // 줄바꿈 개수 계산

    if (lineCount > maxLineCount) {
      setContent(event.target.value.substring(0, event.target.value.lastIndexOf('\n'))); // 마지막 줄 삭제
    } else {
      setContent(event.target.value);
    }
  }

  const boardUpdate = async (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();

    if (title.length === 0) {
      alert("제목을 입력해주세요")
      return
    }

    if (content.length === 0) {
      alert("내용을 입력해주세요")
      return
    }

    axios.put(`${PATH}/board/update`,{
      id: boardInfo.boardId, 
      userEmail: email,
      title,
      content
    },{
      headers: {
        Authorization: token,
      }
    })
    .then((res) => {
      console.log(res.data)
      navigate(`/community/${boardInfo.boardId}`)
    })
    .catch((error : any) => {
      console.log(error)
    })
  }

  useEffect(() => {
    if (!isLogin) {
      alert("로그인이 필요합니다!")
      navigate("/")
    }
  },[])

  return (
    <div>
      <NaviBar />
      <TextBox>글작성</TextBox>
      <BoardBox>
        <div className='box'>
          <div className='item'>작성자</div>
          <div className='item' style={{ display: "flex" , flexDirection: "row", alignItems: "center"}}>
            <img src={profileImage === null ? defaultProfile : profileImage} alt="프로필 이미지" style={{ width: "30px", height: "30px", borderRadius: "100px", marginRight: "10px"}}/>
            <div>{name}</div>
          </div>
        </div>
        <form onSubmit={boardUpdate} style={{ marginBottom: "20px"}}>
          <div className='box'>
            <div className='item'>제목</div>
            <div className='item'>
              <Title value={title} onChange={changeTitle} maxLength={20}/>
            </div>
          </div>
          <div className='box'>
            <div className='item'>내용</div>
            <div className='item'>
              <Content value={content} onChange={changeContent} maxLength={200}/>
            </div>
          </div>
          <div className='btnBox'>
            <button className='btn' onClick={() => navigate("/community")}>취소</button>
            <button className='btn' type='submit'>글수정</button>
          </div>
        </form>
      </BoardBox>
    </div>
  )
}

export default CommunityUpdate