import React, { useState } from "react";
import { Wrapper } from "../../styles/GameGuide/BasicStyle";
import * as s from "../../styles/GameGuide/Items";
import Log from "./Log";
import Ironstone from "./Ironstone";
import CopperOre from "./CopperOre";

function Items() {
  const menus = ["통나무", "철광석", "구리광석"];
  const [status, setStatus] = useState("");
  const [content, setContent] = useState<React.ReactNode>(null); // content를 상태로 관리

  const onClick = (menu: string) => {
    setStatus(menu);
    switch (menu) {
      case "통나무":
        setContent(<Log />);
        break;
      case "철광석":
        setContent(<Ironstone />);
        break;
      case "구리광석":
        setContent(<CopperOre />);
        break;
      default:
        setContent(null);
    }
  };

  return (
    <Wrapper>
      <s.Container>
        <s.Tab>
          {menus.map((value, index) => {
            const className = value === status ? "active" : "";
            return (
              <s.Menu
                className={className}
                onClick={() => onClick(value)}
                key={index}
              >
                {value}
              </s.Menu>
            );
          })}
        </s.Tab>
        <s.Description>{content}</s.Description>
      </s.Container>
    </Wrapper>
  );
}

export default Items;
