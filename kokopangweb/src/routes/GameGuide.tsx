import React from 'react'
import { useParams } from 'react-router-dom'
import NaviBar from './NaviBar'
import Tab from '../components/GameGuide/Tab'
import Story from '../components/GameGuide/Story'
import Guide from '../components/GameGuide/Guide'

const GameGuide = () => {
  const now = useParams();

  let content;

  switch(now.name) {
    case "story":
      content = <Story />;
      break;
    case "guide":
      content = <Guide />;
      break;
  }

  return (
    <div>
      <NaviBar />
      <Tab now={now.name} />
      {content}
    </div>
  )
}

export default GameGuide