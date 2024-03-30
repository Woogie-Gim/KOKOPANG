import React from 'react'
import NaviBar from './NaviBar'
import { Notice,NoticeBox } from '../styles/Notice/Notice'
import { TextBox } from '../styles/Rank/Rank'

const Story = () => {
  return (
    <div>
      <NaviBar />
      <TextBox>📣 공지사항</TextBox>
      <NoticeBox>
        <Notice>

        </Notice>
      </NoticeBox>
    </div>
  )
}

export default Story