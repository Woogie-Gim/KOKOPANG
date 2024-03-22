import React, {useState} from 'react'
import { LoginBox, UserLogin, LoginButton } from '../../styles/Main/LoginForm'
import axios from 'axios';

const LoginForm = () => {
  const [username,setUsername] = useState("");
  const [password,setPassword] = useState("");
  const [isLogin,setIsLogin] = useState(false);

  const PATH = "http://localhost:8080";

  const changeUsername = (event : React.ChangeEvent<HTMLInputElement>) => {
    setUsername(event.target.value);
  }

  const changePassword = (event : React.ChangeEvent<HTMLInputElement>) => {
    setPassword(event.target.value);
  }

  const Login = (event : React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();

    let data = new FormData();
    data.append("username", username);
    data.append("password", password);

    axios.post(`${PATH}/login`,data)
    .then((res) => {
      console.log(res.data)
      setIsLogin(true)
    })
    .catch((error) => console.log(error.response))
  }
   
  return (
    <>
    {isLogin ? <LoginBox></LoginBox> : <LoginBox>
        <div style={{ fontSize: "25px", width:"80%", margin: "20px auto", fontWeight: "700" }}>코코팡 로그인</div>
        <form style={{margin: "0 auto", width:"80%"}} onSubmit={Login}>
          <UserLogin placeholder='id' value={username} onChange={changeUsername}/>
          <UserLogin placeholder='password' type='password' value={password} onChange={changePassword}/>
          <LoginButton type='submit' value='로그인' />
        </form>
      </LoginBox>}
      
    </>
  )
}

export default LoginForm;