import styled from "styled-components";

const CarouselContainer = styled.div`
  position: relative;
  width: 800px;
  height: 600px;
  overflow: hidden;
  border-radius: 10px;
`;

const SlideContainer = styled.div`
  display: flex;
  width: 800px;
  height: 600px;
`;

const Slide = styled.div`
  flex: 0 0 auto;
  width: 100%;
  
`;

const Image = styled.img`
  width: 800px;
  height: 600px;
`;

const IndicatorContainer = styled.div`
    position: absolute;
    z-index: 2;
    bottom: 5%;
    left: 42%;
    width: auto;
    height: auto;
    display: flex;
    flex-direction: row;
`

const Indicator = styled.div`
    width: 10px;
    height: 10px;
    border-radius: 50%;
    margin: 0 10px;
    cursor: pointer;
`

export {CarouselContainer,SlideContainer,Slide,Image, IndicatorContainer, Indicator}