import styled from "styled-components";

const BoardBox = styled.div`
  width: 70%;
  height: auto;
  background-color: whitesmoke;
  border: 3px solid lightgray;
  border-radius: 5px;
  margin: 0 auto;
  display: flex;
  flex-direction: column;
  margin-bottom: 50px;

  .container2 {
    padding: 10px;
    display: flex;
    flex-direction: row;
    background-color: lightgray;
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

const Board = styled.div`
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

const BoardCreateBox = styled.div`
  width: 99%;
  display: flex;
  flex-direction: row-reverse;
  margin-top: 20px;
  justify-content: end;
  
  .btn {
    width: auto;
    text-align: end;
    padding: 10px;
    font-size: 20px;
    border: 0;
    border-radius: 5px;
    background-color: #324b90;
    color: white;
    cursor: pointer;
  }
`

export { BoardBox, Board, BoardCreateBox};