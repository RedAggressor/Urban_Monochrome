import { Container } from '../../shared/Container/Container';
import { ButtonShopNow } from '../../shared/ui-kit/ButtonShopNow/ButtonShopNow';
import { ResponsiveImage } from '../../shared/ui-kit/ResponsiveImage/ResponsiveImage';
import cl from './New.module.scss';

export const New = () => {
  const sources = [
    {
      media: '(max-width: 766px)',
      srcSet: '/pictures/homePictures/homePage_new_small.jpg',
    },
    {
      media: '(min-width: 767px) and (max-width: 1024px)',
      srcSet: '/pictures/homePictures/homePage_new_tablet.jpg',
    },
    {
      media: '(min-width: 1025px)',
      srcSet: '/pictures/homePictures/homePage_new_desktop.jpg',
    },
  ];

  return (
    <Container>
      <section className={cl.new}>
        <p className={cl['word-highlight']}>[New]</p>

        <h3 className={cl['title']}>
          TOTAL <span className={cl['title__desktop']}>BLACK</span>
        </h3>

        <p className={cl['text']}>
          Feel the powerful impact of black in contemporary style.
        </p>

        <div className={cl['img-wrapper']}>
          <ResponsiveImage
            sources={sources}
            img={{
              src: '/pictures/homePictures/homePage_new_small.jpg',
              alt: 'Home Page New',
            }}
            className={cl['img-wrapper__img']}
          />
        </div>

        <ButtonShopNow className={cl['button']} onClick={() => {}} />

        <div className={cl['img-wrapper-2']}>
          <img
            src="/pictures/homePictures/homePage_new_desktop2.jpg"
            alt="Home Page New 2"
            className={cl['img-wrapper-2__img']}
          />
        </div>
      </section>
    </Container>
  );
};
