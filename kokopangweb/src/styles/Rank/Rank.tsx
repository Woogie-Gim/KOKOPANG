import styled from "styled-components";

const SearchBox = styled.form`
    width: 50%;
    height: auto;
    margin: 15px auto;
    display: flex;
    flex-direction: row;
    justify-content: space-around;
`

const SearchInput = styled.input`
    width: 85%;
    height: 30px;
    padding: 10px;
    border-radius: 20px;
    border: 1px solid lightgray;
    &:focus {
      outline-color: lightgray;
    }
`

const SearchBtn = styled.button`
    width: 50px;
    height: 50px;
    border-radius: 100px;
    background-color: lightgray;
    border: 0;
    cursor: pointer;
`

const UserRankBox = styled.div`
    width: 70%;
    margin: 0 auto;
    height: auto; 
    border: 3px solid lightgray;
    border-radius: 5px;
    display: flex;
    flex-direction: column;
    background-color: whitesmoke;
    margin-bottom: 50px;

    .container {
      display: flex;
      flex-direction: row;
      height: auto;
      align-items: center;
      padding: 20px;
      border-bottom: 1px solid lightgray;
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
      flex: 15%;
    }

    .item:nth-child(3) {
      flex: 35%;
    }

    .item:nth-child(4) {
      flex: 35%;
    }
`

const RankTable = styled.div`
  width: 100%;
  height: auto;
  display: flex;
  flex-direction: column;

  .no_result {
    width: 100%;
    font-size: 30px;
    text-align: center;
    padding: 30px;
    color: gray;
  }

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
    flex: 15%;
  }

  .item2:nth-child(2) {
    flex: 50%;
  }

  .item2:nth-child(3) {
    flex: 35%;
  }
`

const TextBox = styled.div`
  width: 70%;
  margin: 30px auto;
  font-size: 35px;
  color: #ffffff;
`

const PageNation = styled.div`
  margin: 20px auto;
  display: flex;
  justify-content: center;
  align-items: center;

  .nav_buttons button {
    padding: 8px 12px;
    margin: 0 5px;
    border: 1px solid #ccc;
    border-radius: 5px;
    background-color: #fff;
    cursor: pointer;
    font-size: 16px;
    color: #464646;
  }

  .nav_buttons button:hover {
    background-color: #eee;
  }

  .nav_buttons button.current {
    font-weight: bold;
    background-color: #ccc;
    color: #fff;
  }
`
export { UserRankBox, RankTable, SearchBox, SearchInput, SearchBtn, TextBox,PageNation };