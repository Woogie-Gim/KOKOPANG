import styled from "styled-components";

const LoginBox = styled.div`
  width: 100%;
  height: 600px;
  display: flex;
  flex-direction: column;
  margin-left: 10px;
  border: 0;
  border-radius: 10px;
  box-shadow: 5px 5px 15px 1px lightgray;
`;

const UserLogin = styled.input`
  width: 95%;
  height: 40px;
  padding: 5px;
  margin: 5px auto;
  border: 1px solid lightgray;
  border-radius: 10px;

  &:focus {
    outline-color: pink;
  }
`;

const LoginButton = styled.input`
  transition: 0.2s;
  width: 100%;
  height: 50px;
  padding: 5px;
  margin: 10px auto;
  border: 1px solid lightgray;
  border-radius: 10px;
  background-color: pink;
  &:hover {
    cursor: pointer;
    transition: 0.2s;
    background-color: lightcoral;
  }
`;

export { LoginBox, UserLogin, LoginButton };
