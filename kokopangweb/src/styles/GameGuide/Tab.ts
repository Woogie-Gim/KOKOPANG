import styled from "styled-components"

export const Nav = styled.div`
  display: flex;
  justify-content: space-around;
  width: 70%;
  height: 50px;
  margin: -20px auto;
  margin-bottom: 0;
  border-radius: 10px;
  opacity: 0.8;
  background-color: white;
  box-shadow: 4px 4px 4px rgba(0, 0, 0, 0.3)
`;

export const Item = styled.div`
  display: flex;
  align-items: center;
  justify-content: center;
  width: fit-content;
  height: 100%;
  font-size: 20px;
  color: black;
  transition: 0.3s;

  &.active{
    cursor: pointer;
    color: #004AAD;
    scale: 1.2;
  }

  &&:hover{
    cursor: pointer;
    scale: 1.2;
    transition: 0.3s;
  } 
`;