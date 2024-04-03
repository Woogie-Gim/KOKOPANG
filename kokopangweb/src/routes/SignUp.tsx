import React, { useEffect, useRef, useState } from 'react'
import axios from 'axios';
import useAuthStore from '../stores/AuthStore';
import { useShallow } from 'zustand/react/shallow';

import {SignUpBox,SignUpButton,Email,Name,Password,Header,ContentBox} from "../styles/Main/SignUp"
import logo from "../assets/koko_logo.png"
import { useNavigate } from 'react-router-dom';

export default function SignUp() {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [name, setName] = useState("");
  const navigate = useNavigate();

  const { PATH } =
    useAuthStore(
      useShallow((state) => ({
        PATH: state.PATH,
      }))
    );

  const changeEmail = (event: React.ChangeEvent<HTMLInputElement>) => {
    setEmail(event.target.value);
  }

  const changePassword = (event: React.ChangeEvent<HTMLInputElement>) => {
    setPassword(event.target.value);
  }

  const changeName = (event: React.ChangeEvent<HTMLInputElement>) => {
    setName(event.target.value);
  }
  const signup = (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault()

    if (name.length < 0 || name.length === 0 || name.length > 4) {
      alert("닉네임을 재 설정 해주세요")
      return
    }

    if (email === "") {
      alert("아이디를 입력해주세요")
      return
    }

    if (password === "") {
      alert("비밀번호를 입력해주세요")
    }

    axios.post(`${PATH}/user/signup`,{
      name,
      email,
      password
    })
    .then((res) => {
      console.log(res.data)
      navigate("/")
    })
    .catch((error) => {
      console.log("회원가입 실패")
      alert("아이디 또는 비밀번호를 확인해주세요!")
    })
  }

  return (
    <>
      <div style={{ width: "200px", height: "200px", display: "flex", margin: "0 auto", cursor:"pointer"}} onClick={() => navigate("/")}>
        <img src={logo} alt="로고"  />
      </div>
      <SignUpBox onSubmit={signup}>
        <Header>KOKOPang 회원가입</Header>
        <Name value={name} onChange={changeName} placeholder='닉네임' maxLength={4}/>
        <ContentBox>4글자 이하로 설정해주세요</ContentBox>
        <Email value={email} onChange={changeEmail} placeholder='이메일' type='email'/>
        <ContentBox>이메일 형식으로 설정해주세요</ContentBox>
        <Password value={password} onChange={changePassword} type='password' placeholder='비밀번호'/>
        <ContentBox>특수문자(~ ! @ # $) 포함 8 ~ 16 글자로 설정해주세요</ContentBox>
        <SignUpButton type='submit' value="회원가입"></SignUpButton>
      </SignUpBox>
    </>
  );
}