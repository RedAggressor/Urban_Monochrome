import { Container } from '../../shared/Container/Container';
import { ButtonShopNow } from '../../shared/ui-kit/ButtonShopNow/ButtonShopNow';
import { ResponsiveImage } from '../../shared/ui-kit/ResponsiveImage/ResponsiveImage';
import cl from './HomePageBanner.module.scss';

export const HomePageBanner = () => {
  const sources = [
    {
      media: '(max-width: 767px)',
      srcSet: '/pictures/homePictures/homePage_banner_small.jpg',
    },
    {
      media: '(min-width: 767px) and (max-width: 1024px)',
      srcSet: '/pictures/homePictures/homePage_banner_medium.jpg',
    },
    {
      media: '(min-width: 1025px)',
      srcSet: '/pictures/homePictures/homePage_banner_large.jpg',
    },
  ];

  return (
    <section className={cl.mainBanner}>
      <div className={cl.wrapperBanner}>
        <ResponsiveImage
          sources={sources}
          img={{
            src: '/pictures/homePictures/homePage_banner_small.jpg',
            alt: 'Home Page Banner',
          }}
          className={cl.imgBanner}
        />
      </div>
      {/* прийшлось обгорнути в контейнер окремо текст і окремо кнопку,
      бо лише вони обмежені по ширині */}
      <Container>
        <h1 className={cl.homePageTitle}>
          Urban <br /> Monochrome
        </h1>
        <p className={cl.homePageText}>
          Shine bright in your unique black-
          <br />
          and-white style.
        </p>
        <ButtonShopNow onClick={() => {}} className={cl.homePageButton} />
      </Container>
    </section>
  );
};
