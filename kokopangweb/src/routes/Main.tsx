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
  const Notices = [
    "새로운 섬 탐험: 비밀 지역 발견! 어떤 보물이 기다리고 있을까요?",
    "밤에는 조심하세요: 야생동물의 위협을 느꼈습니다. 함께 대비해야 합니다.",
    "식량 부족 경고: 생존을 위해 식량을 모아야 합니다. 함께 사냥을 해봅시다!",
    "추락 지점 발견: 비행기 파트를 찾았습니다. 비행기를 고치는데 도움이 될 것입니다.",
    "탈출 계획 준비: 모두 모여서 탈출 계획을 세워봅시다. 함께 논의해요!",
    "신규 생존자 발견: 새로운 생존자가 합류했습니다. 환영해 주세요!",
    "비상 신호 송신: 도움을 요청하는 비상 신호를 발견했습니다. 도와줄까요?",
    "배 고치기 프로젝트 시작: 함께 배를 고치는 프로젝트를 시작합니다.",
    "낚시 대회 개최: 생존을 위한 낚시 대회를 개최합니다. 상품이 준비되어 있습니다!",
    "서바이벌 트레이닝: 생존을 위한 트레이닝 수업을 개최합니다. 참여를 환영합니다!"
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
      <TokenCheker />
      <NaviBar />
      <UpBox>
        <div style={{ width: "80%" , margin: "0 auto", display: "flex", flexDirection: "row", marginBottom: "20px"}}>
          <div style={{ fontSize: "20px"}}>📢</div>
          <NoticeBox>
            <div style={{
              transform: `translateY(-${currentSlide * 10}%)`,
              transition: "transform 1s ease",
              display: "flex",
              flexDirection: "column",
            }}>
              {Notices.map((content,idx) => (
                <div key={idx} style={{ height: "50px", flex: "0 0 auto", cursor:"pointer"}} >
                  {content}
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