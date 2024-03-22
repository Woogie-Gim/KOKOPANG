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
`

const SearchBtn = styled.button`
    width: 50px;
    height: 50px;
    border-radius: 100px;
    background-color: lightgray;
    border: 0;
    font-weight: 700;
    cursor: pointer;
`

const MyRankBox = styled.div`
    width: 70%;
    margin: 0 auto;
    height: 40px;
    padding: 20px;
    border: 1px solid lightgray;
    border-radius: 5px;
    margin-bottom: 30px;
`

const UserRankBox = styled.div`
    width: 70%;
    margin: 0 auto;
    height: 800px;
    padding: 20px;
    border: 1px solid lightgray;
    border-radius: 5px;
    display: flex;
    flex-direction: column;
`

const RankTable = styled.div`
    width: 100%;
    height: auto;
    display: flex;
    flex-direction: column;
`


export {MyRankBox, UserRankBox, RankTable, SearchBox, SearchInput, SearchBtn};