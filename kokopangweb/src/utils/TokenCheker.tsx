import React, { useEffect } from "react";
import axios from "axios";
import useAuthStore from "../stores/AuthStore";
import useUserStore from "../stores/UserStore";
import { useShallow } from "zustand/react/shallow";
import { useLocation } from "react-router-dom";

function TokenCheker() {
  const location = useLocation();
  const { email } = useUserStore(
    useShallow((state) => ({
      email: state.email,
    }))
  );

  const {
    PATH,
    token,
    refToken,
    tokenExpireTime,
    logout,
    setToken,
    setTokenExpireTime,
  } = useAuthStore(
    useShallow((state) => ({
      PATH: state.PATH,
      token: state.token,
      refToken: state.refToken,
      tokenExpireTime: state.tokenExpireTime,
      logout: state.logout,
      setToken: state.setToken,
      setTokenExpireTime: state.setTokenExpireTime,
    }))
  );

  const refresh = async () => {
    console.log("토큰을 재발행합니다.");
    try {
      const res = await axios.post(
        `${PATH}/token/reissue`,
        {},
        {
          params: {
            email: email,
          },
          headers: {
            refreshToken: refToken,
          },
        }
      );

      const now = new Date().getTime();
      setToken(res.headers.authorization, res.headers.refreshtoken);
      setTokenExpireTime(now);
      console.log("토큰 변경 완료");
    } catch (error) {
      console.error("토큰 재발행 중 에러 발생:", error);
      logout();
    }                                                                                                                                                                                                                                                                                                                                                                                                                         
  };


  const updateToken = () => {
    if (
      email == null ||
      token == null ||
      refToken == null ||
      tokenExpireTime == null
    )
      return;
    const now = new Date().getTime();
    console.log(tokenExpireTime, now);
    console.log(
      "남은 시간: ",
      Math.round((tokenExpireTime - now) / 1000 / 60),
      "분"
    );

    console.log(
      tokenExpireTime - now < 1000 * 60 * 5 ? "토큰 재발행 필요" : "토큰 유효"
    );
    if (tokenExpireTime - now < 1000 * 60 * 5) {
      refresh();
    }
  };

  useEffect(() => {
    updateToken();
  }, []);

  return <div></div>;
}

export default TokenCheker;