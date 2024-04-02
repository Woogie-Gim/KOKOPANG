import React, {useState, useEffect} from 'react'
import { useNavigate } from 'react-router-dom';
import * as s from '../../styles/GameGuide/Tab'

interface Props {
  now: string | undefined;
}

function Tab({now}: Props) {
  const navigate = useNavigate();

  const menus = ["스토리", "게임 진행 방법"];
  const [status, setStatus] = useState<string | undefined>();

  const onClick = (menu: String) => {
    switch(menu) {
      case "스토리":
        navigate("/gameGuide/story");
        break;
      case "게임 진행 방법":
        navigate("/gameGuide/guide");
        break;
    }
  }

  useEffect(() => {
    switch(now) {
      case "story":
        setStatus("스토리");
        break;
      case "guide":
        setStatus("게임 진행 방법");
        break;
      default:
        setStatus(undefined);
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
