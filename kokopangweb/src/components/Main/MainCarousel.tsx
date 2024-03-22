import React, { useState, useEffect } from 'react';
import { CarouselContainer,SlideContainer,Slide,Image,IndicatorContainer,Indicator } from '../../styles/Main/MainCarousel';
import carouselImage1 from "../../assets/myedit_ai_image_0318142911.jpg";
import carouselImage2 from "../../assets/myedit_ai_image_0318143148.jpg";
import carouselImage3 from "../../assets/myedit_ai_image_0318143150.jpg";

const MainCarousel = () => {
  const [currentSlide, setCurrentSlide] = useState(0);
  const images = [
    carouselImage1,
    carouselImage2,
    carouselImage3
  ]

  useEffect(() => {
    const interval = setInterval(() => {
      goToNextSlide();
    }, 3000);

    return () => clearInterval(interval);
  }, [currentSlide]);

  const goToNextSlide = () => {
    setCurrentSlide((prevSlide) => (prevSlide + 1) % images.length);
  };

  return (
    <CarouselContainer>
      <SlideContainer
        style={{
          transform: `translateX(-${currentSlide * 100}%)`,
          transition: "transform 1s ease"
        }}
      >
        {images.map((image, index) => (
          <Slide key={index}>
            <Image src={image} alt={`Slide ${index}`}/>
          </Slide>
        ))}
      </SlideContainer>
      <IndicatorContainer>
        {images.map((_, index) => (
          <Indicator key={index} onClick={() => setCurrentSlide(index)} style={{ backgroundColor: index === currentSlide ? 'white':'darkgray'}}/>
        ))}
      </IndicatorContainer>
    </CarouselContainer>
  );
}

export default MainCarousel;
