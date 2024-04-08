import styled from "styled-components";

const BoardBox = styled.div`
  width: 70%;
  height: auto;
  background-color: rgba(255, 255, 255, 0.8);
  border: 3px solid lightgray;
  border-radius: 10px;
  margin: 0 auto;
  display: flex;
  flex-direction: column;
  margin-bottom: 50px;
  padding: 10px;
  box-shadow: 4px 4px 4px rgba(0, 0, 0, 0.3);

  .box {
    width: 100%;
    display: flex;
    flex-direction: row;
    margin-bottom: 30px;
    margin-top: 30px;
    align-items: center;

    .item {
      flex: 1;
      font-size: 20px;
    }

    .item:nth-child(1) {
      flex: 10%;
      text-align: center;
    }

    .item:nth-child(2) {
      flex: 90%;
    }
  }

  .btnBox {
    width: 92%;
    display: flex;
    flex-direction: row-reverse;
    margin-top: 20px;

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
    }
  }
`

const Title = styled.input`
  width: 90%;
  height: 30px;
  padding: 5px;
  border: 1px solid lightgray;
  border-radius: 5px;
  font-size: 20px;

  &:focus {
    outline-color: lightgray;
  }
`

const Content = styled.textarea`
  width: 90%;
  min-height: 200px;
  height: auto;
  padding: 5px;
  border: 1px solid lightgray;
  border-radius: 5px;
  resize: none;
  font-size: 20px;

  &:focus {
    outline-color: lightgray;
  }
`

export {BoardBox, Title, Content};