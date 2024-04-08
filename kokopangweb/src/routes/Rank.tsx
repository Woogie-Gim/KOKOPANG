import { useParams } from 'react-router-dom';
import NaviBar from './NaviBar'

import { TextBox } from '../styles/Rank/Rank'

import Tab from '../components/Rank/Tab';
import Rating from '../components/Rank/Rating';
import TimeAttack from '../components/Rank/TimeAttack';

const Rank = () => {
  const now = useParams();
  let name;
  let content;

  switch(now.name) {
    case "rating":
      content = <Rating />;
      name = "레이팅";
      break;
    case "timeAttack":
      content = <TimeAttack />;
      name = "타임어택";
      break;
  }
  
  return (
    <div>
      <NaviBar />
      <Tab now={now.name} />
      <TextBox>🏆 KOKOPang {name}</TextBox>
      {content}
    </div>
  )
}

export default Rank