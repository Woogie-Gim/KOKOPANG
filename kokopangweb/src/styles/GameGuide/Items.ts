import styled from "styled-components";

export const Container = styled.div`
  width: 100%;
  min-height: 100%;
  display: flex;
  justify-content: space-between;
`

export const Tab = styled.div`
  width: 25%;
  min-height: 500px;
  background-color: #004AAD;
  border-radius: 10px;
  display: flex;
  flex-direction: column;
  justify-content: space-around;
`

export const Menu = styled.div`
  width: 100%;
  height: 50px;
  display: flex;
  align-items: center;
  justify-content: center;
  color: white;
  cursor: pointer;
  transition: 0.2s;

  &.active {
    transform: scale(1.2);
    color: black;
  }

  &:hover {
    transition: 0.2s;
    transform: scale(1.2);
  }
`

export const Description = styled.div`
  width: 70%;
  min-height: 500px;
`