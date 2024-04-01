import styled from "styled-components";

export const Wrapper = styled.div`
  width: 64%;
  margin: 0 auto;
  min-height: 50vh;
  margin-top: 30px;
  border-radius: 10px;
  padding: 3%;
  box-shadow: 4px 4px 4px rgba(0, 0, 0, 0.3);
  background-color: white;
  opacity: 0.8;
  font-size: 25px;
  margin-bottom: 50px;
`;

export const GuideBox = styled.div`
  width: 100%;
  display: flex;
  flex-direction: column;
  margin-bottom: 50px;

  .title {
    font-size: 30px;
    margin-bottom: 20px;
  }

  .content {
    width: 500px;
    height: 400px;
    margin: 0  auto;
  }

`