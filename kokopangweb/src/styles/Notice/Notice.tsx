import styled from "styled-components";

const NoticeBox = styled.div`
  width: 70%;
  height: auto;
  background-color: rgba(255, 255, 255, 0.8);
  border: 3px solid lightgray;
  border-radius: 10px;
  margin: 0 auto;
  display: flex;
  flex-direction: column;
  margin-bottom: 50px;
  box-shadow: 4px 4px 4px rgba(0, 0, 0, 0.3);

  .container2 {
    padding: 10px;
    display: flex;
    flex-direction: row;
    background-color: rgba(211, 211, 211, 0.5);
    border-bottom: 1px solid gray;
    text-align: center;
    padding: 20px;
  }

  .item2 {
    flex: 1;
    text-align: center;
    font-size: 20px;
  }

  .item2:nth-child(1) {
    flex: 5%;
  }

  .item2:nth-child(2) {
    flex: 80%;
  }

  .item2:nth-child(3) {
    flex: 15%;
  }
`

const Notice = styled.div`
  display: flex;
  flex-direction: row;
  padding: 20px;
  border-bottom: 1px solid lightgray;
  justify-content: center;
  align-items: center;

  .item {
    flex: 1;
    text-align: center;
    font-size: 20px;
  }

  .item:nth-child(1) {
    flex: 5%;
  }

  .item:nth-child(2) {
    flex: 80%;
    cursor: pointer;
  }

  .item:nth-child(3) {
    flex: 5%;
  }

  .item:nth-child(4) {
    flex: 10%;
  }
`

export {Notice, NoticeBox};