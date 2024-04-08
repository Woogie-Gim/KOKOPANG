import React from 'react'
import { Wrapper,GuideBox } from '../../styles/GameGuide/BasicStyle'
import guide1 from "../../assets/Main.png"
import guide2 from "../../assets/signup.png"
import guide3 from "../../assets/login.png"
import guide4 from "../../assets/lobby.png"
import guide5 from "../../assets/room.png"
import guide6 from "../../assets/gamgstart.png"
import guide7 from "../../assets/inv.png"
import guide8 from "../../assets/itemget.png"
import guide9 from "../../assets/tabimage.png"
import guide10 from "../../assets/plane.png"

function Guide() {
  return (
    <Wrapper>
      <GuideBox>
        <div className='title'>1. 메인 화면</div>
        <img src={guide1} className='content' alt="이지"/>
      </GuideBox>
      <hr />
      <GuideBox>
        <div className='title'>2. 회원가입 화면</div>
        <img src={guide2} className='content' alt="이지"/>
      </GuideBox>
      <hr />
      <GuideBox>
        <div className='title'>3. 로그인 화면</div>
        <img src={guide3} className='content' alt="이지"/>
      </GuideBox>
      <hr />
      <GuideBox>
        <div className='title'>4. 로비 화면</div>
        <img src={guide4} className='content' alt="이지"/>
      </GuideBox>
      <hr />
      <GuideBox>
        <div className='title'>5. 게임 대기방</div>
        <img src={guide5} className='content' alt="이지"/>
      </GuideBox>
      <hr />
      <GuideBox>
        <div className='title'>6. 게임 시작후 첫 화면</div>
        <img src={guide6} className='content' alt="이지"/>
      </GuideBox>
      <hr />
      <GuideBox>
        <div className='title'>7. 인벤토리 (I)</div>
        <img src={guide7} className='content' alt="이지"/>
      </GuideBox>
      <hr />
      <GuideBox>
        <div className='title'>8. 아이템 획득 (E)</div>
        <img src={guide8} className='content' alt="이지"/>
      </GuideBox>
      <hr />
      <GuideBox>
        <div className='title'>9. 아이템 조합 (Tab)</div>
        <img src={guide9} className='content' alt="이지"/>
      </GuideBox>
      <hr />
      <GuideBox>
        <div className='title'>10. 비행기 수리 (F)</div>
        <img src={guide10} className='content' alt="이지"/>
      </GuideBox>
    </Wrapper>
  )
}

export default Guide