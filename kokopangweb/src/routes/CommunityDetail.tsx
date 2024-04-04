import axios from 'axios';
import React, { useEffect, useState } from 'react'
import { useLocation, useNavigate, useParams } from 'react-router-dom'
import useAuthStore from '../stores/AuthStore';
import { useShallow } from 'zustand/react/shallow';
import NaviBar from './NaviBar';
import { BoardBox,Title, Content, BtnBox, Comment, CreateBtn, CommentBox,CommentSubBtn } from '../styles/Community/CommunityDeatil';
import useUserStore from '../stores/UserStore';

interface Board {
  boardId: number;
  profileImg: string;
  name: string;
  title: string;
  content: string;
}

const CommunityDetail = () => {
  const param = useParams();
  const navigate = useNavigate();
  const [boardInfo, setBoardInfo] = useState<Board>();
  const [commentList, setCommentList] = useState([]);
  const [comment, setComment] = useState("");
  const [refe,setRefe] = useState(true);
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

  const { name, email } = 
    useUserStore(
      useShallow((state) => ({
        name: state.name,
        email: state.email
      }))
    )

  const deleteBoard = () => {
    axios.delete(`${PATH}/board/delete?boardId=${boardInfo?.boardId}`,{
      headers: {
        Authorization: token,
      },
    })
    .then((res) => {
      console.log(res.data)
      setRefe(!refe)
      navigate("/community")
    })
    .catch((error) => console.log(error))
  }
  
  const updateBoard = () => {
    navigate(`/community/${boardInfo?.boardId}/update`,{ state: { boardInfo } })
  }

  const createComment = (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();

    if (comment === "") {
      alert("내용을 입력해주세요")
      return
    }

    axios.post(`${PATH}/comment/create`,{
      userEmail: email,
      content: comment,
      boardId: boardInfo?.boardId
    },{
      headers: {
        Authorization: token,
      },
    })
    .then((res) => {
      console.log(res.data)
      setComment("")
      setRefe(!refe)
    })
    .catch((error: any) => {
      console.log(error)
      alert("로그인이 필요합니다!")
      navigate("/")
    })
  }

  const maxLineCount = 4; // 최대 줄바꿈 횟수

  const changeComment= (event: React.ChangeEvent<HTMLTextAreaElement>) => {
    let lineCount = (event.target.value.match(/\n/g) || []).length + 1; // 줄바꿈 개수 계산

    if (lineCount > maxLineCount) {
      setComment(event.target.value.substring(0, event.target.value.lastIndexOf('\n'))); // 마지막 줄 삭제
    } else {
      setComment(event.target.value);
    }
  }

  const deleteComment = (commentId: Number) => {
    axios.delete(`${PATH}/comment/delete?commentId=${commentId}`,{
      headers: {
        Authorization: token,
      },
    })
    .then((res) => console.log(res))
    .catch((error) => console.log(error))
    setRefe(!refe)
  }

  useEffect(() => {
     axios.get(`${PATH}/board/read?boardId=${param["id"]}`,{
      headers: {
        Authorization: token,
      },
    })
    .then((res) => {
      console.log(res.data)
      setBoardInfo(res.data)

      axios.get(`${PATH}/comment/read?boardId=${res.data.boardId}`,{
        headers: {
          Authorization: token,
        },
      })
      .then((res) => {
        console.log(res.data)
        setCommentList(res.data)
      })
      .catch((error) => console.log(error))
    })
    .catch((error) => {
      console.log(error)
    })
  }, [refe])

  return (
    <div>
      <NaviBar />
      <BoardBox>
        <Title>
          <span className='font'>{boardInfo?.title}</span>
        </Title>
        <div className='box'>
          <div className='item'>작성자</div>
          <div className='item' style={{ display: "flex" , flexDirection: "row", alignItems: "center"}}>
            <img src={boardInfo?.profileImg === null ? defaultProfile : boardInfo?.profileImg} alt="프로필 이미지" style={{ width: "30px", height: "30px", borderRadius: "100px", marginRight: "10px"}}/>
            <div>{boardInfo?.name}</div>
          </div>
        </div>
        <Content>
          <pre className='font'>{boardInfo?.content}</pre>
        </Content>
        
        <div style={{ display: "flex" , flexDirection: "row", alignItems: "center", justifyContent: "space-between", borderBottom: "1px solid lightgray" }}>
          <div style={{ marginLeft: "30px", color: "gray", fontSize: "30px"}}>댓글</div>
          {boardInfo?.name === name ? <BtnBox>
            <div className='btn' onClick={deleteBoard}>삭제</div>
            <div className='btn' onClick={updateBoard}>수정</div>
            <div className='btn' onClick={() => navigate("/community")}>목록</div>
          </BtnBox> : <BtnBox>
            <div className='btn' onClick={() => navigate("/community")}>목록</div>
          </BtnBox>}
        </div>
        <form onSubmit={createComment} style={{ display: "flex", flexDirection: "column", marginBottom: "20px" }}>
          <Comment placeholder='댓글을 작성해주세요' value={comment} onChange={changeComment} maxLength={100}/>
          <CreateBtn>
            <button type="submit" className='btn'>등록</button>
          </CreateBtn>
        </form>
        <CommentBox>
          {commentList.map((ct,idx) => (
            <div className='box1' key={idx}>
              <div className='item1'>
                <pre>{ct["content"]}</pre>
                {ct["username"] === name ? 
                <div style={{ display: "flex", flexDirection: "row"}}>
                  <CommentSubBtn onClick={() => deleteComment(ct["commentId"])}>삭제</CommentSubBtn>
                </div> : null}                
              </div>
              <div className='item1' style={{ display: "flex" , flexDirection: "row", alignItems: "center"}}>
                <img src={ct["profileImg"] === null ? defaultProfile : ct["profileImg"]} alt="프로필 이미지" style={{ width: "30px", height: "30px", borderRadius: "100px", marginRight: "10px"}}/>
                <div>{ct["username"]}</div>
              </div>
            </div>
          ))}
        </CommentBox>
      </BoardBox>
    </div>
  )
}

export default CommunityDetail