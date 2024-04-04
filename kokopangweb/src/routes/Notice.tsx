import React, { useState } from 'react'
import NaviBar from './NaviBar'
import { Notice,NoticeBox } from '../styles/Notice/Notice'
import { TextBox } from '../styles/Rank/Rank'
import { useNavigate } from 'react-router-dom'
import { PageNation } from '../styles/Rank/Rank'

const Story = () => {
  const noticeList = [
    {
      "id": "공지",
      "title": "[공지] 서버 점검 안내 - 2024/04/04",
      "content": "내일 새벽 2시부터 서버 점검이 진행될 예정입니다. 이에 따라 일시적으로 접속이 불가능할 수 있습니다."
    },
    {
      "id": "공지",
      "title": "[공지] 패치 노트 - 1.1 업데이트 - 2024/03/29",
      "content": "이번 업데이트에서는 새로운 지형과 생물이 추가되었습니다. 또한 UI 개선 및 버그 수정이 이루어졌습니다."
    },
    {
      "id": "공지",
      "title": "[공지] 클라이언트 버그 수정 - 2024/03/16",
      "content": "최신 클라이언트 버전에서 발견된 버그들을 수정했습니다. 게임 플레이의 안정성이 향상되었습니다."
    },
  ]

  const data = [
    
    {
      "id": 1,
      "title": "신규 아이템 추가 안내",
      "content": "새로운 아이템 '화염병'이 추가되었습니다. 화염병은 적에게 화상 효과를 입히는 강력한 무기입니다."
    },
    {
      "id": 2,
      "title": "이벤트 공지 - 생존자 모집",
      "content": "이번 주말에 생존자를 모집하는 이벤트가 진행됩니다. 참여하고 보상을 획득하세요!"
    },
    
    {
      "id": 3,
      "title": "플레이어 피드백에 대한 고려",
      "content": "최근에 제출된 피드백을 기반으로 게임 밸런스 조정을 진행하고 있습니다. 유저들의 의견을 소중히 생각합니다."
    },
    {
      "id": 4,
      "title": "신규 지형 '폐허 도시' 추가",
      "content": "새로운 지형 '폐허 도시'가 추가되었습니다. 도시 내에서의 전투와 탐험을 즐기세요!"
    },
    {
      "id": 5,
      "title": "이벤트 공지 - 약탈자 습격",
      "content": "오늘 저녁 약탈자 습격 이벤트가 시작됩니다. 귀하의 기초를 방어하고 보상을 획득하세요!"
    },
    
    {
      "id": 6,
      "title": "신규 아이템 '도끼' 추가",
      "content": "새로운 생존 도구인 도끼가 추가되었습니다. 나무를 베거나 적을 공격하는 데 사용할 수 있습니다."
    },
    {
      "id": 7,
      "title": "매주 수요일 업데이트 안내",
      "content": "매주 수요일에는 정기 업데이트가 진행됩니다. 게임의 새로운 내용을 기대해주세요!"
    },
    {
      "id": 8,
      "title": "이벤트 공지 - 자원 채집 대회",
      "content": "이번 주말에 자원 채집 대회가 열립니다. 가장 많은 자원을 채취하여 상금을 획득하세요!"
    },
    {
      "id": 9,
      "title": "새로운 생물 등장",
      "content": "최근에 발견된 새로운 생물이 추가되었습니다. 적에 대비하여 준비하세요!"
    },
    {
      "id": 10,
      "title": "서버 안정성 개선",
      "content": "서버의 안정성을 개선했습니다. 접속 문제 및 랙 현상이 감소될 것으로 기대됩니다."
    },
    {
      "id": 11,
      "title": "신규 아이템 '의료킷' 추가",
      "content": "생존을 돕는 의료킷이 추가되었습니다. 부상을 치료하고 체력을 회복할 수 있습니다."
    },
    {
      "id": 12,
      "title": "이벤트 공지 - 쾌적한 생활 기획",
      "content": "내일부터 쾌적한 생활을 위한 기획 이벤트가 시작됩니다. 건축과 생존에 참여하세요!"
    },
    {
      "id": 13,
      "title": "게임 이용약관 변경 안내",
      "content": "게임 이용약관이 변경되었습니다. 변경 사항을 확인하고 동의해주세요."
    },
    {
      "id": 14,
      "title": "신규 지역 탐험 - 황무지",
      "content": "신규 지역 '황무지'가 추가되었습니다. 극한의 환경에서의 생존을 경험하세요!"
    },
    {
      "id": 15,
      "title": "이벤트 공지 - 약탈자 습격",
      "content": "오늘 밤 약탈자 습격 이벤트가 시작됩니다. 자원을 지켜내고 보상을 획득하세요!"
    },
    {
      "id": 16,
      "title": "클라이언트 성능 개선",
      "content": "최신 클라이언트 업데이트에서 성능이 향상되었습니다. 게임 실행 속도가 빨라집니다."
    },
    {
      "id": 17,
      "title": "신규 아이템 '총' 추가",
      "content": "신규 무기 '총'이 추가되었습니다. 적을 공격하거나 사냥에 사용할 수 있습니다."
    },
    {
      "id": 18,
      "title": "이벤트 공지 - 생존 도전",
      "content": "이번 주에는 생존 도전 이벤트가 열립니다. 다양한 과제를 완수하고 보상을 획득하세요!"
    },
    {
      "id": 19,
      "title": "새로운 보급품 발견",
      "content": "생존에 도움이 되는 새로운 보급품이 발견되었습니다. 자주 확인하여 사용하세요!"
    },
    {
      "id": 20,
      "title": "서버 이전 작업 안내",
      "content": "내일 오전 10시부터 서버 이전 작업이 진행될 예정입니다. 이에 따라 일시적으로 접속이 불가능할 수 있습니다."
    }
  ]
  const defaultProfile =
    "https://cdn.pixabay.com/photo/2015/10/05/22/37/blank-profile-picture-973460_1280.png";

  const navigate = useNavigate();

  const ITEMS_PER_PAGE = 10;
  const PAGINATION_NUMBERS = 5;

  const [currentPage, setCurrentPage] = useState(1);

  const totalPageCount = Math.ceil(data.length / ITEMS_PER_PAGE);

  const handlePageChange = (page: number) => {
    setCurrentPage(page);
  };

  const renderPaginationNumbers = () => {
    const paginationNumbers = [];
    const start =
      Math.floor((currentPage - 1) / PAGINATION_NUMBERS) * PAGINATION_NUMBERS +
      1;

    for (
      let i = start;
      i < start + PAGINATION_NUMBERS && i <= totalPageCount;
      i++
    ) {
      paginationNumbers.push(
        <button
          key={i}
          onClick={() => handlePageChange(i)}
          className={currentPage === i ? "current" : ""}
        >
          {i}
        </button>
      );
    }

    return paginationNumbers;
  };

  const startIdx = (currentPage - 1) * ITEMS_PER_PAGE;
  const endIdx = startIdx + ITEMS_PER_PAGE;
  const currentItems = data.slice(startIdx, endIdx);

  return (
    <div>
      <NaviBar />
      <TextBox>📣 공지사항</TextBox>
      <NoticeBox>
        <div className='container2'>
          <div className='item2'>#</div>
          <div className='item2'>제목</div>
          <div className='item2'>작성자</div>
        </div>
        {noticeList.map((item,idx) => (
          <Notice key={idx} style={{ color: "#3c90e2"}}>
            <div className='item'>{item.id}</div>
            <div className='item' onClick={() => navigate(`/notice/${item.id}`, { state: { item } })}>{item.title}</div>
            <div className='item'>
              <img src={defaultProfile} alt='프로필이미지'
                style={{ width: "45px", height: "45px", borderRadius: "100px", border: "1px solid lightgray" }}
              />
            </div>
            <div className='item' style={{ color : "black"}}>운영자</div>
        </Notice>
        ))}
        {currentItems.map((item,idx) => (
          <Notice key={idx}>
            <div className='item'>{ data.length - item.id + 1}</div>
            <div className='item' onClick={() => navigate(`/notice/${item.id}`, { state: { item } })}>{item.title}</div>
            <div className='item'>
              <img src={defaultProfile} alt='프로필이미지'
                style={{ width: "45px", height: "45px", borderRadius: "100px", border: "1px solid lightgray" }}
              />
            </div>
            <div className='item' style={{ color : "black"}}>운영자</div>
          </Notice>
        ))}
      {totalPageCount > 0 && (
          <PageNation>
            <div className="nav_buttons">
              <button onClick={() => handlePageChange(1)}>&lt;&lt;</button>
              <button
                onClick={() => handlePageChange(currentPage - 1)}
                disabled={currentPage === 1}
              >
                &lt;
              </button>
              {renderPaginationNumbers()}
              <button
                onClick={() => handlePageChange(currentPage + 1)}
                disabled={currentPage === totalPageCount}
              >
                &gt;
              </button>
              <button onClick={() => handlePageChange(totalPageCount)}>
                &gt;&gt;
              </button>
            </div>
          </PageNation>
        )}
      </NoticeBox>
    </div>
  )
}

export default Story