import { About } from '../../shared/ui-kit/About/About';
import { ButtonShopNow } from '../../shared/ui-kit/ButtonShopNow/ButtonShopNow';
import { ResponsiveImage } from '../../shared/ui-kit/ResponsiveImage/ResponsiveImage';
import './homePage.scss';

export const HomePage = () => {
  const sources = [
    { media: '(max-width: 767px)', srcSet: '/pictures/homePictures/homePage_banner_small.jpg' },
    { media: '(min-width: 767px) and (max-width: 1024px)', srcSet: '/pictures/homePictures/homePage_banner_medium.jpg' },
    { media: '(min-width: 1025px)', srcSet: '/pictures/homePictures/homePage_banner_large.jpg' },
  ]

  return (
    <main className='home-page-main'>
      <section className="main-banner">
        <div className="wrapper-banner">
          <ResponsiveImage
            sources={sources}
            img={{
              src:"/pictures/homePictures/homePage_banner_small.jpg",
              alt:"Home Page Banner",
            }}
            className="img-banner"
          />
        </div>
          <h1 className='home-page-title'>Urban <br /> Monochrome</h1>
          <p className='home-page-text'>Shine bright in your unique black-
          <br />and-white style.</p>
        <div className="home-page-button">
          <ButtonShopNow />
        </div>
      </section>

      <About />
    </main>
  )
}
