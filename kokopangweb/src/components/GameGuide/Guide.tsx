import React from 'react'
import { Wrapper,GuideBox } from '../../styles/GameGuide/BasicStyle'
import guide1 from "../../assets/guide1.png"
import guide2 from "../../assets/guide2.png"
import guide3 from "../../assets/guide3.png"
import guide4 from "../../assets/guide4.png"
import guide5 from "../../assets/guide5.png"

function Guide() {
  return (
    <Wrapper>
      <GuideBox>
        <div className='title'>1. 게임 시작 화면</div>
        <img src={guide3} className='content' alt="이지"/>
      </GuideBox>
      <hr />
      <GuideBox>
        <div className='title'>2. 게임 시작 후 첫 화면</div>
        <img src={guide2} className='content' alt="이지"/>
      </GuideBox>
      <hr />
      <GuideBox>
        <div className='title'>3. 비행기 수리 방법</div>
        <img src={guide4} className='content' alt="이지"/>
      </GuideBox>
      <hr />
      <GuideBox>
        <div className='title'>4. 아이템 근접 시</div>
        <img src={guide1} className='content' alt="이지"/>
      </GuideBox>
      <hr />
      <GuideBox>
        <div className='title'>5. 인벤토리창 (Tab)</div>
        <img src={guide5} className='content' alt="이지"/>
      </GuideBox>
    </Wrapper>
  )
}

export default Guide