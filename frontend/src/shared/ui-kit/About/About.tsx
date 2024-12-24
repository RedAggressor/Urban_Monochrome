import { ResponsiveImage } from '../ResponsiveImage/ResponsiveImage';
import './About.scss';

export const About = () => {
  const sources = [
    { media: '(max-width: 767px)', srcSet: '/pictures/homePictures/homePage_about_small.jpg' },
    { media: '(min-width: 767px) and (max-width: 1024px)', srcSet: '/pictures/homePictures/homePage_about_medium.jpg' },
    { media: '(min-width: 1025px)', srcSet: '/pictures/homePictures/homePage_about_large.jpg' },
  ]

  return (
    <section className="about">
      <p className="word-highlight">[About]</p>
      <h3 className="about-title about-title--style">STYLE THAT <br /> <span className="about-title-second-line">SPEAKS</span></h3>

      <div className="about-content">
        <p className="about-text-style">
        Our <span className="highlight">clothing</span> isn&rsquo;t just black and white — it&rsquo;s a <span className="highlight">statement</span> for those who <span className="highlight">embrace</span> embrace boldness and aren&rsquo;t afraid to <span className="highlight">redefine</span> their <span className="highlight">look</span>.
        </p>

        <div className="about-wrapper-img">
          <ResponsiveImage
            sources={sources}
            img={{
              src:"/pictures/homePictures/homePage_about_small.jpg",
              alt:"Home Page About",
            }}
            className="about-img"
          />
        </div>
      </div>

      <h3 className="about-title about-title--beliefs">WEAR YOUR <br /> BELIEFS</h3>
      <div className="about-info">
        <p className="about-text-beliefs about-text--indent">
        <span className="highlight">[</span>Our commitment to sustainability is reflected in every every detail, making sure that your choices matter as much as the style you carry.<span className="highlight">]</span>
        </p>
        <p className="about-text-beliefs">
        <span className="highlight">[</span>Our mission is to empower your individuality, offering clothing made from premium, eco-friendly materials, crafted with responsibility and care.<span className="highlight">]</span>
        </p>
      </div>
    </section>
  )
}
