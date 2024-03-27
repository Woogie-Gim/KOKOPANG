import styled from "styled-components";

const LoginBox = styled.div`
  width: 100%;
  height: 600px;
  display: flex;
  flex-direction: column;
  margin-left: 10px;
  border: 0;
  border-radius: 10px;
  background-color: white;
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

const UserBox = styled.div`
  width: 90%;
  margin: 20px auto; 
  display: flex;
  flex-direction: row;

  .img_box {
    width: 150px;
    display: flex;
    flex-direction: column;
    margin-right: 30px;

      img {
        width: 150px;
        height: 150px;
        border-radius: 10px;
        border: 1px solid lightgray;  
      }

    .btn {
      text-align: center;
      font-size: 13px;
      padding: 5px;
      color: gray;
      cursor: pointer;
      margin-top: 10px;
    }
  }
`

const ProfileBox = styled.div`
  width: 60%;
  display: flex;
  flex-direction: column;

  .name_box {
    width: 100%;
    height: auto;
    font-size: 25px;
  }

`

const DownLoadBox = styled.div`
  width: 85%;
  margin: 0 auto;
  border: 0;
  padding: 10px;
  border-radius: 10px;
  background-color: tomato;
  color: white;
  font-size: 25px;
  text-align: center;
  cursor: pointer;
`

const FriendsBox = styled.div`
  width: 85%;
  margin: 20px auto;
  border: 1px solid lightgray;
  border-radius: 5px;
  overflow: scroll;
  -ms-overflow-style: none;

  &::-webkit-scrollbar{
    display:none;
  }

  .container {
    position: sticky;
    padding: 10px;
    top: 0;
    display: flex;
    flex-direction: row;
    background-color: lightgray;
    border-bottom: 1px solid gray;
  }

  .item {
    flex: 1;
    text-align: center;
    font-size: 20px;
  }

  .item:nth-child(1) {
    flex: 15%;
  }

  .item:nth-child(2) {
    flex: 50%;
  }

  .item:nth-child(3) {
    flex: 35%;
  }

  .item1 {
    flex: 1;
    text-align: center;
    font-size: 20px;
  }

  .item1:nth-child(1) {
    flex: 15%;
  }

  .item1:nth-child(2) {
    flex: 15%;
  }

  .item1:nth-child(3) {
    flex: 35%;
  }

  .item1:nth-child(4) {
    flex: 35%;
  }
`

export { LoginBox, UserLogin, LoginButton, UserBox, ProfileBox, DownLoadBox, FriendsBox };
