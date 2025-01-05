import { Container } from '../../shared/Container/Container';
import { ResponsiveImage } from '../../shared/ui-kit/ResponsiveImage/ResponsiveImage';

import cl from './About.module.scss';

export const About = () => {
  const sources = [
    {
      media: '(max-width: 767px)',
      srcSet: '/pictures/homePictures/homePage_about_small.jpg',
    },
    {
      media: '(min-width: 767px) and (max-width: 1024px)',
      srcSet: '/pictures/homePictures/homePage_about_medium.jpg',
    },
    {
      media: '(min-width: 1025px)',
      srcSet: '/pictures/homePictures/homePage_about_large.jpg',
    },
  ];

  return (
    <Container>
      <section className={cl.about}>
        {/* назви класів титульних і текстових блоків означають
      title/text-'першіСловаБлоку' */}
        <p className={cl.sectionTitle}>[About]</p>
        <h3 className={`${cl.boldTitle} ${cl['title-styleThatSpeaks']}`}>
          <span className={cl.noWrap}>STYLE THAT</span> SPEAKS
        </h3>

        <p className={cl['text-ourClothing']}>
          Our <em className={cl.highlight}>clothing</em> isn&rsquo;t just black
          and white — it&rsquo;s a <em className={cl.highlight}>statement</em>{' '}
          for those who <em className={cl.highlight}>embrace</em> embrace
          boldness and aren&rsquo;t afraid to{' '}
          <em className={cl.highlight}>redefine</em> their{' '}
          <em className={cl.highlight}>look</em>.
        </p>

        <div className={cl.imgContainer}>
          <ResponsiveImage
            sources={sources}
            img={{
              src: '/pictures/homePictures/homePage_about_small.jpg',
              alt: 'Home Page About',
            }}
            className={cl.imgContainer__img}
          />
        </div>

        <h3 className={`${cl.boldTitle} ${cl['title-wearYour']}`}>
          <span className={cl.noWrap}>WEAR YOUR</span> BELIEFS
        </h3>

        <div className={cl.twoTextsContainer}>
          <p className={cl.twoTextsContainer__text}>
            <span className={cl.highlight}>[</span>Our commitment to
            sustainability is reflected in every every detail, making sure that
            your choices matter as much as the style you carry.
            <span className={cl.highlight}>]</span>
          </p>
          <p className={cl.twoTextsContainer__text}>
            <span className={cl.highlight}>[</span>Our mission is to empower
            your individuality, offering clothing made from premium,
            eco-friendly materials, crafted with responsibility and care.
            <span className={cl.highlight}>]</span>
          </p>
        </div>
      </section>
    </Container>
  );
};
