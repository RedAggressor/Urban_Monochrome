import { useRef, useState } from 'react';
import { useSwipeable } from 'react-swipeable'; // щоб легше було прописати події свайпів
import { Container } from '../../shared/Container/Container';
import cl from './Lookbook.module.scss';
import cn from 'classnames';
import { useAppSelector, useWidthRecalculate } from '../../shared/hooks';

// для рендерингу списку точок і керування станом активного слайду
const selectorsIds = [0, 1, 2, 3, 4, 5, 6, 7];

export const Lookbook = () => {
  const [activeSlideId, setActiveSlideId] = useState(0); // 0 - 7
  const sliderRef = useRef(null); // буде вішатись на ul
  const [sliderWidth] = useWidthRecalculate(sliderRef); // ширина всього списку але її треба використати на кожному елементі і використати в рівнянні розрахунку анімації
  const { screenWidth } = useAppSelector(st => st.global);

  //#region swiping
  function swipeLeft() {
    setActiveSlideId(prev => (prev === 0 ? 7 : prev - 1));
  }

  function swipeRight() {
    setActiveSlideId(prev => (prev === 7 ? 0 : prev + 1));
  }

  const swiper = useSwipeable({
    onSwipedLeft: swipeRight, // в самій бібліотеці назви подій некоректно названі
    onSwipedRight: swipeLeft,
  });
  //#endregion

  const slideStyles = {
    // ці стилі будуть застосовані до кожної картинки. прийшлось отак потанцювати з бубном бо на різних екранах вони на різному виглядають
    width: `${screenWidth < 1024 ? `${sliderWidth}px` : 'auto'}`,
    transform: `${screenWidth < 1024 ? `translateX(-${activeSlideId * sliderWidth}px)` : 'none'}`,
  };

  return (
    <Container>
      <section {...swiper} className={cl.section}>
        <p className={cl.titleRed}>[Lookbook]</p>
        <h3 className={cl.title}>
          DARE TO BE{' '}
          <div className={cl.title__thisStupidFuckingPart}>DIFFERENT</div>
        </h3>
        <p className={cl.text}>
          Experiment with style,{' '}
          <span className={cl.textHighlighted}>express yourself</span> through
          creative combinations.{' '}
        </p>

        <ul className={cl.imgList} ref={sliderRef}>
          <li
            className={`${cl.imgList__item} ${cl.imgList__item_0}`}
            style={slideStyles}
          >
            <img
              src="/pictures/lookbook/lookbook_mob_0.jpg"
              alt="lookbook image"
              className={cl.imgList__img}
            />
          </li>
          <li
            className={`${cl.imgList__item} ${cl.imgList__item_1}`}
            style={slideStyles}
          >
            <img
              src="/pictures/lookbook/lookbook_mob_1.jpg"
              alt="lookbook image"
              className={cl.imgList__img}
            />
          </li>
          <li
            className={`${cl.imgList__item} ${cl.imgList__item_2}`}
            style={slideStyles}
          >
            <img
              src={
                screenWidth < 1024
                  ? '/pictures/lookbook/lookbook_mob_2.jpg'
                  : '/pictures/lookbook/lookbook_desk_2.jpg'
              }
              alt="lookbook image"
              className={cl.imgList__img}
            />
          </li>
          <li
            className={`${cl.imgList__item} ${cl.imgList__item_3}`}
            style={slideStyles}
          >
            <img
              src={
                screenWidth < 1024
                  ? '/pictures/lookbook/lookbook_mob_3.jpg'
                  : '/pictures/lookbook/lookbook_desk_3.jpg'
              }
              alt="lookbook image"
              className={cl.imgList__img}
            />
          </li>
          <li
            className={`${cl.imgList__item} ${cl.imgList__item_4}`}
            style={slideStyles}
          >
            <img
              src="/pictures/lookbook/lookbook_mob_4.jpg"
              alt="lookbook image"
              className={cl.imgList__img}
            />
          </li>
          <li
            className={`${cl.imgList__item} ${cl.imgList__item_5}`}
            style={slideStyles}
          >
            <img
              src="/pictures/lookbook/lookbook_mob_5.jpg"
              alt="lookbook image"
              className={cl.imgList__img}
            />
          </li>
          <li
            className={`${cl.imgList__item} ${cl.imgList__item_6}`}
            style={slideStyles}
          >
            <img
              src={
                screenWidth < 1024
                  ? '/pictures/lookbook/lookbook_mob_6.jpg'
                  : '/pictures/lookbook/lookbook_desk_6.jpg'
              }
              alt="lookbook image"
              className={cl.imgList__img}
            />
          </li>
          <li
            className={`${cl.imgList__item} ${cl.imgList__item_7}`}
            style={slideStyles}
          >
            <img
              src={
                screenWidth < 1024
                  ? '/pictures/lookbook/lookbook_mob_7.jpg'
                  : '/pictures/lookbook/lookbook_desk_7.jpg'
              }
              alt="lookbook image"
              className={cl.imgList__img}
            />
          </li>
        </ul>

        <ul className={cl.selectorsList}>
          {selectorsIds.map(id => (
            <li
              key={id}
              className={cn(cl.selectorsList__item, {
                [cl['selectorsList__item--active']]: activeSlideId === id,
              })}
              onClick={() => setActiveSlideId(id)}
            />
          ))}
        </ul>
      </section>
    </Container>
  );
};
