import styled from "styled-components";

const BoardBox = styled.div`
  width: 70%;
  height: auto;
  background-color: rgba(255, 255, 255, 0.95);
  border: 3px solid lightgray;
  border-radius: 10px;
  margin: 0 auto;
  display: flex;
  flex-direction: column;
  margin-bottom: 50px;
  opacity: 0.8;
  box-shadow: 4px 4px 4px rgba(0, 0, 0, 0.3);

  .box {
    width: 100%;
    display: flex;
    flex-direction: row;
    margin-bottom: 30px;
    margin-top: 30px;
    align-items: center;
    
    padding-bottom: 30px;
    border-bottom: 1px solid lightgray;

    .item {
      flex: 1;
      font-size: 20px;
    }

    .item:nth-child(1) {
      flex: 10%;
      text-align: center;
    }

    .item:nth-child(2) {
      flex: 70%;
    }

    .item:nth-child(3) {
      flex: 20%;
      font-size: 16px;
      text-align: end;
      margin-right: 20px;
    }
  }
`

const Title = styled.div`
  width: 100%;
  font-size: 35px;
  background-color: #e1dede;
  border-bottom: 1px solid black;
  padding-top: 30px;
  padding-bottom: 30px;

  .font {
    margin-left: 30px;
  }
`

const Content = styled.div`
  width: 100%;
  font-size: 20px;
  height: 300px;
  border-bottom: 1px solid lightgray;
  .font {
    margin-left: 30px;
  }
`

const BtnBox = styled.div`
  width: 70%;
  text-align: end;
  display: flex;
  flex-direction: row-reverse;
  margin-top: 20px;
  margin-bottom: 20px;
  .btn {
    width: 80px;
    height: 40px;
    border: 0;
    border-radius: 5px;
    text-align: center;
    display: flex;
    justify-content: center;
    align-items: center;
    font-size: 20px;
    cursor: pointer;
    margin-right: 10px;
    background-color: whitesmoke;
    &:hover {
      background-color: #324b90;
      color: white;
    } 
  }
`

const Comment = styled.textarea`
  width: 80%;
  min-height: 70px;
  height: auto;
  padding: 5px;
  border: 1px solid lightgray;
  border-radius: 5px;
  resize: none;
  font-size: 20px;
  margin: 20px auto;

  &:focus {
    outline: 1px solid lightgray;
  }
`

const CreateBtn = styled.div`
  width: 80%;
  margin: 0 auto;
  display: flex;
  flex-direction: row-reverse;

  .btn {
    width: 80px;
    height: 40px;
    padding: 5px;
    border: 0;
    border-radius: 5px;
    text-align: center;
    display: flex;
    justify-content: center;
    align-items: center;
    font-size: 20px;
    cursor: pointer;
    margin-right: 10px; 
    &:hover {
      background-color: #324b90;
      color: white;
    }
  }
`

const CommentBox = styled.div`
  width: 80%;
  margin: 20px auto;
  display: flex;
  flex-direction: column;
  justify-content: center;

  .box1 {
    width: 100%;
    display: flex;
    flex-direction: column-reverse;
    border-bottom: 1px solid lightgray;
    padding-bottom: 10px;
    margin-bottom: 20px;
    font-size: 20px;

    .item1 {
      margin-bottom: 15px;
    }

    .item1:nth-child(1) {
      width: 100%;
      margin: 0 auto;
      margin-left: 10px;
      font-size: 15px;
      font-weight: 0;
      display: flex;
      flex-direction: row;
      justify-content: space-between;
      align-items: center;
    }
  }
`

const CommentSubBtn = styled.div`
  width: 50px;
  height: 25px;
  padding: 5px;
  border: 0;
  border-radius: 5px;
  text-align: center;
  display: flex;
  justify-content: center;
  align-items: center;
  font-size: 20px;
  cursor: pointer;
  margin-right: 10px; 
  background-color: #ecebeb;
  &:hover {
      background-color: #324b90;
      color: white;
    }
`

export {BoardBox, Title, Content, BtnBox, Comment, CreateBtn, CommentBox, CommentSubBtn}