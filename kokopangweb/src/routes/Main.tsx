import React, {useEffect, useState} from 'react'
import { useNavigate } from 'react-router-dom'

import { InfoBox, UpBox, NoticeBox, NoticeHeader } from '../styles/Main/MainStyle'

import MainCarousel from '../components/Main/MainCarousel'
import LoginForm from '../components/Main/LoginForm'
import NaviBar from './NaviBar'

const Main = () => {
  const navigate = useNavigate();
  const [currentSlide, setCurrentSlide] = useState(0);
  const Notices = [
    "강승원 강승원 강승원 강승원 강승원 강승원 강승원 강승원 강승원 강승원 강승원 강승원 강승원 강승원 강승원",
    "김선욱 김선욱 김선욱 김선욱 김선욱 김선욱 김선욱 김선욱 김선욱 김선욱 김선욱 김선욱 김선욱 김선욱 김선욱",
    "김영일 김영일 김영일 김영일 김영일 김영일 김영일 김영일 김영일 김영일 김영일 김영일 김영일 김영일 김영일",
    "이주현 이주현 이주현 이주현 이주현 이주현 이주현 이주현 이주현 이주현 이주현 이주현 이주현 이주현 이주현",
    "이항우 이항우 이항우 이항우 이항우 이항우 이항우 이항우 이항우 이항우 이항우 이항우 이항우 이항우 이항우",
    "장동재 장동재 장동재 장동재 장동재 장동재 장동재 장동재 장동재 장동재 장동재 장동재 장동재 장동재 장동재"
  ]

  useEffect(() => {
    const interval = setInterval(() => {
      goToNextSlide();
    }, 3000);

    return () => clearInterval(interval);
  }, [currentSlide]);

  const goToNextSlide = () => {
    setCurrentSlide((prevSlide) => (prevSlide + 1) % Notices.length);
  };

  
  return (
    <div>
      <NaviBar />
      <UpBox>
        <InfoBox>
          <MainCarousel />
        </InfoBox>
        <LoginForm />
      </UpBox>
      <NoticeHeader><span style={{cursor: "pointer"}}>공지사항</span></NoticeHeader>
      <NoticeBox>
        <div style={{
          transform: `translateY(-${currentSlide * 16}%)`,
          transition: "transform 1s ease",
          display: "flex",
          flexDirection: "column",
          justifyContent: "center"
        }}>
          {Notices.map((content,idx) => (
            <div key={idx} style={{ height: "30px", flex: "0 0 auto", cursor:"pointer"}} >
              📢 {content}
            </div>
          ))}
        </div>
      </NoticeBox>
    </div>
  )
}

export default Main