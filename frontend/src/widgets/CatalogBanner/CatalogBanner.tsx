import { Container } from '../../shared/Container/Container';
import cl from './CatalogBanner.module.scss';

export const CatalogBanner = ({ className = '' }) => {
  return (
    <section>
      <Container className={`${cl.container} ${className}`}>
        <div className={cl.textContainer}>
          <h1 className={cl.title}>
            MONOCHROME&nbsp;LOOK FOR{' '}
            <span className={cl.title__highlitedWord}>YOU</span>
          </h1>
          <p className={cl.text}>Stand out, embrace your true self</p>
        </div>

        <div className={cl.imgWrapper}>
          <img
            src="/pictures/catalogPictures/catalog-banner-desk.png"
            alt="catalog banner picture"
            className={cl.imgWrapper__img}
          />
        </div>
      </Container>
    </section>
  );
};
