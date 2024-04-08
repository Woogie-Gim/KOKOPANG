import styled from "styled-components";

const SignUpBox = styled.form`
  width: 30%;
  padding: 30px;
  background-color: white;
  border-radius: 10px;
  margin: 15px auto;
  display: flex;
  flex-direction: column;
  font-size: 20px;
`

const Header = styled.div`
  width: 100%;
  height: auto;
  margin-bottom: 10px;
  font-size: 25px;
`

const Name = styled.input`
  width: 97%;
  height: 40px;
  padding: 5px;
  margin: 5px auto;
  border: 1px solid lightgray;
  border-radius: 10px;

  &:focus {
    outline-color: pink;
  }
`

const Email = styled.input`
  width: 97%;
  height: 40px;
  padding: 5px;
  margin: 5px auto;
  border: 1px solid lightgray;
  border-radius: 10px;

  &:focus {
    outline-color: pink;
  }
`

const Password = styled.input`
  width: 97%;
  height: 40px;
  padding: 5px;
  margin: 5px auto;
  border: 1px solid lightgray;
  border-radius: 10px;
  
  &:focus {
    outline-color: pink;
  }
`


const SignUpButton = styled.input`
  transition: 0.2s;
  width: 100%;
  height: 50px;
  padding: 5px;
  margin: 10px auto;
  border: 1px solid lightgray;
  border-radius: 10px;
  background-color: pink;
  cursor: pointer;
  font-size: 20px;
  text-align: center;
`;

const ContentBox = styled.div`
  width: 97%;
  margin: 0 auto;
  color: gray;
  font-size: 15px;
  text-align: start;
  margin-bottom: 20px;
`

export {SignUpBox,SignUpButton,Email,Name,Password,Header, ContentBox}