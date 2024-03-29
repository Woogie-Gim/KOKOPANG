import axios from 'axios'
import React, { useEffect, useState } from 'react'
import useAuthStore from '../stores/AuthStore';
import { useShallow } from 'zustand/react/shallow';
import useUserStore from '../stores/UserStore';

import NaviBar from './NaviBar';

const Comunity = () => {
  const [boardList,setBoardList] = useState([]);

  const { isLogin, PATH, token, } =
    useAuthStore(
      useShallow((state) => ({
        isLogin: state.isLogIn,
        PATH: state.PATH,
        token: state.token,
      }))
    );

  const { name, profileImage } =
    useUserStore(
      useShallow((state) => ({
        name: state.name,
        profileImage: state.profileImage,
      }))
    )

  useEffect(() => {
    axios.get(`${PATH}/board/all`,{
      headers: {
        Authorization: token,
      },
    })
    .then((res: any) => {
      console.log(res.data)
      setBoardList(res.data)
    })
    .catch((error:any) => {
      console.log(error)
    })
  })

  return (
    <div>
      <NaviBar />
      
    </div>
  )
}

export default Comunity