import React, {useEffect, useState} from 'react'
import { useNavigate } from 'react-router-dom'

import { InfoBox, UpBox, NoticeBox,DownBox, NoticeHeader} from '../styles/Main/MainStyle'
import backimg from "../assets/bodyimg.png"

import TokenCheker from '../utils/TokenCheker'
import MainCarousel from '../components/Main/MainCarousel'
import LoginForm from '../components/Main/LoginForm'
import NaviBar from './NaviBar'

const Main = () => {
  const navigate = useNavigate();
  const [currentSlide, setCurrentSlide] = useState(0);
  const noticeList = [
    {
      "id": "공지",
      "title": "[공지] 서버 점검 안내 - 2024/04/04",
      "content": "내일 새벽 2시부터 서버 점검이 진행될 예정입니다. 이에 따라 일시적으로 접속이 불가능할 수 있습니다."
    },
    {
      "id": "공지",
      "title": "[공지] 패치 노트 - 1.1 업데이트 - 2024/03/29",
      "content": "이번 업데이트에서는 새로운 지형과 생물이 추가되었습니다. 또한 UI 개선 및 버그 수정이 이루어졌습니다."
    },
    {
      "id": "공지",
      "title": "[공지] 클라이언트 버그 수정 - 2024/03/16",
      "content": "최신 클라이언트 버전에서 발견된 버그들을 수정했습니다. 게임 플레이의 안정성이 향상되었습니다."
    },
  ]

  useEffect(() => {
    const interval = setInterval(() => {
      goToNextSlide();
    }, 3000);

    return () => clearInterval(interval);
  }, [currentSlide]);

  const goToNextSlide = () => {
    setCurrentSlide((prevSlide) => (prevSlide + 1) % noticeList.length);
  };
  
  return (
    <div>
      <TokenCheker />
      <NaviBar />
      <UpBox>
        <div style={{ width: "70%" , margin: "0 auto", display: "flex", flexDirection: "row", marginBottom: "20px"}}>
          <div style={{ fontSize: "20px"}}>📢</div>
          <NoticeBox>
            <div style={{
              transform: `translateY(-${currentSlide * 32}%)`,
              transition: "transform 1s ease",
              display: "flex",
              flexDirection: "column",
              textShadow: "4px 4px 4px rgba(0, 0, 0, 0.3)"
            }}>
              {noticeList.map((item,idx) => (
                <div key={idx} style={{ height: "50px", flex: "0 0 auto"}} >
                  <span style={{ cursor:"pointer" }} onClick={() => navigate(`/notice/${item.id}`,{ state: { item } })}>{item.title}</span>
                </div>
              ))}
            </div>
          </NoticeBox>
        </div>
        <DownBox>
          <InfoBox>
            <MainCarousel />
          </InfoBox>
          <LoginForm />
        </DownBox>
      </UpBox>
    </div>
  )
}

export default Main