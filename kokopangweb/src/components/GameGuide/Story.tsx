import React, { useEffect, useState } from "react";
import { Wrapper, Wrapper1 } from "../../styles/GameGuide/BasicStyle";
import { Fade } from "react-awesome-reveal";

function Story() {
  const [isVideo, setIsVideo] = useState(true);

  useEffect(() => {
    const timer = setTimeout(() => {
      setIsVideo(false);
    }, 10000);
    return () => clearTimeout(timer);
  }, []);

  return (
    <>

      {isVideo ? <Wrapper1>
        <div style={{ width: "95%", margin: "0 auto", opacity: "1" }}>
          <video src="/video/storyVideo.mp4" autoPlay muted playsInline style={{ width: "100%", height: "auto" }} />
        </div>
      </Wrapper1> : <Wrapper>
        <Fade cascade damping={1.5}>
          <p>
            경비행기를 타고 여행을 하던 주인공과 그의 친구들 하지만 난기류를 만나 경비행기는 이름 모를 섬에 추락하고 만다. 불행히도 조종사는 목숨을 잃었지만 친구들은 살아남았다.
          </p>
          <p>
            하지만 희망이 한 가지 남아 있었는데 공항과 소통할 수 있는 통신 장비었다.
          </p>
          <p>
            하지만 배터리와 회로등이 망가져 있었고 고칠 수 있는 부품이 없었다.
          </p>
          <p>
            섬에서 살아남아 재료를 구해 통신 장비를 고치고 구조 비행기를 불러 살아남아야 한다.
          </p>
          <p>
            야생 속에 어떤 것이 있을 지 모르지만 자원이 비옥한 섬인 것 같다.
          </p>
          <p>
            과연 이 섬에서 작은 사회를 이뤄 나가며 생존 해나갈 수 있을 것인가?
          </p>
        </Fade>
      </Wrapper>
      }

    </>
  );
}

export default Story;
