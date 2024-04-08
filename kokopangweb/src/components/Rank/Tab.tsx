import {useState, useEffect} from 'react'
import { useNavigate } from 'react-router-dom';
import * as s from '../../styles/GameGuide/Tab'

interface Props {
  now: string | undefined;
}

function Tab({now}: Props) {
  const navigate = useNavigate();

  const menus = ["레이팅", "타임어택"];
  const [status, setStatus] = useState<string | undefined>();

  const onClick = (menu: String) => {
    switch(menu) {
      case "레이팅":
        navigate("/rank/rating");
        break;
      case "타임어택":
        navigate("/rank/timeAttack");
        break;
    }
  }

  useEffect(() => {
    switch(now) {
      case "rating":
        setStatus("레이팅");
        break;
      case "timeAttack":
        setStatus("타임어택");
        break;
    }
  },);

  return (
    <s.Nav>
      {menus.map((result, index) => {
        const className = result === status ? 'active' : '';
        return <s.Item className={className} key={index} onClick={() => onClick(result)}>{result}</s.Item>;
      })}
    </s.Nav>
  )
}

export default Tab