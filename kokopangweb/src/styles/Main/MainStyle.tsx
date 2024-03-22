import styled from "styled-components";

const UpBox = styled.div`
    display: flex;
    width: 80%;
    margin: 0 auto;
    flex-direction: row;
    margin-bottom: 20px;
`

const InfoBox = styled.div`
    width: 800px;
    height: 600px;
    border: 0;
    border-radius: 10px;
    box-shadow: 0px 5px 15px 1px lightgray;
`

const Header = styled.div`
    width: 80%;
    height: auto;
    margin: 0 auto;
    margin-bottom: 30px;
`

const MenuBox = styled.div`
    width: 70%;
    margin: 30px auto;
    height: auto;
    display: flex;
    justify-content: space-between;
    align-items: center;
    flex-direction: row;
`

const LogoBox = styled.div`
    width: 10%;
    margin: 0 2%;
    margin-right: 0;
    cursor: pointer;
    text-align: center;

    img {
        width: 100px;
        height: 100px;
    }
`

const Menu = styled.div`
    transition: all 0.2s;
    display: flex;
    align-items: center;
    justify-content: center;
    width: fit-content;
    height: 50px;
    margin: 10px;
    padding: 5px;
    font-size: 25px;
    font-weight: 700;
    text-align: center;
    border-radius: 10px;
    cursor: pointer;

    &:hover {
        transition: all 0.2s;
        transform: scale(1.1);
    }
`


const NoticeHeader = styled.div`
    width: 80%;
    margin: 0 auto;
    font-size: 30px;
    font-weight: 700;
    margin-bottom: 20px;
    
`

const NoticeBox = styled.div`
    width: 80%;
    height: 30px;
    overflow: hidden;
    margin: 0 auto;
    margin-bottom: 50px;
`

export { InfoBox, UpBox, Header, MenuBox, Menu,LogoBox, NoticeBox, NoticeHeader};